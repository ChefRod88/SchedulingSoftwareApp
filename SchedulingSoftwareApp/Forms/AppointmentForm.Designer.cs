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
            this.txtEmail = new System.Windows.Forms.Label();
            this.cmbTimeSlots = new System.Windows.Forms.ComboBox();
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
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoET = new System.Windows.Forms.RadioButton();
            this.rdoCT = new System.Windows.Forms.RadioButton();
            this.rdoMT = new System.Windows.Forms.RadioButton();
            this.rdoPT = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Location = new System.Drawing.Point(25, 26);
            this.lblCustomerName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(85, 13);
            this.lblCustomerName.TabIndex = 0;
            this.lblCustomerName.Text = "Customer Name:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(29, 127);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 266);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Type:";
            // 
            // cmbCustomerName
            // 
            this.cmbCustomerName.FormattingEnabled = true;
            this.cmbCustomerName.Location = new System.Drawing.Point(164, 32);
            this.cmbCustomerName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCustomerName.Name = "cmbCustomerName";
            this.cmbCustomerName.Size = new System.Drawing.Size(214, 21);
            this.cmbCustomerName.TabIndex = 8;
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(164, 258);
            this.cmbType.Margin = new System.Windows.Forms.Padding(2);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(214, 21);
            this.cmbType.TabIndex = 9;
            // 
            // btnSaveAppointment
            // 
            this.btnSaveAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveAppointment.Location = new System.Drawing.Point(482, 101);
            this.btnSaveAppointment.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveAppointment.Name = "btnSaveAppointment";
            this.btnSaveAppointment.Size = new System.Drawing.Size(128, 29);
            this.btnSaveAppointment.TabIndex = 16;
            this.btnSaveAppointment.Text = "Add/Save Appointment ";
            this.btnSaveAppointment.UseVisualStyleBackColor = true;
            this.btnSaveAppointment.Click += new System.EventHandler(this.btnSaveAppointment_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.AutoSize = true;
            this.txtEmail.Location = new System.Drawing.Point(29, 315);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(107, 13);
            this.txtEmail.TabIndex = 18;
            this.txtEmail.Text = "Date of Appointment:";
            // 
            // cmbTimeSlots
            // 
            this.cmbTimeSlots.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimeSlots.FormattingEnabled = true;
            this.cmbTimeSlots.Location = new System.Drawing.Point(164, 366);
            this.cmbTimeSlots.Margin = new System.Windows.Forms.Padding(2);
            this.cmbTimeSlots.Name = "cmbTimeSlots";
            this.cmbTimeSlots.Size = new System.Drawing.Size(214, 21);
            this.cmbTimeSlots.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 369);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Time of Appointment:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Title:";
            // 
            // txtTitle
            // 
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Location = new System.Drawing.Point(164, 78);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(2);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(214, 20);
            this.txtTitle.TabIndex = 23;
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Location = new System.Drawing.Point(164, 120);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescription.MaximumSize = new System.Drawing.Size(750, 200);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(214, 20);
            this.txtDescription.TabIndex = 24;
            // 
            // txtContact
            // 
            this.txtContact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContact.Location = new System.Drawing.Point(164, 212);
            this.txtContact.Margin = new System.Windows.Forms.Padding(2);
            this.txtContact.MaximumSize = new System.Drawing.Size(750, 200);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(214, 20);
            this.txtContact.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 212);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Contact:";
            // 
            // txtLocation
            // 
            this.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocation.Location = new System.Drawing.Point(164, 160);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(2);
            this.txtLocation.MaximumSize = new System.Drawing.Size(750, 200);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(214, 20);
            this.txtLocation.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 166);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Location:";
            // 
            // dtpAppointmentDay
            // 
            this.dtpAppointmentDay.Location = new System.Drawing.Point(164, 309);
            this.dtpAppointmentDay.Margin = new System.Windows.Forms.Padding(2);
            this.dtpAppointmentDay.Name = "dtpAppointmentDay";
            this.dtpAppointmentDay.Size = new System.Drawing.Size(213, 20);
            this.dtpAppointmentDay.TabIndex = 29;
            // 
            // dgvAppointments
            // 
            this.dgvAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointments.Location = new System.Drawing.Point(9, 423);
            this.dgvAppointments.Margin = new System.Windows.Forms.Padding(2);
            this.dgvAppointments.Name = "dgvAppointments";
            this.dgvAppointments.RowHeadersWidth = 51;
            this.dgvAppointments.RowTemplate.Height = 24;
            this.dgvAppointments.Size = new System.Drawing.Size(679, 190);
            this.dgvAppointments.TabIndex = 30;
            // 
            // btnDeleteAppointment
            // 
            this.btnDeleteAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAppointment.Location = new System.Drawing.Point(482, 235);
            this.btnDeleteAppointment.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteAppointment.Name = "btnDeleteAppointment";
            this.btnDeleteAppointment.Size = new System.Drawing.Size(128, 29);
            this.btnDeleteAppointment.TabIndex = 31;
            this.btnDeleteAppointment.Text = "Delete Appointment";
            this.btnDeleteAppointment.UseVisualStyleBackColor = true;
            this.btnDeleteAppointment.Click += new System.EventHandler(this.btnDeleteAppointment_Click);
            // 
            // btnCalendarView
            // 
            this.btnCalendarView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalendarView.Location = new System.Drawing.Point(9, 639);
            this.btnCalendarView.Margin = new System.Windows.Forms.Padding(2);
            this.btnCalendarView.Name = "btnCalendarView";
            this.btnCalendarView.Size = new System.Drawing.Size(128, 29);
            this.btnCalendarView.TabIndex = 32;
            this.btnCalendarView.Text = "Calender View";
            this.btnCalendarView.UseVisualStyleBackColor = true;
            this.btnCalendarView.Click += new System.EventHandler(this.btnCalendarView_Click);
            // 
            // btnAppointmentReports
            // 
            this.btnAppointmentReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppointmentReports.Location = new System.Drawing.Point(178, 639);
            this.btnAppointmentReports.Margin = new System.Windows.Forms.Padding(2);
            this.btnAppointmentReports.Name = "btnAppointmentReports";
            this.btnAppointmentReports.Size = new System.Drawing.Size(128, 29);
            this.btnAppointmentReports.TabIndex = 33;
            this.btnAppointmentReports.Text = "Apppointment Reports";
            this.btnAppointmentReports.UseVisualStyleBackColor = true;
            this.btnAppointmentReports.Click += new System.EventHandler(this.btnAppointmentReports_Click);
            // 
            // btnUpdateAppointment
            // 
            this.btnUpdateAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateAppointment.Location = new System.Drawing.Point(482, 166);
            this.btnUpdateAppointment.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateAppointment.Name = "btnUpdateAppointment";
            this.btnUpdateAppointment.Size = new System.Drawing.Size(128, 29);
            this.btnUpdateAppointment.TabIndex = 34;
            this.btnUpdateAppointment.Text = "Update Appointment ";
            this.btnUpdateAppointment.UseVisualStyleBackColor = true;
            this.btnUpdateAppointment.Click += new System.EventHandler(this.btnUpdateAppointment_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(620, 10);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(68, 29);
            this.btnLogout.TabIndex = 35;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(620, 44);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(68, 29);
            this.btnBack.TabIndex = 36;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoET);
            this.groupBox1.Controls.Add(this.rdoCT);
            this.groupBox1.Controls.Add(this.rdoMT);
            this.groupBox1.Controls.Add(this.rdoPT);
            this.groupBox1.Location = new System.Drawing.Point(390, 284);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 116);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Time Zone:";
            // 
            // rdoET
            // 
            this.rdoET.AutoSize = true;
            this.rdoET.Location = new System.Drawing.Point(7, 87);
            this.rdoET.Name = "rdoET";
            this.rdoET.Size = new System.Drawing.Size(116, 17);
            this.rdoET.TabIndex = 3;
            this.rdoET.TabStop = true;
            this.rdoET.Text = "Eastern Time ( ET )";
            this.rdoET.UseVisualStyleBackColor = true;
            this.rdoET.CheckedChanged += new System.EventHandler(this.rdoET_CheckedChanged);
            // 
            // rdoCT
            // 
            this.rdoCT.AutoSize = true;
            this.rdoCT.Location = new System.Drawing.Point(7, 64);
            this.rdoCT.Name = "rdoCT";
            this.rdoCT.Size = new System.Drawing.Size(113, 17);
            this.rdoCT.TabIndex = 2;
            this.rdoCT.TabStop = true;
            this.rdoCT.Text = "Central Time ( CT )";
            this.rdoCT.UseVisualStyleBackColor = true;
            this.rdoCT.CheckedChanged += new System.EventHandler(this.rdoCT_CheckedChanged);
            // 
            // rdoMT
            // 
            this.rdoMT.AutoSize = true;
            this.rdoMT.Location = new System.Drawing.Point(7, 41);
            this.rdoMT.Name = "rdoMT";
            this.rdoMT.Size = new System.Drawing.Size(126, 17);
            this.rdoMT.TabIndex = 1;
            this.rdoMT.TabStop = true;
            this.rdoMT.Text = "Mountain Time ( MT )";
            this.rdoMT.UseVisualStyleBackColor = true;
            this.rdoMT.CheckedChanged += new System.EventHandler(this.rdoMT_CheckedChanged);
            // 
            // rdoPT
            // 
            this.rdoPT.AutoSize = true;
            this.rdoPT.Location = new System.Drawing.Point(7, 18);
            this.rdoPT.Name = "rdoPT";
            this.rdoPT.Size = new System.Drawing.Size(112, 17);
            this.rdoPT.TabIndex = 0;
            this.rdoPT.TabStop = true;
            this.rdoPT.Text = "Pacific Time ( PT )";
            this.rdoPT.UseVisualStyleBackColor = true;
            this.rdoPT.CheckedChanged += new System.EventHandler(this.rdoPT_CheckedChanged);
            // 
            // AppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 688);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnLogout);
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
            this.Controls.Add(this.cmbTimeSlots);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnSaveAppointment);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.cmbCustomerName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblCustomerName);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AppointmentForm";
            this.Text = "Appointment Form";
            this.Load += new System.EventHandler(this.AppointmentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Label txtEmail;
        private System.Windows.Forms.ComboBox cmbTimeSlots;
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
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoET;
        private System.Windows.Forms.RadioButton rdoCT;
        private System.Windows.Forms.RadioButton rdoMT;
        private System.Windows.Forms.RadioButton rdoPT;
    }
}