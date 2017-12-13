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
        private List<String> group_connected = new List<String>(0);
        private List<String> group_belonging = new List<String>(0);
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
            client_home.New_Tchat_update += create_group_chat;
            Application.Run(client_home);
        }
        
        public void Close_connection()
        {
            Net.ClientSend(client, "@"+ _username + "#disconnection" + "@" + IP);
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
                Close_connection();
            }
        }
        
        private void Connect (string username)
        {
            this._username = username;
            try
            {
                client = new TcpClient(this._ip, 1980); //Tries to Connect

                if (client.Connected/* && Net.Client_Connection(this.client)*/) { //Starts Receiving if Connected

                    Net.ClientSend(this.client, "@" + username + "#" + "connection" + "@" + this.IP);
                    Net.ClientReceive(this, this.client);
                    //Net.ClientReceive(this, this.client, new_tchat);
                    client_login.RequestStop();
                   
                    if (home == null)
                    {
                        //Console.WriteLine("home boot");
                        //creating home page if connected
                        home = new Thread(new ThreadStart(Home_start));
                        home.Start();
                    } else if (!home.IsAlive)
                    {
                        //Console.WriteLine("home reboot");
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
                //Console.WriteLine("Connection failed.");
                MessageBox.Show(ex.Message);
            }
        }

        private void Send_Message (object sender, Send_btn_event pe)
        {
            Tchat tchat = sender as Tchat;
            tchat.Message_sent(Net.ClientSend(client, pe.Message));
        }

        public void handling_message (string data)
        {
            if (data == "client_connected")
            {
                //Messages_Feed.Text += System.Environment.NewLine + "Status : Connected to server.";
            } else {

                Console.WriteLine("\n\n\nClients Data reçue : " + data);
                char[] delimiterChars = { '@', '#' };
                string[] words = data.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                
                //words[0] --> pseudo du mec qui envoie
                //words[1] --> type du message
                //words[2] --> optionnel : pseudo du receveur
                //words[3] --> optionnel : message pour le receveur

                if (words.Length > 3)
                {
                    string sender = words[0];
                    string type_message = words[1];
                    string receiver = words[2];
                    string message = words[3];

                    if (type_message == "List_clients")
                    {
                        people_connected.Clear();
                        group_connected.Clear();
                        lock (this)
                        {
                            //Console.WriteLine("Clients connected : ");
                            for (var i = 2; i < words.Length; i++)
                            {
                                Console.WriteLine("Group chats : ");
                                if (i == words.Length - 1 && words[i].Contains("&"))
                                {
                                    //getting group chats
                                    char[] delimiterChars_2 = { '&' };
                                    string[] words_2 = words[i].Split(delimiterChars_2, StringSplitOptions.RemoveEmptyEntries);
                                    for(var j = 0; j < words_2.Length; j++)
                                    {
                                        Console.WriteLine(words_2[j]);
                                        group_connected.Add(words_2[j]);
                                    } 

                                } else
                                {
                                    people_connected.Add(words[i]);
                                }
                            }
                            Console.WriteLine("End Group chats.");

                            Console.WriteLine("Generate_friend_list function should be called.");
                            if (client_home != null) client_home.Generate_friend_list(people_connected, group_connected);
                            //Console.WriteLine("End clients connected : ");
                        }
                    } else
                    {
                        if (receiver != _username)
                        {
                            //c'est un groupe
                            Open_tchat(receiver, message, true, sender);
                        } else
                        {
                            Open_tchat(sender, message);
                        }
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
            Tchat new_tchat;

            if (Search_Name_Tchat(p.Name) == null)
            //si le Tchat n'existe pas déjà, on évite de l'ouvrir deux fois
            {
                if (p.Group && !Search_Group(p.Name))
                {
                    if (Display_Message_Box("Do you want to join this group chat ?"))
                    {
                        group_belonging.Add(p.Name);
                        new_tchat = new Tchat(this._username, p.Name, true);
                        new_tchat.Action_group_update += Action_on_group;
                        Net.ClientSend(client, "@" + _username + "#JoinGroupChatMessage" + "@" + p.Name);
                    } else
                    {
                        return;
                    }
                } else
                {
                    new_tchat = new Tchat(this._username, p.Name, false);
                }
               
                new_tchat.Send_update += Send_Message;
                tchat_Liste.Add(new_tchat);

                new Thread(() =>
                {
                    Application.Run(new_tchat);
                    Search_Delete_Tchat(new_tchat.FriendName);
                    //on enlève le tchat de la liste pour qu'on puisse le rouvrir plus tard
                    //Console.WriteLine("Tchat fermé");
                }).Start();
            }
        }

        private void create_group_chat (object sender, New_group_event e)
        {
            if (Search_Name_Tchat(e.Name) == null)
            {
                Tchat new_tchat = new Tchat(this._username, e.Name, true);
                new_tchat.Send_update += Send_Message;
                new_tchat.Action_group_update += Action_on_group;
                
                group_belonging.Add(e.Name);
                tchat_Liste.Add(new_tchat);
                
                Net.ClientSend(client, "@" + _username + "#NewGroupChat" + "@" + e.Name);

                new Thread(() =>
                {
                    Application.Run(new_tchat);
                    Search_Delete_Tchat(new_tchat.FriendName); 
                    //on enlève le tchat de la liste pour qu'on puisse le rouvrir plus tard
                    //Console.WriteLine("Tchat fermé");
                }).Start();

            } else
            {
                MessageBox.Show("Please choose an other topic.");
            }
        }

        private void Open_tchat(string sender, string message, bool group = false, string group_sender = "")    //function to be called from client_side
        {
            Tchat tchat = Search_Name_Tchat(sender);

            if (tchat == null)
            //si le Tchat n'existe pas déjà, on le crée
            {
                tchat = new Tchat(this._username, sender, false);
                tchat.Send_update += Send_Message;
                tchat.Action_group_update += Action_on_group;
                tchat_Liste.Add(tchat);
                
                new Thread(() =>
                {
                    Application.Run(tchat);
                    Search_Delete_Tchat(tchat.FriendName);
                    //on enlève le tchat de la liste pour qu'on puisse le rouvrir plus tard
                    //Console.WriteLine("Tchat fermé");
                }).Start();
            }

            //Il existe maintenant forcément, on lui balance le message
            if (group)
            {
                tchat.ThreadProcSafe(group_sender + " : " + message);
            }
            else
            {
                tchat.ThreadProcSafe(sender + " : " + message);
            }
        }

        private Tchat Search_Name_Tchat(string name)
        {
            //Console.WriteLine("Current name tested : " + name);

            foreach (Tchat item in tchat_Liste)
            {
                //Console.WriteLine("Current item : " + item.FriendName);
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

        private bool Search_Group(string name)
        {
            foreach (String item in group_belonging)
            {
                if (item == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void Action_on_group (object sender, Action_group_event e)
        {
            //si on veut supprimer ou quitter le groupe
            if (e.Delete)
            {
                Net.ClientSend(client, "@" + _username + "#DeleteGroupChat" + "@" + e.Data);
            } else
            {
                Net.ClientSend(client, "@" + _username + "#LeaveGroupChatMessage" + "@" + e.Data);
            }
            group_belonging.Remove(e.Data);
        }

        private bool Display_Message_Box (string question)
        {
            // Configure the message box to be displayed
            string messageBoxText = question;
            string caption = "Word Processor";
            MessageBoxButtons button = MessageBoxButtons.YesNoCancel;
            
            // Display message box
            DialogResult result = MessageBox.Show(messageBoxText, caption, button);

            // Process message box results
            switch (result)
            {
                case DialogResult.Yes:
                    return true;
                case DialogResult.No:
                    return false;
                case DialogResult.Cancel:
                    return false;
            }
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
        private string _name;
        public string Name
        {
            get { return _name; }
        }
        private bool _group;   
        public bool Group
        {
            get { return _group; }
        }

        public New_tchat_event(string data, bool data_bool) : base()
        {
            _name = data;
            _group = data_bool;
        }
    }

    public class New_group_event : EventArgs
    {
        private string _name;
        public string Name
        {
            get { return _name; }
        }

        public New_group_event(string name) : base()
        {
            _name = name;
        }
    }

    public class Action_group_event : EventArgs
    {
        private string _data;
        public string Data
        {
            get { return _data; }
        }

        private bool _delete;   //si c'est pas un delete c'est un leave
        public bool Delete
        {
            get { return _delete; }
        }

        public Action_group_event(string data, bool delete) : base()
        {
            _data = data;
            _delete = delete;
        }
    }
}
