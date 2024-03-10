using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mid_Term_Project
{
    public partial class manageCLOs : UserControl
    {
        public manageCLOs()
        {
            InitializeComponent();
            loadData();
        }
        public void loadData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Clo", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CLOsGV.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtboxCLOName.Text != "" )
                {
                    DateTime dateCreated = DateTime.Now;

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Insert into Clo values (@Name, @DateCreated,@DateUpdated)", con);
                    cmd.Parameters.AddWithValue("@Name", txtboxCLOName.Text);
                    cmd.Parameters.AddWithValue("@DateCreated", dateCreated.ToString());
                    cmd.Parameters.AddWithValue("@DateUpdated", dateCreated.ToString());
                    cmd.ExecuteNonQuery();
                    loadData();
                    txtboxCLOName.Text = "";
                    MessageBox.Show("CLO Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Some Fields are Empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
            }
        }

        private void CLOsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CLOsGV.Columns["Delete"].Index == e.ColumnIndex)
            {
                string Id = CLOsGV.CurrentRow.Cells[2].Value.ToString();
                string query = "DELETE FROM Clo WHERE Id = '" + Id + "';";
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                loadData();
                MessageBox.Show("CLO Deleted Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (CLOsGV.Columns["Edit"].Index == e.ColumnIndex)
            {
                btnSave.Enabled = true;
                txtboxCLOName.Text = CLOsGV.CurrentRow.Cells[3].Value.ToString();
                
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dateUpdated = DateTime.Now;
                string Id = CLOsGV.CurrentRow.Cells[2].Value.ToString();
                string Name = txtboxCLOName.Text;
                if (txtboxCLOName.Text != "" )
                {
                    string query = "SET Name = '" + Name + "'" + ", DateUpdated = '" + dateUpdated + "'" ;
                    var con = Configuration.getInstance().getConnection();
                    query = "UPDATE Clo " + query + " WHERE Id = '" + Id + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    loadData();
                    txtboxCLOName.Text = "";
                    btnSave.Enabled = false;
                    MessageBox.Show("CLO Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
            }
        }
    }
    
}
