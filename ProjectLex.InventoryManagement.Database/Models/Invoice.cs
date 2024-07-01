using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    //[PrimaryKey(nameof(InvoiceID), nameof(OrderID))]
    public class Invoice
    {
        public Guid InvoiceID { get; set; }
        public Guid OrderID { get; set; }
        public Guid StaffID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceStatus { get; set; }
        public Customer Customer { get; set; }
        public Staff Staff { get; set; }
        public Order Order { get; set; }
    }
}
