using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    [PrimaryKey(nameof(ProductID), nameof(LocationID))]
    public class ProductLocation
    {
        public Guid LocationID { get; set; }
        public Guid ProductID { get; set; }
        public int ProductQuantity { get; set; }
        public Location Location { get; set; }
        public Product Product { get; set; }
    }
}
