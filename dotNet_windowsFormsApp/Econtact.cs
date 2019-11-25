using dotNet_windowsFormsApp.EcontactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotNet_windowsFormsApp
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxContactID_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
        contactClass contact = new contactClass();
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //get tge value from input fields
            contact.FirstName = textBoxFirstName.Text;
            contact.LastName = textBoxLastName.Text;
            contact.ContactNumber = textBoxContactNumber.Text;
            contact.Address = textBoxAddress.Text;
            contact.Gender = comboGender.Text;

            //Inserting data to database
            bool isSuccess = contact.Insert(contact);

            if(isSuccess == true)
            {
                //insert successfull
                MessageBox.Show("New Contact Successfully Inserted");

            }
            else
            {
                MessageBox.Show("Failed to add contact. error");

            }

            //Load data with new added record on Grid View
            DataTable dt = contact.Select();
            dgvConntactList.DataSource = dt;
            Clear(); //Clear after entering data

        }

        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            //Load data on Grid View
            DataTable dt = contact.Select();
            dgvConntactList.DataSource = dt;
        }

        private void CloseApp_Click(object sender, EventArgs e)
        {
            this.Close(); // close the app when the button is clicked
        }

        //Method to clear fields
        public void Clear()
        {
            textBoxContactID.Text = "";
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxContactNumber.Text = "";
            textBoxAddress.Text = "";
            comboGender.Text = "";
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            //Get the data from txt boxes
            contact.ContactID = int.Parse(textBoxContactID.Text);
            contact.FirstName = textBoxFirstName.Text;
            contact.LastName = textBoxLastName.Text;
            contact.ContactNumber = textBoxContactNumber.Text;
            contact.Address = textBoxAddress.Text;
            contact.Gender = comboGender.Text;

            //Update data in db
            bool success = contact.Update(contact);
            if(success == true)
            {
                MessageBox.Show("Content updated successfully. ");
                //Load data with updated record on Grid View
                DataTable dt = contact.Select();
                dgvConntactList.DataSource = dt;

                //clear after updating
                Clear();
            }
            else
            {
                MessageBox.Show("Content failed to update. ");
            }
        }

        //Updating on click 
        private void dgvConntactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get the data from grid view into the text boxes
            //Identify the row on witch mouse is clicked
            int rowIndex = e.RowIndex;
            textBoxContactID.Text = dgvConntactList.Rows[rowIndex].Cells[0].Value.ToString();
            textBoxFirstName.Text = dgvConntactList.Rows[rowIndex].Cells[1].Value.ToString();
            textBoxLastName.Text = dgvConntactList.Rows[rowIndex].Cells[2].Value.ToString();
            textBoxContactNumber.Text = dgvConntactList.Rows[rowIndex].Cells[3].Value.ToString();
            textBoxAddress.Text = dgvConntactList.Rows[rowIndex].Cells[4].Value.ToString();
            comboGender.Text = dgvConntactList.Rows[rowIndex].Cells[5].Value.ToString();

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //get data from txt boxes
            contact.ContactID = Convert.ToInt32(textBoxContactID.Text);
            //Delete data in db
            bool success = contact.Delete(contact);
            if (success == true)
            {
                MessageBox.Show("Content deleted successfully. ");
                //Load data with updated record on Grid View
                DataTable dt = contact.Select();
                dgvConntactList.DataSource = dt;

                //clear after updating
                Clear();
            }
            else
            {
                MessageBox.Show("Content failed to delete. ");
            }

        }
        static string myconnstr = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            //get the value from textbox
            string keyword = textBoxSearch.Text;
            SqlConnection sqlConnection = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM table_econntact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%' OR ContactNumber LIKE '%" + keyword + "%' OR Gender LIKE '%" + keyword + "%'", sqlConnection);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvConntactList.DataSource = dt;
        }
    }
}
