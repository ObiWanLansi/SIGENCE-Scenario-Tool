using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Models;
using SIGENCEScenarioTool.Models.Validation;



namespace SIGENCEScenarioTool.ViewModels
{
    sealed public class ValidationResultViewModel
    {
        public ValidationResult Result { get; private set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public String Timestamp
        {
            get
            {
                return Result.Timestamp.Fmt_DD_MM_YYYY_HH_MM_SS();
            }
        }

        public Servity Servity
        {
            get
            {
                return Result.Servity;
            }
        }

        public String Message
        {
            get
            {
                return Result.Message;
            }
        }

        public String Source
        {
            get
            {
                return Result.Source != null ? Result.Source.ToString() : "Unknown Source.";
            }
        }


        public Brush ServityForeground
        {
            get
            {
                switch( Servity )
                {
                    case Servity.Information:
                        return Brushes.Blue;

                    case Servity.Warning:
                        return Brushes.Orange;

                    case Servity.Error:
                        return Brushes.Red;

                    case Servity.Fatal:
                        return Brushes.DarkRed;
                }

                return Brushes.Black;
            }
        }


        public FontWeight ServityFontWeight
        {
            get
            {
                return Servity == Servity.Fatal ? FontWeights.Bold : FontWeights.Normal;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        public ValidationResultViewModel( ValidationResult vr )
        {
            this.Result = vr;
        }

    } // end sealed public class ValidationResultViewModel



    sealed public class ValidationResultViewModelList : ObservableCollection<ValidationResultViewModel>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void FirePropertyChanged( [CallerMemberName]string strPropertyName = null )
        {
            PropertyChanged?.Invoke( this , new PropertyChangedEventArgs( strPropertyName ) );
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        static public ValidationResultViewModelList Empty
        {
            get
            {
                return new ValidationResultViewModelList();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        private int iInformation = 0;
        public int Information
        {
            get { return iInformation; }
            set
            {
                iInformation = value;
                FirePropertyChanged();
            }
        }

        private int iWarning = 0;
        public int Warning
        {
            get { return iWarning; }
            set
            {
                iWarning = value;
                FirePropertyChanged();
            }
        }


        private int iError = 0;
        public int Error
        {
            get { return iError; }
            set
            {
                iError = value;
                FirePropertyChanged();
            }
        }


        private int iFatal = 0;
        public int Fatal
        {
            get { return iFatal; }
            set
            {
                iFatal = value;
                FirePropertyChanged();
            }
        }


        private int iCount = 0;
        new public int Count
        {
            get { return iCount; }
            set
            {
                iCount = value;
                FirePropertyChanged();
            }
        }


        private string strLastRun = "Never";
        public string LastRun
        {
            get { return strLastRun; }
            set
            {
                strLastRun = value;
                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        public void Add( ValidationResult vr )
        {
            Add( new ValidationResultViewModel( vr ) );
        }


        public void Add( ValidationResultList vrl )
        {
            vrl.ForEach( vr => Add( vr ) );
        }


        new public void Clear()
        {
            base.Clear();

            Count = 0;
            Information = 0;
            Warning = 0;
            Error = 0;
            Fatal = 0;

            LastRun = "-";
        }


        public void EstimateCounts()
        {
            Count = base.Count;
            Information = this.Count( vrvm => vrvm.Servity == Servity.Information );
            Warning = this.Count( vrvm => vrvm.Servity == Servity.Warning );
            Error = this.Count( vrvm => vrvm.Servity == Servity.Error );
            Fatal = this.Count( vrvm => vrvm.Servity == Servity.Fatal );

            LastRun = DateTime.Now.Fmt_DD_MM_YYYY_HH_MM_SS();
        }

    } // end sealed public class ValidationResultViewModelList

}
