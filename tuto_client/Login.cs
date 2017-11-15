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
    public partial class Login : Form
    {
        public event EventHandler<log_btn_event> log_update;
        public delegate void DelegateRaisingEvent_Bool(bool log);

        public Login()
        {
            InitializeComponent();
        }
        
        private void btn_login_Click(object sender, EventArgs e)
        {
            if (text_usename.Text != "")
            {
                log_update(this, new log_btn_event(true, text_usename.Text));
            } else
            {
                MessageBox.Show("Please enter a username.");
            }
        }

        public void RequestStop()
        {
            this.Close();
        }
    }
}
