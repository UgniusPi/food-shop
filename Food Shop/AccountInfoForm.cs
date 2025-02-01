using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Food_Shop
{
    public partial class AccountInfoForm : Form
    {
        Account account;
        public AccountInfoForm(Account account)
        {
            InitializeComponent();

            this.account = account;

            var accountInfo = new AccountInfo();

            foreach (var item in AccountInfo.accountsInfo)
            {
                if (item.Account_id==account.Id)
                {
                    accountInfo = item;
                }
            }

            if (account.Account_type=="admin")
            {
                lblBalance.Visible = false;
            }

            lblAge.Text = "Age: " + accountInfo.Age;
            lblBalance.Text = "Balance: $" + accountInfo.Balance;
            lblName.Text = "Name: " + accountInfo.Name;
            lblSurname.Text = "Surname: " + accountInfo.Surname;
            lblUsername.Text = account.Username;
        }
    }
}
