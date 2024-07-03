using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectLex.InventoryManagement.Desktop.Services
{
    public interface IDialogService
    {
        bool? OpenDialog<T>(T viewmodel) where T : ViewModelBase;
    }

    public class DialogService : IDialogService
    {
        public bool? OpenDialog<T>(T viewModel) where T : ViewModelBase
        {
            Window window = new Window
            {
                Title = "My User Control Dialog",
                Content = viewModel,
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize
            };
            window.ShowDialog();

            return window.DialogResult;
        }
    }
}
