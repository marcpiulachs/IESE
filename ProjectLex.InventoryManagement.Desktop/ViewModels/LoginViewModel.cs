using MaterialDesignThemes.Wpf;
using CommunityToolkit.Mvvm.Input;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {

        private bool _isDisposed = false;

        public string text { get; set; }
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        [ObservableProperty]
        private string _username = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        private readonly IUnitOfWork _unitOfWork;

        private readonly INavigationStore _navigationStore;
        private readonly IAuthenticationStore _authenticationStore;

        public LoginViewModel(INavigationStore navigationStore, IAuthenticationStore authenticationStore, IUnitOfWork unitOfWork)
        {
            _navigationStore = navigationStore;
            _unitOfWork = unitOfWork;
            _authenticationStore = authenticationStore;

        }

        [RelayCommand]
        public void Help()
        {
            MessageBox.Show(Application.Current.MainWindow,"Contact Us\n\n" +
                "Email: support.projectlexlabs@outlook.com\n" +
                "Facebook Page: ProjectLex Software Lab", "Help");
        }

        [RelayCommand]
        public void ToggleTheme()
        {
            Theme theme = paletteHelper.GetTheme();
            
            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(BaseTheme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(BaseTheme.Dark);
            }

            paletteHelper.SetTheme(theme);
        }

        [RelayCommand]
        public void ExitApp()
        {
            Application.Current.Shutdown();
        }


        [RelayCommand]
        private void LoginUser()
        {
            Staff storedStaff = _unitOfWork.StaffRepository.Get(s => s.StaffUsername == Username && s.StaffPassword == Password, includeProperties: "Role").SingleOrDefault();

            if(storedStaff == null)
            {
                MessageBox.Show("Invalid Credentials. Please try again.");
                return;
            }

            _authenticationStore.CurrentStaff = storedStaff;
            _authenticationStore.IsLoggedIn = true;

            _navigationStore.CurrentViewModel = App.Current.Services.GetRequiredService<DashboardViewModel>();
        }



        protected override void Dispose(bool disposing)
        {
            //Note: Implement finalizer only if the object have unmanaged resources

            if (!this._isDisposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                    //_unitOfWork.Dispose();
                }

            }
            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
