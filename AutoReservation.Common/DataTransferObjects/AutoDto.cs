using System;
using System.Text;
using AutoReservation.Common.Extensions;
using AutoReservation.Common.DataTransferObjects.Core;

namespace AutoReservation.Common.DataTransferObjects
{
    public class AutoDto : DtoBase<AutoDto>
    {
        private double _basistarif;
        private long _id;
        private string _marke;
        private double _tagestarif;
        private AutoKlasse _autoKlasse;

        public double Basistarif
        {
            get { return _basistarif; }
            set
            {
                if (Math.Abs(_basistarif - value) <= Math.Abs(_basistarif*.00001))
                {
                    return;
                }
                _basistarif = value;
                this.OnPropertyChanged(a => a.Basistarif);
            }
        }

        public long Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                {
                    return;
                }
                _id = value;
                this.OnPropertyChanged(a => a.Id);
            }
        }

        public string Marke
        {
            get { return _marke;}
            set
            {
                if (_marke.Equals(value))
                {
                    return;
                }
                _marke = value;
                this.OnPropertyChanged(a => Marke);
            }
        }

        public double Tagestarif
        {
            get { return _tagestarif; }
            set
            {
                if (Math.Abs(_tagestarif - value) <= Math.Abs(_tagestarif*.00001))
                {
                    return;
                }
                _tagestarif = value;
                this.OnPropertyChanged(a => a.Tagestarif);
            }
        }

        public AutoKlasse AutoKlasse
        {
            get { return _autoKlasse; }
            set
            {
                if (_autoKlasse == value)
                {
                    return;
                }
                _autoKlasse = value;
                this.OnPropertyChanged(a => a.AutoKlasse);
            }
        }
        public override string Validate()
        {
            var error = new StringBuilder();
            if (string.IsNullOrEmpty(_marke))
            {
                error.AppendLine("- Marke ist nicht gesetzt.");
            }
            if (_tagestarif <= 0)
            {
                error.AppendLine("- Tagestarif muss grösser als 0 sein.");
            }
            if (AutoKlasse == AutoKlasse.Luxusklasse && _basistarif <= 0)
            {
                error.AppendLine("- Basistarif eines Luxusautos muss grösser als 0 sein.");
            }

            return error.Length == 0 ? null : error.ToString();
        }

        public override AutoDto Clone()
        {
            return new AutoDto
            {
                Id = Id,
                Marke = Marke,
                Tagestarif = Tagestarif,
                AutoKlasse = AutoKlasse,
                Basistarif = Basistarif
            };
        }

        public override string ToString()
        {
            return $"{Id}; {Marke}; {Tagestarif}; {Basistarif}; {AutoKlasse}";
        }
    }
}
