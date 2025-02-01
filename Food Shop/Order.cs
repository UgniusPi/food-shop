using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Shop
{
    public class Order
    {
        public static List<Order> orders = new List<Order>();
        public ulong Id { get; set; }
        public ulong Client_id { get; set; }
        public ulong Courier_id { get; set; }
        public ulong Product_id { get; set; }
        public float Delivery_fee { get; set; }
    }
}
