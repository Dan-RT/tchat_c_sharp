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
    public partial class Form1 : Form
    {
        List<Label> list_label = new List<Label>(); // or stack
        const int labelWidth = 200;  // control variables for TextBox placement
        const int labelHeight = 25;
        const int labelMargin = 4;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Height += labelHeight + labelMargin;
            Label tb = new Label();

            if (list_label.Count == 0)
            {
                tb.Top = labelMargin;
            }
            else
            {
                tb.Top = ((labelHeight + labelMargin) * list_label.Count) + labelMargin;
            }

            tb.Left = labelMargin;
            tb.Height = labelHeight;
            tb.Width = labelWidth;
            //tb.Text = "Test" + list_label.Count;
            tb.Text = "Height : " + this.Size.Height + "    Width : " + this.Size.Width;
            //this.Height = this.Height - 29;
            //this.Size = new System.Drawing.Size(this.Size.Height-10, this.Size.Width);
            list_label.Add(tb);
            this.Controls.Add(tb);
        }
    }
}
