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

namespace DSTry
{
    public partial class MainForm : Form
    {
        public MainForm(){
            InitializeComponent();
            Initial();
        }

        //DirectShow
        private IGraphBuilder graphBuilder = null;
        private IMediaControl mediaCtrl = null;
        private IMediaEventEx mediaEvt = null;
        private IMediaPosition mediaPos = null;
        //private IVideoFrameStep frameStep = null;
        private IVideoWindow videoWin = null;
        
        private const int WM_APP = 0x8000;
        private const int WM_GRAPHNOTIFY = WM_APP + 1;
        private const int EC_COMPLETE = 0x01;
        private const int WS_CHILD = 0x40000000;
        private const int WS_CLIPCHILDREN = 0x2000000;

        //常量
        private const int HINT_WAIT_TIME = 5000;
        private const int HINT_OCCUR_TIME = 500;
        private const String ANS_RIGHT = "恭喜你，回答正确！";
        private const String ANS_WRONG = "回答错误，正确答案是";
        private const int FimgSize = 300;

        //Video
        private int win_width;
        private int win_height;
        private int win_pos_left;
        private int win_pos_top;
        private int unit;
        private double w_h_ratio;

        private String fileName;

        
        enum MediaStatus { None, Stopped, Paused, Running };

        // 信息状态 
        // Play 播放
        // Show 显示私有信息
        // Question 显示问题
        enum InfoStatus  { Play, Show , Question };

        // 显示状态 
        // Normal 正常
        // Full   全屏
        enum DisplayMode { Normal,Full };
        
        private InfoStatus iStatus;
        private DisplayMode dMode;

        //TS_info
        Queue<pane> InfoQueue;
        pane cur_info;

        private void Initial() {
            ButtonPbox.Visible = false;
            this.KeyPreview = true;
            //InfoQueue = new Queue<TS_info>();
            InfoQueue = new Queue<pane>();
            cur_info = null;
            dMode = DisplayMode.Normal;

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }
        
        protected override void Dispose(bool disposing)
        {
            CleanUp();

            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void CleanUp()
        {
            if (mediaCtrl != null)
                mediaCtrl.Stop();
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Media Files|*.ts|All Files|*.*";

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                fileName = openFileDialog.FileName;
                if (graphBuilder != null) {
                    CleanUp();
                    InfoQueue = new Queue<pane>();
                    cur_info = null;
                    dMode = DisplayMode.Normal;
                }
                Play(fileName);
            }
        }

        void Play(String fileName) {
            try
            {
                graphBuilder = (IGraphBuilder)new FilterGraph();
                mediaCtrl = (IMediaControl)graphBuilder;
                mediaEvt = (IMediaEventEx)graphBuilder;
                mediaPos = (IMediaPosition)graphBuilder;
                videoWin = (IVideoWindow)graphBuilder;

                pane.getInfo(InfoQueue, fileName);
                graphBuilder.RenderFile( fileName, null );

                videoWin.put_Owner(Video_panel.Handle);
                //videoWin.put_Owner(this.Handle);
                videoWin.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipChildren);

                setWindow();
                mediaCtrl.Run();

                double total_time;
                mediaPos.get_Duration(out total_time);

                Video_timer.Enabled = true;
                iStatus = InfoStatus.Play;
                message_label.Width = 0;
                message_label.Height = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't start");
            }
        }

        private void setWindow()
        {
            if (dMode == DisplayMode.Normal)
            {
                unit = Video_panel.ClientRectangle.Width / 18;


                this.Width = 690;
                this.Height = 450;
                Video_panel.Width = 480;
                Video_panel.Height = 360;

                w_h_ratio = ((double)Video_panel.ClientRectangle.Height) / Video_panel.ClientRectangle.Width;
                win_width = Video_panel.Width;
                //win_height = Video_panel.Height;
                win_height = (int)(win_width * (w_h_ratio));
                

                
                //win_height = (int)(win_width * (w_h_ratio));
                Video_panel.Location = new Point(this.Width / 2 - Video_panel.Width / 2, 25);
                
                
                //w_h_ratio = ((double)Video_panel.ClientRectangle.Height) / Video_panel.ClientRectangle.Width;
                
                pBox_img.Location = new Point(Video_panel.Location.X+55, Video_panel.Location.Y + Video_panel.Height / 2 );

                //message_label.Font = new Font( 10.5,FontStyle.Bold);

                ButtonPbox.Location = new Point(this.Width - 40, Video_panel.Location.Y + win_height / 3);
                ButtonPbox.Width  = 25;
                ButtonPbox.Height = 100;
                ButtonPbox.Image = Image.FromFile(@"./scr/button.png");
                //ButtonPbox.Visible = true;

                this.message_label.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.answer_label.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            else {
                //Video_panel.Location = new Point(22, 0);

                //Video_panel.Width = 1320;
                //w_h_ratio = ((double)this.Height) / this.Width;
                w_h_ratio = 3.0 / 4;
                Video_panel.Height = this.Height-26;
                Video_panel.Width = (int)(Video_panel.Height /(w_h_ratio)  );
                Video_panel.Location = new Point( this.Width/2 - Video_panel.Width/2, 0);

                ButtonPbox.Location = new Point(this.Width - 25, Video_panel.Location.Y + win_height / 3);
                ButtonPbox.Width = 25;
                ButtonPbox.Height = 100;

                pBox_img.Location = new Point(Video_panel.Location.X + Video_panel.Width/4-FimgSize/2, Video_panel.Location.Y + Video_panel.Height / 2 + 35);
                this.message_label.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.answer_label.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            win_pos_left = Video_panel.ClientRectangle.Left;
            win_pos_top = Video_panel.ClientRectangle.Top;
            win_width = Video_panel.ClientRectangle.Width;
            win_height = Video_panel.ClientRectangle.Height;

            videoWin.SetWindowPosition(win_pos_left, win_pos_top, win_width, win_height);
            //ButtonPbox.Location = new Point(Video_panel.Location.X + win_width, Video_panel.Location.Y + win_height / 3);

            message_label.Location = new Point(Video_panel.Location.X + win_width / 2-70, Video_panel.Location.Y + 10 );
            answer_label.Location = new Point(Video_panel.Location.X + win_width / 2-70, Video_panel.Location.Y + win_height * 2 / 3);
        }

        private void Stop_button_Click(object sender, EventArgs e)  {
            long time;
            mediaCtrl.Pause();

            double cur_time; ;
            double total_time;

            mediaPos.get_CurrentPosition(out cur_time);
            mediaPos.get_Duration(out total_time);
        }

        private void Video_timer_Tick(object sender, EventArgs e){
            double time_s;
            int time_ms;
            mediaPos.get_CurrentPosition(out time_s);
            time_ms = (int)time_s * 1000;

            double totalTime;
            mediaPos.get_Duration(out totalTime);

            if( time_s >= totalTime){
                CleanUp();
                Play(fileName);
            }

            //mediaPos.g
            //显示播放时间
            if (mediaPos!= null)
            {
                int s = (int)time_s;
                int h = s / 3600;
                int m = (s - (h * 3600)) / 60;
                s = s - (h * 3600 + m * 60);

                this.TimeStatusLabel.Text = String.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);

                //s = (int)m_objMediaPosition.CurrentPosition;
            }
            else
            {
                this.TimeStatusLabel.Text = "00:00:00";
            }

            //Time_label.Text = time_ms.ToString();

            ///处理私有信息
            //cur_info != null 并且 过了提示时间,则舍弃该信息
            if ( cur_info != null && iStatus==InfoStatus.Play  ) {
                //ButtonPbox.Visible = true;
                if (time_ms - cur_info.Pos > HINT_WAIT_TIME )
                {
                    cur_info = null;
                    ButtonPbox.Visible = false;
                    //this.KeyPreview = false;
                }
            }
            //TS_info cur_Info==null;
            if (InfoQueue.Count > 0)
            {
                pane tmp = InfoQueue.Peek();
                if ((time_ms >= tmp.Pos - HINT_OCCUR_TIME ) && (time_ms <= tmp.Pos + HINT_OCCUR_TIME))
                {
                    //cur_info = InfoQueue.Dequeue();
                    tmp = InfoQueue.Dequeue();

                    //如果在显示私有信息，则舍弃该条信息
                    if ( iStatus == InfoStatus.Play ){
                        cur_info = tmp;
                    //else
                        ButtonPbox.Visible = true;
                    }
                }
            }
        }

        private void showSize() {
            //this.SizeStatusLabel.Text = "Height: " + Video_panel.Height + " Width: " + Video_panel.Width + "Form Width:"+this.Height+"w_h_ratio:"+w_h_ratio;
            this.SizeStatusLabel.Text = " FormBorder: "+ this.Width+
                                        //" VideoPane X:"+Video_panel.Location.X+" VideoPane Width:"+Video_panel.Width+
                                        " VideoPane:" + (Video_panel.Location.X + Video_panel.Width) +
                                        " messBox: " + (message_label.Location.X + message_label.Width)+
                                        " PicBox:"+(pBox_img.Location.X+pBox_img.Width);
                                        //+ " Form Width:" + this.Height + "w_h_ratio:" + w_h_ratio;
        }

        private void zoomIn(){
            int w_off=2;
            if(this.Width<=900)  w_off = 1;
            //int h_off = 1;

            while ( win_width > Video_panel.ClientRectangle.Width / 2 ) {
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

        private void zoomOut() {
            int w_off = 2;
            if (this.Width <= 900) w_off = 1;

            while (win_width < Video_panel.ClientRectangle.Width ){
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
            setWindow();
        }

        private void Form1_KeyPress(object sender, KeyEventArgs e){
            if (iStatus == InfoStatus.Question) {
                answer_label.Width  = 50;
                answer_label.Height = 10;
                //char tmp = cur_info.Answer + 32;
                Keys KeyAns;
                switch(cur_info.Answer){
                    case 'a':
                        KeyAns = Keys.F1;
                        break;
                    case 'b':
                        KeyAns = Keys.F2;
                        break;
                    case 'c':
                        KeyAns = Keys.F3;
                        break;
                    case 'd':
                        KeyAns = Keys.F4;
                        break;
                    default:
                        KeyAns = Keys.E;
                        break;
                }
                if (e.KeyCode == KeyAns) answer_label.Text = ANS_RIGHT;
                else answer_label.Text = ANS_WRONG+ char.ToUpper(cur_info.Answer);
                answer_label.Width = 0;
                answer_label.Height = 0;
                iStatus = InfoStatus.Show;
                return;
            }

            switch (e.KeyCode) {
                case Keys.F1:
                    if (ButtonPbox.Visible == true && iStatus == InfoStatus.Play)
                    {
                        zoomIn();
                        if (cur_info != null)
                        {
                            //message_label.AutoSize = true;
                            message_label.Width = 200;
                            message_label.Height = 200;
                            if (dMode == DisplayMode.Full) {
                                message_label.Width = 380;
                                message_label.Height = 600;
                            }
                            
                            message_label.Text = cur_info.Text;
                            //message_label.AutoSize = true;

                            if (cur_info.Img != null)
                            {
                                //size
                                //pBox_img.Width=cur_info.Img.
                                pBox_img.Image = Image.FromFile(cur_info.Img);
                                pBox_img.Width = pBox_img.Image.Width;
                                pBox_img.Height = pBox_img.Image.Height;

                                //pBox_img.Width = 150;
                                //pBox_img.Height = 150;
                                if (dMode == DisplayMode.Full) {
                                    //pBox_img.Width = FimgSize;
                                    //pBox_img.Height = FimgSize;
                                }
                                
                            }
                            if (cur_info.Type == pType.info) iStatus = InfoStatus.Show;
                            else iStatus = InfoStatus.Question;
                        }
                    }
                    break;
                case Keys.F2:
                    if (menuStrip1.Visible == true && iStatus == InfoStatus.Play)
                    {
                        this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                        menuStrip1.Visible = false;
                        dMode = DisplayMode.Full;
                        Rectangle ret = Screen.GetWorkingArea(this);

                        /*
                        this.Video_panel.ClientSize = new Size(ret.Width, ret.Height);
                        this.Video_panel.Dock = DockStyle.Fill;
                        */

                        this.Video_panel.BringToFront();
                        setWindow();
                        this.Hide();
                        this.Show();
                        //showSize();
                    }
                    break;
                case Keys.F3:
                    if (menuStrip1.Visible == false && iStatus == InfoStatus.Play)
                    {
                        this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                        dMode = DisplayMode.Normal;
                        setWindow();
                        menuStrip1.Visible = true;
                        this.Hide();
                        this.Show();
                        //showSize();
                    }
                    break;
                case Keys.F4:
                    if (ButtonPbox.Visible == true && iStatus == InfoStatus.Show)
                    {
                        message_label.Text = "";
                        message_label.Width = 0;
                        message_label.Height = 0;
                        
                        answer_label.Text = "";
                        answer_label.Width = 0;
                        answer_label.Height = 0;

                        pBox_img.Image = null;
                        pBox_img.Width = 0;
                        pBox_img.Height = 0;
                        
                        zoomOut();
                        ButtonPbox.Visible = false;
                        iStatus = InfoStatus.Play;
                        cur_info = null;
                    }
                    break;
                default: 
                    break;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            setWindow();
        }

        /*
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {

        }*/
    }
}