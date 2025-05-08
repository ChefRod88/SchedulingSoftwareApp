using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                    string query = "SELECT customerId, customerName, addressId, active FROM customer";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    List<Customer> customers = new List<Customer>();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer
                            {
                                CustomerId = reader.GetInt32("customerId"),
                                CustomerName = reader.GetString("customerName"),
                                AddressId = reader.GetInt32("addressId"),
                                Active = reader.GetBoolean("active")
                            });
                        }
                    }

                    dgvCustomers.DataSource = customers;

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
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                Customer selectedCustomer = (Customer)dgvCustomers.SelectedRows[0].DataBoundItem;
                CustomerForm customerForm = new CustomerForm(selectedCustomer);
                customerForm.ShowDialog();
                LoadCustomers(); // Refresh the customer list
            }
            else
            {
                MessageBox.Show("Please select a customer to update.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCustomers.SelectedRows.Count > 0)
                {
                    Customer selectedCustomer = (Customer)dgvCustomers.SelectedRows[0].DataBoundItem;

                    var confirmResult = MessageBox.Show($"Are you sure you want to delete customer '{selectedCustomer.CustomerName}'?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        bool success = CustomerRepository.DeleteCustomer(selectedCustomer.CustomerId);
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
    }
}
