using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teste_grilă_Star_Wars
{
    public partial class user : Form
    {
        public int idu;
        public user()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            teste_standard f = new teste_standard();
            f.idu = idu;
            f.ShowDialog();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            rezultate r = new rezultate();
            r.idu = idu;
            r.tip = 1;
            r.ShowDialog();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            rezultate r = new rezultate();
            r.idu = idu;
            r.tip = 2;
            r.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            teste_random t = new teste_random();
            t.idu = idu;
            t.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void user_Load(object sender, EventArgs e)
        {

        }
    }
}
