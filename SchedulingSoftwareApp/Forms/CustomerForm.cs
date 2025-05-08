using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SchedulingSoftwareApp.Forms
{
    public partial class CustomerForm : Form // Renamed for clarity
    {
        public CustomerForm()
        {
            InitializeComponent();
            this.Load += CustomerForm_Load; // Bind the Load event to the method
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCustomers(); // Calls the correctly defined method below
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomers() // This method correctly loads customer data
        {
            try
            {
                List<Customer> customers = CustomerRepository.GetAllCustomers();
                customerGridView.DataSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving customer data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                string customerName = txtCustomerName.Text;
                int addressId = Convert.ToInt32(cmbAddressId.SelectedValue);
                bool active = chkActive.Checked;
                string createdBy = "Admin"; // Adjust this based on authentication system

                bool success = CustomerRepository.InsertCustomer(customerName, addressId, active, createdBy);
                if (success)
                {
                    MessageBox.Show("Customer added successfully!");
                    LoadCustomers(); // Refresh data
                }
                else
                {
                    MessageBox.Show("Failed to add customer. Please check input values.", "Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid address ID format. Please enter a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                int customerId = Convert.ToInt32(txtCustomerId.Text);
                string newName = txtCustomerName.Text;
                int addressId = Convert.ToInt32(cmbAddressId.SelectedValue);
                bool active = chkActive.Checked;
                string updatedBy = "Admin";

                bool success = CustomerRepository.UpdateCustomer(customerId, newName, addressId, active, updatedBy);
                if (success)
                {
                    MessageBox.Show("Customer updated successfully!");
                    LoadCustomers(); // Refresh data
                }
                else
                {
                    MessageBox.Show("Failed to update customer. Ensure the customer exists.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid ID format. Please enter a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                int customerId = Convert.ToInt32(txtCustomerId.Text);

                bool success = CustomerRepository.DeleteCustomer(customerId);
                if (success)
                {
                    MessageBox.Show("Customer deleted successfully!");
                    LoadCustomers(); // Refresh data
                }
                else
                {
                    MessageBox.Show("Failed to delete customer. Ensure the customer ID exists.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid ID format. Please enter a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

