using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Media;

using GMap.NET;
using GMap.NET.WindowsPresentation;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.Markers;
using SIGENCEScenarioTool.Models;



namespace SIGENCEScenarioTool.ViewModels
{
    /// <summary>
    /// A ViewModel for a RFDevice.
    /// </summary>
    sealed public class RFDeviceViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// The mc map control
        /// </summary>
        private readonly GMapControl mcMapControl = null;

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Tritt ein, wenn sich ein Eigenschaftswert ändert.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Fires the property changed.
        /// </summary>
        /// <param name="strPropertyName">Name of the string property.</param>
        private void FirePropertyChanged([CallerMemberName]string strPropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strPropertyName));

            // If A Property Changed, It Is Useful To ReValidate ...

            if (strPropertyName != "ValidationBackground" && strPropertyName != "ValidationHint")
            {
                ExecValidation();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the RFDevice.
        /// </summary>
        /// <value>
        /// The RFDevice.
        /// </value>
        public RFDevice RFDevice { get; private set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id
        {
            get { return this.RFDevice.Id; }
            set
            {
                this.RFDevice.Id = value;

                UpdateMarkerShape();

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets the device source.
        /// </summary>
        /// <value>
        /// The device source.
        /// </value>
        public DeviceSource DeviceSource
        {
            get { return this.RFDevice.DeviceSource; }
        }


        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public double StartTime
        {
            get { return this.RFDevice.StartTime; }
            set
            {
                this.RFDevice.StartTime = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return this.RFDevice.Name; }
            set
            {
                this.RFDevice.Name = value;

                UpdateMarkerTooltip();

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        /// <remarks>
        /// Das bleibt mal noch ein double da sonst das Binding in XAML zum editieren nicht mehr funktioniert.
        /// </remarks>
        public double Latitude
        {
            get { return this.RFDevice.Latitude; }
            set
            {
                this.RFDevice.Latitude = value;

                UpdateMarkerPosition();
                UpdateMarkerTooltip();

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        /// <remarks>
        /// Das bleibt mal noch ein double da sonst das Binding in XAML zum editieren nicht mehr funktioniert.
        /// </remarks>
        public double Longitude
        {
            get { return this.RFDevice.Longitude; }
            set
            {
                this.RFDevice.Longitude = value;

                UpdateMarkerPosition();
                UpdateMarkerTooltip();

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the altitude.
        /// </summary>
        /// <value>
        /// The altitude.
        /// </value>
        public int Altitude
        {
            get { return this.RFDevice.Altitude; }
            set
            {
                this.RFDevice.Altitude = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the roll.
        /// </summary>
        /// <value>
        /// The roll.
        /// </value>
        public double Roll
        {
            get { return this.RFDevice.Roll; }
            set
            {
                this.RFDevice.Roll = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the pitch.
        /// </summary>
        /// <value>
        /// The pitch.
        /// </value>
        public double Pitch
        {
            get { return this.RFDevice.Pitch; }
            set
            {
                this.RFDevice.Pitch = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the yaw.
        /// </summary>
        /// <value>
        /// The yaw.
        /// </value>
        public double Yaw
        {
            get { return this.RFDevice.Yaw; }
            set
            {
                this.RFDevice.Yaw = value;

                UpdateDirectionAngle();
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the type of the rx tx.
        /// </summary>
        /// <value>
        /// The type of the rx tx.
        /// </value>
        public RxTxType RxTxType
        {
            get { return this.RFDevice.RxTxType; }
            set
            {
                this.RFDevice.RxTxType = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the type of the antenna.
        /// </summary>
        /// <value>
        /// The type of the antenna.
        /// </value>
        public AntennaType AntennaType
        {
            get { return this.RFDevice.AntennaType; }
            set
            {
                this.RFDevice.AntennaType = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the gain.
        /// </summary>
        /// <value>
        /// The gain.
        /// </value>
        public double Gain
        {
            get { return this.RFDevice.Gain_dB; }
            set
            {
                this.RFDevice.Gain_dB = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the center frequency.
        /// </summary>
        /// <value>
        /// The center frequency.
        /// </value>
        public double CenterFrequency
        {
            get { return this.RFDevice.CenterFrequency_Hz; }
            set
            {
                this.RFDevice.CenterFrequency_Hz = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the bandwidth.
        /// </summary>
        /// <value>
        /// The bandwidth.
        /// </value>
        public double Bandwidth
        {
            get { return this.RFDevice.Bandwidth_Hz; }
            set
            {
                this.RFDevice.Bandwidth_Hz = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the signal to noise ratio.
        /// </summary>
        /// <value>
        /// The signal to noise ratio.
        /// </value>
        public double SignalToNoiseRatio
        {
            get { return this.RFDevice.SignalToNoiseRatio_dB; }
            set
            {
                this.RFDevice.SignalToNoiseRatio_dB = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the x position.
        /// </summary>
        /// <value>
        /// The x position.
        /// </value>
        public int XPos
        {
            get { return this.RFDevice.XPos; }
            set
            {
                this.RFDevice.XPos = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the y position.
        /// </summary>
        /// <value>
        /// The y position.
        /// </value>
        public int YPos
        {
            get { return this.RFDevice.YPos; }
            set
            {
                this.RFDevice.YPos = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the z position.
        /// </summary>
        /// <value>
        /// The z position.
        /// </value>
        public int ZPos
        {
            get { return this.RFDevice.ZPos; }
            set
            {
                this.RFDevice.ZPos = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        public string Remark
        {
            get { return this.RFDevice.Remark; }
            set
            {
                this.RFDevice.Remark = value;
                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The marker
        /// </summary>
        private GMapMarker _Marker = null;

        /// <summary>
        /// Gets or sets the marker.
        /// </summary>
        /// <value>
        /// The marker.
        /// </value>
        public GMapMarker Marker
        {
            get { return this._Marker; }
            set
            {
                this._Marker = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The b is marked
        /// </summary>
        private bool bIsMarked = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is marked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is marked; otherwise, <c>false</c>.
        /// </value>
        public bool IsMarked
        {
            get { return this.bIsMarked; }
            set
            {
                this.bIsMarked = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The b is selected
        /// </summary>
        private bool bIsSelected = false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get { return this.bIsSelected; }
            set
            {
                this.bIsSelected = value;

                UpdateSelectionChanged();

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets the type of the device.
        /// </summary>
        /// <value>
        /// The type of the device.
        /// </value>
        public DeviceType DeviceType
        {
            get
            {
                if (this.RFDevice.Id == 0)
                {
                    return DeviceType.Reference;
                }

                if (this.RFDevice.Id > 0)
                {
                    return DeviceType.Transmitter;
                }

                if (this.RFDevice.Id < 0)
                {
                    return DeviceType.Receiver;
                }

                return DeviceType.Unknown;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        /// <summary>
        /// The string validation hint
        /// </summary>
        private string strValidationHint = "";

        /// <summary>
        /// Gets or sets the validation hint.
        /// </summary>
        /// <value>
        /// The validation hint.
        /// </value>
        public string ValidationHint
        {
            get { return this.strValidationHint; }
            set
            {
                this.strValidationHint = value;
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The b validation background
        /// </summary>
        private Brush bValidationBackground = Brushes.Transparent;

        /// <summary>
        /// Gets or sets the validation background.
        /// </summary>
        /// <value>
        /// The validation background.
        /// </value>
        public Brush ValidationBackground
        {
            get { return this.bValidationBackground; }
            set
            {
                this.bValidationBackground = value;
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Executes the validation.
        /// </summary>
        private void ExecValidation()
        {
            var validation = this.RFDevice.Validate();

            //-----------------------------------------------------------------

            StringBuilder sb = new StringBuilder(512);

            foreach (var v in from val in validation orderby val.Servity descending select val)
            {
                sb.AppendLine("[{0}]: {1}", v.Servity, v.Message);
            }

            this.ValidationHint = sb.ToString().Trim();

            //-----------------------------------------------------------------

            if (validation.Exists(vr => vr.Servity == Servity.Fatal))
            {
                this.ValidationBackground = Brushes.DarkRed;
                return;
            }

            if (validation.Exists(vr => vr.Servity == Servity.Error))
            {
                this.ValidationBackground = Brushes.Red;
                return;
            }

            if (validation.Exists(vr => vr.Servity == Servity.Warning))
            {
                this.ValidationBackground = Brushes.Orange;
                return;
            }

            if (validation.Exists(vr => vr.Servity == Servity.Information))
            {
                this.ValidationBackground = Brushes.Blue;
                return;
            }

            this.ValidationHint = "Congratulations, Everything Is Fine.";
            this.ValidationBackground = Brushes.Transparent;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="bIsSelected">if set to <c>true</c> [b is selected].</param>
        public delegate void SelectionChangedHandler(object sender, bool bIsSelected);

        /// <summary>
        /// Occurs when [on selection changed].
        /// </summary>
        public event SelectionChangedHandler OnSelectionChanged;

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="RFDeviceViewModel" /> class.
        /// </summary>
        /// <param name="mcMapControl">The mc map control.</param>
        /// <param name="device">The device.</param>
        /// <exception cref="ArgumentNullException">device</exception>
        public RFDeviceViewModel(GMapControl mcMapControl, RFDevice device)
        {
            if (mcMapControl == null)
            {
                throw new ArgumentNullException("mcMapControl");
            }

            //this.mcMapControl = mcMapControl ?? throw new ArgumentNullException("mcMapControl");

            if (device == null)
            {
                throw new ArgumentNullException("device");
            }

            //this.RFDevice = device ?? throw new ArgumentNullException( "device" );

            //-----------------------------------------------------------------

            this.mcMapControl = mcMapControl;
            this.RFDevice = device;

            //-----------------------------------------------------------------

            this.Marker = new GMapMarker(new PointLatLng(device.Latitude, device.Longitude))
            {
                Offset = new Point(-15, -15),
                ZIndex = int.MaxValue,
                Tag = device
            };

            UpdateMarkerShape();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the tool tip.
        /// </summary>
        /// <returns></returns>
        private string GetToolTip()
        {
            return string.Format("- {0} -\n{1} ({2})\n{3,1:00.########}\n{4,1:00.########}", this.DeviceType, this.RFDevice.Name, this.RFDevice.Id, this.RFDevice.Latitude, this.RFDevice.Longitude);
        }


        /// <summary>
        /// Updates the marker tooltip.
        /// </summary>
        private void UpdateMarkerTooltip()
        {
            (this.Marker.Shape as AbstractMarker).MarkerToolTip = GetToolTip();
        }


        /// <summary>
        /// Updates the marker shape.
        /// </summary>
        private void UpdateMarkerShape()
        {
            if (this.Marker.Shape != null)
            {
                (this.Marker.Shape as AbstractMarker).OnPositionChanged -= Marker_OnPositionChanged;
                (this.Marker.Shape as AbstractMarker).OnSelectionChanged -= Marker_OnSelectionChanged;

                this.Marker.Shape = null;
            }

            AbstractMarker marker = null;

            // Reference Transmitter
            if (this.RFDevice.Id == 0)
            {
                marker = new CircleMarker(this.mcMapControl, this.Marker, GetToolTip());
                //shape.OnPositionChanged += Shape_OnPositionChanged;
                //this.Marker.Shape = shape;
                //return;
            }

            // Receiver
            if (this.RFDevice.Id < 0)
            {
                marker = new RectangleMarker(this.mcMapControl, this.Marker, GetToolTip());
                //shape.OnPositionChanged += Shape_OnPositionChanged;
                //this.Marker.Shape = shape;
                //return;
            }

            // Last but not least all other are transmitters ... 
            if (this.RFDevice.Id > 0)
            {
                marker = new TriangleMarker(this.mcMapControl, this.Marker, GetToolTip());
                //shape.OnPositionChanged += Shape_OnPositionChanged;
                //this.Marker.Shape = shape;
            }

#if DEBUG
            if (this.RFDevice.Id == 42)
            {
                marker = new DiamondMarker(this.mcMapControl, this.Marker, GetToolTip());
            }
#endif

            marker.OnPositionChanged += Marker_OnPositionChanged;
            marker.OnSelectionChanged += Marker_OnSelectionChanged;

            this.Marker.Shape = marker;

            // Das können wir direkt mal aktualisieren da es ja noch nicht gesetzt wurde ...
            UpdateDirectionAngle();
        }


        /// <summary>
        /// Updates the selection changed.
        /// </summary>
        private void UpdateSelectionChanged()
        {
            (this.Marker.Shape as AbstractMarker).IsSelected = this.IsSelected;
        }


        /// <summary>
        /// Updates the marker position.
        /// </summary>
        private void UpdateMarkerPosition()
        {
            this.Marker.Position = new PointLatLng(this.RFDevice.Latitude, this.RFDevice.Longitude);
        }


        /// <summary>
        /// Updates the direction angle.
        /// </summary>
        private void UpdateDirectionAngle()
        {
            if (this.Marker.Shape is RectangleMarker)
            {
                (this.Marker.Shape as RectangleMarker).DirectionAngle = this.Yaw;
            }

            if (this.Marker.Shape is TriangleMarker)
            {
                (this.Marker.Shape as TriangleMarker).DirectionAngle = this.Yaw;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Shapes the on position changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="pll">The PLL.</param>
        private void Marker_OnPositionChanged(object sender, PointLatLng pll)
        {
            this.RFDevice.Latitude = pll.Lat;
            this.RFDevice.Longitude = pll.Lng;

            UpdateMarkerTooltip();

            FirePropertyChanged("Latitude");
            FirePropertyChanged("Longitude");
        }


        /// <summary>
        /// Markers the on selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="bIsSelected">if set to <c>true</c> [b is selected].</param>
        private void Marker_OnSelectionChanged(object sender, bool bIsSelected)
        {
            // Hier dürfen wir natürlich nicht über das Property gehen da sonst wieder 
            // ein FirePropertyChanged bekommen und wir uns im Kreis drehen ...

            //this.bIsSelected = bIsSelected;
            //FirePropertyChanged("IsSelected");

            // Wir Forwarden das Event nur ...
            OnSelectionChanged?.Invoke(this, bIsSelected);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Performs an implicit conversion from <see cref="RFDeviceViewModel"/> to <see cref="RFDevice"/>.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        static public implicit operator RFDevice(RFDeviceViewModel model)
        {
            return model.RFDevice;
        }

    } // end sealed public class RFDeviceViewModel



    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{SIGENCEScenarioTool.ViewModels.RFDeviceViewModel}" />
    sealed public class RFDeviceViewModelList : ObservableCollection<RFDeviceViewModel>
    {
        ///// <summary>
        ///// Validates this instance.
        ///// </summary>
        ///// <returns></returns>
        //public ValidationResultViewModelList Validate()
        //{
        //    return ValidationResultViewModelList.Empty;
        //}

    } // end sealed public class RFDeviceViewModelList
}
