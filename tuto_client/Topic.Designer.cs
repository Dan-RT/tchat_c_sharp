namespace tuto_client
{
    partial class Topic
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
            this.label1 = new System.Windows.Forms.Label();
            this.topic_box = new System.Windows.Forms.TextBox();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.create_chat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose a topic:";
            // 
            // topic_box
            // 
            this.topic_box.Location = new System.Drawing.Point(308, 105);
            this.topic_box.Name = "topic_box";
            this.topic_box.Size = new System.Drawing.Size(201, 26);
            this.topic_box.TabIndex = 1;
            // 
            // Cancel_button
            // 
            this.Cancel_button.Location = new System.Drawing.Point(170, 173);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(89, 42);
            this.Cancel_button.TabIndex = 2;
            this.Cancel_button.Text = "Cancel";
            this.Cancel_button.UseVisualStyleBackColor = true;
            this.Cancel_button.Click += new System.EventHandler(this.Cancel_button_Click);
            // 
            // create_chat
            // 
            this.create_chat.Location = new System.Drawing.Point(331, 173);
            this.create_chat.Name = "create_chat";
            this.create_chat.Size = new System.Drawing.Size(155, 42);
            this.create_chat.TabIndex = 3;
            this.create_chat.Text = "Create Group chat";
            this.create_chat.UseVisualStyleBackColor = true;
            this.create_chat.Click += new System.EventHandler(this.create_chat_Click);
            // 
            // Topic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 271);
            this.Controls.Add(this.create_chat);
            this.Controls.Add(this.Cancel_button);
            this.Controls.Add(this.topic_box);
            this.Controls.Add(this.label1);
            this.Name = "Topic";
            this.Text = "Topic";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox topic_box;
        private System.Windows.Forms.Button Cancel_button;
        private System.Windows.Forms.Button create_chat;
    }
}