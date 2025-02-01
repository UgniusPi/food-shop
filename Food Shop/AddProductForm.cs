using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using unirest_net.http;

namespace Food_Shop
{
    public partial class AddProductForm : Form
    {
        Form1 mainForm;
        public AddProductForm(Form1 mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            cmbCategory.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCountryOfOrigin.Text) || string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(txtPrice.Text))
            {
                MessageBox.Show("Missing data!");
                return;
            }

            var request = Unirest.post("http://localhost:85/products")
                .body(new Dictionary<string, object>
                {
                    {"name", txtName.Text },
                    {"price", Convert.ToDouble(txtPrice.Text) },
                    {"category", cmbCategory.SelectedItem },
                    {"country_of_origin", txtCountryOfOrigin.Text },
                    {"description", txtDescription.Text }
                })
                .asJsonAsync<string>();

            mainForm.GetProducts();

            MessageBox.Show(request.Result.Body);

            txtCountryOfOrigin.Clear();
            txtDescription.Clear();
            txtName.Clear();
            txtPrice.Clear();
            cmbCategory.SelectedIndex = 0;
        }
    }
}
