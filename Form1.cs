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

namespace StudentEvaluationSystem
{
    //cmd = new SqlCommand("insert into Student(FirstName,LastName,Contact,Email,RegistrationNumber,Status) values(@p1,@p2,@p3,@p4,@p5,@p6)", con);
    //          cmd = new SqlCommand("insert into CLO(Name,DateCreated,DateUpdated) values(@c1,@c2,@c3)", con);
       //cmd = new SqlCommand("insert into Rubric(Id,Details,CloId) values(@c1,@c2)", con);
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=ProjectB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        //ID variable used in Updating and Deleting Record  
        int ID = 0; 
        public Form1()
        {
            InitializeComponent();
        }
          // Add students 
        
        private void btnRubric_Click(object sender, EventArgs e)
        {
            string _DataFields = "cloid,details";
            string _Values ="'" + cmbClo.SelectedValue + "','" + txtRubric.Text.Replace("'", "''") + "'";
            string Result = ajdbClass.InsertIntoDatabase("Rubric", _DataFields, _Values);
            ajdbClass.popGrid(gvRub, "select id,details from Rubric where cloid='" + cmbClo.SelectedValue + "'");
            txtRubric.Text = ""; 
           
            MessageBox.Show(Result);
         }

        private void tabPage2_Click(object sender, EventArgs e)
        {
         
         ////   string query = "Select  CloId from Rubric";
         //////   adapt = new SqlDataAdapter
         ////  // SqlDataAdapter da = new SqlDataAdapter(query,con);
         ////   con.Open();
         ////   DataSet ds = new DataSet();
         ////   da.Fill(ds, "Rubric");
         ////   cmbCloR.DisplayMember = "Name";
         ////   cmbCloR.ValueMember = "CloId";
         ////   cmbCloR.DataSource = ds.Tables["Rubric"];
         ////   con.Close();
         //   DataTable table = new DataTable();

         //       using (SqlDataAdapter da = new SqlDataAdapter())
         //       {
         //           da.SelectCommand.CommandText = @"Select Id, CloId from Rubric";
         //           da.Fill(table);
         //       }
            
         //   cmbCloR.DataSource = table.DefaultView;
         //   cmbCloR.DisplayMember = "CloId";
         //   cmbCloR.ValueMember = "Id";

        }

        DataClass ajdbClass = new DataClass();
        DataSet ds = new DataSet();

        private void Form1_Load(object sender, EventArgs e)
        {
            ajdbClass.popGrid(gvClo, "select id,name from clo");
            ajdbClass.popCmb(cmbClo, "select * from clo", "id", "name");
            ajdbClass.popCmb(cmbRubrics, "select * from Rubric where CloId='" + cmbClo.SelectedValue + "'", "id", "Details");
            ajdbClass.popGrid(gvRub, "select id,details from Rubric where cloid='" + cmbClo.SelectedValue + "'");
            ajdbClass.popGrid(gvRubSettings, "select id,details,MeasurementLevel from RubricLevel where RubricId='" + cmbRubrics.SelectedValue + "'"); 
        }
        private void popClo(DataGridView gv,string qry)
        {
            
        }


        private void btnAddCLO_Click(object sender, EventArgs e)
        {
            string _DataFields = "Name, datecreated,dateupdated";
            string _Values = "'" + txtCLO.Text.Replace("'", "''") + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "'";
            string Result = ajdbClass.InsertIntoDatabase("clo", _DataFields, _Values);
            ajdbClass.popGrid(gvClo, "select id,name from clo");
            ajdbClass.popCmb(cmbClo, "select * from clo", "id", "name");
        }

        

        private void gvClo_KeyDown(object sender, KeyEventArgs e)
        {
            

            if (e.KeyCode == Keys.Delete)
            {
                if (AskYesNo() == false) return;
                int selectedrowindex = gvClo.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = gvClo.Rows[selectedrowindex];
                string _id = Convert.ToString(selectedRow.Cells["id"].Value); 
                DataClass ajdbClass = new DataClass();
                string s = ajdbClass.DeleteFromDatabase("delete from clo where id='" + _id + "'");
                ajdbClass.popGrid(gvClo, "select id,name from clo");
                ajdbClass.popCmb(cmbClo, "select * from clo", "id", "name");
                lblrubricLevels.Text = "Rubrics Level - Settings Clo:" + cmbClo.Text;
            }
        }

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

        private void btnUpdateClo_Click(object sender, EventArgs e)
        {
            for (int rows = 0; rows < gvClo.Rows.Count - 1; rows++)
            {
                string _id = gvClo.Rows[rows].Cells[0].Value.ToString().Trim().Replace("'", "''");
                string _name = gvClo.Rows[rows].Cells[1].Value.ToString().Trim().Replace("'", "''");
                string _dateupdated = DateTime.Now.ToString();
                string s = ajdbClass.UpdateDatabase("update clo set name='" + _name + "',dateupdated='"+_dateupdated+"' where id='" + _id + "'");
                lblrubricLevels.Text = "Rubrics Level - Settings Clo:" + cmbClo.Text;
                MessageBox.Show(s);
            }
            ajdbClass.popGrid(gvClo, "select id,name from clo");
            ajdbClass.popCmb(cmbClo, "select * from clo", "id", "name");
         
        }

        private void cmbClo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ajdbClass.popGrid(gvRub, "select id,details from Rubric where cloid='" + cmbClo.SelectedValue + "'");
                ajdbClass.popCmb(cmbRubrics, "select * from Rubric where CloId='" + cmbClo.SelectedValue + "'", "id", "Details");
                lblrubricLevels.Text = "Rubrics Level - Settings Clo:" + cmbClo.Text;
                ajdbClass.popGrid(gvRubSettings, "select id,details,MeasurementLevel from RubricLevel where RubricId='" + cmbRubrics.SelectedValue + "'"); 
            }
            catch (Exception ex) { }
        }

        private void gvRub_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (AskYesNo() == false) return;
                int selectedrowindex = gvRub.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = gvRub.Rows[selectedrowindex];
                string _id = Convert.ToString(selectedRow.Cells["id"].Value);
                
                string s = ajdbClass.DeleteFromDatabase("delete from rubric where id='" + _id + "'");
                ajdbClass.popGrid(gvRub, "select id,details from Rubric where cloid='" + cmbClo.SelectedValue + "'");
                ajdbClass.popGrid(gvRubSettings, "select id,details,MeasurementLevel from RubricLevel where RubricId='" + cmbRubrics.SelectedValue + "'"); txtRubricSetDetails.Text = ""; txtRubricSetLevel.Text = "";    
                
            }
        }

        private void btnUpdateRubric_Click(object sender, EventArgs e)
        {
            for (int rows = 0; rows < gvRub.Rows.Count - 1; rows++)
            {
                string _id = gvRub.Rows[rows].Cells[0].Value.ToString().Trim().Replace("'", "''");
                string _details = gvRub.Rows[rows].Cells[1].Value.ToString().Trim().Replace("'", "''");
                string s = ajdbClass.UpdateDatabase("update Rubric set details='" + _details + "' where id='" + _id + "'");

            }
            
        }

        private void btnAddRubricLevel_Click(object sender, EventArgs e)
        {
            string _DataFields = "RubricId,details,MeasurementLevel";
            string _Values = "'" + cmbRubrics.SelectedValue + "','" + txtRubricSetDetails.Text.Replace("'", "''") + "','" + txtRubricSetLevel.Text.Replace("'","''") + "'";
            string Result = ajdbClass.InsertIntoDatabase("RubricLevel", _DataFields, _Values);
            ajdbClass.popGrid(gvRubSettings, "select id,details,MeasurementLevel from RubricLevel where RubricId='" + cmbRubrics.SelectedValue + "'"); txtRubricSetDetails.Text = ""; txtRubricSetLevel.Text = "";
            MessageBox.Show(Result);
        }

        private void cmbRubrics_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ajdbClass.popGrid(gvRubSettings, "select id,details,MeasurementLevel from RubricLevel where RubricId='" + cmbRubrics.SelectedValue + "'"); txtRubricSetDetails.Text = ""; txtRubricSetLevel.Text = "";
            }
            catch (Exception ex) { }
        }

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

        private void btnUpdateRubricLevel_Click(object sender, EventArgs e)
        {
            for (int rows = 0; rows < gvRubSettings.Rows.Count - 1; rows++)
            {
                string _id = gvRubSettings.Rows[rows].Cells[0].Value.ToString().Trim().Replace("'", "''");
                string _details = gvRubSettings.Rows[rows].Cells[1].Value.ToString().Trim().Replace("'", "''");
                string _mlevel = gvRubSettings.Rows[rows].Cells[2].Value.ToString().Trim().Replace("'", "''");
                string s = ajdbClass.UpdateDatabase("update RubricLevel set details='" + _details + "',MeasurementLevel='"+ _mlevel+"' where id='" + _id + "'");

            }
            ajdbClass.popGrid(gvRubSettings, "select id,details,MeasurementLevel from RubricLevel where RubricId='" + cmbRubrics.SelectedValue + "'"); txtRubricSetDetails.Text = ""; txtRubricSetLevel.Text = "";
       
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }

}
