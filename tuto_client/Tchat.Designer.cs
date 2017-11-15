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
            this.cLient_Label = new System.Windows.Forms.Label();
            this.Messages_Feed = new System.Windows.Forms.RichTextBox();
            this.Text_Send = new System.Windows.Forms.TextBox();
            this.Btn_Send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // friend_label
            // 
            this.friend_label.AutoSize = true;
            this.friend_label.Location = new System.Drawing.Point(41, 46);
            this.friend_label.Name = "friend_label";
            this.friend_label.Size = new System.Drawing.Size(35, 13);
            this.friend_label.TabIndex = 0;
            this.friend_label.Text = "label1";
            // 
            // cLient_Label
            // 
            this.cLient_Label.AutoSize = true;
            this.cLient_Label.Location = new System.Drawing.Point(41, 361);
            this.cLient_Label.Name = "cLient_Label";
            this.cLient_Label.Size = new System.Drawing.Size(35, 13);
            this.cLient_Label.TabIndex = 1;
            this.cLient_Label.Text = "label2";
            // 
            // Messages_Feed
            // 
            this.Messages_Feed.Location = new System.Drawing.Point(109, 21);
            this.Messages_Feed.Name = "Messages_Feed";
            this.Messages_Feed.Size = new System.Drawing.Size(361, 309);
            this.Messages_Feed.TabIndex = 2;
            this.Messages_Feed.Text = "";
            // 
            // Text_Send
            // 
            this.Text_Send.Location = new System.Drawing.Point(109, 348);
            this.Text_Send.Multiline = true;
            this.Text_Send.Name = "Text_Send";
            this.Text_Send.Size = new System.Drawing.Size(276, 60);
            this.Text_Send.TabIndex = 3;
            // 
            // Btn_Send
            // 
            this.Btn_Send.Location = new System.Drawing.Point(397, 346);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(72, 61);
            this.Btn_Send.TabIndex = 4;
            this.Btn_Send.Text = "Send";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // Tchat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 429);
            this.Controls.Add(this.Btn_Send);
            this.Controls.Add(this.Text_Send);
            this.Controls.Add(this.Messages_Feed);
            this.Controls.Add(this.cLient_Label);
            this.Controls.Add(this.friend_label);
            this.Name = "Tchat";
            this.Text = "Tchat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label friend_label;
        private System.Windows.Forms.Label cLient_Label;
        private System.Windows.Forms.RichTextBox Messages_Feed;
        private System.Windows.Forms.TextBox Text_Send;
        private System.Windows.Forms.Button Btn_Send;
    }
}