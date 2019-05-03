namespace StudentEvaluationSystem
{
    partial class StudentReport
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
            this.gvStudRep = new System.Windows.Forms.DataGridView();
            this.label38 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvStudRep)).BeginInit();
            this.SuspendLayout();
            // 
            // gvStudRep
            // 
            this.gvStudRep.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvStudRep.Location = new System.Drawing.Point(26, 77);
            this.gvStudRep.Name = "gvStudRep";
            this.gvStudRep.Size = new System.Drawing.Size(591, 225);
            this.gvStudRep.TabIndex = 8;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Segoe Print", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(234, 22);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(183, 37);
            this.label38.TabIndex = 7;
            this.label38.Text = "Student Report";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.BackColor = System.Drawing.Color.DodgerBlue;
            this.label35.Font = new System.Drawing.Font("Segoe Print", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(183, 319);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(234, 23);
            this.label35.TabIndex = 24;
            this.label35.Text = "Report formed in local disk E:\\ !!!";
            // 
            // StudentReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 372);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.gvStudRep);
            this.Controls.Add(this.label38);
            this.Name = "StudentReport";
            this.Text = "StudentReport";
            this.Load += new System.EventHandler(this.StudentReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvStudRep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvStudRep;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label35;
    }
}