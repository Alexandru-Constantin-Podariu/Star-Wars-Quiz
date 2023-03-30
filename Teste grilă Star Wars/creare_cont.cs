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
    public partial class creare_cont : Form
    {
        public creare_cont()
        {
            InitializeComponent();
        }
        OleDbConnection conn;
        int idu;
        private void creare_cont_Shown(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=teste.accdb;Jet OLEDB:Database Password=D1234509876B;";
            conn = new OleDbConnection(cs);
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string numeprenume = textBox1.Text;
            string utilizator = textBox2.Text;
            string parola = textBox3.Text;
            string verifparola = textBox4.Text;
            string tip = "user";
            string nivel = "Youngling";
            if (parola == verifparola)
            {
                string q = "INSERT INTO utilizatori (UTILIZATOR,NUME_PRENUME,PAROLA,TIP,NIVEL) VALUES";
                q += "('" + utilizator + "', '" + numeprenume + "', '" + parola + "','" + tip + "','" + nivel + "')";
                OleDbCommand c = new OleDbCommand(q, conn);
                c.ExecuteNonQuery();
                MessageBox.Show("Contul a fost creat cu succes!");
                q = "SELECT * FROM utilizatori WHERE Utilizator='" + utilizator + "' AND Parola='" + parola + "'";
                c = new OleDbCommand(q, conn);
                OleDbDataReader dr = c.ExecuteReader();
                if (dr.Read())
                {
                        user f = new user();
                        f.Text = "Bine ai venit " + nivel + " " + numeprenume;
                        idu = int.Parse(dr[0].ToString());
                        f.idu = idu;
                        f.ShowDialog();
                }
            }
            
        }
    }
}
