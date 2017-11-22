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

namespace tuto_server
{
    public partial class Server_side : Form
    {
        private TcpListener server = new TcpListener(IPAddress.Any, 1980); // Creates a TCP Listener To Listen to Any IPAddress trying to connect to the program with port 1980
        private TcpClient client; // Creates a TCP Client
        private Dictionary<String, TcpClient> listConnectedClients = new Dictionary<String, TcpClient>();
        private bool server_on = false;
        private delegate void SetTextCallback_safe(string data);
        private delegate void SetTextCallback_listen(string data);
        
        public Server_side()
        {
            InitializeComponent();
        }
        
        private void btn_listen_Click(object sender, EventArgs e)
         {
            if (!server_on)
            {
                start_listening();
            }
            else
            {
                try {
                    Net.ServerSend(client, "0110000101100010011011110111001001110100");
                    //on notifie le client que le server ferme
                } catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                //pour éviter que ça plante si personne ne s'est connecté
                server.Stop();
            }
        }

        private void start_listening ()
        {
            server.Start(); // Starts Listening to Any IPAddress trying to connect to the program with port 1980
            btn_listen.Text = "Stop Listening";
            Console.WriteLine("Waiting For Connection");
            change_text("Status : Waiting For Connection...");
            server_on = true;
            new Thread(() => // Creates a New Thread (like a timer)
            {
                while (server_on)
                {
                    try
                    {
                        client = server.AcceptTcpClient(); //Waits for the Client To Connect
                        Net.ServerSend(client, "01110010011001011110011101110101");
                        Console.WriteLine("Connected To Client");
                        if (client.Connected) // If you are connected
                        {
                            Net.ServerReceive(client, this); //Start Receiving
                            update_list_client();
                        }
                    } catch
                    {
                        //detect when server is stopped
                        server_on = false;
                        change_text("Status : Server is off.");
                        change_text_btn_listen("Listen");
                    }
                }
            }).Start();
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (client.Connected && txtSend.Text != "") // if the client is connected
                {
                    Net.ServerSend(client, txtSend.Text); // uses the Function ClientSend and the msg as txtSend.Text
                    txtSend.Clear();
                }
            } catch
            {
                MessageBox.Show("Cannot receive data, client not connected.");
            }
        }

        public void message_handling(byte[] data)
        {
            string data_string = Encoding.Default.GetString(data);
            Console.WriteLine(data_string);
            char[] delimiterChars = { '@', '#'};
            string[] words = data_string.Split(delimiterChars);

            foreach (string s in words)
            {
                System.Console.WriteLine(s);
            }
            //words[1] --> pseudo du mec qui envoie
            //words[2] --> type du message
            //words[3] --> optionnel : pseudo du receveur
            //words[4] --> optionnel : message pour le receveur

            string name = words[1];
            string type_message = words[2];
            
            if (type_message.Equals("011011100110010101110111", StringComparison.OrdinalIgnoreCase)) {
                //connection
                change_text("Status : " + name + " connected.");
                listConnectedClients.Add(name, client);

            } else if (type_message == "01101101011001010111001101110011") {
                //normal message
                string receiver = words[3];
                string message = words[4];
                change_text(name + " to " + receiver + " : " + message);

                foreach (KeyValuePair<string, TcpClient> client_tmp in listConnectedClients)
                {
                    if(name != client_tmp.Key)
                    {
                        message = data_string;
                        Net.ServerSend(client_tmp.Value, message);
                    }
                }
            }
        }

        private void change_text(string data)
        {
            if (this.txtLog.InvokeRequired)
            {
                SetTextCallback_safe d = new SetTextCallback_safe(change_text);
                this.Invoke(d, new object[] { "", data });
            }
            else
            {
                txtLog.Text += System.Environment.NewLine + data;
            }
        }

        public void change_text_btn_listen (string data)
        {
            if (this.btn_listen.InvokeRequired)
            {
                SetTextCallback_listen d = new SetTextCallback_listen(change_text_btn_listen);
                this.Invoke(d, new object[] { data });
            }
            else
            {
                btn_listen.Text = data;
            }
        }

        public void update_list_client ()
        {
            //String data = "";
            new Thread(() =>
            {
                while(server_on)
                {
                    Console.WriteLine("Clients connected : ");

                    foreach (KeyValuePair<string, TcpClient> client_tmp in listConnectedClients)
                    {
                        if (client_tmp.Value.Connected)
                        {
                            Console.WriteLine(client_tmp.Key + " is connected.");
                        }
                        else
                        {
                            Console.WriteLine(client_tmp.Key + " is gone :( ");
                            listConnectedClients.Remove(client_tmp.Key);
                        }
                        //data = data + "\n" + client_tmp.Key;      //marche pas 
                    }
                    Console.WriteLine("Fin clients connected : ");
                    //text_clients_connected.Text = data;

                    Thread.Sleep(5000);
                }
            }).Start(); // Start the Thread
            
        }
    }
}
