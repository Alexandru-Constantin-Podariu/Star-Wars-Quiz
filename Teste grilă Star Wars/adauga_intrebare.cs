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
    public partial class adauga_intrebare : UserControl
    {
        public adauga_intrebare()
        {
            InitializeComponent();
        }
        OleDbConnection conn;
        List<int> LID;//lista cu ID-urile capitolelor
        string imagine = "";//imaginea
        private void adauga_intrebare_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=teste.accdb;Jet OLEDB:Database Password=D1234509876B;";
            conn = new OleDbConnection(cs);
            conn.Open();
            //adauga capitolele in combobox
            LID = new List<int>();
            string q = "SELECT * FROM capitole";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[1].ToString());
                LID.Add(int.Parse(dr[0].ToString()));
            }
            dr.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //salvarea intrebarii
            if(comboBox1.SelectedIndex!=-1)
            {
                //se iau datele din formular
                int cap = LID[comboBox1.SelectedIndex];
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
                //daca s-a completat tot
                if(intrebare != "" && A != "" && B != "" && C != "" && D != "" && corect != "")
                {
                    string q = "INSERT INTO intrebari (ID_CAPITOL, INTREBAREA, A, B, C, D, CORECT, IMAGINE) VALUES ";
                    q = q + "(" + cap + ", '" + intrebare + "', '" + A + "', '" + B + "', '" + C + "', '" + D + "', '" + corect + "', '" + imagine + "')";
                    OleDbCommand c = new OleDbCommand(q, conn);
                    c.ExecuteNonQuery();
                    MessageBox.Show("Întrebarea a fost salvată!");
                }
                else 
                    MessageBox.Show("Nu ați completat toate datele!");
            }
            else
                MessageBox.Show("Nu ați selectat capitolul!");
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
    }
}
