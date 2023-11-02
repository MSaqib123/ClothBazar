using ClothBazar.Entities.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entities
{
    public class OrderVM
    {

        //_______ MultiRecord __________
        public List<Order> orderList { get; set; }

        //_______ Single Record ______________
        public List<OrderUserInfo> OrderdUserInfo { get; set; }
    }
}
