using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static ProjectLex.InventoryManagement.Desktop.Utilities.Constants;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public partial class EditCarrierViewModel : AddEditEntityBase<Carrier>
    {
        //private bool _isDisposed = false;

        //private Carrier _Carrier;

        [ObservableProperty]
        [Required(ErrorMessage = "Name is Required")]
        [MinLength(2, ErrorMessage = "Name should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Name longer than 50 characters is Not Allowed")]
        public string carrierName;

        [ObservableProperty]
        [Required(ErrorMessage = "Address is Required")]
        [MinLength(10, ErrorMessage = "Address should be longer than 2 characters")]
        public string carrierAddress;

        [ObservableProperty]
        [Required(ErrorMessage = "Phone number is Required")]
        private string carrierPhone;

        [ObservableProperty]
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Invalid Email Format")]
        private string carrierEmail;

        [ObservableProperty]
        [Required(ErrorMessage = "Status is Required")]
        private string carrierStatus;

        private readonly IUnitOfWork _unitOfWork;
        private readonly INavigationStore _navigationStore;

        public EditCarrierViewModel(/*INavigationStore navigationStore, IUnitOfWork unitOfWork, Carrier Carrier*/)
        {
            //_navigationStore = navigationStore;
            //_unitOfWork = unitOfWork;

            //_Carrier = Carrier;
            //SetInitialValues(_Carrier);
        }

        public void SetInitialValues()
        {
            CarrierName = Entity.CarrierName;
            CarrierAddress = Entity.CarrierAddress;
            CarrierEmail = Entity.CarrierEmail;
            CarrierPhone = Entity.CarrierPhone;
            CarrierStatus = Entity.CarrierStatus;
        }

        private void FromVMToEntity()
        {
            Entity.CarrierName = CarrierName;
            Entity.CarrierAddress = CarrierAddress;
            Entity.CarrierEmail = CarrierEmail;
            Entity.CarrierPhone = CarrierPhone;
            Entity.CarrierStatus = CarrierStatus;
        }

        [RelayCommand]
        private void Submit()
        {
            FromVMToEntity();
            /*
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }


            _unitOfWork.CarrierRepository.Update(_Carrier);
            _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.CARRIERS, ActionType.UPDATE, $"Carrier updated; CarrierID: {_Carrier.CarrierID};"));
            _unitOfWork.Save();
            */

            Save();

            //Close();
        }

        [RelayCommand]
        private void Cancel()
        {
            Close();
        }

        public static EditCarrierViewModel LoadViewModel(INavigationStore navigationStore, IUnitOfWork unitOfWork, Carrier Carrier)
        {
            EditCarrierViewModel viewModel = new EditCarrierViewModel() { Entity = Carrier, PopupType = PopupTypeEnum.Edit }; //navigationStore, unitOfWork, Carrier);
            viewModel.SetInitialValues();
            return viewModel;
        }
    }
}
