namespace SchedulingSoftwareApp.Forms
{
    partial class AppointmentForm
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
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.cmbCustomerName = new System.Windows.Forms.ComboBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.dtpAppointmentDay = new System.Windows.Forms.DateTimePicker();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnSaveAppointment = new System.Windows.Forms.Button();
            this.btnCancelAppointment = new System.Windows.Forms.Button();
            this.txtEmaill = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.Label();
            this.cmbAppointmentTime = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Location = new System.Drawing.Point(23, 29);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(134, 20);
            this.lblCustomerName.TabIndex = 0;
            this.lblCustomerName.Text = "Customer Name:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(23, 126);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(98, 20);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Type:";
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(23, 232);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(82, 16);
            this.lblStart.TabIndex = 6;
            this.lblStart.Text = "Day Of Appt:";
            // 
            // cmbCustomerName
            // 
            this.cmbCustomerName.FormattingEnabled = true;
            this.cmbCustomerName.Location = new System.Drawing.Point(167, 29);
            this.cmbCustomerName.Name = "cmbCustomerName";
            this.cmbCustomerName.Size = new System.Drawing.Size(284, 24);
            this.cmbCustomerName.TabIndex = 8;
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(167, 74);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(284, 24);
            this.cmbType.TabIndex = 9;
            // 
            // dtpAppointmentDay
            // 
            this.dtpAppointmentDay.Location = new System.Drawing.Point(167, 232);
            this.dtpAppointmentDay.Name = "dtpAppointmentDay";
            this.dtpAppointmentDay.Size = new System.Drawing.Size(284, 22);
            this.dtpAppointmentDay.TabIndex = 10;
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Location = new System.Drawing.Point(167, 126);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(284, 22);
            this.txtDescription.TabIndex = 14;
            // 
            // btnSaveAppointment
            // 
            this.btnSaveAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveAppointment.Location = new System.Drawing.Point(26, 348);
            this.btnSaveAppointment.Name = "btnSaveAppointment";
            this.btnSaveAppointment.Size = new System.Drawing.Size(425, 36);
            this.btnSaveAppointment.TabIndex = 16;
            this.btnSaveAppointment.Text = "Save Appointment ";
            this.btnSaveAppointment.UseVisualStyleBackColor = true;
            this.btnSaveAppointment.Click += new System.EventHandler(this.btnSaveAppointment_Click);
            // 
            // btnCancelAppointment
            // 
            this.btnCancelAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelAppointment.Location = new System.Drawing.Point(26, 413);
            this.btnCancelAppointment.Name = "btnCancelAppointment";
            this.btnCancelAppointment.Size = new System.Drawing.Size(425, 36);
            this.btnCancelAppointment.TabIndex = 17;
            this.btnCancelAppointment.Text = "Cancel Appointment";
            this.btnCancelAppointment.UseVisualStyleBackColor = true;
            this.btnCancelAppointment.Click += new System.EventHandler(this.btnCancelAppointment_Click);
            // 
            // txtEmaill
            // 
            this.txtEmaill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmaill.Location = new System.Drawing.Point(167, 176);
            this.txtEmaill.Name = "txtEmaill";
            this.txtEmaill.Size = new System.Drawing.Size(284, 22);
            this.txtEmaill.TabIndex = 19;
            // 
            // txtEmail
            // 
            this.txtEmail.AutoSize = true;
            this.txtEmail.Location = new System.Drawing.Point(23, 176);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(44, 16);
            this.txtEmail.TabIndex = 18;
            this.txtEmail.Text = "Email:";
            // 
            // cmbAppointmentTime
            // 
            this.cmbAppointmentTime.FormattingEnabled = true;
            this.cmbAppointmentTime.Location = new System.Drawing.Point(167, 284);
            this.cmbAppointmentTime.Name = "cmbAppointmentTime";
            this.cmbAppointmentTime.Size = new System.Drawing.Size(284, 24);
            this.cmbAppointmentTime.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 284);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "Appointment Time:";
            // 
            // AppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 837);
            this.Controls.Add(this.cmbAppointmentTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEmaill);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnCancelAppointment);
            this.Controls.Add(this.btnSaveAppointment);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.dtpAppointmentDay);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.cmbCustomerName);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblCustomerName);
            this.Name = "AppointmentForm";
            this.Text = "Appointment Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.ComboBox cmbCustomerName;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.DateTimePicker dtpAppointmentDay;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnSaveAppointment;
        private System.Windows.Forms.Button btnCancelAppointment;
        private System.Windows.Forms.TextBox txtEmaill;
        private System.Windows.Forms.Label txtEmail;
        private System.Windows.Forms.ComboBox cmbAppointmentTime;
        private System.Windows.Forms.Label label1;
    }
}