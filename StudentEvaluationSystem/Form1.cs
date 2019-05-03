using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace StudentEvaluationSystem
{
   
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=ProjectB2;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
        AJ_DataClass ajdbClass = new AJ_DataClass();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            // fill combos  
            ajdbClass.popCmb(cmbClo, "select * from clo", "id", "name");
            ajdbClass.popCmb(cmbRubrics, "select * from Rubric where CloId='" + cmbClo.SelectedValue + "'", "id", "Details");
            ajdbClass.popCmb(cmbStatus, "select * from Lookup where category='STUDENT_STATUS'", "Lookupid", "Name");
            
            ajdbClass.popCmb(cmbRubAsses, "select * from Rubric ", "id", "details");
            ajdbClass.popCmb(cmbStudent, "SELECT * FROM STUDENT", "id", "FirstName");
            ajdbClass.popCmb(cmbAseesRE, "SELECT *  FROM  Assessment ", " Assessment.Id", "Title");
            ajdbClass.popCmb(cmbAssesR, "SELECT *  FROM  AssessmentComponent where AssessmentId='" + cmbAseesRE.SelectedValue + "'", "  AssessmentComponent.Id", "Name");
            ajdbClass.popCmb(cmbRubRes, "select * from RubricLevel", "Id", "Details");
            ajdbClass.popCmb(cmbAssesment, "SELECT *  FROM  Assessment ", " Assessment.Id", "Title");
            // fill data grid view
            ajdbClass.popGrid(gvClo, "select id,name from clo");
            ajdbClass.popGrid(gvRub, "select id,details from Rubric where cloid='" + cmbClo.SelectedValue + "'");
            ajdbClass.popGrid(gvRubSettings, "select id,details,MeasurementLevel from RubricLevel where RubricId='" + cmbRubrics.SelectedValue + "'");
            string qry = "SELECT Student.Id, Student.FirstName, Student.LastName, Student.Contact, Student.Email, Student.RegistrationNumber, Lookup.Name, Lookup.LookupId " +
              " FROM Student INNER JOIN " + " Lookup ON Student.Status = Lookup.LookupId ";
            ajdbClass.popGrid(gvStudents, qry); txtRubricSetDetails.Text = ""; txtRubricSetLevel.Text = "";
            ajdbClass.popGrid(gvAssesment, "SELECT Assessment.Id, Title, DateCreated, TotalMarks, TotalWeightage  FROM  Assessment");
            gvAttandance.DataSource = GetAttendanceRecord();
            ajdbClass.popGrid(gvAssesComp, "select AssessmentComponent.Id, Name, Assessment.Title,Rubric.details as Rubric ,AssessmentComponent.TotalMarks from AssessmentComponent inner join Rubric on  AssessmentComponent.RubricId=Rubric.Id INNER JOIN Assessment ON AssessmentComponent.AssessmentId = Assessment.Id ");
            ajdbClass.popGrid(gvResult, "select FirstName,Assessment.Title,AssessmentComponent.Name,EvaluationDate,RubricLevel.details as Rubric from  Student INNER JOIN   StudentResult ON Student.Id = StudentResult.StudentId INNER JOIN RubricLevel ON StudentResult.RubricMeasurementId = RubricLevel.Id INNER JOIN AssessmentComponent ON StudentResult.AssessmentComponentId = AssessmentComponent.Id INNER JOIN Assessment ON AssessmentComponent.AssessmentId = Assessment.Id");
        }

        // add student
        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            string _DataFields = "FirstName,LastName,Contact,Email,RegistrationNumber,Status";
            string _Values = "'" + txtfname.Text.Trim().Replace("'", "''") + "','" + txtlname.Text.Trim().Replace("'", "''") + "','" + txtcon.Text.Trim().Replace("'", "''") + "','" + txtemail.Text.Trim().Replace("'", "''") + "','" + txtRegNo.Text.Trim().Replace("'", "''") + "','" + cmbStatus.SelectedValue + "'";
            string Result = ajdbClass.InsertIntoDatabase("Student", _DataFields, _Values);
            string qry = "SELECT Student.Id, Student.FirstName, Student.LastName, Student.Contact, Student.Email, Student.RegistrationNumber, Lookup.Name, Lookup.LookupId " +
            " FROM  Student INNER JOIN " + " Lookup ON Student.Status = Lookup.LookupId ";
            ajdbClass.popGrid(gvStudents, qry); txtRubricSetDetails.Text = ""; txtRubricSetLevel.Text = "";
            MessageBox.Show(Result);
            txtfname.Text = ""; txtlname.Text = ""; txtRegNo.Text = ""; txtemail.Text = ""; txtcon.Text = "";

        }
        // prress key for update and delete student
        private void gvStudents_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (gvStudents.SelectedCells.Count > 0)
                {
                    int selectedrowindex = gvStudents.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = gvStudents.Rows[selectedrowindex];
                    string _id = Convert.ToString(selectedRow.Cells["id"].Value);
                    lblidStud.Text = _id;
                    string _qry = "";
                    DataSet ds = new DataSet();
                    string qry = "SELECT        Student.Id, Student.FirstName, Student.LastName, Student.Contact, Student.Email, Student.RegistrationNumber, Lookup.Name, Lookup.LookupId " +
                   " FROM  Student INNER JOIN " + " Lookup ON Student.Status = Lookup.LookupId where id='" + _id + "'";

                    ds = ajdbClass.GetRecords("tbl", qry);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        txtfname.Text = dr["FirstName"].ToString().Trim();
                        txtlname.Text = dr["LastName"].ToString().Trim();
                        txtcon.Text = dr["Contact"].ToString().Trim();
                        txtemail.Text = dr["Email"].ToString().Trim();
                        txtRegNo.Text = dr["RegistrationNumber"].ToString().Trim();
                        cmbStatus.SelectedValue = dr["LookupId"].ToString().Trim();

                    }
                }
            }
            //delete student
            if (e.KeyCode == Keys.Delete)
            {
                if (AskYesNo() == false) return;
                int selectedrowindex = gvStudents.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = gvStudents.Rows[selectedrowindex];
                string _id = Convert.ToString(selectedRow.Cells["id"].Value);

                string s = ajdbClass.DeleteFromDatabase("delete from student where id='" + _id + "'");
                if (s != "Delete Record(s) Successfully") s = "Unable to Delete. Record might be required in other tables";
                MessageBox.Show(s);

                string qry = "SELECT Student.Id, Student.FirstName, Student.LastName, Student.Contact, Student.Email, Student.RegistrationNumber, Lookup.Name, Lookup.LookupId " +
              " FROM Student INNER JOIN " + " Lookup ON Student.Status = Lookup.LookupId ";
                ajdbClass.popGrid(gvStudents, qry); 
                txtfname.Text = ""; txtlname.Text = ""; txtRegNo.Text = ""; txtemail.Text = ""; txtcon.Text = "";

            }
        }
        // update student
        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {
           // if (lblidStud.Text == "0") return;
            string QryUpdate = " FirstName='" + txtfname.Text.Trim().Replace("'", "''") + "', LastName='" + txtlname.Text.Trim().Replace("'", "''")
           + "', Contact='" + txtcon.Text.Trim().Replace("'", "''") + "', Email='" + txtemail.Text.Trim().Replace("'", "''")
           + "', RegistrationNumber='" + txtRegNo.Text.Trim().Replace("'", "''") + "', status='" + cmbStatus.SelectedValue + "'";
            string Result = ajdbClass.UpdateDatabase("update Student set " + QryUpdate + " where id='" + lblidStud.Text + "'");
            string qry = "SELECT        Student.Id, Student.FirstName, Student.LastName, Student.Contact, Student.Email, Student.RegistrationNumber, Lookup.Name, Lookup.LookupId " +
            " FROM Student INNER JOIN " + " Lookup ON Student.Status = Lookup.LookupId ";
            ajdbClass.popGrid(gvStudents, qry);
            txtfname.Text = "";  txtlname.Text = "";txtRegNo.Text = ""; txtemail.Text = ""; txtcon.Text = "";
            MessageBox.Show("Record updated successfully");
        }
        //add attandence
        private void btnAddAttendance_Click(object sender, EventArgs e)
        {

            int t = ajdbClass.TRec("select * from ClassAttendance");
            string _DataFields = "AttendanceDate";
            string _Values = "'" + dtpAttendance.Value + "'";
            string Result = ajdbClass.InsertIntoDatabase("ClassAttendance", _DataFields, _Values);
            System.Threading.Thread.Sleep(1000);
            DataSet ds = new DataSet();
            ds = ajdbClass.GetRecords("tbl", "SELECT TOP (1) Id, AttendanceDate FROM  ClassAttendance order by id desc");
            string _lastid = "";
            foreach (DataRow dr in ds.Tables[0].Rows) { _lastid = dr["id"].ToString().Trim(); }
            DataSet dsStudents = new DataSet();
            dsStudents = ajdbClass.GetRecords("tbl", "select * from Student where status='5'");
            foreach (DataRow dr in dsStudents.Tables[0].Rows)
            {
                string _atid = _lastid;
                string stid = dr["id"].ToString();
                _DataFields = "AttendanceId, studentid,AttendanceStatus";
                _Values = "'" + _lastid.Replace("'", "''") + "','" + stid + "','1'";
                Result = ajdbClass.InsertIntoDatabase("StudentAttendance", _DataFields, _Values);
                System.Threading.Thread.Sleep(500);
            }
            ajdbClass.popList(lstAttendace, "select * from ClassAttendance order by AttendanceDate desc", "AttendanceDate");
            gvAttandance.DataSource = GetAttendanceRecord();
            MessageBox.Show("Attandance Taken!!!");
        }

        //update attandance
        private void button3_Click(object sender, EventArgs e)
        {

            for (int rows = 0; rows < gvAttandance.Rows.Count - 1; rows++)
            {
                string _atid = gvAttandance.Rows[rows].Cells[0].Value.ToString().Trim().Replace("'", "''");
                string _regno = gvAttandance.Rows[rows].Cells[2].Value.ToString().Trim().Replace("'", "''");
                object atstatus = gvAttandance.Rows[rows].Cells[5].Value.ToString().Trim().Replace("'", "''");
                string _stid = "";

                string status_ID = "";
                DataSet dsn = new DataSet();
                dsn = ajdbClass.GetRecords("tbl", "select * from Lookup where name ='" + atstatus + "'");
                foreach (DataRow dr in dsn.Tables[0].Rows)
                {
                    status_ID = dr["LookupId"].ToString();
                }

                dsn.Clear();
                dsn = ajdbClass.GetRecords("tbl", "select * from Student where RegistrationNumber ='" + _regno + "'");
                foreach (DataRow dr in dsn.Tables[0].Rows)
                {
                    _stid = dr["Id"].ToString();
                }



                string s = ajdbClass.UpdateDatabase("update  StudentAttendance set AttendanceStatus='" + status_ID + "' where AttendanceId='" + _atid + "' and StudentId='" + _stid + "'");

            }
            MessageBox.Show("attendance updated");

        }

        // add clo
        private void btnAddCLO_Click(object sender, EventArgs e)
        {
            string _DataFields = "Name, datecreated,dateupdated";
            string _Values = "'" + txtCLO.Text.Replace("'", "''") + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "'";
            string Result = ajdbClass.InsertIntoDatabase("clo", _DataFields, _Values);
            ajdbClass.popGrid(gvClo, "select id,name from clo");
            ajdbClass.popCmb(cmbClo, "select * from clo", "id", "name");

        }

        // delete rows from clos
        private void gvClo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Delete)
            {
                if (AskYesNo() == false) return;
                int selectedrowindex = gvClo.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = gvClo.Rows[selectedrowindex];
                string _id = Convert.ToString(selectedRow.Cells["id"].Value);
                AJ_DataClass ajdbClass = new AJ_DataClass();
                string s = ajdbClass.DeleteFromDatabase("delete from clo where id='" + _id + "'");
                ajdbClass.popGrid(gvClo, "select id,name from clo");
                ajdbClass.popCmb(cmbClo, "select * from clo", "id", "name");
                lblrubricLevels.Text = "Rubrics Level - Settings Clo:" + cmbClo.Text;
            }
        }

        //update clo
        private void btnUpdateClo_Click(object sender, EventArgs e)
        {
            for (int rows = 0; rows < gvClo.Rows.Count - 1; rows++)
            {
                string _id = gvClo.Rows[rows].Cells[0].Value.ToString().Trim().Replace("'", "''");
                string _name = gvClo.Rows[rows].Cells[1].Value.ToString().Trim().Replace("'", "''");
                string _dateupdated = DateTime.Now.ToString();
                string s = ajdbClass.UpdateDatabase("update clo set name='" + _name + "',dateupdated='" + _dateupdated + "' where id='" + _id + "'");

            }
            ajdbClass.popGrid(gvClo, "select id,name from clo");
            ajdbClass.popCmb(cmbClo, "select * from clo", "id", "name");
            lblrubricLevels.Text = "Rubrics Level - Settings Clo:" + cmbClo.Text;
            MessageBox.Show("Update Successfully");
        }
        // add rubrics
        private void btnRubric_Click(object sender, EventArgs e)
        {
            string Id_incremented = IncrementedID();
            string _DataFields = "Id,cloid,details";
            string _Values ="'" + Id_incremented + "','" + cmbClo.SelectedValue + "','" + txtRubric.Text.Replace("'", "''") + "'";
            string Result = ajdbClass.InsertIntoDatabase("Rubric", _DataFields, _Values);
            ajdbClass.popGrid(gvRub, "select id,details from Rubric where cloid='" + cmbClo.SelectedValue + "'"); txtRubric.Text = "";
            ajdbClass.popCmb(cmbRubrics, "select * from Rubric where CloId='" + cmbClo.SelectedValue + "'", "id", "Details");

            MessageBox.Show(Result);
            
        }
        // update Rubrics
        private void btnUpdateRubric_Click(object sender, EventArgs e)
        {
            for (int rows = 0; rows < gvRub.Rows.Count - 1; rows++)
            {
                string _id = gvRub.Rows[rows].Cells[0].Value.ToString().Trim().Replace("'", "''");
                string _details = gvRub.Rows[rows].Cells[1].Value.ToString().Trim().Replace("'", "''");
                string s = ajdbClass.UpdateDatabase("update Rubric set details='" + _details + "' where id='" + _id + "'");

            }
            ajdbClass.popGrid(gvRub, "select id,details from Rubric where cloid='" + cmbClo.SelectedValue + "'");
            MessageBox.Show("records updated successfully");

        }
        // add rubric level
        private void btnAddRubricLevel_Click(object sender, EventArgs e)
        {
            string _DataFields = "RubricId,details,MeasurementLevel";
            string _Values = "'" + cmbRubrics.SelectedValue + "','" + txtRubricSetDetails.Text.Replace("'", "''") + "','" + txtRubricSetLevel.Text.Replace("'", "''") + "'";
            string Result = ajdbClass.InsertIntoDatabase("RubricLevel", _DataFields, _Values);
            ajdbClass.popGrid(gvRubSettings, "select id,details,MeasurementLevel from RubricLevel where RubricId='" + cmbRubrics.SelectedValue + "'");
            txtRubricSetDetails.Text = ""; txtRubricSetLevel.Text = "";
            MessageBox.Show(Result);
        }
        //update rubric level
        private void btnUpdateRubricLevel_Click(object sender, EventArgs e)
        {
            for (int rows = 0; rows < gvRubSettings.Rows.Count - 1; rows++)
            {
                string _id = gvRubSettings.Rows[rows].Cells[0].Value.ToString().Trim().Replace("'", "''");
                string _details = gvRubSettings.Rows[rows].Cells[1].Value.ToString().Trim().Replace("'", "''");
                string _mlevel = gvRubSettings.Rows[rows].Cells[2].Value.ToString().Trim().Replace("'", "''");
                string s = ajdbClass.UpdateDatabase("update RubricLevel set details='" + _details + "',MeasurementLevel='" + _mlevel + "' where id='" + _id + "'");

            }
            ajdbClass.popGrid(gvRubSettings, "select id,details,MeasurementLevel from RubricLevel where RubricId='" + cmbRubrics.SelectedValue + "'");
            txtRubricSetDetails.Text = ""; txtRubricSetLevel.Text = "";
            MessageBox.Show("records updated successfully");
        }
        //add assessment
        private void btnAddAssesment_Click(object sender, EventArgs e)
        {
            string _DataFields = "Title,DateCreated ,TotalMarks,TotalWeightage";
            string _Values = "'" + txtTitle.Text.Trim().Replace("'", "''") + "','" + dtpAssesment.Value.Date.ToString("yyyyMMdd") + "','"
                + txtTM.Text.Trim().Replace("'", "''") + "','" + txtTw.Text.Trim().Replace("'", "''") + "'";
            string Result = ajdbClass.InsertIntoDatabase("Assessment", _DataFields, _Values);
            string qry = "SELECT Assessment.Id, Title,DateCreated ,TotalMarks,TotalWeightage  FROM  Assessment";
            ajdbClass.popGrid(gvAssesment, qry); txtTitle.Text = ""; txtTM.Text = ""; txtTw.Text = "";
            ajdbClass.popCmb(cmbAssesment, "SELECT *  FROM  Assessment ", " Assessment.Id", "Title");
            MessageBox.Show(Result);
        }

        // update assesment
        private void btnUpdateAsses_Click(object sender, EventArgs e)
        {

            if (lblidStud.Text == "0") return;
            string QryUpdate = " Title='" + txtTitle.Text.Trim().Replace("'", "''") + "', DateCreated='" + dtpAssesment.Value.Date.ToString("yyyyMMdd") +
                "', TotalMarks='" + txtTM.Text.Trim().Replace("'", "''") + "', TotalWeightage='" + txtTw.Text.Trim().Replace("'", "''") +
                 "'";
            string Result = ajdbClass.UpdateDatabase("update Assessment set " + QryUpdate + " where id='" + lblidStud.Text + "'");
            string qry = "SELECT Assessment.Id, Title,DateCreated ,TotalMarks,TotalWeightage " +
            " FROM  Assessment";
            ajdbClass.popGrid(gvAssesment, qry); txtTitle.Text = ""; txtTM.Text = ""; txtTw.Text = "";
            ajdbClass.popCmb(cmbAssesment, "SELECT *  FROM  Assessment ", " Assessment.Id", "Title");

            MessageBox.Show(Result);
        }
        //for update or delete assesment
        private void gvAssesment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (gvAssesment.SelectedCells.Count > 0)
                {
                    int selectedrowindex = gvAssesment.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = gvAssesment.Rows[selectedrowindex];
                    string _id = Convert.ToString(selectedRow.Cells["Id"].Value);
                    lblidStud.Text = _id;
                    string _qry = "";
                    DataSet ds = new DataSet();
                    string qry = "SELECT  Assessment.Id, Title,DateCreated ,TotalMarks,TotalWeightage  FROM  Assessment where Id='" + _id + "'";

                    ds = ajdbClass.GetRecords("tbl", qry);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        txtTitle.Text = dr["Title"].ToString().Trim();
                        txtTM.Text = dr["TotalMarks"].ToString().Trim();
                        txtTw.Text = dr["TotalWeightage"].ToString().Trim();


                    }
                }

            }
            //DELETE ASSESSMENT
            if (e.KeyCode == Keys.Delete)
            {
                if (AskYesNo() == false) return;
                int selectedrowindex = gvAssesment.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = gvAssesment.Rows[selectedrowindex];
                string _id = Convert.ToString(selectedRow.Cells["Id"].Value);

                string s = ajdbClass.DeleteFromDatabase("delete from Assessment where  Assessment.Id='" + _id + "'");
                if (s != "Delete Record(s) Successfully") s = "Unable to Delete. Record might be required in other tables";
                MessageBox.Show(s);

                string qry = "SELECT Assessment.Id, Title,DateCreated ,TotalMarks,TotalWeightage  FROM  Assessment";
                ajdbClass.popGrid(gvAssesment, qry); txtTitle.Text = ""; txtTM.Text = ""; txtTw.Text = "";
                ajdbClass.popCmb(cmbAssesment, "SELECT *  FROM  Assessment ", " Assessment.Id", "Title");


            }
        }
        // add assesment componenet
        private void btnAddAssesComp_Click(object sender, EventArgs e)
        {
            string _DataFields = "Name ,RubricId ,TotalMarks,DateCreated,DateUpdated,AssessmentId";
            string _Values = "'" + txtAName.Text.Replace("'", "''") + "','" + cmbRubAsses.SelectedValue + "','" +
                txtTMa.Text.Replace("'", "''") + "','" + DateTime.Now.ToShortDateString() + "','" +
                DateTime.Now.ToShortDateString() + "','" + cmbAssesment.SelectedValue + "'";
            string Result = ajdbClass.InsertIntoDatabase("AssessmentComponent", _DataFields, _Values);
            ajdbClass.popGrid(gvAssesComp, "select AssessmentComponent.Id, Name, Assessment.Title,Rubric.details as Rubric ,AssessmentComponent.TotalMarks from AssessmentComponent inner join Rubric on  AssessmentComponent.RubricId=Rubric.Id INNER JOIN Assessment ON AssessmentComponent.AssessmentId = Assessment.Id ");


            txtTMa.Text = ""; txtAName.Text = "";
            MessageBox.Show(Result);
        }

        // update assesment component
        private void btnUpdateAssesComp_Click(object sender, EventArgs e)
        {
            if (lblidStud.Text == "0") return;
            string QryUpdate = " Name='" + txtAName.Text.Trim().Replace("'", "''") + "'," +
                    " RubricId = '" + cmbRubAsses.SelectedValue.ToString() + "'," +
                    " TotalMarks='" + txtTMa.Text.Trim().Replace("'", "''") + "'," +
                  " DateCreated='" + DateTime.Now.ToString() + "'," +
                  " DateUpdated='" + DateTime.Now.ToString() + "'," +
                  " AssessmentId = '" + cmbAssesment.SelectedValue.ToString() + "'";

            string Result = ajdbClass.UpdateDatabase("update  AssessmentComponent set " + QryUpdate + " where id='" + lblidStud.Text + "'");
            ajdbClass.popGrid(gvAssesComp, "select AssessmentComponent.Id, Name, Assessment.Title,Rubric.details as Rubric ,AssessmentComponent.TotalMarks from AssessmentComponent inner join Rubric on  AssessmentComponent.RubricId=Rubric.Id INNER JOIN Assessment ON AssessmentComponent.AssessmentId = Assessment.Id ");
            txtAName.Text = ""; txtTMa.Text = "";
            MessageBox.Show(Result);
        }
        // add Result
        private void AddResult_Click(object sender, EventArgs e)
        {
            string _DataFields = "StudentId ,AssessmentComponentId,RubricMeasurementId ,EvaluationDate";
            string _Values = "'" + cmbStudent.SelectedValue + "','" + cmbAssesR.SelectedValue + "','" + cmbRubRes.SelectedValue
                 + "','" + DateTime.Now.ToShortDateString() + "'";
            string Result = ajdbClass.InsertIntoDatabase("StudentResult", _DataFields, _Values);
            MessageBox.Show(Result);
            ajdbClass.popGrid(gvResult, "select FirstName,Assessment.Title,AssessmentComponent.Name,EvaluationDate,RubricLevel.details as Rubric from  Student INNER JOIN   StudentResult ON Student.Id = StudentResult.StudentId INNER JOIN RubricLevel ON StudentResult.RubricMeasurementId = RubricLevel.Id INNER JOIN AssessmentComponent ON StudentResult.AssessmentComponentId = AssessmentComponent.Id INNER JOIN Assessment ON AssessmentComponent.AssessmentId = Assessment.Id");

        }
        //update Result
        private void button4_Click(object sender, EventArgs e)
        {

            if (lblidStud.Text == "0") return;
            string QryUpdate = " StudentId='" + cmbStudent.SelectedValue.ToString() + "'," +
                    " AssessmentComponentId = '" + cmbAssesR.SelectedValue.ToString() + "'," +
                    " RubricMeasurementId='" + cmbRubRes.SelectedValue.ToString() + "'," +
                  "EvaluationDate='" + DateTime.Now.ToString() + "'";

            string Result = ajdbClass.UpdateDatabase("update  StudentResult set " + QryUpdate + " where id='" + lblidStud.Text + "'");
            ajdbClass.popGrid(gvResult, "select FirstName,Assessment.Title,AssessmentComponent.Name,EvaluationDate,RubricLevel.details as Rubric from  Student INNER JOIN   StudentResult ON Student.Id = StudentResult.StudentId INNER JOIN RubricLevel ON StudentResult.RubricMeasurementId = RubricLevel.Id INNER JOIN AssessmentComponent ON StudentResult.AssessmentComponentId = AssessmentComponent.Id INNER JOIN Assessment ON AssessmentComponent.AssessmentId = Assessment.Id");
            MessageBox.Show(Result);
        }
        /// <summary>
        /// button for report
        /// </summary>

        // button for student report
        private void btnStudR_Click(object sender, EventArgs e)
        {
            StudentReport REP = new StudentReport();
            REP.ShowDialog();

        }
        //button for assesment report
        private void BtnAssesR_Click(object sender, EventArgs e)
        {
            AssesmentReport rep = new AssesmentReport();
            rep.ShowDialog();
        }
        // bbutton for CLO Report
        private void BtnCloR_Click(object sender, EventArgs e)
        {
            CloReport rep = new CloReport();
            rep.ShowDialog();
        }
        //button for Attandance Report 
        private void button1_Click(object sender, EventArgs e)
        {
            AttandanceReport rep = new AttandanceReport();
            rep.ShowDialog();
        }
        /// <summary>
        /// Functions 
        /// </summary>
        /// <returns></returns>
        // yes or no function
        public bool AskYesNo()
        {
            bool _ret = false;
            DialogResult dialogResult = MessageBox.Show("Sure", "You you want to delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                _ret = true;
            }
            else if (dialogResult == DialogResult.No)
            {
                _ret = false;
            }
            return _ret;
        }
        //Id autoincrement function
        public string IncrementedID()
        {
            string IncRet = "1";
            DataSet ds = new DataSet();
            ds = ajdbClass.GetRecords("tbl", "select * from Rubric");

            if (ds.Tables[0].Rows.Count == 0)
            {
                IncRet = "1";
            }
            else
            {
                int _id = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    _id = Convert.ToInt16(dr["id"]);
                }
                _id++;
                IncRet = _id.ToString();

            }
            return IncRet;
        }
        // function for attendance
        private DataTable GetAttendanceRecord()
        {
            DataTable inside_AtGridTable = new DataTable();
            inside_AtGridTable.Columns.Add("AttendanceId", typeof(string));
            inside_AtGridTable.Columns.Add("AttendanceDate", typeof(string));
            inside_AtGridTable.Columns.Add("RegNo", typeof(string));
            inside_AtGridTable.Columns.Add("FirstName", typeof(string));
            inside_AtGridTable.Columns.Add("LastName", typeof(string));
            inside_AtGridTable.Columns.Add("Status", typeof(string));
            string sql;

            sql = "SELECT  TOP (100) PERCENT StudentAttendance.AttendanceId,AttendanceDate, Student.RegistrationNumber AS RegNo, Student.FirstName, Student.LastName, Lookup.Name AS Status " +
          " FROM Student INNER JOIN " + "StudentAttendance ON Student.Id = StudentAttendance.StudentId INNER JOIN " +
          " ClassAttendance ON StudentAttendance.AttendanceId = ClassAttendance.Id INNER JOIN " +
          " Lookup ON StudentAttendance.AttendanceStatus = Lookup.LookupId " +
          " ORDER BY ClassAttendance.AttendanceDate, Student.FirstName ,AttendanceId asc ";
            DataSet dsx = new DataSet();
            dsx = ajdbClass.GetRecords("tbl", sql);

            foreach (DataRow dr in dsx.Tables[0].Rows)
            {
                inside_AtGridTable.Rows.Add(dr["AttendanceId"].ToString(), dr["AttendanceDate"].ToString(), dr["RegNo"].ToString(), dr["FirstName"].ToString(), dr["LastName"].ToString(), dr["Status"].ToString());
            }
            return inside_AtGridTable;
        }
       // grid view events
        private void gvRubSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (AskYesNo() == false) return;
                int selectedrowindex = gvRubSettings.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = gvRubSettings.Rows[selectedrowindex];
                string _id = Convert.ToString(selectedRow.Cells["id"].Value);

                string s = ajdbClass.DeleteFromDatabase("delete from RubricLevel where id='" + _id + "'");
                ajdbClass.popGrid(gvRubSettings, "select id,details,MeasurementLevel from RubricLevel where RubricId='" + cmbRubrics.SelectedValue + "'"); txtRubricSetDetails.Text = ""; txtRubricSetLevel.Text = "";
            }

        }
        private void gvAssesComp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (gvAssesComp.SelectedCells.Count > 0)
                {
                    int selectedrowindex = gvAssesComp.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = gvAssesComp.Rows[selectedrowindex];
                    string _id = Convert.ToString(selectedRow.Cells["Id"].Value);
                    lblidStud.Text = _id;
                    DataSet ds = new DataSet();
                    string qry = "select AssessmentComponent.Id, Name,Rubric.details as Rubric , TotalMarks from AssessmentComponent inner join Rubric on  AssessmentComponent.RubricId=Rubric.Id ";
                                     
                    ds = ajdbClass.GetRecords("tbl", qry);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        txtAName.Text = dr["Name"].ToString().Trim();
                        txtTMa.Text = dr["TotalMarks"].ToString().Trim();
                        cmbAseesRE.SelectedValue = dr["TotalMarks"].ToString().Trim();
                        //  txtTw.Text = dr["TotalWeightage"].ToString().Trim();


                    }
                }

            }
            //DELETE ASSESSMENT COMPONENT
            if (e.KeyCode == Keys.Delete)
            {
                if (AskYesNo() == false) return;
                int selectedrowindex = gvAssesComp.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = gvAssesComp.Rows[selectedrowindex];
                string _id = Convert.ToString(selectedRow.Cells["Id"].Value);

                string s = ajdbClass.DeleteFromDatabase("delete from AssessmentComponent where  AssessmentComponent.Id='" + _id + "'");
                if (s != "Delete Record(s) Successfully") s = "Unable to Delete. Record might be required in other tables";
                MessageBox.Show(s);

                ajdbClass.popGrid(gvAssesComp, "select AssessmentComponent.Id, Name, Assessment.Title,Rubric.details as Rubric ,AssessmentComponent.TotalMarks from AssessmentComponent inner join Rubric on  AssessmentComponent.RubricId=Rubric.Id INNER JOIN Assessment ON AssessmentComponent.AssessmentId = Assessment.Id ");

                txtAName.Text = ""; txtTMa.Text = "";

            }
        }
        private void gvAttandance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex > -1)
            {
                DataGridViewComboBoxCell my_objGridDropbox = new DataGridViewComboBoxCell();
                if (gvAttandance.Columns[e.ColumnIndex].Name.Contains("Status"))
                {
                    gvAttandance[e.ColumnIndex, e.RowIndex] = my_objGridDropbox;
                    ajdbClass.popCmbGrid(my_objGridDropbox, "select name as Status from lookup where lookupid <> '5' and Lookupid <> '6' ", "status", "status");
                }

            }
        }
        private void gvResult_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (gvResult.SelectedCells.Count > 0)
                {
                    int selectedrowindex = gvResult.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = gvResult.Rows[selectedrowindex];
                    string _id = Convert.ToString(selectedRow.Cells["Id"].Value);
                    lblidStud.Text = _id;
                    DataSet ds = new DataSet();

                    string qry = "select FirstName,Assessment.Title,AssessmentComponent.Name,EvaluationDate,RubricLevel.details as Rubric from  Student INNER JOIN   StudentResult ON Student.Id = StudentResult.StudentId INNER JOIN RubricLevel ON StudentResult.RubricMeasurementId = RubricLevel.Id INNER JOIN AssessmentComponent ON StudentResult.AssessmentComponentId = AssessmentComponent.Id INNER JOIN Assessment ON AssessmentComponent.AssessmentId = Assessment.Id";

                    ds = ajdbClass.GetRecords("tbl", qry);

                    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //{
                    //    txtAName.Text = dr["Name"].ToString().Trim();
                    //    txtTMa.Text = dr["TotalMarks"].ToString().Trim();
                    //    //  txtTw.Text = dr["TotalWeightage"].ToString().Trim();


                    //}
                }

            }
        }
        // combo box events
        private void cmbClo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ajdbClass.popGrid(gvRub, "select id,details from Rubric where cloid='" + cmbClo.SelectedValue + "'");
                ajdbClass.popCmb(cmbRubrics, "select * from Rubric where CloId='" + cmbClo.SelectedValue + "'", "id", "Details");
                lblrubricLevels.Text = "Rubrics Level - Settings Clo:" + cmbClo.Text;
                ajdbClass.popGrid(gvRubSettings, "select id,details,MeasurementLevel from RubricLevel where RubricId='" + cmbRubrics.SelectedValue + "'");
                ajdbClass.popCmb(cmbRubrics, "select * from Rubric where CloId='" + cmbClo.SelectedValue + "'", "id", "Details");

            }
            catch (Exception ex) { }
            ajdbClass.popCmb(cmbRubrics, "select * from Rubric where CloId='" + cmbClo.SelectedValue + "'", "id", "Details");

        }
        private void cmbRubAsses_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
                 ajdbClass.popGrid(gvAssesComp, "select AssessmentComponent.Id, Name, Rubric.details as Rubric, RubricLevel." +
                 "Details as MeasurementLevel, TotalMarks from AssessmentComponent inner join Rubric on  AssessmentComponent.RubricId=Rubric.Id" +
                 "  inner join RubricLevel on Rubric.Id=RubricLevel.RubricId  where AssessmentComponent.RubricId = '" + cmbRubAsses.SelectedValue + "'");
            }
            catch (Exception ex) {
                //essageBox.Show("error occured"); 
            }
        }
        private void cmbAssesment_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {

                ajdbClass.popGrid(gvAssesComp, "select AssessmentComponent.Id, Name, Rubric.details as Rubric, RubricLevel." +
                "Details as MeasurementLevel, TotalMarks from AssessmentComponent inner join Rubric on  AssessmentComponent.RubricId=Rubric.Id" +
                "  inner join RubricLevel on Rubric.Id=RubricLevel.RubricId  where AssessmentComponent.AssessmentId = '" + cmbAssesment.SelectedValue + "'");
            }
            catch (Exception ex)
            {
             //MessageBox.Show("error occured");
            }
        }
        private void cmbAssesR_SelectedIndexChanged(object sender, EventArgs e)
        {
            ajdbClass.popCmb(cmbAssesR, "SELECT *  FROM  AssessmentComponent where AssessmentId='" + cmbAseesRE.SelectedValue + "'", "  AssessmentComponent.Id", "Name");
        }
        private void cmbAseesRE_SelectedIndexChanged(object sender, EventArgs e)
        {
            ajdbClass.popCmb(cmbAssesR, "SELECT *  FROM  AssessmentComponent where AssessmentComponent.Id='" + cmbAseesRE.SelectedValue + "'", "  AssessmentComponent.Id", "Name");
        }
       
    }

}


