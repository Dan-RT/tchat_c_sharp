﻿using System;
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
            Net.log_update += connection_management;
            
            login = new Thread(new ThreadStart(login_start));
            login.Start();
        }
        
        private void login_start()
        {
            client_login = new Login();
            client_login.log_update += connection_management;
            Application.Run(client_login);
        }

        private void home_start()
        {
            client_home = new Home();
            client_home.log_update += connection_management;
            client_home.Tchat_update += open_new_tchat;
            //Net.Home_listening(this, client);
            Application.Run(client_home);
        }
        
        public void loss_connection()
        {
            client.GetStream().Close();
            client.Close();
            login = new Thread(new ThreadStart(login_start));
            login.Start();
        }

        private void connection_management (object sender, log_btn_event pe)
        {
            if (pe.Log)
            {
                //login
                connect(pe.Username);
            } else
            {
                //logout
                loss_connection();
            }
        }
        
        private void connect (string username)
        {
            this._username = username;
            try
            {
                client = new TcpClient(this._ip, 1980); //Tries to Connect

                if (client.Connected/* && Net.Client_Connection(this.client)*/) { //Starts Receiving if Connected

                    Net.ClientSend(this.client, "@"+username+"#"+ "011011100110010101110111");
                    client_login.RequestStop();

                    /*Console.WriteLine("home boot");
                    home = new Thread(new ThreadStart(home_start));
                    home.Start();*/
                    
                    if (home == null)
                    {
                        Console.WriteLine("home boot");
                        //creating home page if connected
                        home = new Thread(new ThreadStart(home_start));
                        home.Start();
                    } else if (!home.IsAlive)
                    {
                        Console.WriteLine("home reboot");
                        home = new Thread(new ThreadStart(home_start));
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

        private void send_Message (object sender, send_btn_event pe)
        {
            Tchat tchat = sender as Tchat;
            string receiver = "Michel";
            string message = "@" + this._username + "#01101101011001010111001101110011" + "@" + receiver + "#" + pe.Message;
            tchat.message_sent(Net.ClientSend(client, message));
        }

        private void open_new_tchat(object sender, new_tchat_event p)
        {
            if (!Search_Name_Tchat(p.Data))
                //si le Tchat n'existe pas déjà, on évite de l'ouvrir deux fois
            {
                Tchat new_tchat = new Tchat(p.Data);
                new_tchat.Send_update += send_Message;
                tchat_Liste.Add(new_tchat);
                new Thread(() =>
                {
                    Net.ClientReceive(this, this.client, new_tchat);
                    Application.Run(new_tchat);
                    Search_Delete_Tchat(new_tchat.ID);
                    //on enlève le tchat de la liste pour qu'on puisse le rouvrir plus tard
                    Console.WriteLine("Tchat fermé");
                }).Start();
            }
        }

        private bool Search_Name_Tchat(string name)
        {
            foreach (Tchat item in tchat_Liste)
            {
                if (item.ID == name)
                {
                    return true;
                }
            }
            return false;
        }

        private bool Search_Delete_Tchat(string name)
        {
            foreach (Tchat item in tchat_Liste)
            {
                if (item.ID == name)
                {
                    tchat_Liste.Remove(item);
                    return true;
                }
            }
            MessageBox.Show("Error : Tchat not found to be deleted.");
            return false;
        }

        public void message_handling (byte[] data, Tchat client_tchat)
        {
            string data_string = Encoding.Default.GetString(data);
            Console.WriteLine("Message debug : " + data_string);
            Console.WriteLine(data_string);
            char[] delimiterChars = { '@', '#' };
            string[] words = data_string.Split(delimiterChars);
            int cpt = 1;

            try
            {
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

                if (data_string == "0110000101100010011011110111001001110100")  //à changer pour le bon format
                {
                    MessageBox.Show("La connection avec le server a été perdu.");
                    loss_connection();
                }
                else if (type_message == "01100011011000010111001101110100")
                {
                    Console.WriteLine("Liste de clients reçue");
                    people_connected.Clear();
                    Console.WriteLine("People connected :");
                    foreach (string person in words)
                    {
                        if(cpt > 2)
                        {
                            Console.WriteLine(person);
                            people_connected.Add(person);
                        }
                        cpt++;
                    }
                    Console.WriteLine("Fin people connected;");
                    client_home.generate_friend_list(people_connected);
                    Console.WriteLine("data envoyé au Home");

                }
                else if (type_message == "01101101011001010111001101110011")
                {
                    try
                    {
                        client_tchat.Invoke((MethodInvoker)delegate // To Write the Received data
                        {
                            client_tchat.update_message_feed(data);
                        });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }

    }

    public class log_btn_event : System.EventArgs
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

        public log_btn_event(bool log, string name) : base()
        {
            _log = log;
            _username = name;
        }
    }

    public class send_btn_event : System.EventArgs
    {
        private string _message;
        public string Message
        {
            get { return _message; }
        }

        public send_btn_event(string message) : base()
        {
            _message = message;
        }
    }

    public class new_tchat_event : System.EventArgs
    {
        private string _data;
        public string Data
        {
            get { return _data; }
        }

        public new_tchat_event(string data) : base()
        {
            _data = data;
        }
    }
    
}
