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
    public partial class afisare_teste : UserControl
    {
        public afisare_teste()
        {
            InitializeComponent();
        }

        OleDbConnection conn;

        private void afisare_teste_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=teste.accdb;Jet OLEDB:Database Password=D1234509876B;";
            conn = new OleDbConnection(cs);
            conn.Open();
            ///afisez testele in datagridview
            string q = "SELECT * FROM teste ORDER BY ID";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(c);
            DataTable t = new DataTable();
            da.Fill(t);
            dataGridView1.DataSource = t;
        }
    }
}
