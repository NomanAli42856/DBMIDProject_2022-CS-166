

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using System.IO;


namespace Mid_Term_Project
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            studentsGV.loadData();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            studentsGV.Show();
            studentsGV.loadData();
            studentsGV.BringToFront();
            HeaderText.Text = "Manage Students";
            btnManageStudents.BackColor = Color.FromArgb(48, 195, 203);
            btnManageStudents.ForeColor = Color.White;
        }
        private void btnManageStudents_Click(object sender, EventArgs e)
        {
            clearButtonSelectionColor();
            studentsGV.Show();
            studentsGV.loadData();
            studentsGV.BringToFront();
            HeaderText.Text = "Manage Students";
            btnManageStudents.BackColor = Color.FromArgb(48, 195, 203);
            btnManageStudents.ForeColor = Color.White;
        }

        private void btnAttendence_Click(object sender, EventArgs e)
        {
            clearButtonSelectionColor();
            attendence1.Show();

            attendence1.BringToFront();
            HeaderText.Text = "Mark Attendence";
            btnAttendence.BackColor = Color.FromArgb(48, 195, 203);
            btnAttendence.ForeColor = Color.White;
        }

        private void btnManageRubricLevels_Click(object sender, EventArgs e)
        {
            clearButtonSelectionColor();
            manageRubricLevel1.BringToFront();
            manageRubricLevel1.loadEveryThing();
            HeaderText.Text = "Manage Rubric Level";
            btnManageRubricLevels.BackColor = Color.FromArgb(48, 195, 203);
            btnManageRubricLevels.ForeColor = Color.White;
        }

        private void btnAssessmentComponent_Click(object sender, EventArgs e)
        {
            clearButtonSelectionColor();
            manageAssessmentComponent1.BringToFront();
            manageAssessmentComponent1.loadData();
            manageAssessmentComponent1.loadRubrics();
            HeaderText.Text = "Manage Assessments Component";
            btnAssessmentComponent.BackColor = Color.FromArgb(48, 195, 203);
            btnAssessmentComponent.ForeColor = Color.White;
        }

        private void btnManageCLOs_Click(object sender, EventArgs e)
        {
            clearButtonSelectionColor();
            studentsGV.Hide();
            manageCLOs1.loadData();
            manageCLOs1.BringToFront();
            HeaderText.Text = "Manage CLOs";
            btnManageCLOs.BackColor = Color.FromArgb(48, 195, 203);
            btnManageCLOs.ForeColor = Color.White;
        }

        private void btnManageRubrics_Click(object sender, EventArgs e)
        {
            clearButtonSelectionColor();
            manageRubrics1.BringToFront();
            manageRubrics1.loadCLOs();
            manageRubrics1.loadData();
            
            HeaderText.Text = "Manage Rubrics";
            btnManageRubrics.BackColor = Color.FromArgb(48, 195, 203);
            btnManageRubrics.ForeColor = Color.White;

        }

        private void btnManageAssessment_Click(object sender, EventArgs e)
        {
            clearButtonSelectionColor();
            manageAssesment1.BringToFront();
            manageAssesment1.loadData();
            HeaderText.Text = "Manage Assessments";
            btnManageAssessment.BackColor = Color.FromArgb(48, 195, 203);
            btnManageAssessment.ForeColor = Color.White;
        }

        private void btnMarkEvaluation_Click(object sender, EventArgs e)
        {
            clearButtonSelectionColor();
            manageEvaluation1.BringToFront();
            HeaderText.Text = "Mark Evaluation";
            btnMarkEvaluation.BackColor = Color.FromArgb(48, 195, 203);
            btnMarkEvaluation.ForeColor = Color.White;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            clearButtonSelectionColor();
            Reports r = new Reports();
            r.ShowDialog();
        }

        public void clearButtonSelectionColor()
        {
            btnAssessmentComponent.BackColor = Color.FromArgb(25, 57, 86);
            btnAssessmentComponent.ForeColor = Color.Gainsboro; 

            btnManageAssessment.BackColor = Color.FromArgb(25, 57, 86);
            btnManageAssessment.ForeColor = Color.Gainsboro;

            btnManageRubricLevels.BackColor = Color.FromArgb(25, 57, 86);
            btnManageRubricLevels.ForeColor = Color.Gainsboro;

            btnManageRubrics.BackColor = Color.FromArgb(25, 57, 86);
            btnManageRubrics.ForeColor = Color.Gainsboro;

            btnManageCLOs.BackColor = Color.FromArgb(25, 57, 86);
            btnManageCLOs.ForeColor = Color.Gainsboro;

            btnManageStudents.BackColor = Color.FromArgb(25, 57, 86);
            btnManageStudents.ForeColor = Color.Gainsboro;

            btnMarkEvaluation.BackColor = Color.FromArgb(25, 57, 86);
            btnMarkEvaluation.ForeColor = Color.Gainsboro;

            btnReport.BackColor = Color.FromArgb(25, 57, 86);
            btnReport.ForeColor = Color.Gainsboro;
        }

    }
}
