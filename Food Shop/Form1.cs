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
using Newtonsoft.Json;

namespace Food_Shop
{
    public partial class Form1 : Form
    {
        Account currentAccount;
        AccountInfo currentAccountInfo;
        ulong currentAccountId;

        byte r=255, g, b;
        bool rAdd, gAdd=true, bAdd, rDeL, gDel, bDel;
        int s=0;

        bool newLogin;
        enum CurrentData
        {
            products,
            accounts,
            orders,
            unfinishedOrders
        }
        CurrentData currentData;
        int y;
        public Form1(Account currentAccount)
        {
            InitializeComponent();

            this.currentAccount = currentAccount;

        }

        private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblSearchCriteria.Text = "Username:";
            GetAccounts(currentAccount);
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblSearchCriteria.Text = "Name:";
            GetProducts();
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblSearchCriteria.Text = "Order ID:";
            GetOrders();
        }
        public void GetProducts()
        {
            currentData = CurrentData.products;

            UnirestGetProducts();

            LoadProducts();
        }
        public void GetAccounts(Account currentAccount)
        {
            currentData = CurrentData.accounts;

            

            if (currentAccount.Account_type!="admin")
            {
                panelMain.Controls.Clear();
                var entry = new AccountPreview(currentAccount, this, currentAccount);
                entry.Location = new Point(0, y);
                panelMain.Controls.Add(entry);
            }
            else
            {
                UnirestGetAccounts();
                LoadAccounts();
            }

            
        }
        public void GetOrders()
        {
            currentData = CurrentData.orders;

            UnirestGetOrders();

            LoadOrders();
            

            
        }
        public void LoadProducts()
        {
            panelMain.Controls.Clear();

            foreach (var item in Product.products)
            {
                var entry = new ProductPreview(item, this, currentAccount, currentAccountInfo);
                entry.Location = new Point(0, y);
                panelMain.Controls.Add(entry);
                y += entry.Height;
            }
            y = 0;
        }
        private void LoadAccounts()
        {
            panelMain.Controls.Clear();

            foreach (var item in Account.accounts)
            {
                var entry = new AccountPreview(item, this, currentAccount);
                entry.Location = new Point(0, y);
                panelMain.Controls.Add(entry);
                y += entry.Height;
            }
            y = 0;
        }
        private void LoadOrders()
        {
            panelMain.Controls.Clear();

            foreach (var item in Order.orders)
            {
                if ((currentAccount.Account_type == "client" && item.Client_id == currentAccount.Id) || (currentAccount.Account_type == "courier" && item.Courier_id == currentAccount.Id) || currentAccount.Account_type=="admin")
                {
                    var entry = new OrderPreview(item, this, currentAccount);
                    entry.Location = new Point(0, y);
                    panelMain.Controls.Add(entry);
                    y += entry.Height;
                }
            }
            y = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            switch (currentData)
            {
                case CurrentData.products:
                    SearchProducts();
                    break;
                case CurrentData.accounts:
                    SearchAccounts();
                    break;
                case CurrentData.orders:
                    SearchOrders();
                    break;
                case CurrentData.unfinishedOrders:
                    SearchUnfinishedOrders();
                    break;
            }
        }

        private void SearchUnfinishedOrders()
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                LoadUnfinishedOrders();
                return;
            }

            var unfinishedOrders = UnfinishedOrder.unfinishedOrders.FindAll(uo => uo.Id.ToString()==txtSearch.Text);

            panelMain.Controls.Clear();
            foreach (var item in unfinishedOrders)
            {
                var entry = new UnfinishedOrderPreview(item, this, currentAccount, currentAccountInfo);
                entry.Location = new Point(0, y);
                panelMain.Controls.Add(entry);
                y += entry.Height;
            }
            y = 0;
        }

        private void SearchOrders()
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                LoadOrders();
                return;
            }
                

            var orders = Order.orders.FindAll(o => o.Id.ToString() == txtSearch.Text);

            panelMain.Controls.Clear();
            foreach (var item in orders)
            {
                if ((currentAccount.Account_type=="client" && item.Client_id==currentAccount.Id) || (currentAccount.Account_type=="courier" && item.Courier_id==currentAccount.Id))
                {
                    var entry = new OrderPreview(item, this, currentAccount);
                    entry.Location = new Point(0, y);
                    panelMain.Controls.Add(entry);
                    y += entry.Height;
                }
            }
            y = 0;
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            newLogin = true;
            new StartForm().Show();
            this.Close();
        }

        private void btnRGB_Click(object sender, EventArgs e)
        {
            if (rgbBackground.Enabled)
            {
                rgbBackground.Enabled = false;
            }
            else
            {
                rgbBackground.Enabled = true;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!newLogin)
            Application.Exit();
        }

        private void SearchProducts()
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                LoadProducts();
                return;
            }

            var products = Product.products.FindAll(p => p.Name.Contains(txtSearch.Text));

            panelMain.Controls.Clear();
            foreach (var item in products)
            {
                var entry = new ProductPreview(item, this, currentAccount, currentAccountInfo);
                entry.Location = new Point(0, y);
                panelMain.Controls.Add(entry);
                y += entry.Height;
            }
            y = 0;
        }
        private void SearchAccounts()
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                LoadAccounts();
                return;
            }

            var accounts = Account.accounts.FindAll(a => a.Username.Contains(txtSearch.Text));

            panelMain.Controls.Clear();
            foreach (var item in accounts)
            {
                var entry = new AccountPreview(item, this, currentAccount);
                entry.Location = new Point(0, y);
                panelMain.Controls.Add(entry);
                y += entry.Height;
            }
            y = 0;
        }

        public void UnirestGetProducts()
        {
            var result = Unirest.get("http://localhost:85/products")
                           .header("Accept", "")
                           .asJsonAsync<string>();

            Product.products = JsonConvert.DeserializeObject<List<Product>>(result.Result.Body);
        }

        public void UnirestGetAccounts()
        {
            var result = Unirest.get("http://localhost:85/accounts")
                           .header("Accept", "")
                           .asJsonAsync<string>();

            Account.accounts = JsonConvert.DeserializeObject<List<Account>>(result.Result.Body);
        }

        public void UnirestGetAccountsInfo()
        {
            var result = Unirest.get("http://localhost:85/accountsinfo")
                           .header("Accept", "")
                           .asJsonAsync<string>();

            AccountInfo.accountsInfo = JsonConvert.DeserializeObject<List<AccountInfo>>(result.Result.Body);
        }
        public void UnirestGetOrders()
        {
            var result = Unirest.get("http://localhost:85/orders")
                           .header("Accept", "")
                           .asJsonAsync<string>();

            Order.orders = JsonConvert.DeserializeObject<List<Order>>(result.Result.Body);
        }

        public void UnirestGetUnfinishedOrders()
        {
            var result = Unirest.get("http://localhost:85/unfinishedorders")
                           .header("Accept", "")
                           .asJsonAsync<string>();

            UnfinishedOrder.unfinishedOrders = JsonConvert.DeserializeObject<List<UnfinishedOrder>>(result.Result.Body);
        }

        private void TimerBalanceUpdater_Tick(object sender, EventArgs e)
        {
            
            
            

            if (s==15)
            {
                s = 0;
                lblBalanceUpdate.Visible = false;
                TimerBalanceUpdater.Enabled = false;
                return;
            }
            s++;
            

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddProductForm(this).ShowDialog();
        }

        public void UpdateBalance()
        {
            lblBalance.Text = "Balance: $" + currentAccountInfo.Balance.ToString();
        }

        public void GetCurrentAccount()
        {
            UnirestGetAccounts();
            UnirestGetAccountsInfo();

            foreach (var item in Account.accounts)
            {
                if (item.Id==currentAccountId)
                {
                    currentAccount = item;
                }
            }

            foreach (var item in AccountInfo.accountsInfo)
            {
                if (item.Account_id==currentAccountId)
                {
                    currentAccountInfo = item;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UnirestGetAccounts();
            UnirestGetAccountsInfo();
            UnirestGetOrders();
            UnirestGetProducts();
            UnirestGetUnfinishedOrders();

            currentAccountId = currentAccount.Id;

            foreach (var item in AccountInfo.accountsInfo)
            {
                if (currentAccount.Id==item.Account_id)
                {
                    currentAccountInfo = item;
                }
            }
            
            if (currentAccount.Account_type!="admin")
            {
                lblBalance.Visible = true;
                lblBalance.Text = $"Balance:$ {currentAccountInfo.Balance.ToString()}";
            }

            if (currentAccount.Account_type=="admin")
            addToolStripMenuItem.Visible = true;

            accountsToolStripMenuItem.PerformClick();

            if (currentAccount.Account_type!="admin")
            {
                ordersToolStripMenuItem.Text = "My Finished Orders";
                unfinishedOrdersToolStripMenuItem.Text = "My Orders";
                accountsToolStripMenuItem.Text = "My Account";
            }

            if (currentAccount.Account_type=="courier")
            {
                productsToolStripMenuItem.Visible = false;
            }

        }

        private void unfinishedOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblSearchCriteria.Text = "Order ID:";
            GetUnfinishedOrders();
        }

        public void GetUnfinishedOrders()
        {
            currentData = CurrentData.unfinishedOrders;

            UnirestGetUnfinishedOrders();

            if (currentAccount.Account_type == "client")
            {
                var myOrders = new List<UnfinishedOrder>();
                foreach (var item in UnfinishedOrder.unfinishedOrders)
                {
                    if (item.Client_id == currentAccount.Id)
                    {
                        myOrders.Add(item);
                    }
                }
                panelMain.Controls.Clear();

                foreach (var item in myOrders)
                {
                    var entry = new UnfinishedOrderPreview(item, this, currentAccount, currentAccountInfo);
                    entry.Location = new Point(0, y);
                    panelMain.Controls.Add(entry);
                    y += entry.Height;
                }
                y = 0;
                return;
            }
            

            else
            {
                LoadUnfinishedOrders();
            }

            
        }

        public void LoadUnfinishedOrders()
        {
            panelMain.Controls.Clear();

            foreach (var item in UnfinishedOrder.unfinishedOrders)
            {
                var entry = new UnfinishedOrderPreview(item, this, currentAccount, currentAccountInfo);
                entry.Location = new Point(0, y);
                panelMain.Controls.Add(entry);
                y += entry.Height;
            }
            y = 0;
        }

        private void rgbBackground_Tick(object sender, EventArgs e)
        {
            panelMain.BackColor = Color.FromArgb(r, g, b);
            if (rAdd)
            {
                if (r==255)
                {
                    rAdd = false;
                    bDel = true;
                }
                else
                {
                    r++;
                }
            }
            else if (gAdd)
            {
                if (g==255)
                {
                    gAdd = false;
                    rDeL = true;
                }
                else
                {
                    g++;
                }
            }
            else if (bAdd)
            {
                if (b==255)
                {
                    bAdd = false;
                    gDel = true;
                }
                else
                {
                    b++;
                }
            }
            else if (rDeL)
            {
                if (r==0)
                {
                    rDeL = false;
                    bAdd = true;
                }
                else
                {
                    r--;
                }
            }
            else if (gDel)
            {
                if (g==0)
                {
                    gDel = false;
                    rAdd = true;
                }
                else
                {
                    g--;
                }
            }
            else if (bDel)
            {
                if (b==0)
                {
                    bDel = false;
                    gAdd = true;
                }
                else
                {
                    b--;
                }
            }
        }
    }
}
