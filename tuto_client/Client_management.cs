using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace tuto_client
{
    class Client_management
    {
        private string _ip;
        public string IP
        {
            get { return _ip; }
        }
        private string _username;
        private TcpClient client;
        private Home client_home;
        private Login client_login;
        private List<Tchat> tchat_Liste = new List<Tchat>(0);
        private List<String> people_connected = new List<String>(0);
        private Thread login;
        private Thread home = null;
        private Net Net = null;

        public Client_management(string IP)
        {
            this._ip = IP;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Net = new Net();
            Net.Log_update += Connection_management;

            login = new Thread(new ThreadStart(Login_start));
            login.Start();
        }
        
        private void Login_start()
        {
            client_login = new Login();
            client_login.Log_update += Connection_management;
            Application.Run(client_login);
        }

        private void Home_start()
        {
            client_home = new Home(_username);
            client_home.Log_update += Connection_management;
            client_home.Tchat_update += Open_new_tchat;
            Application.Run(client_home);
        }
        
        public void Loss_connection()
        {
            client.GetStream().Close();
            client.Close();
            login = new Thread(new ThreadStart(Login_start));
            login.Start();
        }

        private void Connection_management (object sender, Log_btn_event pe)
        {
            if (pe.Log)
            {
                //login
                Connect(pe.Username);
            } else
            {
                //logout
                Loss_connection();
            }
        }
        
        private void Connect (string username)
        {
            this._username = username;
            try
            {
                client = new TcpClient(this._ip, 1980); //Tries to Connect

                if (client.Connected/* && Net.Client_Connection(this.client)*/) { //Starts Receiving if Connected

                    Net.ClientSend(this.client, "@" + username + "#" + "connection");
                    Net.ClientReceive(this, this.client);
                    //Net.ClientReceive(this, this.client, new_tchat);
                    client_login.RequestStop();
                   
                    if (home == null)
                    {
                        Console.WriteLine("home boot");
                        //creating home page if connected
                        home = new Thread(new ThreadStart(Home_start));
                        home.Start();
                    } else if (!home.IsAlive)
                    {
                        Console.WriteLine("home reboot");
                        home = new Thread(new ThreadStart(Home_start));
                        home.Start();
                    }
                }
                else
                {
                    MessageBox.Show("Connection failed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed.");
                MessageBox.Show(ex.Message);
            }
        }

        private void Send_Message (object sender, Send_btn_event pe)
        {
            Tchat tchat = sender as Tchat;
            string receiver = "Michel"; /// probleme ici
            string message = "@" + this._username + "#message" + "@" + receiver + "#" + pe.Message;
            tchat.Message_sent(Net.ClientSend(client, message));
        }

        public void handling_message (string data)
        {
            if (data == "client_connected")
            {
                //Messages_Feed.Text += System.Environment.NewLine + "Status : Connected to server.";
            } else {
                char[] delimiterChars = { '@', '#' };
                string[] words = data.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                for (var i = 0; i < words.Length; i++)
                {
                    //System.Console.WriteLine(words[i]);
                }

                //pour bien implementer cette fonction on a besoin de la l

                //words[0] --> pseudo du mec qui envoie
                //words[1] --> type du message
                //words[2] --> optionnel : pseudo du receveur
                //words[3] --> optionnel : message pour le receveur

                if (words.Length > 3)
                {
                    string sender = words[0];
                    string type_message = words[1];
                    if (type_message == "List_clients")
                    {
                        people_connected.Clear();
                        for (var i = 2; i < words.Length; i++)
                        {
                            //System.Console.WriteLine(words[i]);
                            people_connected.Add(words[i]);
                        }
                    } else
                    {
                        string message = words[3];
                        Open_tchat(sender, message);
                    }
                }
                
                /*
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

        private void Open_new_tchat(object sender, New_tchat_event p)   //function which can be called from outside with a event
        {
            if (Search_Name_Tchat(p.Data) == null)
                //si le Tchat n'existe pas déjà, on évite de l'ouvrir deux fois
            {
                Tchat new_tchat = new Tchat(p.Data, this._username);
                new_tchat.Send_update += Send_Message;
                tchat_Liste.Add(new_tchat);
                
                new Thread(() =>
                {
                    Application.Run(new_tchat);
                    Search_Delete_Tchat(new_tchat.FriendName);
                    //on enlève le tchat de la liste pour qu'on puisse le rouvrir plus tard
                    Console.WriteLine("Tchat fermé");
                }).Start();
            }
        }

        private void Open_tchat(string sender, string message)    //function to be called from client_side
        {
            Tchat tchat = Search_Name_Tchat(sender);

            if (tchat == null)
            //si le Tchat n'existe pas déjà, on le crée
            {
                MessageBox.Show("New tchat");
                tchat = new Tchat(this._username, sender);
                tchat.Send_update += Send_Message;
                tchat_Liste.Add(tchat);
                
                new Thread(() =>
                {
                    Application.Run(tchat);
                    Search_Delete_Tchat(tchat.FriendName);
                    //on enlève le tchat de la liste pour qu'on puisse le rouvrir plus tard
                    Console.WriteLine("Tchat fermé");
                }).Start();
            }

            //Il existe maintenant forcément, on lui balance le message
            tchat.ThreadProcSafe(sender + " : " + message);

        }

        private Tchat Search_Name_Tchat(string name)
        {
            Console.WriteLine("Current name tested : " + name);

            foreach (Tchat item in tchat_Liste)
            {
                Console.WriteLine("Current item : " + item.FriendName);
                if (item.FriendName == name)
                {
                    return item;
                }
            }
            return null;
        }

        private bool Search_Delete_Tchat(string name)
        {
            foreach (Tchat item in tchat_Liste)
            {
                if (item.FriendName == name)
                {
                    tchat_Liste.Remove(item);
                    return true;
                }
            }
            MessageBox.Show("Error : Tchat not found to be deleted.");
            return false;
        }
        
    }

    public class Log_btn_event : EventArgs
    {
        private bool _log;
        public bool Log
        {
            get { return _log; }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
        }

        public Log_btn_event(bool log, string name) : base()
        {
            _log = log;
            _username = name;
        }
    }

    public class Send_btn_event : EventArgs
    {
        private string _message;
        public string Message
        {
            get { return _message; }
        }

        public Send_btn_event(string message) : base()
        {
            _message = message;
        }
    }

    public class New_tchat_event : EventArgs
    {
        private string _data;
        public string Data
        {
            get { return _data; }
        }

        public New_tchat_event(string data) : base()
        {
            _data = data;
        }
    }
    
}
