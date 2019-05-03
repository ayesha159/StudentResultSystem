namespace StudentEvaluationSystem
{
    partial class AssesmentReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label38 = new System.Windows.Forms.Label();
            this.gvAssesRep = new System.Windows.Forms.DataGridView();
            this.label35 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssesRep)).BeginInit();
            this.SuspendLayout();
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Segoe Print", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(209, 21);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(208, 37);
            this.label38.TabIndex = 4;
            this.label38.Text = "Assesment Report";
            // 
            // gvAssesRep
            // 
            this.gvAssesRep.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvAssesRep.Location = new System.Drawing.Point(38, 72);
            this.gvAssesRep.Name = "gvAssesRep";
            this.gvAssesRep.Size = new System.Drawing.Size(595, 225);
            this.gvAssesRep.TabIndex = 6;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.BackColor = System.Drawing.Color.DodgerBlue;
            this.label35.Font = new System.Drawing.Font("Segoe Print", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(212, 314);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(234, 23);
            this.label35.TabIndex = 25;
            this.label35.Text = "Report formed in local disk E:\\ !!!";
            // 
            // AssesmentReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 346);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.gvAssesRep);
            this.Controls.Add(this.label38);
            this.Name = "AssesmentReport";
            this.Text = "AssesmentReport";
            this.Load += new System.EventHandler(this.AssesmentReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvAssesRep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.DataGridView gvAssesRep;
        private System.Windows.Forms.Label label35;
    }
}