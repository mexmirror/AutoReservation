using System;
using System.Runtime.Serialization;
using System.Text;
using AutoReservation.Common.Extensions;
using AutoReservation.Common.DataTransferObjects.Core;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class AutoDto : DtoBase<AutoDto>
    {
        private int? _basistarif;
        private int _id;
        private string _marke;
        private int _tagestarif;
        private AutoKlasse _autoKlasse;

        [DataMember]
        public int? Basistarif
        {
            get { return _basistarif; }
            set
            {
                if (_basistarif == value)
                {
                    return;
                }
                _basistarif = value;
                this.OnPropertyChanged(a => a.Basistarif);
            }
        }
        [DataMember]
        public int Id
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
        [DataMember]
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
        [DataMember]
        public int Tagestarif
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
        [DataMember]
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
