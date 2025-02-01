using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Shop
{
    public class AccountInfo
    {
        public static List<AccountInfo> accountsInfo = new List<AccountInfo>();
        public ulong Id { get; set; }
        public ulong Account_id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public uint Age { get; set; }
        public float Balance { get; set; }
    }
}
