using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace Mid_Term_Project
{
    public partial class manageStudents : UserControl
    {
        public manageStudents()
        {
            InitializeComponent();
            loadData();
            

        }

        private void studentsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (studentsGV.Columns["Delete"].Index == e.ColumnIndex)
            {
                string Id = studentsGV.CurrentRow.Cells[2].Value.ToString();
                string query = "DELETE FROM Student WHERE Id = '" + Id + "';";
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                loadData();
                clearForm();
                MessageBox.Show("Successfully Deleted");
            }
            else if (studentsGV.Columns["Edit"].Index == e.ColumnIndex)
            {
                btnSave.Enabled = true;
                txtboxFirstName.Text = studentsGV.CurrentRow.Cells[3].Value.ToString();
                txtboxLastName.Text = studentsGV.CurrentRow.Cells[4].Value.ToString();
                txtboxContact.Text = studentsGV.CurrentRow.Cells[5].Value.ToString();
                txtboxEmail.Text = studentsGV.CurrentRow.Cells[6].Value.ToString();
                txtboxRegNo.Text = studentsGV.CurrentRow.Cells[7].Value.ToString();
                cmbStatus.Text = studentsGV.CurrentRow.Cells[8].Value.ToString();
            }


           
        }
        public void loadData()
        {
            
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select FirstName,LastName,Contact,Email,RegistrationNumber from Student Where Status = 5", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            studentsGV.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtboxFirstName.Text != ""   && txtboxEmail.Text != "" && txtboxRegNo.Text != "" && cmbStatus.Text != "")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("SELECT LookupId FROM Lookup WHERE Name = '" + cmbStatus.Text + "';", con);
                    int result = (int)cmd.ExecuteScalar();
                    

                    cmd = new SqlCommand("Insert into Student values (@FirstName, @LastName,@Contact,@Email,@RegistrationNumber,@Status)", con);
                    cmd.Parameters.AddWithValue("@RegistrationNumber", txtboxRegNo.Text);
                    cmd.Parameters.AddWithValue("@FirstName", txtboxFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtboxLastName.Text);
                    cmd.Parameters.AddWithValue("@Contact", txtboxContact.Text);
                    cmd.Parameters.AddWithValue("@Email", txtboxEmail.Text);
                    cmd.Parameters.AddWithValue("@Status", result);
                    cmd.ExecuteNonQuery();
                    loadData();
                    clearForm();
                    MessageBox.Show("Student Registered Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        public void clearForm()
        {
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtboxContact.Text = "";
            txtboxEmail.Text = "";
            txtboxRegNo.Text = "";
            cmbStatus.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string Id = studentsGV.CurrentRow.Cells[2].Value.ToString();
                string FirstName = txtboxFirstName.Text;
                string LastName = txtboxLastName.Text;
                string Contact = txtboxContact.Text;
                string Email = txtboxEmail.Text;
                string RegNo = txtboxRegNo.Text;
                

                if (txtboxFirstName.Text != "" && txtboxEmail.Text != "" && txtboxRegNo.Text != "" && cmbStatus.Text != "")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("SELECT LookupId FROM Lookup WHERE Name = '" + cmbStatus.Text + "';", con);
                    int Status = (int)cmd.ExecuteScalar();

                    string query = "SET RegistrationNumber = '" + RegNo + "'" + ", FirstName = '" + FirstName + "'" + ", LastName = '" + LastName + "'" + ", Contact = '" + Contact + "'" + ", Email = '" + Email + "'" + ", Status = '" + Status + "'";
                    con = Configuration.getInstance().getConnection();
                    query = "UPDATE Student " + query + " WHERE Id = '" + Id + "';";
                    
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    loadData();
                    clearForm();
                    btnSave.Enabled=false;
                    MessageBox.Show("Student Record Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            catch(Exception z)
            {
                MessageBox.Show(z.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text;
            if (search != null)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select * from Student Where Id Like '%" + search + "%' OR FirstName Like '%" + search + "%' OR LastName LIKE '%" + search + "%' OR Contact Like '%" + search + "%' OR Email LIKE '%" + search + "%' OR RegistrationNumber LIKE '%" + search + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                studentsGV.DataSource = dt;
            }
            else
            {
                txtSearch.Text = "";
            }
        }
    }
}
