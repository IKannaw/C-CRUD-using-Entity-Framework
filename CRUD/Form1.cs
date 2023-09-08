using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class Form1 : Form
    {

        testEntities customerDb = new testEntities();
       
        public Form1()
        {
            InitializeComponent();
            btnUpdate.Visible = false;
        }
        
        public void clearData()
        {
           txtID.Text = "";
           txtName.Text = "";
           txtAddress.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            Customer customer = new Customer()
            {
                name = txtName.Text,
                address = txtAddress.Text
            };
            customerDb.Customers.Add(customer);
            customerDb.SaveChanges();
            customerDataGrid.DataSource = customerDb.Customers.ToList();
            clearData();
            Console.WriteLine("Added");
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           if(e.ColumnIndex ==  customerDataGrid.Columns["btnEdit"].Index && e.RowIndex >= 0)
            {
                string id = customerDataGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                string name = customerDataGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                string address = customerDataGrid.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtID.Text = id.ToString();
                txtName.Text = name;
                txtAddress.Text = address;
                btnUpdate.Visible = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int Id = Int16.Parse(txtID.Text);
            Customer updateCustomer = (from customer in customerDb.Customers where customer.id == Id select customer).Single();
            updateCustomer.name = txtName.Text;
            updateCustomer.address = txtAddress.Text;
            customerDb.SaveChanges();
            customerDataGrid.DataSource = customerDb.Customers.ToList();
            clearData();
        }

    }
}
