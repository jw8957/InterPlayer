using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectShowLib;
using System.Diagnostics;

namespace InvokeMRP
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            Initial();
        }

        private void Initial() {
            mrp = new MRP();
            ButtonPbox.Visible = false;
            this.KeyPreview = true;

            dMode = DisplayMode.Normal;
        }

        //交互
        MRP mrp;
        Queue<SYN_INF_STRUCT> syn_inf_struct_list;
        SYN_INF_STRUCT cur_info;
        SceneUI sceneUI;
        
        //DirectShow
        private IGraphBuilder graphBuilder = null;
        private IMediaControl mediaCtrl = null;
        private IMediaEventEx mediaEvt = null;
        private IMediaPosition mediaPos = null;
        private IVideoWindow videoWin = null;

        //常量
        private const int HINT_OCCUR_TIME = 500;
        private const int WM_APP = 0x8000;
        private const int WM_GRAPHNOTIFY = WM_APP + 1;
        private const int EC_COMPLETE = 0x01;
        private const int WS_CHILD = 0x40000000;
        private const int WS_CLIPCHILDREN = 0x2000000;

        enum MediaStatus { None, Stopped, Paused, Running };

        // 信息状态 
        // Play 播放
        // Show 显示私有信息
        // Question 显示问题
        enum InfoStatus { Play, Show, Question };

        // 显示状态 
        // Normal 正常
        // Full   全屏
        enum DisplayMode { Normal, Full };

        private InfoStatus iStatus;
        private DisplayMode dMode;

        private int win_width;
        private int win_height;
        private int win_pos_left;
        private int win_pos_top;
        private double w_h_ratio;

        private int time_ms;

        private void Play(string filename) {
            if (graphBuilder != null){
                //CleanUp();
                syn_inf_struct_list = new Queue<SYN_INF_STRUCT>();
                cur_info = null;
                //dMode = DisplayMode.Normal;
                //iStatus==InfoStatus.
            }
            try{
                graphBuilder = (IGraphBuilder)new FilterGraph();
                mediaCtrl = (IMediaControl)graphBuilder;
                mediaEvt =  (IMediaEventEx)graphBuilder;
                mediaPos =  (IMediaPosition)graphBuilder;
                videoWin =  (IVideoWindow)graphBuilder;

                graphBuilder.RenderFile(filename, null);
                //graphBuilder.RenderFile(@"D:\课程\创新项目\MRP\6.TS", null);

                videoWin.put_Owner(Video_panel.Handle);
                videoWin.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipChildren);

                setWindow();
                mediaCtrl.Run();

                double total_time;
                mediaPos.get_Duration(out total_time);

                Video_timer.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't start");
            }
        }

        private void setWindow() {
            win_width = Video_panel.Width;
            win_height = Video_panel.Height;
            win_pos_left = Video_panel.ClientRectangle.Left;
            win_pos_top = Video_panel.ClientRectangle.Top;
            w_h_ratio = ((double)Video_panel.ClientRectangle.Height) / Video_panel.ClientRectangle.Width;
            videoWin.SetWindowPosition(win_pos_left, win_pos_top, win_width, win_height);
        }

        private void displayTime() {
            double time_s;
            //int time_ms;
            mediaPos.get_CurrentPosition(out time_s);
            time_ms = (int)time_s * 1000;
            //显示播放时间
            if (mediaPos != null)
            {
                int s = (int)time_s;
                int h = s / 3600;
                int m = (s - (h * 3600)) / 60;
                s = s - (h * 3600 + m * 60);

                this.TimeStatusLabel.Text = String.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);

                //s = (int)m_objMediaPosition.CurrentPosition;
                h = s / 3600;
                m = (s - (h * 3600)) / 60;
                s = s - (h * 3600 + m * 60);

                this.TimeStatusLabel.Text = String.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);
            }
            else
            {
                this.TimeStatusLabel.Text = "00:00:00";
            }
        }

        private void TSHandle(){
            ///处理私有信息
            //cur_info != null 并且 过了提示时间,则舍弃该信息
            if (cur_info != null && iStatus == InfoStatus.Play)
            {
                if (time_ms - cur_info.ntime > cur_info.duration)
                {
                    cur_info = null;
                    ButtonPbox.Visible = false;
                }
            }

            if ( syn_inf_struct_list.Count > 0 )
            {
                SYN_INF_STRUCT tmp = syn_inf_struct_list.Peek();
                if ((time_ms >= tmp.ntime - HINT_OCCUR_TIME) && (time_ms <= tmp.ntime + HINT_OCCUR_TIME))
                {
                    cur_info = syn_inf_struct_list.Dequeue();

                    //如果正在显示私有信息，则舍弃该条信息
                    if (iStatus == InfoStatus.Show) cur_info = null;
                    else ButtonPbox.Visible = true;
                    //zoomIn();
                }
            }
        }

        private void Video_timer_Tick(object sender, EventArgs e)
        {
            displayTime();
            TSHandle();
        }

        private void zoomIn()
        {
            int w_off = 2;
            while (win_width > Video_panel.ClientRectangle.Width / 3)
            {
                win_width -= w_off;
                win_height = (int)(win_width * w_h_ratio);
                //win_height -= w_off;
                videoWin.SetWindowPosition(
                                       win_pos_left,
                                       win_pos_top,
                                       win_width,
                                       win_height
                                       );
            }
        }

        private void zoomOut()
        {
            int w_off = 2;
            //if (this.Width <= 900) w_off = 1;

            while (win_width < Video_panel.ClientRectangle.Width)
            {
                win_width += w_off;
                win_height = (int)(win_width * w_h_ratio);
                if (win_height > Video_panel.Height) win_height = Video_panel.Height;
                //win_height += w_off;
                videoWin.SetWindowPosition(
                                       win_pos_left,
                                       win_pos_top,
                                       win_width,
                                       win_height
                                       );
            }
            //setWindow();
        }

        public delegate void KeyHandler(int keyCode);

        private void MainWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (iStatus == InfoStatus.Show) {
                KeyHandler keyhandler = new KeyHandler(sceneUI.PaneChange);
                switch (e.KeyChar) {
                    case 'A':
                    case 'a':
                        keyhandler(1);
                    break;
                    case 'B':
                    case 'b':
                        keyhandler(2);
                    break;
                    case 'C':    
                    case 'c':
                        keyhandler(3);
                    break;
                    case 'D':    
                    case 'd':
                        keyhandler(4);
                    break;
                    default:break;
                }
            }

            if ( ( e.KeyChar == 'e' || e.KeyChar == 'E' ) ) {
                if (iStatus==InfoStatus.Play && cur_info != null) {
                    zoomIn();
                    sceneUI = new SceneUI(cur_info,this);
                    iStatus = InfoStatus.Show;
                }
                else if (iStatus == InfoStatus.Show) {
                    sceneUI.cur_pUI.setVisible(false);
                    zoomOut();
                    ButtonPbox.Visible = false;
                }
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media Files|*.ts|All Files|*.*";
            if (DialogResult.OK == openFileDialog.ShowDialog()) {

                String arg = openFileDialog.FileName;
                //arg.Replace("\\","\\\\");
                //Process InvokeMrp = Process.Start(@"D:\InterPlayer\NewPlayer\MRP_Invoke\MRP_DEMO\Debug\MRP_DEMO.exe", arg);
                Process InvokeMrp = new Process();
                InvokeMrp.StartInfo.FileName = @"D:\InterPlayer\NewPlayer\MRP_Invoke\MRP_DEMO\Debug\MRP_DEMO.exe";
                InvokeMrp.StartInfo.Arguments = arg;
                InvokeMrp.StartInfo.UseShellExecute = false;
                //InvokeMrp.StartInfo.RedirectStandardInput = true;
                //InvokeMrp.StartInfo.RedirectStandardOutput = true;
                //InvokeMrp.StartInfo.RedirectStandardOutput = true;
                //InvokeMrp.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                InvokeMrp.Start();

                //while (true) {
                //    if (myProcess.HasExited) break;
                //}
                InvokeMrp.WaitForExit();
                //myProcess.
                //myProcess.CloseMainWindow();
                InvokeMrp.Close();

                mrp.ReadFile(@"tmp.txt");
                //this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainWindow_KeyPress);
                //this.KeyPreview = true;
                cur_info = null;

                syn_inf_struct_list = mrp.syn_inf_struct_list;
                Play(openFileDialog.FileName);
            }
        }
    }

    class SceneUI {
        public SceneUI(SYN_INF_STRUCT scene,MainWindow form) {
            if (scene.sub_pane_count > 0)
            {
                panesUI = new PaneUI[scene.sub_pane_count];
                for (int i = 0; i < scene.sub_pane_count; ++i) panesUI[i] = new PaneUI(scene.panes[i], form);
                cur_pUI = panesUI[0];
                cur_pUI.setVisible(true);
            }
        }

        public void PaneChange(int keycode) {
            if (cur_pUI != null && cur_pUI.Key.ContainsKey(keycode) ) {
                cur_pUI.setVisible(false);
                cur_pUI=panesUI[cur_pUI.Key[keycode]-1];
                cur_pUI.setVisible(true);
            }
        }

        private PaneUI[] panesUI;
        public PaneUI cur_pUI;
    }

    class PaneUI {
        public PaneUI(PANE pane,MainWindow form) {
            this.pane = pane;
            panel = new Panel();
            form.Video_panel.Controls.Add(panel);
            draw();
            setKey();
        }

        public void setVisible(bool visible) {
            panel.Visible = visible;
        }

        public void draw() {
            panel.Location = new System.Drawing.Point(250, 5);
            panel.AutoSize = true;
            panel.Visible = false;
            textTot = 0;
            for (int i = 0; i < pane.total; ++i)
            {
                if (pane.pane_components[i].pane_type == 0) ++textTot;
            }
            text = new Label[textTot];

            for (int i = 0, j = 0; i < pane.total; ++i)
            {
                if (pane.pane_components[i].pane_type == 0)
                {
                    Pane_Component_Text pcT = pane.pane_components[i].pane_component_text;
                    text[j] = new Label();
                    text[j].Location = new System.Drawing.Point(pcT.pos_left, pcT.pos_top);
                    text[j].Font = new Font("楷体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    text[j].ForeColor = SystemColors.ControlLightLight;
                    panel.Controls.Add(text[j]);
                    text[j].Text = pcT.text;
                    ++j;
                }
            }
        }

        public void setKey() {
            Key = new Dictionary<int, int>();
            for (int i = 0; i < pane.total; ++i) {
                if (pane.pane_components[i].pane_type == 2) Key.Add(pane.pane_components[i].pane_component_key.kcode, pane.pane_components[i].pane_component_key.href_info);
            }
        }

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label[] text;
        private int textTot;
        private PANE pane;
        public Dictionary<int, int> Key;
    }
}