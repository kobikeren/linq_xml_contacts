using LinqXmlContactsBLL;
using System;
using System.Windows.Forms;

namespace LinqXmlContactsPL
{
    public partial class frmAdd : Form
    {
        LinqXmlContactsBLManager blManager = new LinqXmlContactsBLManager();

        public frmAdd()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text == "")
            {
                MessageBox.Show("Please enter first name", "Message");
                txtFirstName.Focus();
                return;
            }
            if (txtLastName.Text == "")
            {
                MessageBox.Show("Please enter last name", "Message");
                txtLastName.Focus();
                return;
            }
            if (txtCompany.Text == "")
            {
                MessageBox.Show("Please enter company", "Message");
                txtCompany.Focus();
                return;
            }
            if (txtCity.Text == "")
            {
                MessageBox.Show("Please enter city", "Message");
                txtCity.Focus();
                return;
            }
            if (txtTelephone.Text == "")
            {
                MessageBox.Show("Please enter telephone", "Message");
                txtTelephone.Focus();
                return;
            }
            if (txtFax.Text == "")
            {
                MessageBox.Show("Please enter fax", "Message");
                txtFax.Focus();
                return;
            }
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Please enter email", "Message");
                txtEmail.Focus();
                return;
            }
            
            //create a contact and set values to the properties from the controls
            int id = blManager.GetNextId();
            Contact contact = new Contact()
            {
                Id = id,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Company = txtCompany.Text,
                City = txtCity.Text,
                Telephone = txtTelephone.Text,
                Fax = txtFax.Text,
                Email = txtEmail.Text
            };

            //add the contact
            blManager.AddContact(contact);

            //clear the controls
            txtFirstName.Clear();
            txtLastName.Clear();
            txtCompany.Clear();
            txtCity.Clear();
            txtTelephone.Clear();
            txtFax.Clear();
            txtEmail.Clear();

            txtFirstName.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
