namespace AutoReservation.Ui.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(AutoViewModel autoViewModel, KundeViewModel kundeViewModel,
            ReservationViewModel reservationViewModel)
        {
            AutoViewModel = autoViewModel;
            KundeViewModel = kundeViewModel;
            ReservationViewModel = reservationViewModel;
        }

        public async void Init()
        {
            await AutoViewModel.Init();
            await KundeViewModel.Init();
            await ReservationViewModel.Init();
        }

        public AutoViewModel AutoViewModel { get; }

        public KundeViewModel KundeViewModel { get; }

        public ReservationViewModel ReservationViewModel { get; }
    }
}
