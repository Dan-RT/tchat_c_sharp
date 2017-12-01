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

namespace tuto_client
{
    public partial class Client_side : Form
    {
        /*

        delegate void SetTextCallback(string text);
        delegate void SetTextCallback_safe(string text);

        TcpClient client; // Creates a TCP Client
        NetworkStream stream; //Creats a NetworkStream (used for sending and receiving data)
        
        public Client_side()
        {
            InitializeComponent();
        }
        

        private void btnConnect_Click_1(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient("127.0.0.1", 1980); //Trys to Connect
                Net.ClientReceive(this.client, this.stream, this); //Starts Receiving When Connected
                Console.WriteLine("Connected");
                change_text("Status : Waiting for connection...");
                Net.ClientSend(this.client, this.stream, "connection");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connection");
                MessageBox.Show(ex.Message); // Error handler :D
            }
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {
            if (client.Connected && txtSend.Text != "") // if the client is connected
            {
                Net.ClientSend(this.client, this.stream, txtSend.Text); // uses the Function ClientSend and the msg as txtSend.Text
                txtSend.Clear();
            }
        }

        private void ThreadProcSafe(string text)
        {
            this.change_text(text);
        }
        
        public void change_text (byte[] data)
        {
            if (Encoding.Default.GetString(data) == "client_connected")
            {
                txtLog.Text += System.Environment.NewLine + "Status : Connected to server.";
            } else
            {
                txtLog.Text += System.Environment.NewLine + "Server : " + Encoding.Default.GetString(data); // Encoding.Default.GetString(data); Converts Bytes Received to String
            }
        }

        public void change_text(string data)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtLog.InvokeRequired)
            {
                SetTextCallback_safe d = new SetTextCallback_safe(change_text);
                this.Invoke(d, new object[] { data });
            }
            else
            {
                this.txtLog.Text += System.Environment.NewLine + data; 
            }
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {

        }

    */
    }
}
