using System;
using System.Text;
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

        public event EventHandler<Send_btn_event> Send_update;
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
                Send_update(this, new Send_btn_event(message));
            }
        }

        public void Message_sent (bool sent)
        {
            if (sent)
            {
                Text_Send.Text = "";
            } else {
                MessageBox.Show("Message non envoyé, veuillez vous reconnecter.");
            }
        }
        
        public void Update_message_feed (byte[] data)
        {
            //string message = "@" + this._username + "#message" + "@" + receiver + "#" + pe.Message;

            String data_string = Encoding.Default.GetString(data);

            if (data_string == "client_connected")
            {
                Messages_Feed.Text += System.Environment.NewLine + "Status : Connected to server.";
            } else
            {
                char[] delimiterChars = { '@', '#' };
                string[] words = data_string.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                for (var i = 0; i < words.Length; i++)
                {
                    System.Console.WriteLine(words[i]);
                }
                //words[0] --> pseudo du mec qui envoie
                //words[1] --> type du message
                //words[2] --> optionnel : pseudo du receveur
                //words[3] --> optionnel : message pour le receveur

                string sender = words[0];
                string type_message = words[1];
                string message = words[3];

               // MessageBox.Show("Type message : " + type_message);

                //not secured
                /*data_string = sender + " : " + message;
                Messages_Feed.Text += System.Environment.NewLine + data_string;

                
                if (type_message == "connection")     //problem here :(
                {
                    string receiver = words[2];
                    data_string = sender + " : " + message;
                    Messages_Feed.Text += System.Environment.NewLine + data_string; 
                } else
                {
                    Messages_Feed.Text += System.Environment.NewLine + "Message error.";
                    MessageBox.Show("Type message : " + type_message + " Sender : " + sender + " Message : " + message);
                }*/
                
            }
            
        }

        public void Update_message_feed(string data)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            
            if (this.Messages_Feed.InvokeRequired)
            {
                SetTextCallback_safe d = new SetTextCallback_safe(Update_message_feed);
                this.Invoke(d, new object[] { data });
            }
            else
            {
                this.Messages_Feed.Text += System.Environment.NewLine + data;
            }
        }

        private void ThreadProcSafe(string text)
        {
            this.Update_message_feed(text);
        }

        public void Exit_tchat()
        {
            this.Close();
        }
        
    }
}
