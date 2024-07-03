using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;

namespace ProjectLex.InventoryManagement.Desktop.Stores
{
    public interface INavigationStore
    {
        ViewModelBase CurrentViewModel { get; set; }

        event Action CurrentViewModelChanged;

        DialogViewModelBase CurrentDialogViewModel { get; }

        event Action CurrentDialogViewModelChanged;
        event Action CurrentDialogClosed;

        public void ShowDialog(DialogViewModelBase dialog, Action closeCallback);
    }
}