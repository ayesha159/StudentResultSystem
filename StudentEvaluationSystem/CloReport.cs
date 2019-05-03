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
    public partial class CloReport : Form
    {
        public CloReport()
        {
            InitializeComponent();
        }
        AJ_DataClass ajdbClass = new AJ_DataClass();
        private void CloReport_Load(object sender, EventArgs e)
        {

            string qry = "SELECT        Student.FirstName, Clo.Name, Rubric.Details AS Rubric, RubricLevel.Details AS [Level] FROM Student INNER JOIN StudentResult ON Student.Id = StudentResult.StudentId INNER JOIN" +
                       "  RubricLevel ON StudentResult.RubricMeasurementId = RubricLevel.Id INNER JOIN" +
                       "   Rubric ON RubricLevel.RubricId = Rubric.Id INNER JOIN" +
                       "  Clo ON Rubric.CloId = Clo.Id";
            ajdbClass.popGrid(gvClo, qry);
            //Document doc = new Document(PageSize.A4, 7f, 5f, 5f, 0f);
            //iTextSharp.text.Font mainFont = FontFactory.GetFont("Segoe UI", 22, new iTextSharp.text.BaseColor(System.Drawing.ColorTranslator.FromHtml("#999")));
            //Phrase mainPharse = new Phrase();
            
            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(gvClo.ColumnCount);
            pdfTable.DefaultCell.Padding = 5;
            pdfTable.WidthPercentage = 90;
            int[] firstTablecellwidth = { 30, 25, 32, 25 };
            pdfTable.SetWidths(firstTablecellwidth);
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;
      
            //Adding Header row
            foreach (DataGridViewColumn column in gvClo.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                pdfTable.AddCell(cell);
            }

            //Adding DataRow
            //foreach (DataGridViewRow row in gvClo.Rows)
            //{
            //    foreach (DataGridViewCell cell in row.Cells)
            //    {
            //        pdfTable.AddCell(cell.Value.ToString());
            //    }
            //}
            int row = gvClo.Rows.Count;
            int cell2 = gvClo.Rows[1].Cells.Count;
            for (int i = 0; i < row - 1; i++)
            {
                for (int j = 0; j < cell2; j++)
                {
                    if (gvClo.Rows[i].Cells[j].Value == null)
                    {
                        //return directly
                        //return;
                        //or set a value for the empty data
                        gvClo.Rows[i].Cells[j].Value = "null";
                    }
                    pdfTable.AddCell(gvClo.Rows[i].Cells[j].Value.ToString());
                }
            }

            //Exporting to PDF
            try
            {
                string folderPath = @"e:\";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (FileStream stream = new FileStream(folderPath + "CloReport.pdf", FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase("\t\t\t CLO Report\n"));
                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();
                    stream.Close();
                }
            }
            catch
            {

            }
           
        }

        private void gvClo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
