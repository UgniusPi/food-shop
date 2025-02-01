using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Shop
{
    public class Account
    {
        public static List<Account> accounts = new List<Account>();

        public ulong Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Account_type { get; set; }
    }
}
