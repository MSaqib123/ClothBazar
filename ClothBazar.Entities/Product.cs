using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entities
{
    public class Product: BaseEntity
    {
        [Key]
        public int Id { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category Category{ get; set; }
    }
}
