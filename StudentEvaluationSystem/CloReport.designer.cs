﻿namespace StudentEvaluationSystem
{
    partial class CloReport
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
            this.label36 = new System.Windows.Forms.Label();
            this.gvClo = new System.Windows.Forms.DataGridView();
            this.label35 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvClo)).BeginInit();
            this.SuspendLayout();
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Segoe MDL2 Assets", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.Black;
            this.label36.Location = new System.Drawing.Point(192, 9);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(169, 21);
            this.label36.TabIndex = 8;
            this.label36.Text = "CLO WISE REPORT";
            // 
            // gvClo
            // 
            this.gvClo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvClo.Location = new System.Drawing.Point(32, 56);
            this.gvClo.Name = "gvClo";
            this.gvClo.Size = new System.Drawing.Size(498, 233);
            this.gvClo.TabIndex = 7;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.BackColor = System.Drawing.Color.DodgerBlue;
            this.label35.Font = new System.Drawing.Font("Segoe Print", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(163, 312);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(234, 23);
            this.label35.TabIndex = 26;
            this.label35.Text = "Report formed in local disk E:\\ !!!";
            // 
            // CloReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 344);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.gvClo);
            this.Name = "CloReport";
            this.Text = "CloReport";
            this.Load += new System.EventHandler(this.CloReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvClo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.DataGridView gvClo;
        private System.Windows.Forms.Label label35;
    }
}