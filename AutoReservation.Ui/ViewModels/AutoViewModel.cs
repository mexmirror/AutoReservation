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
    public class AutoViewModel : ViewModelBase
    {
        private readonly List<AutoDto> _autosOriginal = new List<AutoDto>();

        public AutoViewModel(IServiceFactory factory) : base(factory)
        {
            
        }

        public ObservableCollection<AutoDto> Autos { get; } = new ObservableCollection<AutoDto>();

        private AutoDto _selectedAuto;
        public AutoDto SelectedAuto
        {
            get { return _selectedAuto; }
            set
            {
                if (_selectedAuto == value)
                {
                    return;
                }
                _selectedAuto = value;
                this.OnPropertyChanged(p => p.SelectedAuto);
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
            Autos.Clear();
            _autosOriginal.Clear();
            foreach (var auto in await Service.GetCars())
            {
                Autos.Add(auto);
                _autosOriginal.Add(auto.Clone());
            }
            SelectedAuto = Autos.FirstOrDefault();
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
            foreach (var auto in Autos)
            {
                if (auto.Id == default(int))
                {
                    await Service.InsertCar(auto);
                }
                else
                {
                    var original = _autosOriginal.FirstOrDefault(ao => ao.Id == auto.Id);
                    await Service.UpdateCar(auto, original);
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

            return Validate(Autos);
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
            Autos.Add(new AutoDto());
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
            await Service.DeleteCar(SelectedAuto);
            await Load();
        }

        private bool CanDelete()
        {
            return
                ServiceExists &&
                SelectedAuto != null &&
                SelectedAuto.Id != default(int);
        }

        #endregion

    }
}