namespace tuto_server
{
    partial class Server_side
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSend = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btn_listen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(230, 485);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(261, 55);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click_1);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(80, 132);
            this.txtLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(546, 244);
            this.txtLog.TabIndex = 2;
            this.txtLog.Text = "";
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(80, 417);
            this.txtSend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(544, 26);
            this.txtSend.TabIndex = 3;
            // 
            // btn_listen
            // 
            this.btn_listen.Location = new System.Drawing.Point(230, 43);
            this.btn_listen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_listen.Name = "btn_listen";
            this.btn_listen.Size = new System.Drawing.Size(261, 55);
            this.btn_listen.TabIndex = 4;
            this.btn_listen.Text = "Listen";
            this.btn_listen.UseVisualStyleBackColor = true;
            this.btn_listen.Click += new System.EventHandler(this.Btn_listen_Click);
            // 
            // Server_side
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 614);
            this.Controls.Add(this.btn_listen);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnSend);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Server_side";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btn_listen;
    }
}