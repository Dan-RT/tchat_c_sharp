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
    public partial class Tchat : Form
    {
        private string _id;
        public string ID
        {
            get { return _id; }
        }

        delegate void SetTextCallback(string text);
        delegate void SetTextCallback_safe(string text);

        public event EventHandler<send_btn_event> Send_update;
        public delegate void DelegateRaisingEvent(string message);
        
        public Tchat(string data)
        {
            _id = data;
            InitializeComponent();
        }

        private void Btn_Send_Click(object sender, EventArgs e)
        {
            string message = Text_Send.Text;
            if (message != "")
            {
                Send_update(this, new send_btn_event(message));
            }
        }

        public void message_sent (bool sent)
        {
            if (sent)
            {
                Text_Send.Text = " ";
            } else {
                MessageBox.Show("Message non envoyé, veuillez vous reconnecter.");
            }
        }
        
        public void update_message_feed (byte[] data)
        {
            if (Encoding.Default.GetString(data) == "01110010011001011110011101110101")
            {
                Messages_Feed.Text += System.Environment.NewLine + "Status : Connected to server.";
            } else {
                Messages_Feed.Text += System.Environment.NewLine + "Server : " + Encoding.Default.GetString(data); // Encoding.Default.GetString(data); Converts Bytes Received to String
            }
        }

        public void update_message_feed(string data)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.Messages_Feed.InvokeRequired)
            {
                SetTextCallback_safe d = new SetTextCallback_safe(update_message_feed);
                this.Invoke(d, new object[] { data });
            }
            else
            {
                this.Messages_Feed.Text += System.Environment.NewLine + data;
            }
        }

        private void ThreadProcSafe(string text)
        {
            this.update_message_feed(text);
        }

        public void exit_tchat()
        {
            this.Close();
        }
        
    }
}
