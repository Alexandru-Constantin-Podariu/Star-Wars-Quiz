using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Teste_grilă_Star_Wars
{
    public partial class adauga_test : UserControl
    {
        public adauga_test()
        {
            InitializeComponent();
        }

        OleDbConnection conn;

        private void  incarca_intrebari()
        {//încarcă întrebările în checkedlistbox
            string q = "SELECT * FROM intrebari";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            checkedListBox1.Items.Clear();
            while (dr.Read())
                checkedListBox1.Items.Add(dr[0].ToString() + " - " + dr[2].ToString());
            dr.Close();
            string q1 = "SELECT * FROM capitole";
            OleDbCommand c1 = new OleDbCommand(q1, conn);
            OleDbDataReader dr1 = c1.ExecuteReader();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Toate întrebările");
            comboBox1.SelectedIndex = 0;
            while (dr1.Read())
            {
                comboBox1.Items.Add(dr1[0].ToString() + " - " + dr1[1].ToString());
            }
            dr1.Close();
        }

        private void add_test_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=teste.accdb;Jet OLEDB:Database Password=D1234509876B;";
            conn = new OleDbConnection(cs);
            conn.Open();
            incarca_intrebari();
        }

        private void button1_Click(object sender, EventArgs e)
        {///salvarea testului
            string titlu = textBox1.Text;//titlu
            string data = DateTime.Now.ToShortDateString();//data
            //număr de întrebări
            int nrintrebari = checkedListBox1.CheckedItems.Count;
            if (nrintrebari != 0 && titlu != "")//date corecte
            {
                string id_q = "";//id-urile întrebărilor din checkedlistbox
                foreach(string s in checkedListBox1.CheckedItems)
                {
                    string[] S = s.Split();
                    id_q = id_q + S[0] + " ";
                }
                string q = "INSERT INTO teste (TITLU, DATA, INTREBARI) VALUES ";
                q = q + "('" + titlu + "', '" + data + "', '" + id_q + "')";
                OleDbCommand c = new OleDbCommand(q, conn);
                c.ExecuteNonQuery();
                MessageBox.Show("Testul a fost salvat");
            }
            else MessageBox.Show("Nu ați completat toate datele!");
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {///afișează enunțul complet în label3
            label3.Text = checkedListBox1.SelectedItem.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //salvarea testului random
            string titlu = textBox2.Text;//titlu
            string data = DateTime.Now.ToShortDateString();//data
            //număr de întrebări
            int nrintrebari = 10;
            if (titlu != "")//date corecte
            {
                string id_q = "";//id-urile întrebărilor din random
                ///selecteza aleatoriu (RND) întrebările și se ia pe primele nrîntrebări (TOP)
                string qr = "SELECT TOP " + nrintrebari + " ID FROM intrebari ORDER BY rnd(ID)";
                OleDbCommand cr = new OleDbCommand(qr, conn);
                OleDbDataReader dr = cr.ExecuteReader();
                while (dr.Read())
                    id_q = id_q + dr[0].ToString() + " ";
                dr.Close();
                string q = "INSERT INTO teste (Titlu, Data, Intrebari) VALUES ";
                q = q + "('" + titlu + "', '" + data + "', '" + id_q + "')";
                OleDbCommand c = new OleDbCommand(q, conn);
                c.ExecuteNonQuery();
                MessageBox.Show("Testul a fost salvat");
            }
            else MessageBox.Show("Completati toate datele!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] text = comboBox1.Text.Split();
            if (text[0] == "Toate")
            {
                string q = "SELECT * FROM intrebari ORDER BY ID";
                OleDbCommand c = new OleDbCommand(q, conn);
                OleDbDataReader dr = c.ExecuteReader();
                checkedListBox1.Items.Clear();
                while (dr.Read())
                    checkedListBox1.Items.Add(dr[0].ToString() + " - " + dr[2].ToString());
                dr.Close();
            }
            else 
            { 
                int id = int.Parse(text[0]);
                string q1 = "SELECT * FROM intrebari WHERE ID_CAPITOL=" + id + " ORDER BY ID";
                OleDbCommand c1 = new OleDbCommand(q1, conn);
                OleDbDataReader dr1 = c1.ExecuteReader();
                checkedListBox1.Items.Clear();
                while (dr1.Read())
                    checkedListBox1.Items.Add(dr1[0].ToString() + " - " + dr1[2].ToString());
                dr1.Close();
            }
            
        }
    }
}
