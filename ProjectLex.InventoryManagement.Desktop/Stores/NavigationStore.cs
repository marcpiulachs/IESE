using ProjectLex.InventoryManagement.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.Stores
{
    public class NavigationStore : INavigationStore
    {
        private ViewModelBase _currentViewModel;
        private DialogViewModelBase _dialogViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public DialogViewModelBase CurrentDialogViewModel
        {
            get => _dialogViewModel;/*
            set
            {
                _dialogViewModel?.Dispose();
                _dialogViewModel = value;
                _dialogViewModel.closeDialogCallback = () =>
                {
                    OnCurrentDialogClosed();
                };
                OnCurrentDialogViewModelChanged();
            }*/
        }

        public event Action CurrentDialogClosed;
        public event Action CurrentViewModelChanged;
        public event Action CurrentDialogViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        private void OnCurrentDialogViewModelChanged()
        {
            CurrentDialogViewModelChanged?.Invoke();
        }

        private void OnCurrentDialogClosed()
        {
            CurrentDialogClosed?.Invoke();
        }

        public void ShowDialog(DialogViewModelBase dialog, Action closeCallback)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = dialog;
            _dialogViewModel.closeDialogCallback = () =>
            {
                OnCurrentDialogClosed();
                closeCallback?.Invoke();
            };
            OnCurrentDialogViewModelChanged();
        }
    }
}
