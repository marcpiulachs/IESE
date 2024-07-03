using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly INavigationStore _navigationStore;
        private readonly IAuthenticationStore _authenticationStore;
        public IAuthenticationStore AuthenticationStore => _authenticationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public DialogViewModelBase CurrentDialogViewModel => _navigationStore.CurrentDialogViewModel;

        public bool IsLoggedIn => _authenticationStore.IsLoggedIn;

        public Staff CurrentStaff => _authenticationStore.CurrentStaff;

        public IUnitOfWork unitOfWork = new UnitOfWork();

        private bool _IsDialogOpen;
        public bool IsDialogOpen
        {
            get => _IsDialogOpen;
            set => SetProperty(ref _IsDialogOpen, value);
        }

        public MainViewModel(INavigationStore navigationStore, IAuthenticationStore authenticationStore)
        {
            _navigationStore = navigationStore;
            _authenticationStore = authenticationStore;
            
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore, _authenticationStore, unitOfWork);

            _navigationStore.CurrentDialogViewModelChanged += _navigationStore_CurrentDialogViewModelChanged;
            _navigationStore.CurrentDialogClosed += _navigationStore_CurrentDialogClosed;

            _authenticationStore.IsLoggedIn = false;
            _authenticationStore.IsLoggedInChanged += OnIsLoggedInChanged;

            _authenticationStore.IsCurrentStaffChanged += OnIsCurrentStaffChanged;
        }

        private void _navigationStore_CurrentDialogClosed()
        {
            IsDialogOpen = false;
            OnPropertyChanged(nameof(IsDialogOpen));
            OnPropertyChanged(nameof(CurrentDialogViewModel));
        }

        private void _navigationStore_CurrentDialogViewModelChanged()
        {
            // OnPropertyChanged(nameof(DialogViewModel));
            IsDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
            OnPropertyChanged(nameof(CurrentDialogViewModel));
        }

        [RelayCommand]
        private void LogOut()
        {
            _authenticationStore.CurrentStaff = null;
            _authenticationStore.IsLoggedIn = false;
            _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore, _authenticationStore, unitOfWork);
        }

        [RelayCommand]
        private void NavigateToDashboard()
        {
            
            _navigationStore.CurrentViewModel = new DashboardViewModel(_navigationStore, unitOfWork);
        }

        [RelayCommand]
        private void NavigateToLogList()
        {

            _navigationStore.CurrentViewModel = LogListViewModel.LoadViewModel(_navigationStore, unitOfWork);
        }

        [RelayCommand]
        public void NavigateToStorageList()
        {
            _navigationStore.CurrentViewModel = StorageListViewModel.LoadViewModel(_navigationStore, unitOfWork);
        }

        [RelayCommand]
        public void NavigateToRoleList()
        {
            _navigationStore.CurrentViewModel = RoleListViewModel.LoadViewModel(_navigationStore, unitOfWork);
        }

        [RelayCommand]
        public void NavigateToLocationList()
        {
            _navigationStore.CurrentViewModel =LocationListViewModel.LoadViewModel(_navigationStore, unitOfWork);
        }

        [RelayCommand]
        public void NavigateToCategoryList()
        {
            _navigationStore.CurrentViewModel = CategoryListViewModel.LoadViewModel(_navigationStore, unitOfWork);
        }

        [RelayCommand]
        public void NavigateToSupplierList()
        {
            _navigationStore.CurrentViewModel = SupplierListViewModel.LoadViewModel(_navigationStore, unitOfWork);
        }

        [RelayCommand]
        public void NavigateToCarrierList()
        {
            _navigationStore.CurrentViewModel = App.Current.Services.GetRequiredService<CarrierListViewModel>();
        }

        [RelayCommand]
        public void NavigateToStaffList()
        {
            _navigationStore.CurrentViewModel = StaffListViewModel.LoadViewModel(_navigationStore, unitOfWork);
        }

        [RelayCommand]
        public void NavigateToProductList()
        {
            _navigationStore.CurrentViewModel = ProductListViewModel.LoadViewModel(_navigationStore, unitOfWork);
        }

        [RelayCommand]
        public void NavigateToCustomerList()
        {
            _navigationStore.CurrentViewModel = CustomerListViewModel.LoadViewModel(_navigationStore, unitOfWork);
        }

        [RelayCommand]
        public void NavigateToOrderList()
        {
            _navigationStore.CurrentViewModel = OrderListViewModel.LoadViewModel(_navigationStore, unitOfWork);
        }

        [RelayCommand]
        public void NavigateToDefectiveList()
        {
            _navigationStore.CurrentViewModel = DefectiveListViewModel.LoadViewModel(_navigationStore, unitOfWork);
        }

        [RelayCommand]
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        [RelayCommand]
        private void OnIsLoggedInChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        [RelayCommand]
        private void OnIsCurrentStaffChanged()
        {
            OnPropertyChanged(nameof(CurrentStaff));
        }
    }
}
