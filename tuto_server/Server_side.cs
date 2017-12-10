using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Net.NetworkInformation;
using System.Linq;

namespace tuto_server
{
    public partial class Server_side : Form
    {
        private TcpListener server = new TcpListener(IPAddress.Any, 1980); // Creates a TCP Listener To Listen to Any IPAddress trying to connect to the program with port 1980
        //private TcpClient client; // Creates a TCP Client
        //private Dictionary<TcpClient, Infos_client> listConnectedClients = new Dictionary<TcpClient, Infos_client>();
        private List<Client> listConnectedClients = new List<Client>(); //On teste une liste plutôt qu'un dico, car on veut pouvoir modif les données
        private bool server_on = false;
        private delegate void SetTextCallback_safe(string data);
        private delegate void SetTextCallback_listen(string data);
        private Object thisLock = new Object();

        public Server_side()
        {
            InitializeComponent();
            Btn_listen_Click(this, null);
        }
        
        private void Btn_listen_Click(object sender, EventArgs e)
         {
            if (!server_on)
            {
                Start_listening();
                Update_list_client();
            }
            else
            {
                try {
                    Net.ServerBroadcast(this, listConnectedClients, "server_close");
                    //Net.ServerSend(client, "server_close");
                    //on notifie le client que le server ferme
                } catch (Exception ex)
                {
                    //Console.WriteLine(ex);
                }
                //pour éviter que ça plante si personne ne s'est connecté
                server.Stop();
            }
        }

        private void Start_listening ()
        {
            server.Start(); // Starts Listening to Any IPAddress trying to connect to the program with port 1980
            btn_listen.Text = "Stop Listening";
            //Console.WriteLine("Waiting For Connection");
            Change_text("Status : Waiting For Connection...");
            server_on = true;
            new Thread(() => // Creates a New Thread (like a timer)
            {
                while (server_on)
                {
                    try
                    {
                        TcpClient client = server.AcceptTcpClient(); //Waits for the Client To Connect
                        Client client_tmp = new Client() { Name = "", IP = "", tcp_client = client };
                        
                        listConnectedClients.Add(client_tmp);
                        
                        Net.ServerSend(client, "client_connected");
                        //Console.WriteLine("Connected To Client");
                        if (client.Connected) // If you are connected
                        {
                            Net.ServerReceive(client, this); //Start Receiving
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
                Net.ServerBroadcast(this, listConnectedClients, txtSend.Text);
                txtSend.Clear();
            } catch
            {
                MessageBox.Show("Cannot receive data, no client connected.");
            }
        }

        public void Message_handling(byte[] data, TcpClient client)
        {
            string data_string = Encoding.Default.GetString(data);
            //Console.WriteLine(data_string);
            char[] delimiterChars = { '@', '#'};
            string[] words = data_string.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < words.Length; i++)
            {
                //System.Console.WriteLine("server : " + words[i]);
            }
            //words[0] --> pseudo du mec qui envoie
            //words[1] --> type du message
            //words[2] --> optionnel : pseudo du receveur
            //words[3] --> optionnel : message pour le receveur
            
            string type_message = words[1];
            Client client_tmp = new Client() { Name = words[0], IP = words[2], tcp_client = client };
            
            //System.Console.WriteLine(type_message);

            if (type_message.Equals("connection", StringComparison.OrdinalIgnoreCase)) {
                
                //connection
                Change_text("Status : " + client_tmp.Name + " connected.");
                ModifyListConnectedClients(client_tmp);

            } else if (type_message.Equals("disconnection", StringComparison.OrdinalIgnoreCase)) {
               
                //disconnection
                //Console.WriteLine(client_tmp.Name + " is gone :( ");
                //listConnectedClients.Remove(client);
                remove_item_listConnectedClients(client_tmp);
                Change_text("Status : " + client_tmp.Name + " is gone.");

            } else if (type_message == "message") {

                //normal message
                string receiver = words[2];
                string message = words[3];
                Change_text(Name + " to " + receiver + " : " + message);

                for (int i = 0; i < listConnectedClients.Count; i++)
                {
                    //Console.WriteLine("receiver : " + receiver);
                    //if (name != client_tmp.Key)
                    if (receiver == listConnectedClients[i].Name)
                    {
                        //System.Console.WriteLine("forwarded to : " + listConnectedClients[i].Name);
                        message = data_string;
                        Net.ServerSend(listConnectedClients[i].tcp_client, message);
                    }
                }
            }
        }

        public void Change_text(string data)
        {
            if (this.txtLog.InvokeRequired)
            {
                SetTextCallback_safe d = new SetTextCallback_safe(Change_text);
                this.Invoke(d, new object[] { data });
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
                //this.Name = "update_thread";
                while (server_on)
                {
                    ////Console.WriteLine("Clients connected : ");
                    lock(thisLock)
                    {
                        for (int i = 0; i < listConnectedClients.Count; i++)
                        {
                            Ping pingSender = new Ping();
                            IPAddress address = IPAddress.Parse(listConnectedClients[i].IP);
                            PingReply reply = pingSender.Send(address);

                            if (reply.Status == IPStatus.Success)
                            {
                                /*//Console.WriteLine("Address: {0}", reply.Address.ToString());
                                //Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                                //Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                                //Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                                //Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);*/
                            }
                            else
                            {
                                /*//Console.WriteLine(reply.Status);
                                //Console.WriteLine(client_tmp.Key.Name + " is gone :( ");*/
                                remove_item_listConnectedClients(listConnectedClients[i]);
                                display_listConnectedClients();
                                Change_text("Status : " + listConnectedClients[i].Name + " is gone.");
                                break;
                            }
                        }
                    }
                    display_listConnectedClients();
                    ////Console.WriteLine("Fin clients connected : ");
                    //text_clients_connected.Text = data;
                    Net.ServerBroadcast(this, listConnectedClients, listConnectedClients_parser());
                    Thread.Sleep(5000);
                }
            }).Start(); // Start the Thread
        }

        public void ModifyListConnectedClients(Client client_tmp)
        {
            bool flag = false;
            int index = 0;

            for (int i = 0; i < listConnectedClients.Count; i++)
            {
                if (listConnectedClients[i].tcp_client == client_tmp.tcp_client)
                {
                    flag = true;
                    index = i;
                    break;
                }
            }

            if (flag)
            {
                listConnectedClients[index] = new Client() { Name = client_tmp.Name, IP = client_tmp.IP, tcp_client = client_tmp.tcp_client };
            }
        }
        
        public String listConnectedClients_parser()
        {
            string list = "@server#List_clients";
            for (int i = 0; i < listConnectedClients.Count; i++)
            {
                list += "@" + listConnectedClients[i].Name;
            }
            return list;
        }

        public void remove_item_listConnectedClients(Client client_tmp)
        {
            bool flag = false;
            int index = 0;

            for (int i = 0; i < listConnectedClients.Count; i++)
            {
                if (listConnectedClients[i].tcp_client == client_tmp.tcp_client)
                {
                    flag = true;
                    index = i;
                    break;
                }
            }

            if (flag)
            {
                //Console.WriteLine("Removing " + listConnectedClients[index].Name);
                listConnectedClients[index].tcp_client.Close();
                listConnectedClients.RemoveAt(index);
            }
        }

        public void display_listConnectedClients ()
        {
            lock(this)
            {
                //Console.WriteLine("\n\nDébut recap clients.");
                for (int i = 0; i < listConnectedClients.Count; i++)
                {
                    //Console.WriteLine("Name : " + listConnectedClients[i].Name);
                    //Console.WriteLine("IP : " + listConnectedClients[i].IP);
                    //Console.WriteLine("Client : " + listConnectedClients[i].tcp_client);
                }
                //Console.WriteLine("Fin recap clients.\n\n");
            }
        }
    }

    public struct Client
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public TcpClient tcp_client { get; set; }
    }
}
