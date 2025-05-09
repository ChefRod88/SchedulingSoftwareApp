namespace SchedulingSoftwareApp.Forms
{
    partial class ReportsForm
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
            this.dgvTypesByMonth = new System.Windows.Forms.DataGridView();
            this.dgvUserSchedules = new System.Windows.Forms.DataGridView();
            this.dgvCustomerSummary = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTypesByMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserSchedules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTypesByMonth
            // 
            this.dgvTypesByMonth.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTypesByMonth.Location = new System.Drawing.Point(21, 122);
            this.dgvTypesByMonth.Name = "dgvTypesByMonth";
            this.dgvTypesByMonth.RowHeadersWidth = 51;
            this.dgvTypesByMonth.RowTemplate.Height = 24;
            this.dgvTypesByMonth.Size = new System.Drawing.Size(454, 711);
            this.dgvTypesByMonth.TabIndex = 0;
            // 
            // dgvUserSchedules
            // 
            this.dgvUserSchedules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserSchedules.Location = new System.Drawing.Point(533, 122);
            this.dgvUserSchedules.Name = "dgvUserSchedules";
            this.dgvUserSchedules.RowHeadersWidth = 51;
            this.dgvUserSchedules.RowTemplate.Height = 24;
            this.dgvUserSchedules.Size = new System.Drawing.Size(519, 711);
            this.dgvUserSchedules.TabIndex = 1;
            // 
            // dgvCustomerSummary
            // 
            this.dgvCustomerSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomerSummary.Location = new System.Drawing.Point(1111, 122);
            this.dgvCustomerSummary.Name = "dgvCustomerSummary";
            this.dgvCustomerSummary.RowHeadersWidth = 51;
            this.dgvCustomerSummary.RowTemplate.Height = 24;
            this.dgvCustomerSummary.Size = new System.Drawing.Size(519, 711);
            this.dgvCustomerSummary.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Appointment Types by Month:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(566, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "User Schedules:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1106, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(314, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Customer Appointment Counts:";
            // 
            // btnBack
            // 
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Location = new System.Drawing.Point(732, 871);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(174, 35);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1691, 932);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvCustomerSummary);
            this.Controls.Add(this.dgvUserSchedules);
            this.Controls.Add(this.dgvTypesByMonth);
            this.Name = "ReportsForm";
            this.Text = "Reports Form";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTypesByMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserSchedules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerSummary)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTypesByMonth;
        private System.Windows.Forms.DataGridView dgvUserSchedules;
        private System.Windows.Forms.DataGridView dgvCustomerSummary;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBack;
    }
}