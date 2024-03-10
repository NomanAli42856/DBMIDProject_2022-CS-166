using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Mid_Term_Project
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
            loadCLOs();
        }
        public void loadCLOs()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select Id,Name From Clo", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbCLO.DataSource = dt;
                cmbCLO.DisplayMember = "Name";
                cmbCLO.ValueMember = "Id";
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
            }


        }

        private void cmbCLOs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReports.Text == "Assessment Wise")
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
                    cmbAssessment.Enabled = true;
                    cmbCLO.Enabled = false;
                }
                catch (Exception z)
                {
                    MessageBox.Show(z.Message);
                }
            }
            else if(cmbReports.Text == "Attendence Report")
            {
                cmbAssessment.Enabled = false;
                cmbCLO.Enabled = false;
            }
            else if (cmbReports.Text == "CLO Wise Result")
            {
                cmbAssessment.Enabled = false;
                cmbCLO.Enabled = true;
                loadCLOs();
            }
            else if (cmbReports.Text == "Class Result")
            {
                cmbAssessment.Enabled = false;
                cmbCLO.Enabled = false;

            }
            


        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {

            try
            {
                if (cmbReports.Text == "Assessment Wise")
                {
                    string assessment = cmbAssessment.Text;
                    var con = Configuration.getInstance().getConnection();

                    string query = "Select Stu.RegistrationNumber,Stu.FirstName + ' ' + Stu.LastName as Name,sum(AC.TotalMarks) as [Total Marks],sum(cast(RL.MeasurementLevel * AC.TotalMarks / 4 as decimal(10, 2))) as [Obtained Marks] FROM StudentResult StuR JOIN Student Stu ON Stu.Id = StuR.StudentId JOIN RubricLevel RL ON StuR.RubricMeasurementId = RL.Id  JOIn AssessmentComponent AC ON AC.Id = StuR.AssessmentComponentId  JOIN Assessment A ON A.Id = AC.AssessmentId JOIN Rubric R ON R.Id = RL.RubricId  Where A.Title = '" + assessment + "'  Group by A.Title,Stu.RegistrationNumber,Stu.FirstName,Stu.LastName,R.Details";
                    SqlCommand command = new SqlCommand(query, con);
                    SqlDataReader reader = command.ExecuteReader();
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    saveFileDialog.Title = "Save PDF Report";
                    saveFileDialog.ShowDialog();

                    if (saveFileDialog.FileName != "")
                    {
                        Document document = new Document();
                        PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
                        document.Open();

                        iTextSharp.text.Font titleFont = FontFactory.GetFont("Times New Roman", 26, iTextSharp.text.Font.BOLD);
                        Paragraph title = new Paragraph("Assessment Wise Report", titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        title.SpacingAfter = 20f;
                        iTextSharp.text.Font AssessmentFont = FontFactory.GetFont("Times New Roman", 16, iTextSharp.text.Font.BOLD);
                        Paragraph assessmentName = new Paragraph(assessment, AssessmentFont);
                        assessmentName.Alignment = Element.ALIGN_CENTER;
                        assessmentName.SpacingAfter = 10f;

                        document.Add(title);
                        document.Add(assessmentName);


                        // Add table to PDF document
                        PdfPTable table = new PdfPTable(4);
                        table.AddCell("RegNo");
                        table.AddCell("Name");
                        table.AddCell("Total Marks");
                        table.AddCell("Obtained Marks");
                        while (reader.Read())
                        {
                            table.AddCell(reader["RegistrationNumber"].ToString());
                            table.AddCell(reader["Name"].ToString());
                            table.AddCell(reader["Total Marks"].ToString());
                            table.AddCell(reader["Obtained Marks"].ToString());
                        }
                        document.Add(table);

                        // Close PDF document and dispose of resources
                        document.Close();
                        MessageBox.Show("Report Saved Successfully!");
                        this.Close();
                    }

                }
                else if (cmbReports.Text == "CLO Wise Result")
                {
                    string cloId = cmbCLO.SelectedValue.ToString();
                    var con = Configuration.getInstance().getConnection();

                    string query = "Select Stu.RegistrationNumber,Stu.FirstName + ' '+ Stu.LastName as [Student Name],sum(AC.TotalMarks) as [Total Marks],sum(cast( RL.MeasurementLevel*AC.TotalMarks/4 as decimal(10,2))) as [Obtained Marks]  FROM StudentResult StuR  JOIN Student Stu ON Stu.Id=StuR.StudentId  JOIN RubricLevel RL  ON StuR.RubricMeasurementId=RL.Id  JOIn AssessmentComponent AC  ON AC.Id=StuR.AssessmentComponentId  JOIN Assessment A  ON A.Id=AC.AssessmentId  JOIN Rubric R   ON R.Id=RL.RubricId  JOIN Clo On Clo.Id=R.CloId  Where Clo.Id='" + cloId+ "'  Group By Stu.Id,Stu.RegistrationNumber,stu.FirstName,stu.LastName,Clo.Id,Clo.Name";
                    SqlCommand command = new SqlCommand(query, con);
                    SqlDataReader reader = command.ExecuteReader();
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    saveFileDialog.Title = "Save PDF Report";
                    saveFileDialog.ShowDialog();

                    if (saveFileDialog.FileName != "")
                    {
                        Document document = new Document();
                        PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
                        document.Open();

                        iTextSharp.text.Font titleFont = FontFactory.GetFont("Times New Roman", 26, iTextSharp.text.Font.BOLD);
                        Paragraph title = new Paragraph("CLO Wise Report", titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        title.SpacingAfter = 20f;
                        iTextSharp.text.Font AssessmentFont = FontFactory.GetFont("Times New Roman", 16, iTextSharp.text.Font.BOLD);
                        Paragraph assessmentName = new Paragraph(cmbCLO.Text.ToString(), AssessmentFont);
                        assessmentName.Alignment = Element.ALIGN_CENTER;
                        assessmentName.SpacingAfter = 10f;

                        document.Add(title);
                        document.Add(assessmentName);


                        // Add table to PDF document
                        PdfPTable table = new PdfPTable(4);
                        table.AddCell("RegNo");
                        table.AddCell("Name");
                        table.AddCell("Total Marks");
                        table.AddCell("Obtained Marks");
                        while (reader.Read())
                        {
                            table.AddCell(reader["RegistrationNumber"].ToString());
                            table.AddCell(reader["Student Name"].ToString());
                            table.AddCell(reader["Total Marks"].ToString());
                            table.AddCell(reader["Obtained Marks"].ToString());
                        }
                        document.Add(table);

                        // Close PDF document and dispose of resources
                        document.Close();
                        MessageBox.Show("Report Saved Successfully!");
                        this.Close();
                    }

                }

                else if (cmbReports.Text == "Class Result")
                {
                    string assessment = cmbAssessment.Text;
                    var con = Configuration.getInstance().getConnection();

                    string query = "Select N.StudentId,N.Name,N.[Total Marks],N.[Total Obtained],cast((N.[Total Obtained] / N.[Total MArks]) *100 as numeric(18,2)) as [Percentage obtained] From(Select B.StudentId, B.Name, sum(B.TotalMarks) as [Total Marks], Sum(B.[Obtained Marks]) as [Total Obtained] From(Select T.StudentId, T.Name, T.TotalMarks, (cast(T.[Student obtained rub leve] as float) / 4) * T.TotalMarks as [Obtained Marks] From(Select S.StudentId, (St.FirstName + ' ' + St.LastName) as[Name], A.RubricId, A.TotalMarks, (Select MeasurementLevel From RubricLevel R where R.Id = S.RubricMeasurementId) as [Student obtained rub leve] From StudentResult S Join AssessmentComponent A on S.AssessmentComponentId = A.Id join Student ST on St.Id = S.StudentId) as T) as B Group by B.StudentId,B.Name) As N  order by[Percentage obtained] DESC";
                    SqlCommand command = new SqlCommand(query, con);
                    SqlDataReader reader = command.ExecuteReader();
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    saveFileDialog.Title = "Save PDF Report";
                    saveFileDialog.ShowDialog();

                    if (saveFileDialog.FileName != "")
                    {
                        Document document = new Document();
                        PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
                        document.Open();

                        iTextSharp.text.Font titleFont = FontFactory.GetFont("Times New Roman", 26, iTextSharp.text.Font.BOLD);
                        Paragraph title = new Paragraph("Class Result", titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        title.SpacingAfter = 20f;


                        document.Add(title);



                        // Add table to PDF document
                        PdfPTable table = new PdfPTable(5);
                        table.AddCell("Student Id");
                        table.AddCell("Name");
                        table.AddCell("Total Marks");
                        table.AddCell("Total Obtained");
                        table.AddCell("Percentage obtained");
                        while (reader.Read())
                        {
                            table.AddCell(reader["StudentId"].ToString());
                            table.AddCell(reader["Name"].ToString());
                            table.AddCell(reader["Total Marks"].ToString());
                            table.AddCell(reader["Total Obtained"].ToString());
                            table.AddCell(reader["Percentage obtained"].ToString());
                        }
                        document.Add(table);

                        // Close PDF document and dispose of resources
                        document.Close();
                        MessageBox.Show("Report Saved Successfully!");
                        this.Close();

                    }
                }
                else if (cmbReports.Text == "Attendence Report")
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();

                    // Set the default file extension to PDF
                    saveFileDialog.DefaultExt = "pdf";

                    // Set the filter to only show PDF files
                    saveFileDialog.Filter = "PDF files (.pdf)|.pdf";

                    // Show the dialog box and get the result
                    DialogResult result = saveFileDialog.ShowDialog();

                    // If the user clicked OK
                    if (result == DialogResult.OK)
                    {
                        // Get the filename and path
                        string filename = saveFileDialog.FileName;
                        
                        string query = "DECLARE @cols AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)\r\nWITH DateList AS (\r\n  SELECT DISTINCT FORMAT(CC.AttendanceDate, 'dd/MM/yyyy') AS AttendanceDateFormatted\r\n  FROM ClassAttendance CC\r\n)\r\nSELECT @cols = STUFF((SELECT distinct ',' + QUOTENAME(AttendanceDateFormatted)\r\nFROM DateList\r\nFOR XML PATH(''), TYPE\r\n).value('.', 'NVARCHAR(MAX)')\r\n,1,1,'')\r\n\r\nSET @query = 'SELECT RegistrationNumber, Name, ' + @cols + '\r\nFROM\r\n(\r\nSELECT s.RegistrationNumber, s.FirstName as Name,\r\nFORMAT(ca.AttendanceDate, ''dd/MM/yyyy'') AS AttendanceDateFormatted,\r\nISNULL(L.Name, ''N/A'') AS AttendanceStatus\r\nFROM student s\r\nLEFT JOIN StudentAttendance sa ON s.Id = sa.StudentId\r\nJOIN ClassAttendance CA ON CA.Id = SA.AttendanceId\r\nLEFT JOIN Lookup L ON L.LookupId = SA.AttendanceStatus\r\n) AS source_table\r\nPIVOT\r\n(\r\nMAX(AttendanceStatus)\r\nFOR AttendanceDateFormatted IN (' + @cols + ')\r\n) AS pivot_table'\r\n\r\nEXECUTE(@query)\r\n";
                        var connection = Configuration.getInstance().getConnection();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // Determine the number of columns in the resultset
                                int numColumns = reader.FieldCount;

                                // Create a PdfPTable object with the same number of columns as the resultset
                                PdfPTable table = new PdfPTable(numColumns);

                                // Add the column headers to the PdfPTable object
                                for (int i = 0; i < numColumns; i++)
                                {
                                    table.AddCell(new PdfPCell(new Phrase(reader.GetName(i))));
                                }

                                // Iterate through the resultset and add each tuple to a new row in the PdfPTable object
                                while (reader.Read())
                                {
                                    for (int i = 0; i < numColumns; i++)
                                    {
                                        table.AddCell(new PdfPCell(new Phrase(reader[i].ToString())));
                                    }
                                }

                                // Add the PdfPTable object to the PDF document
                                Document document = new Document(PageSize.A4.Rotate());
                                iTextSharp.text.Font titleFont = FontFactory.GetFont("Times New Roman", 26, iTextSharp.text.Font.BOLD);
                                Paragraph title = new Paragraph("Attendance Report", titleFont);
                                title.Alignment = Element.ALIGN_CENTER;
                                title.SpacingAfter = 20f;

                                PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
                                document.Open();
                                document.Add(title);
                                document.Add(table);
                                document.Close();
                                MessageBox.Show("Report Saved Successfully!");
                            }
                        }
        
                    }
                }
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
            }
        }
    }
    
}
