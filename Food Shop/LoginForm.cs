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
    public partial class LoginForm : Form
    {
        bool isAccountValid;
        public LoginForm()
        {
            InitializeComponent();

            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Enter Username and Password!");
                return;
            }

            var result = Unirest.get("http://localhost:85/accounts")
                           .header("Accept", "")
                           .asJsonAsync<string>();

            Account.accounts = JsonConvert.DeserializeObject<List<Account>>(result.Result.Body);

            foreach (var item in Account.accounts)
            {
                if (item.Username==txtUsername.Text && item.Password==txtPassword.Text)
                {
                    isAccountValid = true;
                    new Form1(item).Show();
                    this.Close();
                }
            }
            if (!isAccountValid)
            {
                MessageBox.Show("Username and (or) Password are Incorrent!");
                txtUsername.Clear();
                txtPassword.Clear();
            }
            
        }
    }
}
