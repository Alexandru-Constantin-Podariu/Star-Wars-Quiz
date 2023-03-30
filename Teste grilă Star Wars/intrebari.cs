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
    public partial class intrebari : Form
    {
        public intrebari()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            adauga_intrebare i = new adauga_intrebare();
            panel2.Controls.Add(i);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            afisare_intrebari c1 = new afisare_intrebari();
            panel2.Controls.Add(c1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            modificare_intrebari c1 = new modificare_intrebari();
            panel2.Controls.Add(c1);
        }
    }
}
