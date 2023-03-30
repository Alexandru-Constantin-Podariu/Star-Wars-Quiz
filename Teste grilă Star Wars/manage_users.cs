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
    public partial class manage_users : Form
    {
        public manage_users()
        {
            InitializeComponent();
        }
        OleDbConnection conn;
        private void manage_users_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=teste.accdb;Jet OLEDB:Database Password=D1234509876B;";
            conn = new OleDbConnection(cs);
            conn.Open();
            ///afișez utilizatorii În datagridview
            string q = "SELECT * FROM utilizatori ORDER BY ID";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(c);
            DataTable t = new DataTable();
            da.Fill(t);
            dataGridView1.DataSource = t;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int n = (int)numericUpDown1.Value;
            ///Ștergerea utilizatorului și a rezultatelor sale
            ///Căsuță de dialog pentru a verifica dacă se dorește ștergerea cu adevărat a utilizatorului
            DialogResult result = MessageBox.Show("Sunteți sigur că vreți să ștergeți acest utilizator?","Confirmare", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                string q = "DELETE * FROM utilizatori WHERE ID=" + n;
                OleDbCommand c = new OleDbCommand(q, conn);
                c.ExecuteNonQuery();

                q = "DELETE * FROM rezultate WHERE IDU=" + n;
                c = new OleDbCommand(q, conn);
                c.ExecuteNonQuery();

                q = "DELETE * FROM rezultate_random WHERE IDU=" + n;
                c = new OleDbCommand(q, conn);
                c.ExecuteNonQuery();

                //afișez utilizatorii În datagridview după ștergere
                q = "SELECT * FROM utilizatori ORDER BY ID";
                c = new OleDbCommand(q, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(c);
                DataTable t = new DataTable();
                da.Fill(t);
                dataGridView1.DataSource = t;
            }
            
        }
    }
}
