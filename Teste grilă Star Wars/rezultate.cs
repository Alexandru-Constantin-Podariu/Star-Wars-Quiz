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
    public partial class rezultate : Form
    {

        public int idu;
        OleDbConnection conn;
        public int tip;

        public rezultate()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        string nume_by_id(int id)
        {
            string q = "SELECT Nume_prenume FROM utilizatori WHERE ID=" + id;
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            while (dr.Read())
                return dr[0].ToString();
            return "";
        }

        private void rezultate_Shown(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=teste.accdb;Jet OLEDB:Database Password=D1234509876B;";
            conn = new OleDbConnection(cs);
            conn.Open();
            if(tip==1)
            {
                string q;
                if (idu != 0)
                    q = "SELECT REZULTAT, ID_TEST, DATA, NIVEL FROM rezultate WHERE Idu=" + idu;
                else
                    q = "SELECT * FROM rezultate";
                OleDbCommand c = new OleDbCommand(q, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(c);
                DataTable t = new DataTable();
                da.Fill(t);
                dataGridView1.DataSource = t;
            }
            else 
            {
                string q;
                if (idu != 0)
                    q = "SELECT NUMAR_INTREBARI, REZULTAT, DATA FROM rezultate_random WHERE IDU=" + idu;
                else
                    q = "SELECT * FROM rezultate_random";
                OleDbCommand c = new OleDbCommand(q, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(c);
                DataTable t = new DataTable();
                da.Fill(t);
                dataGridView1.DataSource = t;
            }
        }
    }
}
