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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Mid_Term_Project
{
    public partial class manageEvaluation : UserControl
    {
        public manageEvaluation()
        {
            InitializeComponent();
            loadEveryThing();
            
        }
        public void loadEveryThing()
        {
            loadStudents();
            loadAssessment();
            loadAssessmentComponent();
            loadEvaluationTable();
        }
        public void loadAssessmentComponent()
        {
            try
            {
                string Id = cmbAssessment.SelectedValue.ToString();
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select Id,Name From AssessmentComponent Where AssessmentId= '" + Id + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbAssessmentComponent.DataSource = dt;
                cmbAssessmentComponent.DisplayMember = "Name";
                cmbAssessmentComponent.ValueMember = "Id";

            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
        private void loadEvaluationTable()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select SR.StudentId,S.RegistrationNumber as RegNo,SR.AssessmentComponentId,SR.RubricMeasurementId, AC.Name as Component,RL.Details as RubricLevel,R.Details,SR.EvaluationDate as Rubric from StudentResult SR  JOIN Student S on SR.StudentId = S.Id Join AssessmentComponent AC ON AC.Id = SR.AssessmentComponentId Join RubricLevel RL ON RL.Id = SR.RubricMeasurementId Join Rubric R ON R.Id = RL.RubricId;", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable d = new DataTable();
            da.Fill(d);
            EvaluationGV.DataSource = d;
            

        }
        public void loadStudents()
        {
            try
            { 
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select (FirstName +  ' ' + LastName) as FullName,Id From Student Where Status=5", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbStudent.DataSource = dt;
                cmbStudent.DisplayMember = "FullName";
                cmbStudent.ValueMember = "Id";
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
            }

        }
        public void loadMeasurementLevel(int rubricId)
        {
            try
            { 
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select Id,Details From RubricLevel Where RubricId= '" + rubricId + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbMeasurementLevel.DataSource = dt;
                cmbMeasurementLevel.DisplayMember = "Details";
                cmbMeasurementLevel.ValueMember = "Id";
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
            }

        }
        public void loadAssessment()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select Id,Title From Assessment", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbAssessment.DataSource = dt;
                cmbAssessment.DisplayMember = "Title";
                cmbAssessment.ValueMember = "Id";
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
            }

        }

        private void cmbAssessment_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadAssessmentComponent();

        }

        private void cmbAssessmentComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string AssessmentComponentId = cmbAssessmentComponent.SelectedValue.ToString();
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Details FROM Rubric WHERE Id = (Select RubricId FROM AssessmentComponent WHERE Id = '" + AssessmentComponentId + "');", con);
                string Detail = cmd.ExecuteScalar().ToString();
                txtRubric.Text = Detail;


                con = Configuration.getInstance().getConnection();
                cmd = new SqlCommand("Select Id,Details From RubricLevel Where RubricId = (Select RubricId FROM AssessmentComponent WHERE Id = '" + AssessmentComponentId + "'); ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbMeasurementLevel.DataSource = dt;
                cmbMeasurementLevel.DisplayMember = "Details";
                cmbMeasurementLevel.ValueMember = "Id";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert Into StudentResult values(@StudentId,@AssessmentComponentId,@RubricMeasurementId,@EvaluationDate)", con);
                cmd.Parameters.AddWithValue("@StudentId", int.Parse(cmbStudent.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@AssessmentComponentId", int.Parse(cmbAssessmentComponent.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@RubricMeasurementId", int.Parse(cmbMeasurementLevel.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@EvaluationDate", DateTime.Now);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Evaluation Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                loadEvaluationTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EvaluationGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (EvaluationGV.Columns["Delete"].Index == e.ColumnIndex)
                {
                    string Id = EvaluationGV.CurrentRow.Cells[2].Value.ToString();
                    string query = "DELETE FROM StudentResult WHERE Id = '" + Id + "';";
                    txtRubric.Text = query;
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    loadEvaluationTable();
                    MessageBox.Show("Successfully Deleted");
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    }
