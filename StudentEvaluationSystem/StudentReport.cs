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
    public partial class StudentReport : Form
    {
        public StudentReport()
        {
            InitializeComponent();
        }
        AJ_DataClass ajdbClass = new AJ_DataClass();
        private void StudentReport_Load(object sender, EventArgs e)

        {
            string qry = "SELECT Student.Id, Student.FirstName AS StudentName, Student.Contact, Student.Email, Student.RegistrationNumber as RegNo, Lookup.Name as Status " +
            " FROM Student INNER JOIN " + " Lookup ON Student.Status = Lookup.LookupId ";

            ajdbClass.popGrid(gvStudRep, qry);
           
            PdfPTable pdfTable = new PdfPTable(gvStudRep.ColumnCount);
            pdfTable.DefaultCell.Padding = 5;
            pdfTable.WidthPercentage = 60;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;

            //set overall width
            pdfTable.WidthPercentage = 90f;
            //set column widths
            int[] firstTablecellwidth = { 20, 25, 25, 32 ,25,25};
            pdfTable.SetWidths(firstTablecellwidth);
            //Adding Header row
            foreach (DataGridViewColumn column in gvStudRep.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                pdfTable.AddCell(cell);
               
            }

            int row = gvStudRep.Rows.Count;
            int cell2 = gvStudRep.Rows[1].Cells.Count;
            for (int i = 0; i < row - 1; i++)
            {
                for (int j = 0; j < cell2; j++)
                {
                    if (gvStudRep.Rows[i].Cells[j].Value == null)
                    {
                        gvStudRep.Rows[i].Cells[j].Value = "null";
                    }
                    pdfTable.AddCell(gvStudRep.Rows[i].Cells[j].Value.ToString());
                    this.gvStudRep.Columns[1].Width = 100;
                }
            }

            //Exporting to PDF
            string folderPath = @"e:\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (FileStream stream = new FileStream(folderPath + "StudentReport1.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(new Phrase("\t\t\t STUDENT REPORT\n"));
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
            }
           

        }
    }
}
