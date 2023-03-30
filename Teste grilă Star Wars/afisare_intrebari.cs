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
    public partial class afisare_intrebari : UserControl
    {
        public afisare_intrebari()
        {
            InitializeComponent();
        }
        OleDbConnection conn;
        private void afisare_intrebari_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=teste.accdb;Jet OLEDB:Database Password=D1234509876B;";
            conn = new OleDbConnection(cs);
            conn.Open();
            string q = "SELECT * FROM intrebari ORDER BY ID";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(c);
            DataTable t = new DataTable();
            da.Fill(t);
            dataGridView1.DataSource = t;
            string q1 = "SELECT * FROM capitole";
            OleDbCommand c1 = new OleDbCommand(q1, conn);
            OleDbDataReader dr = c1.ExecuteReader();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Toate întrebările");
            comboBox1.SelectedIndex = 0;
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString() + " - " + dr[1].ToString());
            }
            dr.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] text = comboBox1.Text.Split();
            if (text[0] == "Toate")
            {
                string q = "SELECT * FROM intrebari ORDER BY ID";
                OleDbCommand c = new OleDbCommand(q, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(c);
                DataTable t = new DataTable();
                da.Fill(t);
                dataGridView1.DataSource = t;
            }
            else 
            { 
                int id = int.Parse(text[0]);
                string q = "SELECT * FROM intrebari WHERE ID_CAPITOL="+id + " ORDER BY ID";
                OleDbCommand c = new OleDbCommand(q, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(c);
                DataTable t = new DataTable();
                da.Fill(t);
                dataGridView1.DataSource = t;
            }
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
