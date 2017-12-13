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
        Topic new_topic;

        public event EventHandler<New_tchat_event> Tchat_update;
        public delegate void DelegateRaisingEvent_String(string data);

        public event EventHandler<Log_btn_event> Log_update;
        public delegate void DelegateRaisingEvent_Bool(bool log);

        public event EventHandler<New_group_event> New_Tchat_update;
        
        delegate void Generate_friend_list_Callback_safe(List<String> friend_list, List<String> group_list);

        private List<LinkLabel> label_list = new List<LinkLabel>();

        public Home(string name)
        {
            _name = name;
            InitializeComponent();
            Change_log_status("Connected");
            client_Label.Text = _name;
            //set_static_Friend_name();
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
        
        public void Exit_home ()
        {
            this.Close();
        }
        
        public void Generate_friend_list(List<String> friend_list, List<String> group_list)
        {
            if (this.InvokeRequired)
            {
                Generate_friend_list_Callback_safe d = new Generate_friend_list_Callback_safe(Generate_friend_list);
                this.Invoke(d, new object[] { friend_list, group_list });
            }
            else
            {
                Console.WriteLine("Generate_friend_list function called.");
                displaying_group_list(group_list);
                foreach (Label label_tmp in label_list)
                {
                    Console.WriteLine("\n\nRemoving " + label_tmp.Text + " item."); 
                    this.Controls.Remove(label_tmp);
                }
                label_list.Clear();

                foreach (String friend in friend_list)
                {
                    //Console.WriteLine("\nCreating friend list");
                    if (friend != _name)
                    {
                        //Console.WriteLine("\nCreating " + friend + " item");
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

                foreach (String group in group_list)
                {
                    LinkLabel label_tmp = new LinkLabel();
                    label_tmp.Text = group;
                    label_tmp.Links[0].LinkData = group;
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


                    label_tmp.Name = "Group_" + label_tmp.Top;
                    label_tmp.Left = 252;
                    label_tmp.Width = labelWidth;

                    label_tmp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Label_LinkClicked);

                    label_list.Add(label_tmp);
                    this.Controls.Add(label_tmp);
                }


            }
        }

        private void Label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            String target = e.Link.LinkData as String;
            Label label_tmp = sender as Label;

            if(label_tmp.Name.Contains("Group_")) {
                Tchat_update(this, new New_tchat_event(target, true));
            } else
            {
                Tchat_update(this, new New_tchat_event(target, false));
            }
        }
    
        private void create_group_chat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new_topic = new Topic();
            new_topic.Group_chat_event += create_group_chat_;

            Thread topic;
            topic = new Thread(new ThreadStart(topic_start));
            topic.Start();
        }

        private void topic_start ()
        {
            Application.Run(new_topic);
        }

        private void create_group_chat_(object sender, New_group_chat_event e)
        {
            New_Tchat_update(this, new New_group_event(e.Data));
        }

        private void displaying_group_list (List<String> group_list)
        {
            Console.WriteLine("Display group_list");
            foreach (string item in group_list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("End display group_list");
        }
    }
}
