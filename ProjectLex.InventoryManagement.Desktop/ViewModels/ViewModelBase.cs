using System;
using System.Windows.Forms;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using ProjectLex.InventoryManagement.Desktop.DAL;

namespace ProjectLex.InventoryManagement.Desktop.ViewModels
{
    public class ViewModelBase : ObservableValidator, IDisposable
    {
        
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                }

            }

            // call methods to cleanup the unamanaged resources

            disposed = true;
        }
    }

    public class DialogViewModelBase : ViewModelBase
    {
        private bool _IsDialogOpen;
        public bool IsDialogOpen
        {
            get => _IsDialogOpen;
            set => SetProperty(ref _IsDialogOpen, value);
        }

        public Action closeDialogCallback;

        protected void Close()
        {
            closeDialogCallback?.Invoke();
        }
    }

    /// <summary>
    /// Enum that represent the Crud PopupwindowType
    /// </summary>
    public enum PopupTypeEnum
    {
        Add,
        Edit
    }

    /// <summary>
    /// Represent the AddEditEntity object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract partial class AddEditEntityBase<T> : DialogViewModelBase where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddEditEntityBase{T}<Entity>"/> class.
        /// </summary>
        public AddEditEntityBase() { }

        #region Public Properties
        /// <summary>
        /// Gets or sets the Crud Pop-up window type.
        /// </summary>
        public PopupTypeEnum PopupType { get; set; }
        /// <summary>
        /// Gets or sets if data saved successfully or not.
        /// </summary>
        public bool IsSavedSuccessfully { get; set; }

        private T _entity;
        /// <summary>
        /// Gets or sets the Entity that will be added and updated.
        /// <para>The Entity object. <see cref="GenericCodes.Core.Entities.Entity>"/></para>
        /// </summary>

        [ObservableProperty]
        public T _Entity;
        #endregion

        #region Public Delegates
        /// <summary>
        /// Represent delegate that will be called after click reset in add mode <see cref="Action"/>
        /// </summary>
        public Action CallBackAfterRestingInAddMode { get; set; }
        /// <summary>
        /// Represent delegate that will be called after click reset in Edit mode <see cref="Action"/>
        /// </summary>
        public Action CallBackWhenRestingInEditMode { get; set; }
        #endregion

        #region Command
        private RelayCommand _saveCommand;
        /// <summary>
        /// The Save command
        /// </summary>
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(Save));
            }
        }


        private RelayCommand _resetCommand;
        /// <summary>
        /// The Reset command
        /// </summary>
        public RelayCommand ResetCommand
        {
            get
            {
                return _resetCommand ?? (_resetCommand = new RelayCommand(ResetValues));
            }
        }
        #endregion

        #region public Methods
        /// <summary>
        /// Reset entity properties values in add and edit mode
        /// </summary>
        public virtual void ResetValues()
        {
            if (PopupType == PopupTypeEnum.Add)
            {
                Entity = new T();
                if (CallBackAfterRestingInAddMode != null)
                    CallBackAfterRestingInAddMode();
            }
            else
            {
                //Entity.CancelEdit();
                if (CallBackWhenRestingInEditMode != null)
                    CallBackWhenRestingInEditMode();

                ValidateAllProperties();
            }
        }

        /// <summary>
        /// The entry point of validate and save entity object in add or edit mode
        /// </summary>
        public virtual void Save()
        {
            ValidateAllProperties();

            if (HasErrors)
                return;

            IsSavedSuccessfully = PopupType == PopupTypeEnum.Add ? InsertEnity() : UpdateEnity();

            if (IsSavedSuccessfully)
                this.Close();
        }

        /// <summary>
        /// Update entity in database 
        /// </summary>
        /// <returns>return true in case entity has been updated successfully otherwise false </returns>
        public virtual bool UpdateEnity()
        {
            int affected = 0;
            var unitOfWork = App.Current.Services.GetRequiredService<IUnitOfWork>();

            var repo = unitOfWork.GenericRepository<T>();
            repo.Update(Entity);
            affected = unitOfWork.Save();

            return affected > 0;
        }

        /// <summary>
        /// Add entity in database 
        /// </summary>
        /// <returns>return true in case entity has been added successfully otherwise false </returns>
        public virtual bool InsertEnity()
        {
            int affected = 0;
            var unitOfWork = App.Current.Services.GetRequiredService<IUnitOfWork>();
            
            var repo = unitOfWork.GenericRepository<T>();
            repo.Insert(Entity);
            affected = unitOfWork.Save();

            return affected > 0;
        }

        /// <summary>
        /// Called while Window is closing 
        /// </summary>
        public virtual void Closing()
        {
            if (PopupType == PopupTypeEnum.Edit)
            {
                if (!IsSavedSuccessfully)
                {
                    //Entity.CancelEdit();
                }

                ValidateAllProperties();
            }
        }
        #endregion
    }
}
