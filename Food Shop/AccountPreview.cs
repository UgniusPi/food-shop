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
    public partial class AccountPreview : UserControl
    {
        Account account;
        Form1 mainForm;
        Account currentAccount;
        public AccountPreview(Account account, Form1 form, Account currentAccount)
        {
            InitializeComponent();

            this.account = account;
            this.mainForm = form;
            this.currentAccount = currentAccount;

            lblAccountType.Text = "Account Type: " + account.Account_type;
            lblEmail.Text = account.Email;
            lblPassword.Text = "Password: " + account.Password;
            lblUsername.Text = account.Username;

            if(currentAccount.Account_type!="admin")
            {
                btnDelete.Visible = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = Unirest.delete("http://localhost:85/accounts?id=" + account.Id)
              .header("Accept", "")
              .asJsonAsync<string>();

            mainForm.GetAccounts(currentAccount);
            MessageBox.Show(result.Result.Body);
            
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            new AccountInfoForm(account).ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            
            new EditAccountForm(account, mainForm, currentAccount).ShowDialog();


        }
    }
}
