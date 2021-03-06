﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tuto_client
{
    public partial class Login : Form
    {
        public event EventHandler<Log_btn_event> Log_update;
        public delegate void DelegateRaisingEvent_Bool(bool log);

        public Login()
        {
            InitializeComponent();
        }
        
        private void Btn_login_Click(object sender, EventArgs e)
        {
            if (text_usename.Text != "" && text_password.Text != "")
            {
                if (text_password.Text == "password")
                {
                    Log_update(this, new Log_btn_event(true, text_usename.Text));
                } else
                {
                    MessageBox.Show("Incorrect password, please try again.");
                    text_password.Text = "";
                }
            } else
            {
                MessageBox.Show("Please fill all the required fields.");
            }
        }

        public void RequestStop()
        {
            this.Close();
        }

        private void text_usename_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                text_password.Focus();
            }
        }

        private void text_password_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_login.PerformClick();
            }
        }
    }
}
