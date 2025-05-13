using System;
using System.Windows.Forms;

namespace SchedulingSoftwareApp.Forms
{
    public partial class CustomerForm : Form
    {
        private Customer _selectedCustomer;
        private bool _isUpdateMode;
        private Action RefreshCustomerGrid;

        // For adding new customer
        public CustomerForm(Action refreshCustomerGrid)
        {
            InitializeComponent();
            _isUpdateMode = false;
            this.Text = "Add New Customer";
            RefreshCustomerGrid = refreshCustomerGrid;
        }

        // For updating existing customer
        public CustomerForm(Customer selectedCustomer, Action refreshCustomerGrid)
        {
            InitializeComponent();
            _selectedCustomer = selectedCustomer;
            _isUpdateMode = true;
            this.Text = $"Update Customer - {selectedCustomer.CustomerName}";
            RefreshCustomerGrid = refreshCustomerGrid;

            // Populate fields
            txtCustomerName.Text = selectedCustomer.CustomerName;
            txtAddress.Text = selectedCustomer.Address;
            txtPhone.Text = selectedCustomer.Phone;
            chkActive.Checked = selectedCustomer.Active;
        }

        private bool ValidateCustomerInputs(out string customerName, out string address, out string phone, out bool active)
        {
            customerName = txtCustomerName.Text.Trim();
            address = txtAddress.Text.Trim();
            phone = txtPhone.Text.Trim();
            active = chkActive.Checked;

            if (string.IsNullOrWhiteSpace(customerName))
            {
                MessageBox.Show("Customer name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Address is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(phone) || !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\d{3}-\d{3}-\d{4}$"))
            {
                MessageBox.Show("Phone number must be in the format XXX-XXX-XXXX.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateCustomerInputs(out string customerName, out string address, out string phone, out bool active))
                    return;

                bool success;

                if (_isUpdateMode)
                {
                    success = CustomerRepository.UpdateCustomer(
                        _selectedCustomer.CustomerId,
                        customerName,
                        address,
                        phone,
                        active,
                        "Admin"
                    );
                }
                else
                {
                    success = CustomerRepository.InsertCustomer(
                        customerName,
                        address,
                        phone,
                        active,
                        "Admin"
                    );
                }

                if (success)
                {
                    MessageBox.Show(_isUpdateMode ? "Customer updated successfully!" : "Customer added successfully!");
                    RefreshCustomerGrid?.Invoke();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("An error occurred. Please check your input or try again.", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
