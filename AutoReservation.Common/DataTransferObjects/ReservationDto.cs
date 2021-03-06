﻿using System;
using System.Runtime.Serialization;
using System.Text;
using AutoReservation.Common.Extensions;
using AutoReservation.Common.DataTransferObjects.Core;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class ReservationDto : DtoBase<ReservationDto>
    {
        private DateTime _bis;
        private DateTime _von;
        private int _reservationsNr;
        private AutoDto _auto;
        private KundeDto _kunde;

        [DataMember]
        public DateTime Bis
        {
            get { return _bis;}
            set
            {
                if (_bis.Equals(value))
                {
                    return;
                }
                _bis = value;
                this.OnPropertyChanged(r => r.Bis);
            }
        }

        [DataMember]
        public DateTime Von
        {
            get { return _von; }
            set
            {
                if (_von.Equals(value))
                {
                    return;
                }
                _von = value;
                this.OnPropertyChanged(r => r.Von);
            }
        }

        [DataMember]
        public int ReservationNr
        {
            get { return _reservationsNr; }
            set
            {
                if (_reservationsNr == value)
                {
                    return;
                }
                _reservationsNr = value;
                this.OnPropertyChanged(r => r.ReservationNr);
            }
        }

        [DataMember]
        public AutoDto Auto
        {
            get { return _auto; }
            set
            {
                if (_auto != null && _auto.Equals(value))
                {
                    return;
                }
                _auto = value;
                this.OnPropertyChanged(r => r.Auto);
            }
        }

        [DataMember]
        public KundeDto Kunde
        {
            get { return _kunde;}
            set
            {
                if (_kunde != null && _kunde.Equals(value))
                {
                    return;
                }
                _kunde = value;
                this.OnPropertyChanged(r => r.Kunde);
            }
        }
        public override string Validate()
        {
            var error = new StringBuilder();
            if (Von == DateTime.MinValue)
            {
                error.AppendLine("- Von-Datum ist nicht gesetzt.");
            }
            if (Bis == DateTime.MinValue)
            {
                error.AppendLine("- Bis-Datum ist nicht gesetzt.");
            }
            if (Von > Bis)
            {
                error.AppendLine("- Von-Datum ist grösser als Bis-Datum.");
            }
            if (Auto == null)
            {
                error.AppendLine("- Auto ist nicht zugewiesen.");
            }
            else
            {
                var autoError = Auto.Validate();
                if (!string.IsNullOrEmpty(autoError))
                {
                    error.AppendLine(autoError);
                }
            }
            if (Kunde == null)
            {
                error.AppendLine("- Kunde ist nicht zugewiesen.");
            }
            else
            {
                var kundeError = Kunde.Validate();
                if (!string.IsNullOrEmpty(kundeError))
                {
                    error.AppendLine(kundeError);
                }
            }


            return error.Length == 0 ? null : error.ToString();
        }

        public override ReservationDto Clone()
        {
            return new ReservationDto
            {
                ReservationNr = ReservationNr,
                Von = Von,
                Bis = Bis,
                Auto = Auto.Clone(),
                Kunde = Kunde.Clone()
            };
        }

        public override string ToString()
        {
            return $"{ReservationNr}; {Von}; {Bis}; {Auto}; {Kunde}";
        }
    }
}
