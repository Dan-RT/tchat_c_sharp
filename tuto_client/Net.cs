using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tuto_client
{
    class Net
    {
        public event EventHandler<Log_btn_event> Log_update;
        public delegate void DelegateRaisingEvent_Bool(bool log);

        public bool ClientSend(TcpClient client, string msg)
        {
            System.Console.WriteLine("clientSend called");
            try
            {
                NetworkStream stream = client.GetStream(); //Gets The Stream of The Connection
                byte[] data; // creates a new byte without mentioning the size of it cuz its a byte used for sending
                data = Encoding.Default.GetBytes(msg); // put the msg in the byte ( it automaticly uses the size of the msg )
                int length = data.Length; // Gets the length of the byte data
                byte[] datalength = new byte[4]; // Creates a new byte with length of 4
                datalength = BitConverter.GetBytes(length); //put the length in a byte to send it
                stream.Write(datalength, 0, 4); // sends the data's length
                stream.Write(data, 0, data.Length); //Sends the real data
                return true;
            }
            catch (System.ObjectDisposedException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        
        public void ClientReceive (Client_management client_side, TcpClient client_tcp)
        {
            int i = 0;
            byte[] datalength = new byte[4]; // creates a new byte with length 4 ( used for receivng data's lenght)
            System.Console.WriteLine("clientReceive called");
            NetworkStream stream = client_tcp.GetStream(); //Gets The Stream of The Connection

            if (!client_tcp.Connected)
            {
                MessageBox.Show("Cannot receive data, client not connected.");
                return;
            }

            new Thread(() =>
            {
                try
                {
                    while ((i = stream.Read(datalength, 0, 4)) != 0)
                    {
                        byte[] data = new byte[BitConverter.ToInt32(datalength, 0)];
                        stream.Read(data, 0, data.Length);
                        string data_string = Encoding.Default.GetString(data);

                        if (data_string == "server_close")
                        {
                            MessageBox.Show("La connection avec le serveur a été perdue.");
                            client_side.Close_connection();
                            break;
                        }
                        else
                        {
                            client_side.handling_message(data_string);
                            /*try
                            {
                                client_tchat.Invoke((MethodInvoker)delegate // To Write the Received data
                                //TODO: erreur ici : System.InvalidOperationException:
                                //Impossible d'appeler Invoke ou BeginInvoke sur un contrôle tant que le handle de fenêtre n'a pas été créé
                                {
                                    client_tchat.Update_message_feed(data);
                                });
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }*/
                        }
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Console.WriteLine(ex);
                }
            }).Start(); // Start the Thread

        }

        /*public void ClientReceive(Client_management source, TcpClient client, Tchat client_tchat)
        {
            int i = 0;
            byte[] datalength = new byte[4]; // creates a new byte with length 4 ( used for receivng data's lenght)
            System.Console.WriteLine("clientReceive called");
            NetworkStream stream = client.GetStream(); //Gets The Stream of The Connection

            if (!client.Connected)
            {
                MessageBox.Show("Cannot receive data, client not connected.");
                return;
            }

            new Thread(() => // Thread (like Timer)
            {
                try
                {
                    while ((i = stream.Read(datalength, 0, 4)) != 0)//Keeps Trying to Receive the Size of the Message or Data
                    {
                        // how to make a byte E.X byte[] examlpe = new byte[the size of the byte here] , i used BitConverter.ToInt32(datalength,0) cuz i received the length of the data in byte called datalength :D
                        byte[] data = new byte[BitConverter.ToInt32(datalength, 0)]; // Creates a Byte for the data to be Received On
                        stream.Read(data, 0, data.Length); //Receives The Real Data not the Size
                        
                        if (Encoding.Default.GetString(data) == "server_close")
                        {
                            MessageBox.Show("La connection avec le serveur a été perdue.");
                            source.Loss_connection();
                            break;
                        } else {
                            try
                            {
                                client_tchat.Invoke((MethodInvoker)delegate // To Write the Received data
                                //TODO: erreur ici : System.InvalidOperationException:
                                 //Impossible d'appeler Invoke ou BeginInvoke sur un contrôle tant que le handle de fenêtre n'a pas été créé
                                {
                                    client_tchat.Update_message_feed(data);
                                });
                            } catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                    }
                } catch (System.IO.IOException ex)
                {
                    Console.WriteLine(ex);
                }
            }).Start(); // Start the Thread
        }*/
    }
}
