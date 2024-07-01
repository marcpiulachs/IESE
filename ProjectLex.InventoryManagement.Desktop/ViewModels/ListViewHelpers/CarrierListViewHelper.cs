using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels.ListViewHelpers
{
    public class CarrierListViewHelper : ListViewHelperBase<CarrierViewModel>
    {
        

        

        public CarrierListViewHelper(ObservableCollection<CarrierViewModel> databaseCollection, ObservableCollection<CarrierViewModel> displayCollection)
            :base(databaseCollection, displayCollection)
        {

        }



        protected override bool FilterCollection(CarrierViewModel obj)
        {
            if(obj is CarrierViewModel viewModel)
            {
                return viewModel.CarrierName.Contains(Filter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
