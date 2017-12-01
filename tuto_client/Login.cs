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
        public event EventHandler<Log_btn_event> Log_update;
        public delegate void DelegateRaisingEvent_Bool(bool log);

        public Login()
        {
            InitializeComponent();
        }
        
        private void Btn_login_Click(object sender, EventArgs e)
        {
            if (text_usename.Text != "")
            {
                Log_update(this, new Log_btn_event(true, text_usename.Text));
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
