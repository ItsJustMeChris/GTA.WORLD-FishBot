using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auto_Fish_V2
{
    public partial class Form1 : Form
    {
        Form2 messageBox = new Form2();
        public Form1()
        {
            InitializeComponent();
            label1.Text = $"GTA Process: {Fisher.GTA().Id}";
            label2.Text = $"Status: {Fisher.status}";
            label3.Text = "Bottom Chat Location";
            label5.Text = $"Bottom Chat Color: {Fisher.bottomChatYellow.ToString()}";
            textBox1.Text = Fisher.bottomChatX.ToString();
            textBox2.Text = Fisher.bottomChatY.ToString();

            Timer tm = new Timer();
            tm.Interval = 100;
            tm.Tick += Fisher.fish;
            tm.Start();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            messageBox.BackColor = Fisher.bottomChatYellow;
            messageBox.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Fisher.bottomChatX = int.Parse(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Fisher.bottomChatY = int.Parse(textBox2.Text);
        }
    }
}
