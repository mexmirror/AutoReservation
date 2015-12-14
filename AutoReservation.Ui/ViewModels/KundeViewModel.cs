using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Extensions;
using AutoReservation.Ui.Factory;

namespace AutoReservation.Ui.ViewModels
{
    public class KundeViewModel : ViewModelBase
    {
        private readonly List<KundeDto> _customersOriginal = new List<KundeDto>();

        public KundeViewModel(IServiceFactory factory) : base(factory)
        {
            
        }

        public ObservableCollection<KundeDto> Kunden { get; } = new ObservableCollection<KundeDto>();

        private KundeDto _selectedKunde;
        public KundeDto SelectedKunde
        {
            get { return _selectedKunde; }
            set
            {
                if (_selectedKunde == value)
                {
                    return;
                }
                _selectedKunde = value;
                this.OnPropertyChanged(p => p.SelectedKunde);
            }
        }


        #region Load-Command

        private RelayCommand _loadCommand;
        public ICommand LoadCommand
        {
            get
            {
                return _loadCommand ?? (_loadCommand = new RelayCommand(async param => await Load(), param => CanLoad()));
            }
        }

        protected override async Task Load()
        {
            Kunden.Clear();
            _customersOriginal.Clear();
            foreach (var customer in await Service.GetCustomers())
            {
                Kunden.Add(customer);
                _customersOriginal.Add(customer.Clone());
            }
            SelectedKunde = Kunden.FirstOrDefault();
        }

        private bool CanLoad()
        {
            return ServiceExists;
        }
        #endregion

        #region Save-Command

        private RelayCommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(param => SaveData(), param => CanSaveData()));
            }
        }

        private async void SaveData()
        {
            foreach (var customer in Kunden)
            {
                if (customer.Id == default(int))
                {
                    await Service.InsertCustomer(customer);
                }
                else
                {
                    var original = _customersOriginal.FirstOrDefault(ao => ao.Id == customer.Id);
                    await Service.UpdateCustomer(customer, original);
                }
            }
            await Load();
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

        private RelayCommand _newCommand;
        public ICommand NewCommand
        {
            get
            {
                return _newCommand ?? (_newCommand = new RelayCommand(param => New(), param => CanNew()));
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

        private RelayCommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new RelayCommand(param => Delete(), param => CanDelete()));
            }
        }
        private async void Delete()
        {
            await Service.DeleteCustomer(SelectedKunde);
            await Load();
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
