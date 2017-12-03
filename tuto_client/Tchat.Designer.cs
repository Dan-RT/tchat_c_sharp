namespace tuto_client
{
    partial class Tchat
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
            this.friend_label = new System.Windows.Forms.Label();
            this.client_Label = new System.Windows.Forms.Label();
            this.Messages_Feed = new System.Windows.Forms.RichTextBox();
            this.Text_Send = new System.Windows.Forms.TextBox();
            this.Btn_Send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // friend_label
            // 
            this.friend_label.AutoSize = true;
            this.friend_label.Location = new System.Drawing.Point(62, 71);
            this.friend_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.friend_label.Name = "friend_label";
            this.friend_label.Size = new System.Drawing.Size(51, 20);
            this.friend_label.TabIndex = 0;
            this.friend_label.Text = "label1";
            // 
            // client_Label
            // 
            this.client_Label.AutoSize = true;
            this.client_Label.Location = new System.Drawing.Point(62, 555);
            this.client_Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.client_Label.Name = "client_Label";
            this.client_Label.Size = new System.Drawing.Size(51, 20);
            this.client_Label.TabIndex = 1;
            this.client_Label.Text = "label2";
            // 
            // Messages_Feed
            // 
            this.Messages_Feed.Location = new System.Drawing.Point(164, 32);
            this.Messages_Feed.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Messages_Feed.Name = "Messages_Feed";
            this.Messages_Feed.Size = new System.Drawing.Size(540, 473);
            this.Messages_Feed.TabIndex = 2;
            this.Messages_Feed.Text = "";
            // 
            // Text_Send
            // 
            this.Text_Send.Location = new System.Drawing.Point(164, 535);
            this.Text_Send.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Text_Send.Multiline = true;
            this.Text_Send.Name = "Text_Send";
            this.Text_Send.Size = new System.Drawing.Size(412, 90);
            this.Text_Send.TabIndex = 3;
            // 
            // Btn_Send
            // 
            this.Btn_Send.Location = new System.Drawing.Point(596, 532);
            this.Btn_Send.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(108, 94);
            this.Btn_Send.TabIndex = 4;
            this.Btn_Send.Text = "Send";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // Tchat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 660);
            this.Controls.Add(this.Btn_Send);
            this.Controls.Add(this.Text_Send);
            this.Controls.Add(this.Messages_Feed);
            this.Controls.Add(this.client_Label);
            this.Controls.Add(this.friend_label);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Tchat";
            this.Text = "Tchat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label friend_label;
        private System.Windows.Forms.Label client_Label;
        private System.Windows.Forms.RichTextBox Messages_Feed;
        private System.Windows.Forms.TextBox Text_Send;
        private System.Windows.Forms.Button Btn_Send;
    }
}