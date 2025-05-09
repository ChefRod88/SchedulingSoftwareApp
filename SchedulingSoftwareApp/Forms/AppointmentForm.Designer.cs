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
            this.cmbCustomerName = new System.Windows.Forms.ComboBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.btnSaveAppointment = new System.Windows.Forms.Button();
            this.btnCancelAppointment = new System.Windows.Forms.Button();
            this.txtEmail = new System.Windows.Forms.Label();
            this.cmbAppointmentTime = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpAppointmentDay = new System.Windows.Forms.DateTimePicker();
            this.dgvAppointments = new System.Windows.Forms.DataGridView();
            this.btnDeleteAppointment = new System.Windows.Forms.Button();
            this.btnCalendarView = new System.Windows.Forms.Button();
            this.btnAppointmentReports = new System.Windows.Forms.Button();
            this.btnUpdateAppointment = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Location = new System.Drawing.Point(33, 32);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(107, 16);
            this.lblCustomerName.TabIndex = 0;
            this.lblCustomerName.Text = "Customer Name:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(39, 156);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(78, 16);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 327);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Type:";
            // 
            // cmbCustomerName
            // 
            this.cmbCustomerName.FormattingEnabled = true;
            this.cmbCustomerName.Location = new System.Drawing.Point(219, 39);
            this.cmbCustomerName.Name = "cmbCustomerName";
            this.cmbCustomerName.Size = new System.Drawing.Size(284, 24);
            this.cmbCustomerName.TabIndex = 8;
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(219, 317);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(284, 24);
            this.cmbType.TabIndex = 9;
            // 
            // btnSaveAppointment
            // 
            this.btnSaveAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveAppointment.Location = new System.Drawing.Point(642, 124);
            this.btnSaveAppointment.Name = "btnSaveAppointment";
            this.btnSaveAppointment.Size = new System.Drawing.Size(170, 36);
            this.btnSaveAppointment.TabIndex = 16;
            this.btnSaveAppointment.Text = "Add Appointment ";
            this.btnSaveAppointment.UseVisualStyleBackColor = true;
            this.btnSaveAppointment.Click += new System.EventHandler(this.btnSaveAppointment_Click);
            // 
            // btnCancelAppointment
            // 
            this.btnCancelAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelAppointment.Location = new System.Drawing.Point(736, 811);
            this.btnCancelAppointment.Name = "btnCancelAppointment";
            this.btnCancelAppointment.Size = new System.Drawing.Size(170, 36);
            this.btnCancelAppointment.TabIndex = 17;
            this.btnCancelAppointment.Text = "Cancel";
            this.btnCancelAppointment.UseVisualStyleBackColor = true;
            this.btnCancelAppointment.Click += new System.EventHandler(this.btnCancelAppointment_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.AutoSize = true;
            this.txtEmail.Location = new System.Drawing.Point(39, 388);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(131, 16);
            this.txtEmail.TabIndex = 18;
            this.txtEmail.Text = "Date of Appointment:";
            // 
            // cmbAppointmentTime
            // 
            this.cmbAppointmentTime.FormattingEnabled = true;
            this.cmbAppointmentTime.Location = new System.Drawing.Point(219, 450);
            this.cmbAppointmentTime.Name = "cmbAppointmentTime";
            this.cmbAppointmentTime.Size = new System.Drawing.Size(284, 24);
            this.cmbAppointmentTime.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 454);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "Time of Appointment:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "Title:";
            // 
            // txtTitle
            // 
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Location = new System.Drawing.Point(219, 96);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(284, 22);
            this.txtTitle.TabIndex = 23;
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Location = new System.Drawing.Point(219, 148);
            this.txtDescription.MaximumSize = new System.Drawing.Size(1000, 200);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(284, 22);
            this.txtDescription.TabIndex = 24;
            // 
            // txtContact
            // 
            this.txtContact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContact.Location = new System.Drawing.Point(219, 261);
            this.txtContact.MaximumSize = new System.Drawing.Size(1000, 200);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(284, 22);
            this.txtContact.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 261);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 16);
            this.label3.TabIndex = 25;
            this.label3.Text = "Contact:";
            // 
            // txtLocation
            // 
            this.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocation.Location = new System.Drawing.Point(219, 197);
            this.txtLocation.MaximumSize = new System.Drawing.Size(1000, 200);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(284, 22);
            this.txtLocation.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 27;
            this.label4.Text = "Location:";
            // 
            // dtpAppointmentDay
            // 
            this.dtpAppointmentDay.Location = new System.Drawing.Point(219, 380);
            this.dtpAppointmentDay.Name = "dtpAppointmentDay";
            this.dtpAppointmentDay.Size = new System.Drawing.Size(283, 22);
            this.dtpAppointmentDay.TabIndex = 29;
            // 
            // dgvAppointments
            // 
            this.dgvAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointments.Location = new System.Drawing.Point(12, 521);
            this.dgvAppointments.Name = "dgvAppointments";
            this.dgvAppointments.RowHeadersWidth = 51;
            this.dgvAppointments.RowTemplate.Height = 24;
            this.dgvAppointments.Size = new System.Drawing.Size(905, 234);
            this.dgvAppointments.TabIndex = 30;
            // 
            // btnDeleteAppointment
            // 
            this.btnDeleteAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAppointment.Location = new System.Drawing.Point(642, 289);
            this.btnDeleteAppointment.Name = "btnDeleteAppointment";
            this.btnDeleteAppointment.Size = new System.Drawing.Size(170, 36);
            this.btnDeleteAppointment.TabIndex = 31;
            this.btnDeleteAppointment.Text = "Delete Appointment";
            this.btnDeleteAppointment.UseVisualStyleBackColor = true;
            this.btnDeleteAppointment.Click += new System.EventHandler(this.btnDeleteAppointment_Click);
            // 
            // btnCalendarView
            // 
            this.btnCalendarView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalendarView.Location = new System.Drawing.Point(12, 811);
            this.btnCalendarView.Name = "btnCalendarView";
            this.btnCalendarView.Size = new System.Drawing.Size(170, 36);
            this.btnCalendarView.TabIndex = 32;
            this.btnCalendarView.Text = "Calender View";
            this.btnCalendarView.UseVisualStyleBackColor = true;
            this.btnCalendarView.Click += new System.EventHandler(this.btnCalendarView_Click);
            // 
            // btnAppointmentReports
            // 
            this.btnAppointmentReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppointmentReports.Location = new System.Drawing.Point(373, 811);
            this.btnAppointmentReports.Name = "btnAppointmentReports";
            this.btnAppointmentReports.Size = new System.Drawing.Size(170, 36);
            this.btnAppointmentReports.TabIndex = 33;
            this.btnAppointmentReports.Text = "Apppointment Reports";
            this.btnAppointmentReports.UseVisualStyleBackColor = true;
            this.btnAppointmentReports.Click += new System.EventHandler(this.btnAppointmentReports_Click);
            // 
            // btnUpdateAppointment
            // 
            this.btnUpdateAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateAppointment.Location = new System.Drawing.Point(642, 204);
            this.btnUpdateAppointment.Name = "btnUpdateAppointment";
            this.btnUpdateAppointment.Size = new System.Drawing.Size(170, 36);
            this.btnUpdateAppointment.TabIndex = 34;
            this.btnUpdateAppointment.Text = "Update Appointment ";
            this.btnUpdateAppointment.UseVisualStyleBackColor = true;
            this.btnUpdateAppointment.Click += new System.EventHandler(this.btnUpdateAppointment_Click);
            // 
            // AppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 859);
            this.Controls.Add(this.btnUpdateAppointment);
            this.Controls.Add(this.btnAppointmentReports);
            this.Controls.Add(this.btnCalendarView);
            this.Controls.Add(this.btnDeleteAppointment);
            this.Controls.Add(this.dgvAppointments);
            this.Controls.Add(this.dtpAppointmentDay);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtContact);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbAppointmentTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnCancelAppointment);
            this.Controls.Add(this.btnSaveAppointment);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.cmbCustomerName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblCustomerName);
            this.Name = "AppointmentForm";
            this.Text = "Appointment Form";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCustomerName;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Button btnSaveAppointment;
        private System.Windows.Forms.Button btnCancelAppointment;
        private System.Windows.Forms.Label txtEmail;
        private System.Windows.Forms.ComboBox cmbAppointmentTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpAppointmentDay;
        private System.Windows.Forms.DataGridView dgvAppointments;
        private System.Windows.Forms.Button btnDeleteAppointment;
        private System.Windows.Forms.Button btnCalendarView;
        private System.Windows.Forms.Button btnAppointmentReports;
        private System.Windows.Forms.Button btnUpdateAppointment;
    }
}