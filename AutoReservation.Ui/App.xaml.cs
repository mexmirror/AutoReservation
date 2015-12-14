using System.Windows;
using AutoReservation.Ui.ViewModels;
using Ninject;

namespace AutoReservation.Ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private IKernel _kernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _kernel = LoadNinject();

            var vm = _kernel.Get<MainWindowViewModel>();
            var view = new MainWindow {DataContext = vm};
            vm.Init();
            MainWindow = view;
            MainWindow.Show();
        }

        private IKernel LoadNinject()
        {
            var kernel = new StandardKernel(new AutoReservationModule());
            kernel.Load("Dependencies.Ninject.xml");
            return kernel;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _kernel.Dispose();
            base.OnExit(e);
        }
    }
}
