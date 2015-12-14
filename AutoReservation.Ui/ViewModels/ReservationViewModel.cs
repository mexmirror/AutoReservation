using System;
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
    public class ReservationViewModel : ViewModelBase
    {
        private readonly List<ReservationDto> _reservationenOriginal = new List<ReservationDto>();

        public ReservationViewModel(IServiceFactory factory) : base(factory)
        {
            
        }

        public ObservableCollection<ReservationDto> Reservationen { get; } = new ObservableCollection<ReservationDto>();

        private ReservationDto _selectedReservation;
        public ReservationDto SelectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                if (_selectedReservation == value)
                {
                    return;
                }
                _selectedReservation = value;
                SelectedAutoId = value?.Auto?.Id ?? 0;
                SelectedKundeId = value?.Kunde?.Id ?? 0;

                this.OnPropertyChanged(p => p.SelectedReservation);
            }
        }

        private int _selectedAutoId;
        public int SelectedAutoId
        {
            get { return _selectedAutoId; }
            set
            {
                if (_selectedAutoId == value)
                {
                    return;
                }
                _selectedAutoId = value;
                if (SelectedReservation != null)
                {
                    SelectedReservation.Auto = Autos.SingleOrDefault(a => a.Id == value);
                }

                this.OnPropertyChanged(p => p.SelectedAutoId);
            }
        }

        private int _selectedKundeId;
        public int SelectedKundeId
        {
            get { return _selectedKundeId; }
            set
            {
                if (_selectedKundeId == value)
                {
                    return;
                }
                _selectedKundeId = value;
                if (SelectedReservation != null)
                {
                    SelectedReservation.Kunde = Kunden.SingleOrDefault(k => k.Id == value);
                }

                this.OnPropertyChanged(p => p.SelectedKundeId);
            }
        }

        public ObservableCollection<AutoDto> Autos { get; } = new ObservableCollection<AutoDto>();

        public ObservableCollection<KundeDto> Kunden { get; } = new ObservableCollection<KundeDto>();

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
            Reservationen.Clear();
            _reservationenOriginal.Clear();
            
            Kunden.Clear();
            Autos.Clear();

            foreach (var kunde in await Service.GetCustomers())
            {
                Kunden.Add(kunde);
            }
            foreach (var auto in await Service.GetCars())
            {
                Autos.Add(auto);
            }
            foreach (var reservation in await Service.GetReservations())
            {
                Reservationen.Add(reservation);
                _reservationenOriginal.Add(reservation.Clone());
            }
            SelectedReservation = Reservationen.FirstOrDefault();
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
            foreach (var reservation in Reservationen)
            {
                if (reservation.ReservationNr == default(int))
                {
                    await Service.InsertReservation(reservation);
                }
                else
                {
                    var original = _reservationenOriginal.FirstOrDefault(ao => ao.ReservationNr == reservation.ReservationNr);
                    await Service.UpdateReservation(reservation, original);
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

            return Validate(Reservationen);
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
            Reservationen.Add(new ReservationDto
            {
                Von = DateTime.Today,
                Bis = DateTime.Today
            });
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
            await Service.DeleteReservation(SelectedReservation);
            await Load();
        }

        private bool CanDelete()
        {
            return
                ServiceExists &&
                SelectedReservation != null &&
                SelectedReservation.ReservationNr != default(int);
        }

        #endregion

    }
}