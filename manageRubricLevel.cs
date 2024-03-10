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
    public partial class manageRubricLevel : UserControl
    {
        public manageRubricLevel()
        {
            InitializeComponent();
            loadEveryThing();
             
        }
        public void loadEveryThing()
        {
            loadRubrics();
            loadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRubricsLevelDetails.Text != "" && numMeasurementLevel.Value != 0)
                {

                    string detail = txtRubricsLevelDetails.Text;
                    int measurementLevel = (int)numMeasurementLevel.Value;
                    string RubricId = cmbRubrics.Text.ToString();

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Insert into RubricLevel values (@RubricId, @Details,@MeasurementLevel)", con);
                    cmd.Parameters.AddWithValue("@RubricId",RubricId );
                    cmd.Parameters.AddWithValue("@Details", detail);
                    cmd.Parameters.AddWithValue("@MeasurementLevel", measurementLevel);
                    cmd.ExecuteNonQuery();
                    loadData();
                    
                    MessageBox.Show("Successfully saved");


                }
                else
                {
                    MessageBox.Show("Some Fields are Empty");
                }
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
            }
        }
        public void clearForm()
        {
            numMeasurementLevel.Value = 0;
            txtRubricsLevelDetails.Text = "";
        }
        public void loadRubrics()
        {
            try
            { 
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select Details,Id from Rubric", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "Id");
                cmbRubrics.DisplayMember = "Id";
                cmbRubrics.ValueMember = "Id";
                cmbRubrics.DataSource = ds.Tables["Id"];

                string currentRubricId = cmbRubrics.Text;
                con = Configuration.getInstance().getConnection();
                cmd = new SqlCommand("SELECT Details FROM Rubric WHERE Id = '" + currentRubricId + "'", con);
                string Detail = cmd.ExecuteScalar().ToString();
                txtRubricsDetails.Text = Detail;
            }
            catch(Exception z)
            {
                MessageBox.Show(z.Message);
            }
        }
        public void loadData()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT RL.Id,RL.Details,RL.MeasurementLevel,R.Id as [Rubric Id],R.Details  as [Rubric Details]\r\nFROM RubricLevel RL\r\nJoin Rubric R\r\nON   RL.RubricId = R.Id", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                RubricsLevelGV.DataSource = dt;

            }
            catch(Exception z) 
            {
                MessageBox.Show(z.Message);  
            }

        }

        private void cmbRubrics_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string currentRubricId = cmbRubrics.Text;
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Details FROM Rubric WHERE Id = '" + currentRubricId + "'", con);          
                string Detail = cmd.ExecuteScalar().ToString();
                txtRubricsDetails.Text = Detail;

            }
            catch(Exception z)
            {
                MessageBox.Show(z.Message);
            }
        }

        private void RubricsLevelGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (RubricsLevelGV.Columns["Delete"].Index == e.ColumnIndex)
                {
                    string Id = RubricsLevelGV.CurrentRow.Cells[2].Value.ToString();
                    string query = "DELETE FROM RubricLevel WHERE Id = '" + Id + "';";
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    loadData();
                    MessageBox.Show("Successfully Deleted");
                }
                else if (RubricsLevelGV.Columns["Edit"].Index == e.ColumnIndex)
                {
                    btnSave.Enabled = true;
                    txtRubricsLevelDetails.Text = RubricsLevelGV.CurrentRow.Cells[3].Value.ToString();
                    string mLevel = RubricsLevelGV.CurrentRow.Cells[4].Value.ToString();
                    numMeasurementLevel.Value = int.Parse(mLevel);
                    cmbRubrics.Text = RubricsLevelGV.CurrentRow.Cells[5].Value.ToString();
                    txtRubricsDetails.Text = RubricsLevelGV.CurrentRow.Cells[6].Value.ToString();

                }

            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
