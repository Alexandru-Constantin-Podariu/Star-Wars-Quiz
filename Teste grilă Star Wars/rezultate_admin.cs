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
    public partial class rezultate_admin : Form
    {
        public rezultate_admin()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rezultate r = new rezultate();
            r.idu = 0;
            r.tip = 1;
            r.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rezultate r = new rezultate();
            r.idu = 0;
            r.tip = 2;
            r.ShowDialog();
        }
    }
}
