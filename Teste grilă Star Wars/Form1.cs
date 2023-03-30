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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            intrebari f = new intrebari();
                f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            teste f1 = new teste();
            f1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rezultate_admin r = new rezultate_admin();
            r.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            manage_users m = new manage_users();
            m.ShowDialog();
        }
    }
}
