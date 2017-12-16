namespace tuto_client
{
    partial class Login
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
            this.text_usename = new System.Windows.Forms.TextBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.text_password = new System.Windows.Forms.TextBox();
            this.label_password = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 153);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username :";
            // 
            // text_usename
            // 
            this.text_usename.Location = new System.Drawing.Point(310, 150);
            this.text_usename.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.text_usename.Name = "text_usename";
            this.text_usename.Size = new System.Drawing.Size(148, 26);
            this.text_usename.TabIndex = 1;
            this.text_usename.KeyUp += new System.Windows.Forms.KeyEventHandler(this.text_usename_KeyUp);
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(245, 265);
            this.btn_login.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(112, 35);
            this.btn_login.TabIndex = 2;
            this.btn_login.Text = "Login";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.Btn_login_Click);
            // 
            // text_password
            // 
            this.text_password.Location = new System.Drawing.Point(310, 197);
            this.text_password.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.text_password.Name = "text_password";
            this.text_password.Size = new System.Drawing.Size(148, 26);
            this.text_password.TabIndex = 4;
            this.text_password.MaxLength = 8;
            this.text_password.PasswordChar = '*';
            this.text_password.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.text_password.KeyUp += new System.Windows.Forms.KeyEventHandler(this.text_password_KeyUp);
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(140, 200);
            this.label_password.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(86, 20);
            this.label_password.TabIndex = 3;
            this.label_password.Text = "Password :";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 440);
            this.Controls.Add(this.text_password);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.text_usename);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_usename;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.TextBox text_password;
        private System.Windows.Forms.Label label_password;
    }
}