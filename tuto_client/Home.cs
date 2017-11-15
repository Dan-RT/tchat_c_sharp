using System;
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
    public partial class Home : Form
    {
        public event EventHandler<new_tchat_event> Tchat_update;
        public delegate void DelegateRaisingEvent_String(string data);

        public event EventHandler<log_btn_event> log_update;
        public delegate void DelegateRaisingEvent_Bool(bool log);

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
    }
}
