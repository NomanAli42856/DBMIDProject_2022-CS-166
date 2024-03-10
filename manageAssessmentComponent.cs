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
    public partial class manageAssessmentComponent : UserControl
    {
        public manageAssessmentComponent()
        {
            InitializeComponent();
            loadData();
            loadRubrics();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "" && cmbRubrics.Text != "" && cmbAssessment.Text!= "")
                {

                    string Name = txtName.Text;
                    DateTime dt = DateTime.Now;
                    int TotalMarks = int.Parse(numTotalMarks.Value.ToString());
                    string rubricId = cmbRubrics.SelectedValue.ToString();
                    string AssessmentId = cmbAssessment.SelectedValue.ToString();
                    

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Insert into AssessmentComponent values (@Name, @RubricId,@TotalMarks,@DateCreated,@DateUpdated,@AssessmentId)", con);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now.ToString());
                    cmd.Parameters.AddWithValue("@TotalMarks", TotalMarks);
                    cmd.Parameters.AddWithValue("@RubricId", rubricId);
                    cmd.Parameters.AddWithValue("@DateUpdated", dt);
                    cmd.Parameters.AddWithValue("@AssessmentId", AssessmentId);
                    cmd.ExecuteNonQuery();
                    loadData();
                    MessageBox.Show("Record Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        public void loadData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select AC.Id,AC.Name,R.Details as Rubric,AC.TotalMarks,A.Title,AC.DateCreated,AC.DateUpdated  from AssessmentComponent AC Join Rubric R on AC.RubricId = R.Id Join Assessment A on AC.AssessmentId = A.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            AssessmentComponentGV.DataSource = dt;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string Id = AssessmentComponentGV.CurrentRow.Cells[2].Value.ToString();
                string Name = txtName.Text;
                string RubricId = cmbRubrics.SelectedValue.ToString();
                string TotalMarks = numTotalMarks.Value.ToString();
                DateTime dt = DateTime.Now;
                string AssessmentId = cmbAssessment.SelectedValue.ToString();



                if (txtName.Text != "" && cmbRubrics.Text != "" && cmbAssessment.Text != "")
                {
                    string query = "SET Name = '" + Name+ "'" + ", RubricId = '" + RubricId+ "', TotalMarks = '" + TotalMarks+ "', AssessmentId ='" + AssessmentId + "', DateUpdated = '" + dt + "'";
                    query = "UPDATE AssessmentComponent " + query + " WHERE Id = '" + Id + "';";
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    loadData();
                    txtName.Text = "";
                    numTotalMarks.Value = 0;
                    btnSave.Enabled = false;
                    MessageBox.Show("Record Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception z)
            {
                MessageBox.Show(z.ToString());
            }
        }
        public void loadRubrics()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select Details,Id from Rubric", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbRubrics.DataSource = dt;
                cmbRubrics.DisplayMember = "Details";
                cmbRubrics.ValueMember = "Id";


                con = Configuration.getInstance().getConnection();
                cmd = new SqlCommand("Select Title,Id from Assessment", con);

                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                cmbAssessment.DataSource = dt1;
                cmbAssessment.DisplayMember = "Title";
                cmbAssessment.ValueMember = "Id";


            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
            }
        }

        private void AssessmentComponentGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (AssessmentComponentGV.Columns["Delete"].Index == e.ColumnIndex)
                {
                    string Id = AssessmentComponentGV.CurrentRow.Cells[2].Value.ToString();
                    string query = "DELETE FROM AssessmentComponent WHERE Id = '" + Id + "';";
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    loadData();
                    MessageBox.Show("Record Deleted Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else if (AssessmentComponentGV.Columns["Edit"].Index == e.ColumnIndex)
                {
                    btnSave.Enabled = true;
                    txtName.Text = AssessmentComponentGV.CurrentRow.Cells[3].Value.ToString();
                    string TotalMarks = AssessmentComponentGV.CurrentRow.Cells[5].Value.ToString();
                    numTotalMarks.Value = int.Parse(TotalMarks);
                    cmbRubrics.Text = AssessmentComponentGV.CurrentRow.Cells[4].Value.ToString();
                    cmbAssessment.Text = AssessmentComponentGV.CurrentRow.Cells[8].Value.ToString();



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
