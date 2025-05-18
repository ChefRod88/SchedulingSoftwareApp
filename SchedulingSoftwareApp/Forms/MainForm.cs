using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SchedulingSoftwareApp.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadCustomers();
            
        }

        

        private void LoadCustomers()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    // SQL query to get customer, address, and phone details
                    string query = @"
                SELECT 
                    c.customerId,
                    c.customerName,
                    a.address,
                    a.phone,
                    c.active,
                    c.createDate,
                    c.createdBy,
                    c.lastUpdate,
                    c.lastUpdateBy
                FROM customer c
                JOIN address a ON c.addressId = a.addressId";

                    DataTable customerTable = new DataTable();
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        customerTable.Load(reader);
                    }

                    // ✅ Convert timestamp columns from UTC to EST
                    foreach (DataRow row in customerTable.Rows)
                    {
                        if (row["createDate"] is DateTime createDateUtc)
                            row["createDate"] = TimeHelper.ToEST(createDateUtc);

                        if (row["lastUpdate"] is DateTime lastUpdateUtc)
                            row["lastUpdate"] = TimeHelper.ToEST(lastUpdateUtc);
                    }

                    // Clear existing columns to avoid duplication
                    dgvCustomers.Columns.Clear();

                    // Add columns to the DataGridView (without Customer ID)
                    dgvCustomers.AutoGenerateColumns = false;

                    dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "customerName",
                        HeaderText = "Customer Name",
                        Width = 150
                    });

                    dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "address",
                        HeaderText = "Address",
                        Width = 200
                    });

                    dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "phone",
                        HeaderText = "Phone Number",
                        Width = 120
                    });

                    dgvCustomers.Columns.Add(new DataGridViewCheckBoxColumn
                    {
                        DataPropertyName = "active",
                        HeaderText = "Active",
                        Width = 60
                    });

                    dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "createDate",
                        HeaderText = "Create Date (EST)",
                        Width = 130
                    });

                    dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "createdBy",
                        HeaderText = "Created By",
                        Width = 100
                    });

                    dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "lastUpdate",
                        HeaderText = "Last Update (EST)",
                        Width = 130
                    });

                    dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "lastUpdateBy",
                        HeaderText = "Last Update By",
                        Width = 100
                    });

                    // Set the data source
                    dgvCustomers.DataSource = customerTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm(LoadCustomers); // 👈 Pass refresh method
            customerForm.ShowDialog();

        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCustomers.SelectedRows.Count > 0)
                {
                    // Get the DataRowView from the selected row
                    DataRowView rowView = (DataRowView)dgvCustomers.SelectedRows[0].DataBoundItem;

                    // Extract customer data from the selected row
                    string customerName = rowView["customerName"].ToString();
                    string address = rowView["address"].ToString();
                    string phone = rowView["phone"].ToString();
                    bool active = Convert.ToBoolean(rowView["active"]);

                    // Open the customer form with the selected customer data
                    Customer selectedCustomer = new Customer
                    {
                        CustomerId = Convert.ToInt32(rowView["customerId"]),
                        CustomerName = rowView["customerName"].ToString(),
                        Address = rowView["address"].ToString(),
                        Phone = rowView["phone"].ToString(),
                        Active = Convert.ToBoolean(rowView["active"]),
                        CreateDate = Convert.ToDateTime(rowView["createDate"]),
                        CreatedBy = rowView["createdBy"].ToString(),
                        LastUpdate = Convert.ToDateTime(rowView["lastUpdate"]),
                        LastUpdateBy = rowView["lastUpdateBy"].ToString()
                    };

                    CustomerForm customerForm = new CustomerForm(selectedCustomer, LoadCustomers);


                    customerForm.ShowDialog();

                    // Refresh the customer list after closing the form
                    LoadCustomers();
                }
                else
                {
                    MessageBox.Show("Please select a customer to update.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCustomers.SelectedRows.Count > 0)
                {
                    // Get the DataRowView from the selected row
                    DataRowView rowView = (DataRowView)dgvCustomers.SelectedRows[0].DataBoundItem;

                    // Extract values
                    int customerId = Convert.ToInt32(rowView["customerId"]);
                    string customerName = rowView["customerName"].ToString();
                    bool isActive = Convert.ToBoolean(rowView["active"]);

                    // Prevent deleting active customers
                    if (isActive)
                    {
                        MessageBox.Show("Active customers cannot be deleted. Please deactivate the customer first.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Confirm deletion
                    var confirmResult = MessageBox.Show($"Are you sure you want to delete customer '{customerName}'?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        bool success = CustomerRepository.DeleteCustomer(customerId);
                        if (success)
                        {
                            MessageBox.Show("Customer deleted successfully!");
                            LoadCustomers(); // Refresh the customer list
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete customer. Ensure the customer exists.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a customer to delete.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // Ensure the entire application closes
        }

        private void btnManageAppointments_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ FIX: Clear connection pool to avoid "already open" error
                MySqlConnection.ClearAllPools();

                AppointmentForm appointmentForm = new AppointmentForm();
                appointmentForm.Show();
                this.Hide();  // Hide the current form
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Appointment Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomers(); // 🧼 Force refresh of DataGridView
            MessageBox.Show("Customer list refreshed.", "Refresh Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
