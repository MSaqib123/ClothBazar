using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entities
{
    public class OrderUserInfo
    {
        public int id { get; set; }
        public int orderId {get;set;}
        public string userId { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string userAddress { get; set; }
        public string phoneNumber { get; set; }
    }
}
