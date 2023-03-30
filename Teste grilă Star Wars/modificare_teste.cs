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
    public partial class modificare_teste : UserControl
    {
        public modificare_teste()
        {
            InitializeComponent();
        }

        OleDbConnection conn;

        private void incarca_teste()
        {
            string q = "SELECT * FROM teste ORDER BY ID";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            listBox1.Items.Clear();
            while(dr.Read())
            {
                listBox1.Items.Add(dr[0].ToString() + " - " + dr[1].ToString());
            }
            dr.Close();
        }

        private void modif_test_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=teste.accdb;Jet OLEDB:Database Password=D1234509876B;";
            conn = new OleDbConnection(cs);
            conn.Open();
            incarca_teste();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex!=-1)
            {///daca s-a selectat un test
                string[] S = listBox1.SelectedItem.ToString().Split();
                int id = int.Parse(S[0]);
                string q = "SELECT * FROM teste WHERE ID=" + id+ " ORDER BY ID";
                OleDbCommand c = new OleDbCommand(q, conn);
                OleDbDataReader dr = c.ExecuteReader();
                while(dr.Read())
                {
                    string[] SI = dr[3].ToString().Split();
                    checkedListBox1.Items.Clear();
                    int index = 0;
                    string qi = "SELECT * FROM intrebari ORDER BY ID";
                            OleDbCommand ci = new OleDbCommand(qi, conn);
                            OleDbDataReader dri = ci.ExecuteReader();
                            while(dri.Read())
                            {
                                checkedListBox1.Items.Add(dri[0].ToString() + " - " + dri[2].ToString());                       
                                for (int i = 0; SI[i] != ""; i++)
                                if(dri[0].ToString()==SI[i])
                                {
                                    checkedListBox1.SetItemChecked(index, true);
                                }
                                index++;
                            }                          
                    textBox1.Text = dr[1].ToString();
                }
                dr.Close();
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Text = checkedListBox1.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {///salvarea testului modificat
            string[] S_ind = listBox1.SelectedItem.ToString().Split();
            int id = int.Parse(S_ind[0]);
            string titlu = textBox1.Text;//titlu
            string data = DateTime.Now.ToShortDateString();//data
            //numar de teste
            int nrintrebari = checkedListBox1.CheckedItems.Count;
            if (nrintrebari != 0 && titlu != "")//date corecte
            {
                string id_q = "";//id-urile testelor din checkedlistbox
                foreach (string s in checkedListBox1.CheckedItems)
                {
                    string[] S = s.Split();
                    id_q = id_q + S[0] + " ";
                }
                string q = "UPDATE teste SET ";
                q += "TITLU='"+titlu+"', ";
                q += "DATA='"+data+"', ";
                q += "INTREBARI='"+id_q+"' ";
                q += "WHERE ID=" + id;
                OleDbCommand c = new OleDbCommand(q, conn);
                c.ExecuteNonQuery();
                MessageBox.Show("Testul a fost salvat");
            }
            else MessageBox.Show("Nu ați completat toate datele!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                ///daca s-a selectat un test
                ///il sterg
                //////Căsuță de dialog pentru a verifica dacă se dorește ștergerea cu adevărat a testului
                DialogResult result = MessageBox.Show("Sunteți sigur că vreți să ștergeți această test?", "Confirmare", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string[] S = listBox1.SelectedItem.ToString().Split();
                    int id = int.Parse(S[0]);
                    string q = "DELETE FROM teste WHERE ID=" + id;
                    OleDbCommand c = new OleDbCommand(q, conn);
                    c.ExecuteNonQuery();
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    if (listBox1.SelectedIndex == -1)
                        listBox1.SelectedIndex = 0;
                }
            }
            else MessageBox.Show("Nu s-a selectat testul");
        }
    }
}
