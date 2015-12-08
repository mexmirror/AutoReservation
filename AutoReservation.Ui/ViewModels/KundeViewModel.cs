using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Extensions;
using AutoReservation.Ui.Factory;

namespace AutoReservation.Ui.ViewModels
{
    public class KundeViewModel : ViewModelBase
    {
        private readonly List<KundeDto> customersOriginal = new List<KundeDto>();

        public KundeViewModel(IServiceFactory factory) : base(factory)
        {
            
        }

        public ObservableCollection<KundeDto> Kunden { get; } = new ObservableCollection<KundeDto>();

        private KundeDto selectedKunde;
        public KundeDto SelectedKunde
        {
            get { return selectedKunde; }
            set
            {
                if (selectedKunde == value)
                {
                    return;
                }
                selectedKunde = value;
                this.OnPropertyChanged(p => p.selectedKunde);
            }
        }


        #region Load-Command

        private RelayCommand loadCommand;
        public ICommand LoadCommand
        {
            get
            {
                return loadCommand ?? (loadCommand = new RelayCommand(param => Load(), param => CanLoad()));
            }
        }

        protected override async void Load()
        {
            Kunden.Clear();
            customersOriginal.Clear();
            foreach (var customer in await Service.GetCustomers())
            {
                Kunden.Add(customer);
                customersOriginal.Add(customer.Clone());
            }
            SelectedKunde = Kunden.FirstOrDefault();
        }

        private bool CanLoad()
        {
            return ServiceExists;
        }
        #endregion

        #region Save-Command

        private RelayCommand saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(param => SaveData(), param => CanSaveData()));
            }
        }

        private void SaveData()
        {
            foreach (var customer in Kunden)
            {
                if (customer.Id == default(int))
                {
                    Service.InsertCustomer(customer);
                }
                else
                {
                    var original = customersOriginal.FirstOrDefault(ao => ao.Id == customer.Id);
                    Service.UpdateCustomer(customer, original);
                }
            }
            Load();
        }


        private bool CanSaveData()
        {
            if (!ServiceExists)
            {
                return false;
            }

            return Validate(Kunden);
        }


        #endregion

        #region New-Command

        private RelayCommand newCommand;
        public ICommand NewCommand
        {
            get
            {
                return newCommand ?? (newCommand = new RelayCommand(param => New(), param => CanNew()));
            }
        }

        private void New()
        {
            Kunden.Add(new KundeDto());
        }

        private bool CanNew()
        {
            return ServiceExists;
        }
        #endregion

        #region Delete-Command

        private RelayCommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(param => Delete(), param => CanDelete()));
            }
        }
        private void Delete()
        {
            Service.DeleteCustomer(SelectedKunde);
            Load();
        }

        private bool CanDelete()
        {
            return
                ServiceExists &&
                SelectedKunde != null &&
                SelectedKunde.Id != default(int);
        }

        #endregion
    }
}
