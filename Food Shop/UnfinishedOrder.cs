using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Shop
{
    public class UnfinishedOrder
    {
        public static List<UnfinishedOrder> unfinishedOrders = new List<UnfinishedOrder>();
        public ulong Id { get; set; }
        public ulong Client_id { get; set; }
        public ulong Product_id { get; set; }
        public float Delivery_fee { get; set; }
    }
}
