using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SchedulingSoftwareApp.Forms
{
    public partial class CustomerForm : Form
    {
        private Customer _selectedCustomer;
        private bool _isUpdateMode;

        public CustomerForm()
        {
            InitializeComponent();
            LoadAddresses();
            _isUpdateMode = false;
            this.Text = "Add New Customer";
        }

        public CustomerForm(Customer selectedCustomer)
        {
            InitializeComponent();
            LoadAddresses();
            _selectedCustomer = selectedCustomer;
            _isUpdateMode = true;
            this.Text = $"Update Customer - {selectedCustomer.CustomerName}";

            // Populate the fields with the selected customer data
            txtCustomerName.Text = selectedCustomer.CustomerName;
            cmbAddress.SelectedValue = selectedCustomer.AddressId;
            chkActive.Checked = selectedCustomer.Active;
        }

        private void LoadAddresses()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT addressId, address FROM address";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    List<Address> addresses = new List<Address>();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            addresses.Add(new Address
                            {
                                AddressId = reader.GetInt32("addressId"),
                                AddressLine = reader.GetString("address")
                            });
                        }
                    }

                    cmbAddress.DataSource = addresses;
                    cmbAddress.DisplayMember = "AddressLine1";
                    cmbAddress.ValueMember = "AddressId";

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading addresses: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateCustomerInputs(out string customerName, out int addressId, out bool active)
        {
            customerName = txtCustomerName.Text.Trim();
            active = chkActive.Checked;
            addressId = -1;  // Initialize the out parameter

            // Validate customer name
            if (string.IsNullOrEmpty(customerName))
            {
                MessageBox.Show("Customer name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate address selection
            if (cmbAddress.SelectedValue == null || !int.TryParse(cmbAddress.SelectedValue.ToString(), out addressId))
            {
                MessageBox.Show("Please select a valid address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        

        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateCustomerInputs(out string customerName, out int addressId, out bool active))
                    return;

                if (_isUpdateMode)
                {
                    // Update existing customer
                    bool success = CustomerRepository.UpdateCustomer(_selectedCustomer.CustomerId, customerName, addressId, active, "Admin");
                    if (success)
                    {
                        MessageBox.Show("Customer updated successfully!");
                        this.Close(); // Close the form after successful update
                    }
                    else
                    {
                        MessageBox.Show("Failed to update customer. Please check the input values.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // Add new customer
                    bool success = CustomerRepository.InsertCustomer(customerName, addressId, active, "Admin");
                    if (success)
                    {
                        MessageBox.Show("Customer added successfully!");
                        this.Close(); // Close the form after successful addition
                    }
                    else
                    {
                        MessageBox.Show("Failed to add customer. Please check the input values.", "Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
