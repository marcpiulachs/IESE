using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    [PrimaryKey(nameof(FulFillID), nameof(OrderID))]
    public class FulFill
    {
        public Guid FulFillID { get; set; }
        public Guid OrderID { get; set; }
        public Guid StaffID { get; set; }
        public DateTime FulFillDate { get; set; }
        public string FulFillStatus { get; set; }
        public Customer Customer { get; set; }
        public Staff Staff { get; set; }
        public Order Order { get; set; }
        public List<Package> OrderDetails { get; set; }
    }
}
