using System;
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
        public event EventHandler<new_tchat_event> Tchat_update;
        public delegate void DelegateRaisingEvent_String(string data);

        public event EventHandler<log_btn_event> log_update;
        public delegate void DelegateRaisingEvent_Bool(bool log);

        private List<Label> friend_list = new List<Label>();

        public Home()
        {
            InitializeComponent();
            change_log_status("Connected");
        }
        
        public void change_log_status (string data)
        {
            status.Text = data;
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            log_update(this, new log_btn_event(false, ""));
            this.Close();
        }

        private void beginining_Message_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tchat_update(this, new new_tchat_event("1"));
        }

        private void beginining_Message_2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tchat_update(this, new new_tchat_event("2"));
        }

        public void exit_home ()
        {
            this.Close();
        }

        //public void generate_friend_list (Dictionary<String, TcpClient> listConnectedClients)
        public void generate_friend_list(List<String> people_connected)
        {
            //foreach (KeyValuePair<string, TcpClient> client_tmp in listConnectedClients)
            friend_list.Clear();
            int j = 0;
            Console.WriteLine("Home people_connected");
            foreach (String person_tmp in people_connected)
            {
                Console.WriteLine(person_tmp);
                Label label_tmp = new Label();
                label_tmp.AutoSize = true;
                label_tmp.Location = new System.Drawing.Point(259, 337 + j);
                label_tmp.Name = "Label" + "test" + j;
                label_tmp.Size = new System.Drawing.Size(107, 43);
                label_tmp.TabIndex = 0;
                label_tmp.Text = person_tmp;

                this.Controls.Add(label_tmp);
                friend_list.Add(label_tmp);

                j = j + 15;
            }
            Console.WriteLine("End home people_connected");


            /*
            for (int i = 0; i < 3; i++)
            {
                j = j + 15;

                Label label_tmp = new Label();
                
                label_tmp.AutoSize = true;
                label_tmp.Location = new System.Drawing.Point(259, 337+j);
                label_tmp.Name = "Label" + "test" + i;
                label_tmp.Size = new System.Drawing.Size(107, 43);
                label_tmp.TabIndex = 0;
                label_tmp.Text = "test" + i;

                this.Controls.Add(label_tmp);
                friend_list.Add(label_tmp);
            }*/
        }

        private void btn_friend_list_Click(object sender, EventArgs e)
        {
            //generate_friend_list();
        }
        
    }
}
