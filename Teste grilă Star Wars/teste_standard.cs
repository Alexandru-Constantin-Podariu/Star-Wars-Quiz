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
    public partial class teste_standard : Form
    {
        public teste_standard()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;         
        }

        OleDbConnection conn;
        List<intrebare> LI;
        int poz;
        public int idu;
        int idt;

        private void incarca_teste()
        {
            string q = "SELECT * FROM teste";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            //încărcarea testelor în combobox
            comboBox1.Items.Clear();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString()+" - "+dr[1].ToString());
            }
            dr.Close();
        }

        private void testeaza_Shown(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=teste.accdb;Jet OLEDB:Database Password=D1234509876B;";
            conn = new OleDbConnection(cs);
            conn.Open();
            incarca_teste();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
        }

        private void afis_intrebare()
        {
            //afișarea întrebării curente
            intrebare I = LI[poz];
            textBox1.Text = I.Enunt;
            textBox2.Text = I.A;
            textBox3.Text = I.B;
            textBox4.Text = I.C;
            textBox5.Text = I.D;
            string r = I.Raspuns;
            if (r.IndexOf('A') != -1) checkBox1.Checked = true;
            else checkBox1.Checked = false;
            if (r.IndexOf('B') != -1) checkBox2.Checked = true;
            else checkBox2.Checked = false;
            if (r.IndexOf('C') != -1) checkBox3.Checked = true;
            else checkBox3.Checked = false;
            if (r.IndexOf('D') != -1) checkBox4.Checked = true;
            else checkBox4.Checked = false;
            string imagine = I.Imagine;
            if (imagine != "")
                pictureBox1.Load(Application.StartupPath + "/imagini/" + imagine);
            else
                pictureBox1.ImageLocation = null;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dacă s-a selectat un test se încarcă întrebările aferente
            if (comboBox1.SelectedIndex != -1)
            {
                string[] S = comboBox1.SelectedItem.ToString().Split();
                idt = int.Parse(S[0]);
                string q = "SELECT * FROM teste WHERE ID=" + idt;
                OleDbCommand c = new OleDbCommand(q, conn);
                OleDbDataReader dr = c.ExecuteReader();
                LI = new List<intrebare>();
                while (dr.Read())
                {
                    string[] SI = dr[3].ToString().Split();
                    foreach (string idi in SI)
                        if (idi != "")
                        {
                            //selecția întrebărilor din baza de date
                            string qi = "SELECT * FROM intrebari WHERE ID=" + idi;
                            OleDbCommand ci = new OleDbCommand(qi, conn);
                            OleDbDataReader dri = ci.ExecuteReader();
                            string enunt, a, b, C, d, corect, imagine;
                            while (dri.Read())
                            {
                                enunt = dri[2].ToString();
                                a = dri[3].ToString();
                                b = dri[4].ToString();
                                C = dri[5].ToString();
                                d = dri[6].ToString();
                                corect = dri[7].ToString();
                                imagine = dri[8].ToString();
                                intrebare I = new intrebare(idt, enunt, a, b, C, d, corect, imagine);
                                I.Raspuns = "";
                                LI.Add(I);
                            }
                            
                            dri.Close();
                        }
                }
                dr.Close();
                poz = 0;
                afis_intrebare();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //întrebarea anterioară
            string r = "";
            if (checkBox1.Checked) r = r + "A";
            if (checkBox2.Checked) r = r + "B";
            if (checkBox3.Checked) r = r + "C";
            if (checkBox4.Checked) r = r + "D";
            LI[poz].Raspuns = r;
            poz--;
            if (poz < 0) poz = 0;
            afis_intrebare();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //întrebarea următoare
            string r = "";
            if (checkBox1.Checked) r = r + "A";
            if (checkBox2.Checked) r = r + "B";
            if (checkBox3.Checked) r = r + "C";
            if (checkBox4.Checked) r = r + "D";
            LI[poz].Raspuns = r;
            poz++;
            if (poz >= LI.Count) poz--;
            afis_intrebare();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //finalizarea testului
            string r = "";
            if (checkBox1.Checked) r = r + "A";
            if (checkBox2.Checked) r = r + "B";
            if (checkBox3.Checked) r = r + "C";
            if (checkBox4.Checked) r = r + "D";
            LI[poz].Raspuns = r;
            int c = 0;
            foreach (intrebare I in LI)
                if (I.Corect == I.Raspuns)
                    c++;
            string nivel="";
            if (c <= 2) nivel = "Youngling";
            else if (c <= 4) nivel = "Padawan";
            else if (c <= 6) nivel = "Knight";
            else if (c <= 8) nivel = "Master";
            else if (c <= 10) nivel = "Great Master";
            //salevarea testului în baza de date
            MessageBox.Show("Ați răspuns corect la " + c + " întrebări. Rangul curent este de "+nivel);
            string q = "INSERT INTO rezultate (IDU, REZULTAT, DATA, ID_TEST, NIVEL) VALUES";
            q = q + "(" + idu + ", " + c+ ",'" + DateTime.Now.ToShortDateString() + "'," + idt + ",'" + nivel + "')";
            OleDbCommand cc = new OleDbCommand(q, conn);
            cc.ExecuteNonQuery();
            //actualizarea nivelului utilizatorului
            string q1 = "UPDATE utilizatori SET NIVEL='"+nivel+"' WHERE ID="+idu;
            OleDbCommand cc1 = new OleDbCommand(q1, conn);
            cc1.ExecuteNonQuery();
            q = "SELECT * FROM utilizatori WHERE ID=" +idu;
            OleDbCommand cm = new OleDbCommand(q, conn);
            OleDbDataReader dr = cm.ExecuteReader();
            if (dr.Read())
            {
                string np = dr[2].ToString();
                user u = new user();
                u.idu = idu;
                u.Text = "Bine ai venit " + nivel + " " + np;
                u.ShowDialog();
            }
            this.Close();
        }
    }
}
