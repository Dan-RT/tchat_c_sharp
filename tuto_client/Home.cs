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
        private string _name;
        public string username
        {
            get { return _name; }
            set { _name = username; }
        }

        public event EventHandler<New_tchat_event> Tchat_update;
        public delegate void DelegateRaisingEvent_String(string data);

        public event EventHandler<Log_btn_event> Log_update;
        public delegate void DelegateRaisingEvent_Bool(bool log);

        delegate void Generate_friend_list_Callback_safe(List<String> friend_list);

        private List<LinkLabel> label_list = new List<LinkLabel>();
        //private List<String> friend_list = new List<String>();

        public Home(string name)
        {
            _name = name;
            InitializeComponent();
            Change_log_status("Connected");
            client_Label.Text = _name;
            set_static_Friend_name();
            //Generate_friend_list();
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

        private void set_static_Friend_name ()
        {
            if (_name == "Dan")  friend_name_Label.Text = "Nico";
            if (_name == "Nico") friend_name_Label.Text = "Dan";
        }

        private void Beginining_Message_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_name == "Dan") Tchat_update(this, new New_tchat_event("Nico"));
            if (_name == "Nico") Tchat_update(this, new New_tchat_event("Dan"));
            //static for now
        }
        
        public void Exit_home ()
        {
            this.Close();
        }


        /*  La suite est à contruire.  */

        public void Generate_friend_list(List<String> friend_list)
        {
            if (this.InvokeRequired)
            {
                Generate_friend_list_Callback_safe d = new Generate_friend_list_Callback_safe(Generate_friend_list);
                this.Invoke(d, new object[] { friend_list });
            }
            else
            {
                foreach (Label label_tmp in label_list)
                {
                    this.Controls.Remove(label_tmp);
                }
                label_list.Clear();

                foreach (String friend in friend_list)
                {
                    if (friend != _name)
                    {
                        LinkLabel label_tmp = new LinkLabel();
                        label_tmp.Text = friend;
                        label_tmp.Links[0].LinkData = friend;
                        label_tmp.LinkColor = System.Drawing.Color.Black;

                        const int labelWidth = 200;  // control variables for TextBox placement
                        const int labelHeight = 25;
                        const int labelMargin = 4;

                        if (label_list.Count == 0)
                        {
                            label_tmp.Top = 186 + labelMargin;
                        }
                        else
                        {
                            label_tmp.Top = 186 + ((labelHeight + labelMargin) * label_list.Count) + labelMargin;
                        }
                        

                        label_tmp.Left = 252;
                        label_tmp.Width = labelWidth;

                        label_tmp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Label_LinkClicked);

                        label_list.Add(label_tmp);
                        this.Controls.Add(label_tmp);
                    }
                }
            }
        }

        private void Label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            String target = e.Link.LinkData as String;
            Tchat_update(this, new New_tchat_event(target));
        }
    }
    
}
