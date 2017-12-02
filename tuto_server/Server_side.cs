using System;
using System.Collections.Generic;
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
        
        private void Btn_listen_Click(object sender, EventArgs e)
         {
            if (!server_on)
            {
                Start_listening();
            }
            else
            {
                try {
                    Net.ServerSend(client, "server_close");
                    //on notifie le client que le server ferme
                } catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                //pour éviter que ça plante si personne ne s'est connecté
                server.Stop();
            }
        }

        private void Start_listening ()
        {
            server.Start(); // Starts Listening to Any IPAddress trying to connect to the program with port 1980
            btn_listen.Text = "Stop Listening";
            Console.WriteLine("Waiting For Connection");
            Change_text("Status : Waiting For Connection...");
            server_on = true;
            new Thread(() => // Creates a New Thread (like a timer)
            {
                while (server_on)
                {
                    try
                    {
                        client = server.AcceptTcpClient(); //Waits for the Client To Connect
                        Net.ServerSend(client, "client_connected");
                        Console.WriteLine("Connected To Client");
                        if (client.Connected) // If you are connected
                        {
                            Net.ServerReceive(client, this); //Start Receiving
                            Update_list_client();
                        }
                    } catch
                    {
                        //detect when server is stopped
                        server_on = false;
                        Change_text("Status : Server is off.");
                        Change_text_btn_listen("Listen");
                    }
                }
            }).Start();
        }

        private void BtnSend_Click_1(object sender, EventArgs e)
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

        public void Message_handling(byte[] data)
        {

            string data_string = Encoding.Default.GetString(data);
            Console.WriteLine(data_string);
            char[] delimiterChars = { '@', '#'};
            string[] words = data_string.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < words.Length; i++)
            {
                System.Console.WriteLine("server : " + words[i]);
            }
            //words[0] --> pseudo du mec qui envoie
            //words[1] --> type du message
            //words[2] --> optionnel : pseudo du receveur
            //words[3] --> optionnel : message pour le receveur

            string name = words[0];
            string type_message = words[1];
            System.Console.WriteLine(type_message);
            if (type_message.Equals("connection", StringComparison.OrdinalIgnoreCase)) {
                //connection
                Change_text("Status : " + name + " connected.");
                listConnectedClients.Add(name, client);

            } else if (type_message == "message") {
                //normal message
                string receiver = words[2];
                string message = words[3];
                Change_text(name + " to " + receiver + " : " + message);

                foreach (KeyValuePair<string, TcpClient> client_tmp in listConnectedClients)
                {
                    System.Console.WriteLine("teeeest1");
                    if (name != client_tmp.Key)
                    {
                        System.Console.WriteLine("teeeeest2");
                        message = data_string;
                        Net.ServerSend(client_tmp.Value, message);
                    }
                }
            }
        }

        private void Change_text(string data)
        {
            if (this.txtLog.InvokeRequired)
            {
                SetTextCallback_safe d = new SetTextCallback_safe(Change_text);
                this.Invoke(d, new object[] { "", data });
            }
            else
            {
                txtLog.Text += System.Environment.NewLine + data;
            }
        }

        public void Change_text_btn_listen (string data)
        {
            if (this.btn_listen.InvokeRequired)
            {
                SetTextCallback_listen d = new SetTextCallback_listen(Change_text_btn_listen);
                this.Invoke(d, new object[] { data });
            }
            else
            {
                btn_listen.Text = data;
            }
        }

        public void Update_list_client()
        {
            new Thread(() =>
            {
                while (server_on)
                {
                    //Console.WriteLine("Clients connected : ");

                    foreach (KeyValuePair<string, TcpClient> client_tmp in listConnectedClients)
                    {
                        if (client_tmp.Value.Connected)
                        {
                           //Console.WriteLine(client_tmp.Key + " is connected.");
                        }
                        else
                        {
                            Console.WriteLine(client_tmp.Key + " is gone :( ");
                            listConnectedClients.Remove(client_tmp.Key);
                            Change_text("Status : " + client_tmp.Key + " is gone.");

                            //Console.WriteLine("Actualisation de la liste en cours...");
                            break;
                        }
                        //data = data + "\n" + client_tmp.Key;      //marche pas 
                    }
                    //Console.WriteLine("Fin clients connected : ");
                    //text_clients_connected.Text = data;

                    Net.ServerBroadcast(this, listConnectedClients, listConnectedClients_parser());

                    Thread.Sleep(5000);
                }
            }).Start(); // Start the Thread
        }

        public string listConnectedClients_parser()
        {
            string list = "@server#List_clients";
            foreach (KeyValuePair<string, TcpClient> client_tmp in listConnectedClients)
            {
                if (client_tmp.Value.Connected) //you never know ahah
                {
                    list += "@" + client_tmp.Key;
                }
            }
            //Console.WriteLine(list);
            return list;
        }
    }
}
