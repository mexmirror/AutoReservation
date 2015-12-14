using System.Collections.Generic;
using AutoReservation.Common.Interfaces;
using AutoReservation.Ui.Factory;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.DataTransferObjects.Core;
using AutoReservation.Common.Extensions;

namespace AutoReservation.Ui.ViewModels
{
    public abstract class ViewModelBase : IExtendedNotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IServiceFactory _factory;

        protected ViewModelBase(IServiceFactory factory)
        {
            _factory = factory;
        }

        protected IAutoReservationService Service { get; private set; }

        public bool ServiceExists => Service  != null;

        public async Task Init()
        {
            Service = _factory.GetService();
            await Load();
        }

        protected abstract Task Load();

        protected bool Validate(IEnumerable<IValidatable> items) 
        {
            var errorText = new StringBuilder();
            foreach (var item in items)
            {
                var error = item.Validate();
                if (!string.IsNullOrEmpty(error))
                {
                    errorText.AppendLine(item.ToString());
                    errorText.AppendLine(error);
                }
            }

            ErrorText = errorText.ToString();
            return string.IsNullOrEmpty(ErrorText);
        }

        private string _errorText;
        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                if (_errorText == value)
                {
                    return;
                }
                _errorText = value;
                this.OnPropertyChanged(p => p.ErrorText);
            }
        }

        #region Helper Methods
        
        void IExtendedNotifyPropertyChanged.OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
