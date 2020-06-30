using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
namespace App8
{
    class TrainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public newtrain TRain { get; private set; }

        public TrainViewModel()
        {
            TRain = new newtrain();
        }

        public string Id
        {
            get { return TRain.Id; }
            set
            {
                if (TRain.Id != value)
                {
                    TRain.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Type
        {
            get { return TRain.Type; }
            set
            {
                if (TRain.Type != value)
                {
                    TRain.Type = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        public string Startdate
        {
            get { return TRain.Startdate; }
            set
            {
                if (TRain.Startdate != value)
                {
                    TRain.Startdate = value;
                    OnPropertyChanged("Startdate");
                }
            }
        }
        public string Finishtime
        {
            get { return TRain.Finishtime; }
            set
            {
                if (TRain.Finishtime != value)
                {
                    TRain.Finishtime = value;
                    OnPropertyChanged("Finishtime");
                }
            }
        }
        public string Finishdate
        {
            get { return TRain.Finishdate; }
            set
            {
                if (TRain.Finishdate != value)
                {
                    TRain.Finishdate = value;
                    OnPropertyChanged("Finishdate");
                }
            }
        }
        public string Totaltime
        {
            get { return TRain.Totaltime; }
            set
            {
                if (TRain.Totaltime != value)
                {
                    TRain.Totaltime = value;
                    OnPropertyChanged("Totaltime");
                }
            }
        }
        public string check
        {
            get { return TRain.check; }
            set
            {
                if (TRain.check != value)
                {
                    TRain.check = value;
                    OnPropertyChanged("check");
                }
            }
        }
        public string Starttime
        {
            get { return TRain.Starttime; }
            set
            {
                if (TRain.Starttime != value)
                {
                    TRain.Starttime = value;
                    OnPropertyChanged("Starttime");
                }
            }
        }

        public bool IsValid
        {
            get
            {
                return ((!string.IsNullOrEmpty(Id.Trim())) ||
                    (!string.IsNullOrEmpty(Finishtime.Trim())) ||
                    (!string.IsNullOrEmpty(check.Trim())) ||
                    (!string.IsNullOrEmpty(Totaltime.Trim())) ||
                    (!string.IsNullOrEmpty(Finishdate.Trim())) ||
                    (!string.IsNullOrEmpty(Startdate.Trim())) ||
                    (!string.IsNullOrEmpty(Starttime.Trim())) ||
                    (!string.IsNullOrEmpty(Type.Trim())));
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
