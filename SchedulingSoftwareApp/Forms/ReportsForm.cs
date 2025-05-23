﻿using MySql.Data.MySqlClient;
using SchedulingSoftwareApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SchedulingSoftwareApp.Forms
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
            LoadAppointmentTypesByMonth();
            LoadUserSchedules();
            LoadCustomerAppointmentSummary();
        }

        private void LoadAppointmentTypesByMonth()
        {
            try
            {
                var appointments = Appointment.GetAllAppointments();

                var result = appointments
                    .GroupBy(a => new { a.Start.Month, a.Type })
                    .Select(g => new
                    {
                        Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),
                        Type = g.Key.Type,
                        Count = g.Count()
                    })
                    .OrderBy(r => r.Month)
                    .ThenBy(r => r.Type)
                    .ToList();

                dgvTypesByMonth.DataSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointment types by month: {ex.Message}", "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUserSchedules()
        {
            try
            {
                var appointments = Appointment.GetAllAppointments();

                var result = appointments
                    .GroupBy(a => a.CreatedBy)
                    .SelectMany(g => g.Select(a => new
                    {
                        User = g.Key,
                        a.Title,
                        a.Type,
                        //Time Helper
                        Start = TimeHelper.ToEST(a.Start),
                        End = TimeHelper.ToEST(a.End)

                    }))
                    .OrderBy(r => r.User)
                    .ThenBy(r => r.Start)
                    .ToList();

                dgvUserSchedules.DataSource = result;
                dgvUserSchedules.Columns["Start"].HeaderText = "Start (EST)";
                dgvUserSchedules.Columns["End"].HeaderText = "End (EST)";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user schedules: {ex.Message}", "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomerAppointmentSummary()
        {
            try
            {
                var appointments = Appointment.GetAllAppointments();
                var customers = CustomerRepository.GetAllCustomers();

                var result = appointments
                    .GroupBy(a => a.CustomerId)
                    .Select(g => new
                    {
                        Customer = customers.FirstOrDefault(c => c.CustomerId == g.Key)?.CustomerName ?? "Unknown",
                        TotalAppointments = g.Count(),
                        //Time Helper
                        FirstAppointment = TimeHelper.ToEST(g.Min(a => a.Start)),
                        LastAppointment = TimeHelper.ToEST(g.Max(a => a.End))

                    })
                    .OrderBy(r => r.Customer)
                    .ToList();

                dgvCustomerSummary.DataSource = result;
                dgvCustomerSummary.Columns["FirstAppointment"].HeaderText = "First Appointment (EST)";
                dgvCustomerSummary.Columns["LastAppointment"].HeaderText = "Last Appointment (EST)";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer appointment summary: {ex.Message}", "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
