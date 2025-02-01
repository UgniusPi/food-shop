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
    public partial class EditProductForm : Form
    {
        Product product;
        Form1 mainForm;
        public EditProductForm(Product product,Form1 form)
        {
            InitializeComponent();

            this.product = product;
            this.mainForm = form;

            txtCountryOfOrigin.Text = product.Country_of_origin;
            txtDescription.Text = product.Description;
            txtName.Text = product.Name;
            txtPrice.Text = product.Price.ToString();
            cmbCategory.SelectedItem = product.Category;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var request = Unirest.put("http://localhost:85/products?id=" + product.Id)
                .body(new Dictionary<string, object>
                {
                    { "name", txtName.Text},
                    { "price", Convert.ToSingle(txtPrice.Text)},
                    {"category", cmbCategory.SelectedItem },
                    {"country_of_origin", txtCountryOfOrigin.Text },
                    {"description", txtDescription.Text }
                })
                .asJsonAsync<string>();

            var result = request.Result;
            mainForm.GetProducts();
            MessageBox.Show(result.Body.ToString());
        }
    }
}
