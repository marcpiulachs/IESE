using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    [PrimaryKey(nameof(ProductID), nameof(OrderID))]
    public class OrderDetail
    {
        public Guid ProductID { get; set; }
        public Guid OrderID { get; set; }
        public int OrderDetailQuantity { get; set; }
        public int OrderFulFillQuantity { get; set; }
        public string OrderFulFillStatus { get; set; }
        public decimal OrderDetailAmount { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
