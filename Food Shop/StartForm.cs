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
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();

            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().Show();
            
        }


        private void btnSignUp_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            new SignUpForm().Show();
        }
    }
}
