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
    //[PrimaryKey(nameof(PackageID), nameof(OrderID))]
    public class Package
    {
        public Guid PackageID { get; set; }
        public Guid OrderID { get; set; }
        public decimal Weight { get; set; }
        public Order Order { get; set; }
    }
}
