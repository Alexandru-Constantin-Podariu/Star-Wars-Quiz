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
using System.IO;
namespace Teste_grilă_Star_Wars
{
    public partial class modificare_intrebari : UserControl
    {
        public modificare_intrebari()
        {
            InitializeComponent();
        }
        OleDbConnection conn;
        string imagine = "";
        private void incarca_intrebari()
        {
            string q = "SELECT * FROM intrebari ORDER BY ID";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            listBox1.Items.Clear();
            while (dr.Read())
            {///precedate de ID
                listBox1.Items.Add(dr[0].ToString() + " - " + dr[2].ToString());
            }
            dr.Close();
        }

        private void modificare_intrebari_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=teste.accdb;Jet OLEDB:Database Password=D1234509876B;";
            conn = new OleDbConnection(cs);
            conn.Open();
            ///pun enunturile si ID in listbox1
            incarca_intrebari();
            ///pun toate capilolele in combobox1
            string q = "SELECT * FROM capitole";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            comboBox1.Items.Clear();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString() + " - " + dr[1].ToString());
            }
            dr.Close();
        }
        string get_capitol_by_id(int id)
        {///returneaza titlul capitolului in functie de ID
            string q = "SELECT * FROM capitole WHERE ID=" + id;
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                return dr[1].ToString();
            }
            return "";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {///daca s-a selectat  intrebare
             ///aduc datele despre ea in partea dreapta               
                string[] S = listBox1.SelectedItem.ToString().Split();
                int id = int.Parse(S[0]);
                string q = "SELECT * FROM intrebari WHERE ID=" + id;
                OleDbCommand c = new OleDbCommand(q, conn);
                OleDbDataReader dr = c.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Text = dr[1].ToString() + " - " + get_capitol_by_id(int.Parse(dr[1].ToString()));
                    textBox1.Text = dr[2].ToString();
                    textBox2.Text = dr[3].ToString();
                    textBox3.Text = dr[4].ToString();
                    textBox4.Text = dr[5].ToString();
                    textBox5.Text = dr[6].ToString();
                    string corect = dr[7].ToString();
                    if (corect.IndexOf('A') != -1) checkBox1.Checked = true;
                    else checkBox1.Checked = false;
                    if (corect.IndexOf('B') != -1) checkBox2.Checked = true;
                    else checkBox2.Checked = false;
                    if (corect.IndexOf('C') != -1) checkBox3.Checked = true;
                    else checkBox3.Checked = false;
                    if (corect.IndexOf('D') != -1) checkBox4.Checked = true;
                    else checkBox4.Checked = false;
                    string imagine = dr[8].ToString();
                    if (imagine != "")
                        pictureBox1.Load(Application.StartupPath + "/imagini/" + imagine);
                    else
                        pictureBox1.ImageLocation = null;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ///salvare modificari intrebare
            if (listBox1.SelectedIndex != -1)
            {
                ///preiau datele din formular
                string[] S = listBox1.SelectedItem.ToString().Split();
                int id = int.Parse(S[0]);
                S = comboBox1.Text.Split();
                int id_cap = int.Parse(S[0]);
                string intrebare = textBox1.Text;
                string A = textBox2.Text;
                string B = textBox3.Text;
                string C = textBox4.Text;
                string D = textBox5.Text;
                string corect = "";
                if (checkBox1.Checked) corect = corect + "A";
                if (checkBox2.Checked) corect = corect + "B";
                if (checkBox3.Checked) corect = corect + "C";
                if (checkBox4.Checked) corect = corect + "D";
                ///daca sunt completate
                if (intrebare != "" && A != "" && B != "" && C != "" && D != "" && corect != "")
                {//creez comanda update
                    string q = "UPDATE intrebari SET ";
                    q = q + "ID_CAPITOL=" + id_cap + ", ";
                    q = q + "INTREBAREA='" + intrebare + "', ";
                    q = q + "A='" + A + "', ";
                    q = q + "B='" + B + "', ";
                    q = q + "C='" + C + "', ";
                    q = q + "D='" + D + "', ";
                    q = q + "CORECT='" + corect + "', ";
                    q = q + "IMAGINE='" + imagine + "' ";
                    q = q + " WHERE ID=" + id;
                    OleDbCommand c = new OleDbCommand(q, conn);
                    c.ExecuteNonQuery();
                    MessageBox.Show("Modificarile au fost salvate!");
                    incarca_intrebari();
                }
            }
            else MessageBox.Show("Alegeti o intrebare");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {///daca s-a selectat o intrebare
             ///o sterg
             ///Căsuță de dialog pentru a verifica dacă se dorește ștergerea cu adevărat a întrebării
                DialogResult result = MessageBox.Show("Sunteți sigur că vreți să ștergeți această întrebare?", "Confirmare", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string[] S = listBox1.SelectedItem.ToString().Split();
                    int id = int.Parse(S[0]);
                    string q = "DELETE FROM intrebari WHERE ID=" + id;
                    OleDbCommand c = new OleDbCommand(q, conn);
                    c.ExecuteNonQuery();
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    if (listBox1.SelectedIndex == -1)
                        listBox1.SelectedIndex = 0;
                }
            }
            else MessageBox.Show("Nu s-a selectat întrebarea");
        }
        string nume_fisier(string s)
        {///numele fiesierului (fara cale)
            int i = s.Length - 1;
            while (s[i] != '\\') i--;
            return s.Substring(i + 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ///alegerea imaginii
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fisier = openFileDialog1.FileName;
                imagine = nume_fisier(openFileDialog1.FileName);
                string fisier2 = Application.StartupPath + "/imagini/" + nume_fisier(openFileDialog1.FileName);
                File.Copy(fisier, fisier2, true);///copiaza fisierul
                pictureBox1.Load(fisier);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
