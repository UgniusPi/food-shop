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
    public partial class ProductPreview : UserControl
    {
        Product product;
        Form1 mainForm;
        Account currentAccount;
        AccountInfo currentAccountInfo;
        Random rnd;
        public ProductPreview(Product product, Form1 form, Account currentAccount, AccountInfo currentAccountInfo)
        {
            InitializeComponent();

            this.product = product;
            this.mainForm = form;
            this.currentAccount = currentAccount;
            this.currentAccountInfo = currentAccountInfo;

            lblName.Text = product.Name;
            lblPrice.Text = $"${product.Price}";
            lblCategory.Text = "Category: " + product.Category;
            lblCountryOfOrigin.Text = "Country Of Origin: " + product.Country_of_origin;
            lblDescription.Text = "Description: " + product.Description;

            if (currentAccount.Account_type!="admin")
            {
                btnDelete.Visible = false;
                btnEdit.Visible = false;
            }
            if (currentAccount.Account_type!="client")
            {
                btnBuy.Visible = false;
            }
            rnd = new Random();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = Unirest.delete("http://localhost:85/products?id=" + product.Id)
              .header("Accept", "")
              .asJsonAsync<string>();

            mainForm.GetProducts();
            MessageBox.Show(result.Result.Body);
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            new EditProductForm(product, mainForm).ShowDialog();
            
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            float deliveryFee = rnd.Next(1, 4);
            if (currentAccountInfo.Balance >= product.Price + deliveryFee)
            {
                float newBalance = currentAccountInfo.Balance - product.Price - deliveryFee;

                var request = Unirest.post("http://localhost:85/unfinished_orders")
                .body(new Dictionary<string, object>
                {
                    { "client_id", currentAccount.Id},
                    {"product_id", product.Id },
                    {"delivery_fee", deliveryFee }
                })
                .asJsonAsync<string>();

                var request2 = Unirest.put("http://localhost:85/edit_balance?id=" + currentAccountInfo.Id)
                .body(new Dictionary<string, object>
                {
                    {"balance", newBalance }
                })
                .asJsonAsync<string>();

                MessageBox.Show(request.Result.Body + ", " + request2.Result.Body);

                mainForm.lblBalanceUpdate.Visible = true;
                mainForm.lblBalanceUpdate.ForeColor = Color.Red;
                mainForm.lblBalanceUpdate.Text = $"- ${product.Price+deliveryFee}";
                mainForm.TimerBalanceUpdater.Enabled = true;

                mainForm.GetCurrentAccount();
                mainForm.UpdateBalance();
            }
            else
            {
                MessageBox.Show("You don't have enough money!");
            }
        }
    }
}
