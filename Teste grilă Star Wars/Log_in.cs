using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Teste_grilă_Star_Wars
{
    public partial class Log_in : Form
    {
        public Log_in()
        {
            InitializeComponent();
        }
        OleDbConnection conn;
        int idu;
        private void Log_in_Shown(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=teste.accdb;Jet OLEDB:Database Password=D1234509876B;";
            conn = new OleDbConnection(cs);
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string u = textBox1.Text;
            string p = textBox2.Text;
            string q = "SELECT * FROM utilizatori WHERE Utilizator='" + u + "' AND Parola='" + p + "'";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            if (dr.Read())
            {
                string np = dr[2].ToString();
                string tip = dr[4].ToString();
                string nivel = dr[5].ToString();
                if (tip == "admin")
                {
                    Form1 f = new Form1();
                    f.Text = "Bine ai venit Grand Master Administrator";
                    f.ShowDialog();
                }
                else
                {
                    user f = new user();
                    f.Text = "Bine ai venit " + nivel + " " + np;
                    idu = int.Parse(dr[0].ToString());
                    f.idu = idu;
                    f.ShowDialog();
                }
            }
            else
                MessageBox.Show("Date de logare gresite!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            creare_cont c = new creare_cont();
            c.ShowDialog();
        }
    }
}
