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
    public partial class SignUpForm : Form
    {
        Account newAccount;
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void txtPassword2_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text == txtPassword2.Text)
            {
                txtPassword2.ForeColor = Color.Green;
            }
            else
                txtPassword2.ForeColor = Color.Black;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text == txtPassword2.Text)
            {
                txtPassword2.ForeColor = Color.Green;
            }
            else
                txtPassword2.ForeColor = Color.Black;
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAge.Text) || string.IsNullOrEmpty(txtBalance.Text) || string.IsNullOrEmpty(txtEmail.Text)
                || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtSurname.Text) 
                || string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Missing data!");
                return;
            }
            else if (txtPassword.Text!=txtPassword2.Text)
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }

            var request = Unirest.post("http://localhost:85/accounts")
                .body(new Dictionary<string, string>
                {
                    {"username", txtUsername.Text },
                    {"password", txtPassword.Text },
                    {"email", txtEmail.Text },
                    {"account_type", cmbAccountType.SelectedItem.ToString() }
                })
                .asJsonAsync<string>();

            var result = Unirest.get("http://localhost:85/accounts")
                           .header("Accept", "")
                           .asJsonAsync<string>();

            Account.accounts = JsonConvert.DeserializeObject<List<Account>>(result.Result.Body);

            var currentAccount = new Account();
            foreach (var item in Account.accounts)
            {
                currentAccount = item;
            }


            var request2 = Unirest.post("http://localhost:85/accounts_info")
                .body(new Dictionary<string, object>
                {
                    {"account_id", Convert.ToInt64(currentAccount.Id)},
                    {"name", txtName.Text },
                    {"surname", txtSurname.Text },
                    {"age", Convert.ToInt32(txtAge.Text) },
                    {"balance", Convert.ToDecimal(txtBalance.Text) }
                })
                .asJsonAsync<string>();

            MessageBox.Show(request2.Result.Body);

            //MessageBox.Show("Account Added");
        }
    }
}
