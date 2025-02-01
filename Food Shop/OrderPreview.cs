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
    public partial class OrderPreview : UserControl
    {
        Order order;
        Form1 mainForm;
        Account currentAccount;
        public OrderPreview(Order order, Form1 form, Account currentAccount)
        {
            InitializeComponent();

            this.order = order;
            this.mainForm = form;
            this.currentAccount = currentAccount;

            var client = new Account();
            var courier = new Account();
            var product = new Product();

            foreach (var account in Account.accounts)
            {
                if (account.Id == order.Client_id)
                {
                    client = account;
                }
                if (account.Id == order.Courier_id)
                {
                    courier = account;
                }
            }
            ///////NEVEIKIA
            foreach (var item in Product.products)
            {
                if (item.Id == order.Product_id)
                {
                    product = item;
                }
            }
            /////NEVEIKIA

            lblId.Text = "Order ID: " + order.Id;
            lblClientUsername.Text = $"Client: {client.Username}";
            lblCourierUsername.Text = $"Courier: {courier.Username}";
            lblDeliveryFee.Text = $"Delivery Fee: ${order.Delivery_fee}";
            lblProductName.Text = $"Product: {product.Name}";
            lblTotalPrice.Text = $"Total Price: ${product.Price + order.Delivery_fee}";

            if (currentAccount.Account_type!="admin")
            {
                btnDelete.Visible = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = Unirest.delete("http://localhost:85/orders?id=" + order.Id)
              .header("Accept", "")
              .asJsonAsync<string>();

            mainForm.GetOrders();
            MessageBox.Show(result.Result.Body);
            
        }
    }
}
