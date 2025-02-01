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
    public partial class UnfinishedOrderPreview : UserControl
    {
        UnfinishedOrder unfinishedOrder;
        Form1 mainForm;
        Account currentAccount;
        AccountInfo currentAccountInfo;
        public UnfinishedOrderPreview(UnfinishedOrder unfinishedOrder, Form1 form, Account currentAccount, AccountInfo currentAccountInfo)
        {
            InitializeComponent();

            this.unfinishedOrder = unfinishedOrder;
            this.mainForm = form;
            this.currentAccount = currentAccount;
            this.currentAccountInfo = currentAccountInfo;

            var client = new Account();
            var product = new Product();

            foreach (var account in Account.accounts)
            {
                if (account.Id == unfinishedOrder.Client_id)
                {
                    client = account;
                }
            }
            
            foreach (var item in Product.products)
            {
                if (item.Id == unfinishedOrder.Product_id)
                {
                    product = item;
                }
            }
            

            if (currentAccount.Account_type!="courier")
            {
                btnAccept.Visible = false;
            }

            if (currentAccount.Account_type=="courier")
            {
                btnDelete.Visible = false;
            }

            lblId.Text = "Order ID: " + unfinishedOrder.Id;
            lblClientUsername.Text = $"Client: {client.Username}";
            lblDeliveryFee.Text = $"Delivery Fee: ${unfinishedOrder.Delivery_fee}";
            lblProductName.Text = $"Product: {product.Name}";
            lblTotalPrice.Text = $"Total Price: ${product.Price + unfinishedOrder.Delivery_fee}";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var product = new Product();

            mainForm.UnirestGetProducts();

            foreach (var item in Product.products)
            {
                if (item.Id == unfinishedOrder.Product_id)
                    product = item;
            }

            float newBalance = currentAccountInfo.Balance + unfinishedOrder.Delivery_fee + product.Price;

            var result = Unirest.delete("http://localhost:85/unfinishedorders?id=" + unfinishedOrder.Id)
              .header("Accept", "")
              .asJsonAsync<string>();

            mainForm.GetUnfinishedOrders();

            var request2 = Unirest.put("http://localhost:85/edit_balance?id=" + currentAccountInfo.Id)
                .body(new Dictionary<string, object>
                {
                    {"balance", newBalance }
                })
                .asJsonAsync<string>();

            MessageBox.Show(result.Result.Body + ", " + request2.Result.Body);

            mainForm.lblBalanceUpdate.Visible = true;
            mainForm.lblBalanceUpdate.ForeColor = Color.Green;
            mainForm.lblBalanceUpdate.Text = $"+ ${product.Price + unfinishedOrder.Delivery_fee}";
            mainForm.TimerBalanceUpdater.Enabled = true;

            mainForm.GetCurrentAccount();
            mainForm.UpdateBalance();

            

            

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            float newBalance = currentAccountInfo.Balance + unfinishedOrder.Delivery_fee;

            var request = Unirest.post("http://localhost:85/orders")
                .body(new Dictionary<string, object>
                {
                    { "client_id", unfinishedOrder.Client_id},
                    {"courier_id", currentAccount.Id },
                    {"product_id", unfinishedOrder.Product_id },
                    {"delivery_fee", unfinishedOrder.Delivery_fee },
                })
                .asJsonAsync<string>();

            var request2 = Unirest.delete("http://localhost:85/unfinishedorders?id=" + unfinishedOrder.Id)
              .header("Accept", "")
              .asJsonAsync<string>();

            mainForm.GetUnfinishedOrders();

            var request3 = Unirest.put("http://localhost:85/edit_balance?id=" + currentAccountInfo.Id)
                .body(new Dictionary<string, object>
                {
                    {"balance", newBalance }
                })
                .asJsonAsync<string>();

            MessageBox.Show(request.Result.Body + ", " + request3.Result.Body);

            mainForm.lblBalanceUpdate.Visible = true;
            mainForm.lblBalanceUpdate.ForeColor = Color.Green;
            mainForm.lblBalanceUpdate.Text = $"+ ${unfinishedOrder.Delivery_fee}";
            mainForm.TimerBalanceUpdater.Enabled = true;

            mainForm.GetCurrentAccount();
            mainForm.UpdateBalance();
        }
    }
}
