using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Services;
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
    public partial class CarrierListViewModel : ViewModelBase
    {
        private bool _isDisposed = false;
        
        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;

        private readonly IUnitOfWork _unitOfWork;
        private readonly INavigationStore _navigationStore;

        private readonly ObservableCollection<CarrierViewModel> _Carriers;
        public ObservableCollection<CarrierViewModel> Carriers { get; }

        public CarrierListViewHelper CarrierListViewHelper { get; }

        public RelayCommand<CarrierViewModel> RemoveCarrierCommand { get; }
        public RelayCommand<CarrierViewModel> EditCarrierCommand { get; }

        public CarrierListViewModel(INavigationStore navigationStore, IUnitOfWork unitOfWork)
        {
            _navigationStore = navigationStore;
            _unitOfWork = unitOfWork;
            _Carriers = new ObservableCollection<CarrierViewModel>();
            Carriers = new ObservableCollection<CarrierViewModel>();
            CarrierListViewHelper = new CarrierListViewHelper(_Carriers, Carriers);

            RemoveCarrierCommand = new RelayCommand<CarrierViewModel>(RemoveCarrier);
            EditCarrierCommand = new RelayCommand<CarrierViewModel>(EditCarrier);

            LoadCarriersCommand.Execute(null);
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

        [RelayCommand]
        private void CreateCarrier()
        {
            //var a = App.Current.Services.GetRequiredService<CreateCarrierViewModel>();

            var a = new EditCarrierViewModel() { Entity = new Carrier(), PopupType = PopupTypeEnum.Add };

            _navigationStore.ShowDialog(a, () => {
                LoadCarriersCommand.Execute(null);
            });

            /*
            _dialogViewModel?.Dispose();
            //_dialogViewModel = CreateCarrierViewModel.LoadViewModel(_navigationStore, _unitOfWork, CloseDialogCallback);

            var b = App.Current.Services.GetRequiredService<CreateCarrierViewModel>();

            var a = App.Current.Services.GetRequiredService<IDialogService>().OpenDialog<CreateCarrierViewModel>(b);
            
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));*/
        }

        private void EditCarrier(CarrierViewModel CarrierViewModel)
        {
            //_dialogViewModel?.Dispose();
            //_dialogViewModel = EditCarrierViewModel.LoadViewModel(_navigationStore, _unitOfWork, CarrierViewModel.Carrier, CloseDialogCallback);

            var a = EditCarrierViewModel.LoadViewModel(_navigationStore, _unitOfWork, CarrierViewModel.Carrier);

            _navigationStore.ShowDialog(a, () => {
                LoadCarriersCommand.Execute(null);
            });

            /*
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));*/

            //LoadCarriersCommand.Execute(null);
        }


        private void CloseDialogCallback()
        {
            LoadCarriersCommand.Execute(null);

            _isDialogOpen = false;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        [RelayCommand]
        private void LoadCarriers()
        {
            _Carriers.Clear();
            foreach(Carrier s in _unitOfWork.CarrierRepository.Get())
            {
                _Carriers.Add(new CarrierViewModel(s));
            }
            CarrierListViewHelper.RefreshCollection();
        }

        /*
        public static CarrierListViewModel LoadViewModel(INavigationStore navigationStore, IUnitOfWork unitOfWork)
        {
            CarrierListViewModel viewModel = new CarrierListViewModel(navigationStore, unitOfWork);
            viewModel.LoadCarriersCommand.Execute(null);
            return viewModel;
        }*/

        protected override void Dispose(bool disposing)
        {
            //Note: Implement finalizer only if the object have unmanaged resources

            if (!this._isDisposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                    //_unitOfWork.Dispose();
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
