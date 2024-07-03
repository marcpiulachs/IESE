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
    public partial class CreateCarrierViewModel : DialogViewModelBase
    {
        private bool _isDisposed = false;

        public string _CarrierName;

        [Required(ErrorMessage = "Name is Required")]
        [MinLength(2, ErrorMessage = "Name should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Name longer than 50 characters is Not Allowed")]
        public string CarrierName
        {
            get => _CarrierName;
            set
            {
                SetProperty(ref _CarrierName, value, true);
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
                SetProperty(ref _CarrierAddress, value, true);
            }
        }

        private string _CarrierPhone;

        [Required(ErrorMessage = "Phone number is Required")]
        public string CarrierPhone
        {
            get => _CarrierPhone;
            set
            {
                SetProperty(ref _CarrierPhone, value, true);
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
                SetProperty(ref _CarrierEmail, value, true);
            }
        }


        private string _CarrierStatus;

        [Required(ErrorMessage = "Status is Required")]
        public string CarrierStatus
        {
            get { return _CarrierStatus; }
            set
            {
                SetProperty(ref _CarrierStatus, value, true);
            }
        }


        private readonly IUnitOfWork _unitOfWork;
        private readonly INavigationStore _navigationStore;

        public CreateCarrierViewModel(INavigationStore navigationStore, IUnitOfWork unitOfWork)
        {
            _navigationStore = navigationStore;
            _unitOfWork = unitOfWork;
        }

        [RelayCommand]
        private void Submit()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }

            Carrier Carrier = new Carrier()
            {
                CarrierID = Guid.NewGuid(),
                CarrierName = CarrierName,
                CarrierAddress = CarrierAddress,
                CarrierPhone = CarrierPhone,
                CarrierEmail = CarrierEmail,
                CarrierStatus = CarrierStatus
            };

            _unitOfWork.CarrierRepository.Insert(Carrier);

            _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.CARRIERS, ActionType.CREATE, $"New Carrier Created; CarrierID: {Carrier.CarrierID};"));

            _unitOfWork.Save();

            Close();
        }

        [RelayCommand]
        private void Cancel()
        {
            Close();
        }

        public static CreateCarrierViewModel LoadViewModel(INavigationStore navigationStore, IUnitOfWork unitOfWork)
        {
            return new CreateCarrierViewModel(navigationStore, unitOfWork);
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

        public bool CanCreateCarrier(object obj)
        {
            return true;
        }
    }
}
