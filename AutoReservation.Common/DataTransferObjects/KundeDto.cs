using System;
using System.Text;
using AutoReservation.Common.Extensions;
using AutoReservation.Common.DataTransferObjects.Core;

namespace AutoReservation.Common.DataTransferObjects
{
    public class KundeDto : DtoBase<KundeDto>
    {
        private DateTime _geburtstdatum;
        private int _id;
        private string _nachname;
        private string _vorname;

        public DateTime Geburtsdatum
        {
            get { return _geburtstdatum;}
            set
            {
                if (_geburtstdatum.Equals(value))
                {
                    return;
                }
                _geburtstdatum = value;
                this.OnPropertyChanged(a=> a.Geburtsdatum);
            }
        }

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

        public string Nachname
        {
            get { return _nachname;}
            set
            {
                if (_nachname.Equals(value))
                {
                    return;
                }
                _nachname = value;
                this.OnPropertyChanged(a => a.Nachname);
            }
        }

        public string Vorname
        {
            get { return _vorname; }
            set
            {
                if (_vorname.Equals(value))
                {
                    return;
                }
                _vorname = value;
                this.OnPropertyChanged(a => a.Vorname);
            }
        }
        public override string Validate()
        {
            var error = new StringBuilder();
            if (string.IsNullOrEmpty(Nachname))
            {
                error.AppendLine("- Nachname ist nicht gesetzt.");
            }
            if (string.IsNullOrEmpty(Vorname))
            {
                error.AppendLine("- Vorname ist nicht gesetzt.");
            }
            if (Geburtsdatum == DateTime.MinValue)
            {
                error.AppendLine("- Geburtsdatum ist nicht gesetzt.");
            }

            return error.Length == 0 ? null : error.ToString();
        }

        public override KundeDto Clone()
        {
            return new KundeDto
            {
                Id = Id,
                Nachname = Nachname,
                Vorname = Vorname,
                Geburtsdatum = Geburtsdatum
            };
        }

        public override string ToString()
        {
            return $"{Id}; {Nachname}; {Vorname}; {Geburtsdatum}";
        }
    }
}
