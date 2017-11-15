namespace tuto_client
{
    partial class Home
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
            this.client_Label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.name_Label = new System.Windows.Forms.Label();
            this.beginining_Message = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.name_Label_2 = new System.Windows.Forms.Label();
            this.beginining_Message_2 = new System.Windows.Forms.LinkLabel();
            this.status = new System.Windows.Forms.Label();
            this.logout_btn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // client_Label
            // 
            this.client_Label.AutoSize = true;
            this.client_Label.Location = new System.Drawing.Point(23, 35);
            this.client_Label.Name = "client_Label";
            this.client_Label.Size = new System.Drawing.Size(64, 13);
            this.client_Label.TabIndex = 0;
            this.client_Label.Text = "Name Client";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.name_Label);
            this.panel1.Controls.Add(this.beginining_Message);
            this.panel1.Location = new System.Drawing.Point(26, 116);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(378, 30);
            this.panel1.TabIndex = 3;
            // 
            // name_Label
            // 
            this.name_Label.AutoSize = true;
            this.name_Label.Location = new System.Drawing.Point(14, 10);
            this.name_Label.Name = "name_Label";
            this.name_Label.Size = new System.Drawing.Size(76, 13);
            this.name_Label.TabIndex = 3;
            this.name_Label.Text = "Name Friend 1";
            // 
            // beginining_Message
            // 
            this.beginining_Message.AutoSize = true;
            this.beginining_Message.LinkColor = System.Drawing.Color.Black;
            this.beginining_Message.Location = new System.Drawing.Point(167, 10);
            this.beginining_Message.Name = "beginining_Message";
            this.beginining_Message.Size = new System.Drawing.Size(196, 13);
            this.beginining_Message.TabIndex = 2;
            this.beginining_Message.TabStop = true;
            this.beginining_Message.Text = "New Message : \"Hey buddy it\'s been...\"";
            this.beginining_Message.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.beginining_Message_LinkClicked);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.name_Label_2);
            this.panel2.Controls.Add(this.beginining_Message_2);
            this.panel2.Location = new System.Drawing.Point(26, 170);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(378, 30);
            this.panel2.TabIndex = 4;
            // 
            // name_Label_2
            // 
            this.name_Label_2.AutoSize = true;
            this.name_Label_2.Location = new System.Drawing.Point(14, 10);
            this.name_Label_2.Name = "name_Label_2";
            this.name_Label_2.Size = new System.Drawing.Size(76, 13);
            this.name_Label_2.TabIndex = 3;
            this.name_Label_2.Text = "Name Friend 2";
            // 
            // beginining_Message_2
            // 
            this.beginining_Message_2.AutoSize = true;
            this.beginining_Message_2.LinkColor = System.Drawing.Color.Black;
            this.beginining_Message_2.Location = new System.Drawing.Point(167, 10);
            this.beginining_Message_2.Name = "beginining_Message_2";
            this.beginining_Message_2.Size = new System.Drawing.Size(196, 13);
            this.beginining_Message_2.TabIndex = 2;
            this.beginining_Message_2.TabStop = true;
            this.beginining_Message_2.Text = "New Message : \"Hey buddy it\'s been...\"";
            this.beginining_Message_2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.beginining_Message_2_LinkClicked);
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(238, 35);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(79, 13);
            this.status.TabIndex = 5;
            this.status.Text = "Not Connected";
            // 
            // logout_btn
            // 
            this.logout_btn.Location = new System.Drawing.Point(334, 26);
            this.logout_btn.Name = "logout_btn";
            this.logout_btn.Size = new System.Drawing.Size(83, 30);
            this.logout_btn.TabIndex = 6;
            this.logout_btn.Text = "Logout";
            this.logout_btn.UseVisualStyleBackColor = true;
            this.logout_btn.Click += new System.EventHandler(this.logout_btn_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 226);
            this.Controls.Add(this.logout_btn);
            this.Controls.Add(this.status);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.client_Label);
            this.Name = "Home";
            this.Text = "Home";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label client_Label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel beginining_Message;
        private System.Windows.Forms.Label name_Label;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label name_Label_2;
        private System.Windows.Forms.LinkLabel beginining_Message_2;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Button logout_btn;
    }
}