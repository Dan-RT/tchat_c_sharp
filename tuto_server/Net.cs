using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tuto_server
{
    class Net
    {
        public static void ServerSend (TcpClient client, string msg)
        {
            System.Console.WriteLine("serverSend called");
            try
            {
                NetworkStream stream = client.GetStream(); //Gets The Stream of The Connection
                byte[] data; // creates a new byte without mentioning the size of it cuz its a byte used for sending
                data = Encoding.Default.GetBytes(msg); // put the msg in the byte ( it automaticly uses the size of the msg )
                int length = data.Length; // Gets the length of the byte data
                byte[] datalength = new byte[4]; // Creates a new byte with length of 4
                datalength = BitConverter.GetBytes(length); //put the length in a byte to send it
                stream.Write(datalength, offset: 0, size: 4); // sends the data's length
                stream.Write(data, 0, data.Length); //Sends the real data
            } catch (System.IO.IOException ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        public static void ServerReceive(TcpClient client, Server_side server_obj)
        { 
            int i;
            byte[] datalength = new byte[4]; // creates a new byte with length 4 ( used for receivng data's lenght)
            System.Console.WriteLine("serverReceive called");
            NetworkStream stream = client.GetStream(); //Gets The Stream of The Connection
            new Thread(() => // Thread (like Timer)
            {
                try
                {
                    while ((i = stream.Read(datalength, 0, 4)) != 0)//Keeps Trying to Receive the Size of the Message or Data
                    {
                        // how to make a byte E.X byte[] examlpe = new byte[the size of the byte here] , i used BitConverter.ToInt32(datalength,0) cuz i received the length of the data in byte called datalength :D
                        byte[] data = new byte[BitConverter.ToInt32(datalength, 0)]; // Creates a Byte for the data to be Received On
                        stream.Read(data, 0, data.Length); //Receives The Real Data not the Size
                        server_obj.Invoke((MethodInvoker)delegate // To Write the Received data
                        {
                            server_obj.Message_handling(data);
                        });
                    }
                } catch {
                    Console.WriteLine("Stop receiving data, host disconnected.");
                }
            }).Start(); // Start the Thread
        }
        
        public static void ServerBroadcast(Server_side server, Dictionary<String, TcpClient> listConnectedClients, string msg)
        {
            //Console.WriteLine("Net ServerBroadcast :");
            foreach (KeyValuePair<string, TcpClient> client_tmp in listConnectedClients)
            {
                //Console.WriteLine(client_tmp.Key);
                //Une connexion peut être interrompue entre le temps que le code capte la déco et qu'elle envoie la liste précédente
                if (client_tmp.Value.Connected == true)
                {
                    try
                    {
                        NetworkStream stream = client_tmp.Value.GetStream();
                        byte[] data;
                        data = Encoding.Default.GetBytes(msg);
                        int length = data.Length;
                        byte[] datalength = new byte[4];
                        datalength = BitConverter.GetBytes(length);
                        stream.Write(datalength, 0, 4);
                        stream.Write(data, 0, data.Length);
                    }
                    catch (System.IO.IOException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                } else
                {
                    server.remove_item_listConnectedClients(client_tmp.Key);
                }
            }
        }
    }
}
