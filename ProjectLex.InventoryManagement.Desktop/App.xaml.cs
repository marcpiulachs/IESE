using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectLex.InventoryManagement.Database.Data;
using ProjectLex.InventoryManagement.Database.Models;
using ProjectLex.InventoryManagement.Database.Services;
using System;
using System.Windows;

using ProjectLex.InventoryManagement.Desktop.DAL;
using ProjectLex.InventoryManagement.Desktop.Services;
using ProjectLex.InventoryManagement.Desktop.Stores;
using ProjectLex.InventoryManagement.Desktop.ViewModels;

namespace ProjectLex.InventoryManagement.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            SplashScreen splashScreen = new SplashScreen(@"./Assets/SplashScreen.png");
            splashScreen.Show(true);

            Services = ConfigureServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = Services.GetService<MainViewModel>()
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<INavigationStore, NavigationStore>();
            services.AddSingleton<IAuthenticationStore, AuthenticationStore>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();

            services.AddTransient<LoginViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<CarrierListViewModel>();
            services.AddTransient<CreateCarrierViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
