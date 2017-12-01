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
                update_message_feed("You : " + message);
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
            //string message = "@" + this._username + "#01101101011001010111001101110011" + "@" + receiver + "#" + pe.Message;

            String data_string = Encoding.Default.GetString(data);

            if (data_string == "01110010011001011110011101110101")
            {
                //plus possible, à supprimer
                Messages_Feed.Text += System.Environment.NewLine + "Status : Connected to server.";
            } else
            {
                char[] delimiterChars = { '@', '#' };
                string[] words = data_string.Split(delimiterChars);

                foreach (string s in words)
                {
                    System.Console.WriteLine(s);
                }
                //words[1] --> pseudo du mec qui envoie
                //words[2] --> type du message
                //words[3] --> optionnel : pseudo du receveur
                //words[4] --> optionnel : message pour le receveur

                string sender = words[1];
                string type_message = words[2];
                string message = words[4];

                //MessageBox.Show("Type message : " + type_message);

                //not secured
                data_string = sender + " : " + message;
                Messages_Feed.Text += System.Environment.NewLine + data_string;

                /*
                if (type_message == "011011100110010101110111")     //problem here :(
                {
                    //string receiver = words[3];
                    data_string = sender + " : " + message;
                    Messages_Feed.Text += System.Environment.NewLine + data_string; 
                } else
                {
                    Messages_Feed.Text += System.Environment.NewLine + "Message error.";
                    MessageBox.Show("Type message : " + type_message + " Sender : " + sender + " Message : " + message);
                }
                */
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
