namespace SocketServ_SuperSocket
{
    partial class SuperSocketServ
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_heart = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_log = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_count = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.txt_inner_iis = new System.Windows.Forms.TextBox();
            this.txt_inner_socket = new System.Windows.Forms.TextBox();
            this.txt_cloud_socket = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_listen = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btGameOper = new System.Windows.Forms.Button();
            this.list_SocketClient = new System.Windows.Forms.ListBox();
            this.socketObjectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txt_message = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.socketObjectBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_heart);
            this.panel1.Controls.Add(this.btn_clear);
            this.panel1.Controls.Add(this.btn_log);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lb_count);
            this.panel1.Controls.Add(this.btn_save);
            this.panel1.Controls.Add(this.txt_inner_iis);
            this.panel1.Controls.Add(this.txt_inner_socket);
            this.panel1.Controls.Add(this.txt_cloud_socket);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1031, 76);
            this.panel1.TabIndex = 0;
            // 
            // btn_heart
            // 
            this.btn_heart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_heart.Location = new System.Drawing.Point(656, 45);
            this.btn_heart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_heart.Name = "btn_heart";
            this.btn_heart.Size = new System.Drawing.Size(137, 25);
            this.btn_heart.TabIndex = 8;
            this.btn_heart.Text = "启用心跳处理";
            this.btn_heart.UseVisualStyleBackColor = true;
            this.btn_heart.Click += new System.EventHandler(this.btn_heart_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_clear.Location = new System.Drawing.Point(808, 45);
            this.btn_clear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(101, 25);
            this.btn_clear.TabIndex = 8;
            this.btn_clear.Text = "清除日志";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_log
            // 
            this.btn_log.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_log.Enabled = false;
            this.btn_log.Location = new System.Drawing.Point(915, 46);
            this.btn_log.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_log.Name = "btn_log";
            this.btn_log.Size = new System.Drawing.Size(101, 25);
            this.btn_log.TabIndex = 9;
            this.btn_log.Text = "显示日志";
            this.btn_log.UseVisualStyleBackColor = true;
            this.btn_log.Click += new System.EventHandler(this.btn_log_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "在线SOCKET客户端:";
            // 
            // lb_count
            // 
            this.lb_count.AutoSize = true;
            this.lb_count.Location = new System.Drawing.Point(159, 56);
            this.lb_count.Name = "lb_count";
            this.lb_count.Size = new System.Drawing.Size(15, 15);
            this.lb_count.TabIndex = 11;
            this.lb_count.Text = "0";
            // 
            // btn_save
            // 
            this.btn_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_save.Location = new System.Drawing.Point(916, 6);
            this.btn_save.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(100, 26);
            this.btn_save.TabIndex = 7;
            this.btn_save.Text = "保存配置";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_inner_iis
            // 
            this.txt_inner_iis.Location = new System.Drawing.Point(676, 6);
            this.txt_inner_iis.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_inner_iis.Name = "txt_inner_iis";
            this.txt_inner_iis.Size = new System.Drawing.Size(193, 25);
            this.txt_inner_iis.TabIndex = 6;
            // 
            // txt_inner_socket
            // 
            this.txt_inner_socket.Location = new System.Drawing.Point(401, 6);
            this.txt_inner_socket.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_inner_socket.Name = "txt_inner_socket";
            this.txt_inner_socket.Size = new System.Drawing.Size(193, 25);
            this.txt_inner_socket.TabIndex = 6;
            // 
            // txt_cloud_socket
            // 
            this.txt_cloud_socket.Location = new System.Drawing.Point(103, 6);
            this.txt_cloud_socket.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_cloud_socket.Name = "txt_cloud_socket";
            this.txt_cloud_socket.Size = new System.Drawing.Size(193, 25);
            this.txt_cloud_socket.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(599, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "内部IIS:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(300, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "内部SOCKET:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "云SOCKET:";
            // 
            // btn_close
            // 
            this.btn_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_close.Enabled = false;
            this.btn_close.Location = new System.Drawing.Point(907, 10);
            this.btn_close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(109, 45);
            this.btn_close.TabIndex = 8;
            this.btn_close.Text = "关闭监听";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_listen
            // 
            this.btn_listen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_listen.Location = new System.Drawing.Point(324, 10);
            this.btn_listen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_listen.Name = "btn_listen";
            this.btn_listen.Size = new System.Drawing.Size(401, 45);
            this.btn_listen.TabIndex = 4;
            this.btn_listen.Text = "启动监听";
            this.btn_listen.UseVisualStyleBackColor = true;
            this.btn_listen.Click += new System.EventHandler(this.btn_listen_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btGameOper);
            this.panel2.Controls.Add(this.btn_close);
            this.panel2.Controls.Add(this.btn_listen);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 646);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1031, 65);
            this.panel2.TabIndex = 2;
            // 
            // btGameOper
            // 
            this.btGameOper.Location = new System.Drawing.Point(16, 10);
            this.btGameOper.Margin = new System.Windows.Forms.Padding(4);
            this.btGameOper.Name = "btGameOper";
            this.btGameOper.Size = new System.Drawing.Size(100, 45);
            this.btGameOper.TabIndex = 9;
            this.btGameOper.Text = "管理比赛";
            this.btGameOper.UseVisualStyleBackColor = true;
            this.btGameOper.Click += new System.EventHandler(this.btGameOper_Click);
            // 
            // list_SocketClient
            // 
            this.list_SocketClient.Dock = System.Windows.Forms.DockStyle.Left;
            this.list_SocketClient.FormattingEnabled = true;
            this.list_SocketClient.ItemHeight = 15;
            this.list_SocketClient.Location = new System.Drawing.Point(0, 76);
            this.list_SocketClient.Margin = new System.Windows.Forms.Padding(4);
            this.list_SocketClient.Name = "list_SocketClient";
            this.list_SocketClient.Size = new System.Drawing.Size(169, 570);
            this.list_SocketClient.TabIndex = 4;
            // 
            // txt_message
            // 
            this.txt_message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_message.Location = new System.Drawing.Point(169, 76);
            this.txt_message.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_message.Name = "txt_message";
            this.txt_message.ReadOnly = true;
            this.txt_message.Size = new System.Drawing.Size(862, 570);
            this.txt_message.TabIndex = 5;
            this.txt_message.Text = "";
            this.txt_message.WordWrap = false;
            // 
            // SocketServ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 711);
            this.Controls.Add(this.txt_message);
            this.Controls.Add(this.list_SocketClient);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SocketServ";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.SocketServ_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.socketObjectBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_listen;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.TextBox txt_inner_iis;
        private System.Windows.Forms.TextBox txt_inner_socket;
        private System.Windows.Forms.TextBox txt_cloud_socket;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox list_SocketClient;
        private System.Windows.Forms.Button btGameOper;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_log;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_count;
        private System.Windows.Forms.RichTextBox txt_message;
        private System.Windows.Forms.BindingSource socketObjectBindingSource;
        private System.Windows.Forms.Button btn_heart;
    }
}

