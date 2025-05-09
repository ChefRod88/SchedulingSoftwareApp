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

                    // Join customer and address tables to include address and phone
                    string query = @"
                SELECT 
                    c.customerId,
                    c.customerName,
                    c.addressId,  -- Include addressId for update
                    a.address,
                    a.phone,
                    c.active,
                    c.createDate,
                    c.createdBy,
                    c.lastUpdate,
                    c.lastUpdateBy
                FROM customer c
                JOIN address a ON c.addressId = a.addressId";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Use a DataTable to handle complex joins
                    DataTable customerTable = new DataTable();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        customerTable.Load(reader);
                    }

                    // Bind the DataTable to the DataGridView
                    dgvCustomers.DataSource = customerTable;

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            customerForm.ShowDialog();
            LoadCustomers(); // Refresh the customer list
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCustomers.SelectedRows.Count > 0)
                {
                    // Get the DataRowView from the selected row
                    DataRowView rowView = (DataRowView)dgvCustomers.SelectedRows[0].DataBoundItem;

                    // Create a Customer object from the DataRowView
                    Customer selectedCustomer = new Customer
                    {
                        CustomerId = Convert.ToInt32(rowView["customerId"]),
                        CustomerName = rowView["customerName"].ToString(),
                        AddressId = Convert.ToInt32(rowView["addressId"]),
                        Active = Convert.ToBoolean(rowView["active"]),
                        CreateDate = Convert.ToDateTime(rowView["createDate"]),
                        CreatedBy = rowView["createdBy"].ToString(),
                        LastUpdate = Convert.ToDateTime(rowView["lastUpdate"]),
                        LastUpdateBy = rowView["lastUpdateBy"].ToString()
                    };

                    // Prevent updating inactive customers if needed
                    if (!selectedCustomer.Active)
                    {
                        MessageBox.Show("Inactive customers cannot be updated. Please activate the customer first.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Open the customer form with the selected customer
                    CustomerForm customerForm = new CustomerForm(selectedCustomer);
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
                    int customerId = Convert.ToInt32(rowView["customerId"]);
                    string customerName = rowView["customerName"].ToString();
                    bool isActive = Convert.ToBoolean(rowView["active"]);

                    // Prevent deleting active customers if required
                    if (isActive)
                    {
                        MessageBox.Show("Active customers cannot be deleted. Please deactivate the customer first.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

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
                // Open the Appointment Form
                AppointmentForm appointmentForm = new AppointmentForm();
                appointmentForm.Show();
                this.Hide();  // Hide the current form
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Appointment Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
