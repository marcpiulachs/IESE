using CommunityToolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.Utilities;
using ProjectLex.InventoryManagement.Desktop.ViewModels.ListViewHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static ProjectLex.InventoryManagement.Desktop.Utilities.Constants;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class CarrierListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;
        
        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;

        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;

        private readonly ObservableCollection<CarrierViewModel> _Carriers;
        public ObservableCollection<CarrierViewModel> Carriers { get; }

        public CarrierListViewHelper CarrierListViewHelper { get; }

        public ICommand ToCreateCarrierCommand { get; }
        public RelayCommand LoadCarriersCommand { get; }
        public RelayCommand<CarrierViewModel> RemoveCarrierCommand { get; }
        public RelayCommand<CarrierViewModel> EditCarrierCommand { get; }
        public RelayCommand CreateCarrierCommand { get; }

        public CarrierListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _Carriers = new ObservableCollection<CarrierViewModel>();
            Carriers = new ObservableCollection<CarrierViewModel>();
            CarrierListViewHelper = new CarrierListViewHelper(_Carriers, Carriers);

            LoadCarriersCommand = new RelayCommand(LoadCarriers);
            RemoveCarrierCommand = new RelayCommand<CarrierViewModel>(RemoveCarrier);
            EditCarrierCommand = new RelayCommand<CarrierViewModel>(EditCarrier);
            CreateCarrierCommand = new RelayCommand(CreateCarrier);
        }

        private void RemoveCarrier(CarrierViewModel CarrierViewModel)
        {
            var result = MessageBox.Show("Do you really want to remove this item?", "Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _unitOfWork.CarrierRepository.Delete(CarrierViewModel.Carrier);
                _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.CARRIERS, ActionType.DELETE, $"Carrier deleted; CarrierID:{CarrierViewModel.CarrierID};"));
                _unitOfWork.Save();
                _Carriers.Remove(CarrierViewModel);
                CarrierListViewHelper.RefreshCollection();
                MessageBox.Show("Successful");
            }
        }


        private void CreateCarrier()
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = CreateCarrierViewModel.LoadViewModel(_navigationStore, _unitOfWork, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }


        private void EditCarrier(CarrierViewModel CarrierViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = EditCarrierViewModel.LoadViewModel(_navigationStore, _unitOfWork, CarrierViewModel.Carrier, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }


        private void CloseDialogCallback()
        {
            LoadCarriersCommand.Execute(null);

            _isDialogOpen = false;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void LoadCarriers()
        {
            _Carriers.Clear();
            foreach(Carrier s in _unitOfWork.CarrierRepository.Get())
            {
                _Carriers.Add(new CarrierViewModel(s));
            }
            CarrierListViewHelper.RefreshCollection();
        }

        public static CarrierListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            CarrierListViewModel viewModel = new CarrierListViewModel(navigationStore);
            viewModel.LoadCarriersCommand.Execute(null);
            return viewModel;
        }

        protected override void Dispose(bool disposing)
        {
            //Note: Implement finalizer only if the object have unmanaged resources

            if (!this._isDisposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                    _unitOfWork.Dispose();
                    _dialogViewModel?.Dispose();
                    CarrierListViewHelper.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
