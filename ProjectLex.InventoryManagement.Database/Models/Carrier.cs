using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Database.Models
{
    public class Carrier
    {
        [Key]
        public Guid CarrierID { get; set; }
        public string CarrierName { get; set; }
        public string CarrierAddress { get; set; }
        public string CarrierPhone { get; set; }
        public string CarrierEmail { get; set; }
        public string CarrierStatus { get; set; }
    }
}
