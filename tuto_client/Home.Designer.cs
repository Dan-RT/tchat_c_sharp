﻿namespace tuto_client
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
            this.create_group_chat = new System.Windows.Forms.LinkLabel();
            this.status = new System.Windows.Forms.Label();
            this.logout_btn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // client_Label
            // 
            this.client_Label.AutoSize = true;
            this.client_Label.Location = new System.Drawing.Point(34, 54);
            this.client_Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.client_Label.Name = "client_Label";
            this.client_Label.Size = new System.Drawing.Size(95, 20);
            this.client_Label.TabIndex = 0;
            this.client_Label.Text = "Name Client";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.create_group_chat);
            this.panel1.Location = new System.Drawing.Point(39, 178);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(567, 46);
            this.panel1.TabIndex = 3;
            // 
            // create_group_chat
            // 
            this.create_group_chat.AutoSize = true;
            this.create_group_chat.LinkColor = System.Drawing.Color.Black;
            this.create_group_chat.Location = new System.Drawing.Point(250, 15);
            this.create_group_chat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.create_group_chat.Name = "create_group_chat";
            this.create_group_chat.Size = new System.Drawing.Size(115, 20);
            this.create_group_chat.TabIndex = 2;
            this.create_group_chat.TabStop = true;
            this.create_group_chat.Text = "Create a group";
            this.create_group_chat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.create_group_chat_LinkClicked);
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(357, 54);
            this.status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(116, 20);
            this.status.TabIndex = 5;
            this.status.Text = "Not Connected";
            // 
            // logout_btn
            // 
            this.logout_btn.Location = new System.Drawing.Point(501, 40);
            this.logout_btn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.logout_btn.Name = "logout_btn";
            this.logout_btn.Size = new System.Drawing.Size(124, 46);
            this.logout_btn.TabIndex = 6;
            this.logout_btn.Text = "Logout";
            this.logout_btn.UseVisualStyleBackColor = true;
            this.logout_btn.Click += new System.EventHandler(this.Logout_btn_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 525);
            this.Controls.Add(this.logout_btn);
            this.Controls.Add(this.status);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.client_Label);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Home";
            this.Text = "Home";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label client_Label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel create_group_chat;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Button logout_btn;
    }
}