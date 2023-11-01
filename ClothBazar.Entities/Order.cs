using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalProducts { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
