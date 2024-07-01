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
    public class EditCarrierViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Carrier _Carrier;

        public string _CarrierName;

        [Required(ErrorMessage = "Name is Required")]
        [MinLength(2, ErrorMessage = "Name should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Name longer than 50 characters is Not Allowed")]
        public string CarrierName
        {
            get => _CarrierName;
            set
            {
                SetProperty(ref _CarrierName, value);
            }

        }


        private string _CarrierAddress;

        [Required(ErrorMessage = "Address is Required")]
        [MinLength(10, ErrorMessage = "Address should be longer than 2 characters")]
        public string CarrierAddress
        {
            get => _CarrierAddress;
            set
            {
                SetProperty(ref _CarrierAddress, value);
            }
        }


        private string _CarrierPhone;

        [Required(ErrorMessage = "Phone number is Required")]
        public string CarrierPhone
        {
            get => _CarrierPhone;
            set
            {
                SetProperty(ref _CarrierPhone, value);
            }
        }


        private string _CarrierEmail;

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Invalid Email Format")]
        public string CarrierEmail
        {
            get => _CarrierEmail;
            set
            {
                SetProperty(ref _CarrierEmail, value);
            }
        }


        private string _CarrierStatus;

        [Required(ErrorMessage = "Status is Required")]
        public string CarrierStatus
        {
            get { return _CarrierStatus; }
            set
            {
                SetProperty(ref _CarrierStatus, value);
            }
        }


        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;
        private readonly Action _closeDialogCallback;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }

        public EditCarrierViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Carrier Carrier, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _unitOfWork = unitOfWork;
            _closeDialogCallback = closeDialogCallback;

            _Carrier = Carrier;
            SetInitialValues(_Carrier);



            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void SetInitialValues(Carrier Carrier)
        {
            CarrierName = Carrier.CarrierName;
            CarrierAddress = Carrier.CarrierAddress;
            CarrierEmail = Carrier.CarrierEmail;
            CarrierPhone = Carrier.CarrierPhone;
            CarrierStatus = Carrier.CarrierStatus;
        }


        private void Submit()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }

            _Carrier.CarrierName = _CarrierName;
            _Carrier.CarrierAddress = _CarrierAddress;
            _Carrier.CarrierEmail = _CarrierEmail;
            _Carrier.CarrierPhone = _CarrierPhone;
            _Carrier.CarrierStatus = _CarrierStatus;

            _unitOfWork.CarrierRepository.Update(_Carrier);
            _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.CARRIERS, ActionType.UPDATE, $"Carrier updated; CarrierID: {_Carrier.CarrierID};"));
            _unitOfWork.Save();
            _closeDialogCallback();
        }


        private void Cancel()
        {
            _closeDialogCallback();
        }

        public static EditCarrierViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Carrier Carrier, Action closeDialogCallback)
        {
            EditCarrierViewModel viewModel = new EditCarrierViewModel(navigationStore, unitOfWork, Carrier, closeDialogCallback);
            return viewModel;
        }

        

        protected override void Dispose(bool disposing)
        {
            if(!this._isDisposed)
            {
                if(disposing)
                {
                    // dispose managed resources
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }


        public bool CanModifyCarrier(object obj)
        {
            return true;
        }
    }
}
