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
    public partial class EditAccountForm : Form
    {
        Account account;
        Form1 mainForm;
        Account currentAccount;
        public EditAccountForm(Account account, Form1 form, Account currentAccount)
        {
            InitializeComponent();

            this.account = account;
            this.mainForm = form;
            this.currentAccount = currentAccount;

            cmbAccountType.SelectedItem = account.Account_type;
            txtEmail.Text = account.Email;
            txtPassword.Text = account.Password;
            txtUsername.Text = account.Username;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var request = Unirest.put("http://localhost:85/accounts?id=" + account.Id)
                .body(new Dictionary<string, string>
                {
                    {"username", txtUsername.Text },
                    {"email", txtEmail.Text },
                    {"password", txtPassword.Text },
                    {"account_type", cmbAccountType.SelectedItem.ToString() }
                })
                .asJsonAsync<string>();

            var result = request.Result;
            mainForm.GetAccounts(currentAccount);
            MessageBox.Show(result.Body.ToString());
            
        }
    }
}
