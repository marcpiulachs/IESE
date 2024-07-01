using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CarrierViewModel : ViewModelBase
    {
        private readonly Carrier _Carrier;
        public Carrier Carrier => _Carrier;

        public string CarrierID => _Carrier.CarrierID.ToString();
        public string CarrierName => _Carrier.CarrierName;
        public string CarrierAddress => _Carrier.CarrierAddress;
        public string CarrierPhone => _Carrier.CarrierPhone;
        public string CarrierEmail => _Carrier.CarrierEmail;
        public string CarrierStatus => _Carrier.CarrierStatus;

        public CarrierViewModel(Carrier Carrier)
        {
            _Carrier = Carrier;
        }
    }
}
