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
    public partial class teste : Form
    {
        public teste()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            adauga_test c = new adauga_test();
            panel2.Controls.Add(c);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            afisare_teste c = new afisare_teste();
            panel2.Controls.Add(c);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            modificare_teste c = new modificare_teste();
            panel2.Controls.Add(c);
        }
    }
}
