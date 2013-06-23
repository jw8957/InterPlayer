namespace DSTry
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        /*
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        */
        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Video_panel = new System.Windows.Forms.Panel();
            this.answer_label = new System.Windows.Forms.Label();
            this.pBox_img = new System.Windows.Forms.PictureBox();
            this.message_label = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.TimeStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.SizeStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Video_timer = new System.Windows.Forms.Timer(this.components);
            this.ButtonPbox = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.Video_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBox_img)).BeginInit();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonPbox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(675, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // Video_panel
            // 
            this.Video_panel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Video_panel.Controls.Add(this.answer_label);
            this.Video_panel.Controls.Add(this.pBox_img);
            this.Video_panel.Controls.Add(this.message_label);
            this.Video_panel.Location = new System.Drawing.Point(22, 26);
            this.Video_panel.Name = "Video_panel";
            this.Video_panel.Size = new System.Drawing.Size(630, 360);
            this.Video_panel.TabIndex = 1;
            // 
            // answer_label
            // 
            this.answer_label.AutoSize = true;
            this.answer_label.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.answer_label.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.answer_label.Location = new System.Drawing.Point(356, 338);
            this.answer_label.Name = "answer_label";
            this.answer_label.Size = new System.Drawing.Size(0, 14);
            this.answer_label.TabIndex = 7;
            // 
            // pBox_img
            // 
            this.pBox_img.Location = new System.Drawing.Point(38, 211);
            this.pBox_img.Name = "pBox_img";
            this.pBox_img.Size = new System.Drawing.Size(0, 0);
            this.pBox_img.TabIndex = 6;
            this.pBox_img.TabStop = false;
            // 
            // message_label
            // 
            this.message_label.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.message_label.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.message_label.Location = new System.Drawing.Point(324, 13);
            this.message_label.Name = "message_label";
            this.message_label.Size = new System.Drawing.Size(279, 268);
            this.message_label.TabIndex = 0;
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.Enabled = false;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TimeStatusLabel,
            this.SizeStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 386);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(675, 26);
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip1";
            // 
            // TimeStatusLabel
            // 
            this.TimeStatusLabel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.TimeStatusLabel.Margin = new System.Windows.Forms.Padding(0, 3, 6, 2);
            this.TimeStatusLabel.Name = "TimeStatusLabel";
            this.TimeStatusLabel.Size = new System.Drawing.Size(0, 21);
            // 
            // SizeStatusLabel
            // 
            this.SizeStatusLabel.Name = "SizeStatusLabel";
            this.SizeStatusLabel.Size = new System.Drawing.Size(0, 21);
            // 
            // Video_timer
            // 
            this.Video_timer.Interval = 1000;
            this.Video_timer.Tick += new System.EventHandler(this.Video_timer_Tick);
            // 
            // ButtonPbox
            // 
            this.ButtonPbox.Location = new System.Drawing.Point(654, 28);
            this.ButtonPbox.Name = "ButtonPbox";
            this.ButtonPbox.Size = new System.Drawing.Size(18, 72);
            this.ButtonPbox.TabIndex = 8;
            this.ButtonPbox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(675, 412);
            this.Controls.Add(this.ButtonPbox);
            this.Controls.Add(this.Video_panel);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "InterPlayer";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyPress);
            //this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.Video_panel.ResumeLayout(false);
            this.Video_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBox_img)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonPbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.Panel Video_panel;
        private System.Windows.Forms.Timer Video_timer;
        private System.Windows.Forms.Label message_label;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel TimeStatusLabel;
        private System.Windows.Forms.PictureBox pBox_img;
        private System.Windows.Forms.Label answer_label;
        private System.Windows.Forms.ToolStripStatusLabel SizeStatusLabel;
        private System.Windows.Forms.PictureBox ButtonPbox;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
    }
}