using LinqXmlContactsBLL;
using System;
using System.Windows.Forms;

namespace LinqXmlContactsPL
{
    public partial class frmMain : Form
    {
        LinqXmlContactsBLManager blManager = new LinqXmlContactsBLManager();

        public frmMain()
        {
            InitializeComponent();

            //if the xml file is missing, create it
            blManager.SetXmlFile();
            dataGridView1.DataSource = blManager.GetAllContacts();
        }

        private void btnViewContact_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Please enter id", "Message");
                txtId.Focus();
                return;
            }
            int x;
            if (!int.TryParse(txtId.Text, out x))
            {
                MessageBox.Show("Please enter a valid id number", "Message");
                txtId.Focus();
                txtId.SelectAll();
                return;
            }
            if (!blManager.IdExists(int.Parse(txtId.Text)))
            {
                MessageBox.Show("The id " + txtId.Text + " does not exist", "Message");
                txtId.Focus();
                txtId.SelectAll();
                return;
            }

            //get a contact by id
            Contact contact = blManager.GetContactById(int.Parse(txtId.Text));

            //show the contact properties on the controls
            lblId.Text = contact.Id.ToString();
            txtFirstName.Text = contact.FirstName;
            txtLastName.Text = contact.LastName;
            txtCompany.Text = contact.Company;
            txtCity.Text = contact.City;
            txtTelephone.Text = contact.Telephone;
            txtFax.Text = contact.Fax;
            txtEmail.Text = contact.Email;

            txtId.Clear();
        }

        private void btnNewContact_Click(object sender, EventArgs e)
        {
            frmAdd frmAdd1 = new frmAdd();
            frmAdd1.ShowDialog();
            dataGridView1.DataSource = blManager.GetAllContacts();
        }

        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            if (lblId.Text == "")
            {
                MessageBox.Show("a contact was not selected", "Message");
                txtId.Focus();
                txtId.SelectAll();
                return;
            }
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

            //create a contact and set his properties from the controls
            Contact contact = new Contact()
            {
                Id = int.Parse(lblId.Text),
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Company = txtCompany.Text,
                City = txtCity.Text,
                Telephone = txtTelephone.Text,
                Fax = txtFax.Text,
                Email = txtEmail.Text
            };

            //update the contact
            blManager.UpdateContact(contact);
            dataGridView1.DataSource = blManager.GetAllContacts();
        }

        private void btnDeleteContact_Click(object sender, EventArgs e)
        {
            if (lblId.Text == "")
            {
                MessageBox.Show("a contact was not selected", "Message");
                txtId.Focus();
                txtId.SelectAll();
                return;
            }

            Contact contact = blManager.GetContactById(int.Parse(lblId.Text));

            string messageToPrint = "The following contact will get deleted:\n\n" +
                "Id : " + contact.Id.ToString() + "\n" +
                "First name : " + contact.FirstName + "\n" +
                "Last name : " + contact.LastName + "\n" +
                "Company : " + contact.Company + "\n" +
                "City : " + contact.City + "\n" +
                "Telephone : " + contact.Telephone + "\n" +
                "Fax : " + contact.Fax + "\n" +
                "Email : " + contact.Email + "\n\n" +
                "Are you sure?";

            DialogResult dialogResult = MessageBox.Show(messageToPrint, "Message", 
                MessageBoxButtons.YesNo);

            //if the user clicked 'no', cancel the delete operation
            if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Operation cancelled", "Message");
                return;
            }

            //if the user clicked 'yes', delete the contact
            blManager.DeleteContact(contact.Id);

            dataGridView1.DataSource = blManager.GetAllContacts();

            //clear the controls
            lblId.Text = "";
            txtFirstName.Clear();
            txtLastName.Clear();
            txtCompany.Clear();
            txtCity.Clear();
            txtTelephone.Clear();
            txtFax.Clear();
            txtEmail.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
