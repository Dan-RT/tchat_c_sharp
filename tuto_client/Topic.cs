using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tuto_client
{
    public partial class Topic : Form
    {
        public event EventHandler<New_group_chat_event> Group_chat_event;
        public delegate void DelegateRaisingEvent_String(string data);

        public Topic()
        {
            InitializeComponent();
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void create_chat_Click(object sender, EventArgs e)
        {
            if (topic_box.Text != "")
            {
                Group_chat_event(this, new New_group_chat_event(topic_box.Text));
                this.Close();
            } else
            {
                MessageBox.Show("Please choose a topic.");
            }
        }

        private void topic_box_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                create_chat.PerformClick();
            }
        }
    }

    public class New_group_chat_event : EventArgs
    {
        private string _data;
        public string Data
        {
            get { return _data; }
        }

        public New_group_chat_event(string data) : base()
        {
            _data = data;
        }
    }
}
