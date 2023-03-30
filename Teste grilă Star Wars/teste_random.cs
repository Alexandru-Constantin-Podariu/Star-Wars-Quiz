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
    public partial class teste_random : Form
    {
        public teste_random()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        OleDbConnection conn;
        List<intrebare> LI;
        int poz;
        public int idu;
        int nrintrebari=0;

        private void incarca_teste()
        {
            string q = "SELECT * FROM teste";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();         
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

        private void button1_Click(object sender, EventArgs e)
        {
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
            string r = "";
            if (checkBox1.Checked) r = r + "A";
            if (checkBox2.Checked) r = r + "B";
            if (checkBox3.Checked) r = r + "C";
            if (checkBox4.Checked) r = r + "D";
            LI[poz].Raspuns = r;
            int c = 0, nr = 0;
            foreach (intrebare I in LI)
            {
                nr++;
                if (I.Corect == I.Raspuns)
                    c++;
            }
                    
            MessageBox.Show("Ați răspuns corect la " + c + " întrebări din "+nr+".");
            string q = "INSERT INTO rezultate_random (IDU, NUMAR_INTREBARI, REZULTAT, DATA) VALUES";
            q = q + "(" + idu + ", " + nr + "," + c+ ",'" + DateTime.Now.ToShortDateString() + "')";
            OleDbCommand cc = new OleDbCommand(q, conn);
            cc.ExecuteNonQuery();
            q = "SELECT * FROM utilizatori WHERE ID=" + idu;
            OleDbCommand cm = new OleDbCommand(q, conn);
            OleDbDataReader dr = cm.ExecuteReader();
            if (dr.Read())
            {
                string np = dr[2].ToString();
                string nivel = dr[5].ToString();
                user u = new user();
                u.idu = idu;
                u.Text = "Bine ai venit " + nivel + " " + np;
                u.ShowDialog();
            }
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            nrintrebari = (int)numericUpDown1.Value;
            LI = new List<intrebare>();
            string id_q = "";//id-urile întrebărilor din random
            ///selecteza aleatoriu (RND) intrebarile si se ia pe primele nrintrebari (TOP)
            string qr = "SELECT TOP " + nrintrebari + " ID FROM intrebari ORDER BY rnd(ID)";
            OleDbCommand cr = new OleDbCommand(qr, conn);
            OleDbDataReader dr = cr.ExecuteReader();
            while (dr.Read())
                id_q = id_q + dr[0].ToString() + " ";
            dr.Close();
                    string[] SI =id_q.Split();
                    foreach (string idi in SI)
                        if (idi != "")
                        {

                            string qi = "SELECT * FROM intrebari WHERE ID=" + idi;
                            OleDbCommand ci = new OleDbCommand(qi, conn);
                            OleDbDataReader dri = ci.ExecuteReader();
                            string enunt, a, b, C, d, corect, imagine;
                            while (dri.Read())
                            {
                                int idt = int.Parse(dri[0].ToString());
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
                poz = 0;
                afis_intrebare();
            }
    }
}
