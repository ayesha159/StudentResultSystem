using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace StudentEvaluationSystem
{
    public partial class AssesmentReport : Form
    {
        public AssesmentReport()
        {
            InitializeComponent();
        }
        AJ_DataClass ajdbClass = new AJ_DataClass();


        
    private void AssesmentReport_Load(object sender, EventArgs e)
        {
            try { 
            ajdbClass.popGrid(gvAssesRep, "select FirstName,Assessment.Title,AssessmentComponent.Name,EvaluationDate,RubricLevel.details as Rubric from  Student INNER JOIN   StudentResult ON Student.Id = StudentResult.StudentId INNER JOIN RubricLevel ON StudentResult.RubricMeasurementId = RubricLevel.Id INNER JOIN AssessmentComponent ON StudentResult.AssessmentComponentId = AssessmentComponent.Id INNER JOIN Assessment ON AssessmentComponent.AssessmentId = Assessment.Id");

           // ajdbClass.popGrid(gvAssesRep, qry);

                PdfPTable pdfTable = new PdfPTable(gvAssesRep.ColumnCount);

                //set overall width

                pdfTable.DefaultCell.Padding = 5;
                pdfTable.WidthPercentage = 60;
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfTable.DefaultCell.BorderWidth = 1;

                pdfTable.WidthPercentage = 90f;
                //set column widths
              //  int[] firstTablecellwidth = { 20, 25, 25,25};
             //   pdfTable.SetWidths(firstTablecellwidth);
                //Adding Header row
                foreach (DataGridViewColumn column in gvAssesRep.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    pdfTable.AddCell(cell);

                }

                int row = gvAssesRep.Rows.Count;
                int cell2 = gvAssesRep.Rows[0].Cells.Count;
                for (int i = 0; i < row - 1; i++)
                {
                    for (int j = 0; j < cell2; j++)
                    {
                        if (gvAssesRep.Rows[i].Cells[j].Value == null)
                        {
                            gvAssesRep.Rows[i].Cells[j].Value = "null";
                        }
                        pdfTable.AddCell(gvAssesRep.Rows[i].Cells[j].Value.ToString());
                        this.gvAssesRep.Columns[0].Width = 100;
                    }
                }

                //Exporting to PDF
                string folderPath = @"e:\";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (FileStream stream = new FileStream(folderPath + "AssesmentReport.pdf", FileMode.Create))
                {

                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();                 
                    pdfDoc.Add(new Phrase("\t\t\t Assesment Report\n"));
                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();
                    stream.Close();
                }

        }
            catch(Exception ex)
            {
             //   MessageBox.Show("error");
            }

}
 

 
    }
}
