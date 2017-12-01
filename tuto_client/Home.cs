﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace tuto_client
{
    public partial class Home : Form
    {
        public event EventHandler<New_tchat_event> Tchat_update;
        public delegate void DelegateRaisingEvent_String(string data);

        public event EventHandler<Log_btn_event> Log_update;
        public delegate void DelegateRaisingEvent_Bool(bool log);

        private List<Label> friend_list = new List<Label>();

        public Home()
        {
            InitializeComponent();
            Change_log_status("Connected");
        }
        
        public void Change_log_status (string data)
        {
            status.Text = data;
        }

        private void Logout_btn_Click(object sender, EventArgs e)
        {
            Log_update(this, new Log_btn_event(false, ""));
            this.Close();
        }

        private void Beginining_Message_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tchat_update(this, new New_tchat_event("1"));
        }

        private void Beginining_Message_2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tchat_update(this, new New_tchat_event("2"));
        }

        public void Exit_home ()
        {
            this.Close();
        }

        //public void generate_friend_list (Dictionary<String, TcpClient> listConnectedClients)
        public void Generate_friend_list()
        {
            /*foreach (KeyValuePair<string, TcpClient> client_tmp in listConnectedClients)
            {
                Label label_tmp = new Label();
                label_tmp.Text = client_tmp.Key;
                this.Controls.Add(label_tmp);
                friend_list.Add(label_tmp);
            }*/
            int j = 0;
            for (int i = 0; i < 3; i++)
            {
                j = j + 15;

                Label label_tmp = new Label
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(259, 337 + j),
                    Name = "Label" + "test" + i,
                    Size = new System.Drawing.Size(107, 43),
                    TabIndex = 0,
                    Text = "test" + i
                };

                this.Controls.Add(label_tmp);
                friend_list.Add(label_tmp);
            }
        }

        private void Btn_friend_list_Click(object sender, EventArgs e)
        {
            Generate_friend_list();
        }
    }
}
