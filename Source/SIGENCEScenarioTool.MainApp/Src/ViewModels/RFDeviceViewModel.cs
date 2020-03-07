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
using SIGENCEScenarioTool.Models.RxTxTypes;
using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool.ViewModels
{
    /// <summary>
    /// A ViewModel for a RFDevice.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public sealed class RFDeviceViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// The mc map control
        /// </summary>
        private readonly GMapControl mcMapControl = null;

        /// <summary>
        /// The sb validation hint
        /// </summary>
        private readonly StringBuilder sbValidationHint = new StringBuilder(1024);

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

            if (strPropertyName != "ValidationBackground" && strPropertyName != "ValidationHint" && strPropertyName != "FirstValidationHint")
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
            get => this.RFDevice.Id;
            set
            {
                this.RFDevice.Id = value;

                UpdateMarkerShape();

                FirePropertyChanged();
                FirePropertyChanged("DeviceType");
            }
        }


        ///// <summary>
        ///// Gets the device source.
        ///// </summary>
        ///// <value>
        ///// The device source.
        ///// </value>
        //public DeviceSource DeviceSource
        //{
        //    get { return this.RFDevice.DeviceSource; }
        //}


        /// <summary>
        /// Gets or sets the device source.
        /// </summary>
        /// <value>
        /// The device source.
        /// </value>
        public DeviceSource DeviceSource
        {
            get => this.RFDevice.DeviceSource;
            set
            {
                this.RFDevice.DeviceSource = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public double StartTime
        {
            get => this.RFDevice.StartTime;
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
            get => this.RFDevice.Name;
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
            get => this.RFDevice.Latitude;
            set
            {
                this.RFDevice.Latitude = value;

                UpdateMarkerPosition();
                UpdateMarkerTooltip();

                FirePropertyChanged();
                FirePropertyChanged("HumanLatitude");
                FirePropertyChanged("LatitudeAsGeoAngle");
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
            get => this.RFDevice.Longitude;
            set
            {
                this.RFDevice.Longitude = value;

                UpdateMarkerPosition();
                UpdateMarkerTooltip();

                FirePropertyChanged();
                FirePropertyChanged("HumanLongitude");
                FirePropertyChanged("LongitudeAsGeoAngle");
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
            get => this.RFDevice.Altitude;
            set
            {
                this.RFDevice.Altitude = value;

                FirePropertyChanged();
                FirePropertyChanged("HumanAltitude");
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
            get => this.RFDevice.Roll;
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
            get => this.RFDevice.Pitch;
            set
            {
                this.RFDevice.Pitch = value;

                UpdatePitch();
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
            get => this.RFDevice.Yaw;
            set
            {
                this.RFDevice.Yaw = value;

                UpdateYaw();
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
            get => this.RFDevice.RxTxType;
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
            get => this.RFDevice.AntennaType;
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
            get => this.RFDevice.Gain_dB;
            set
            {
                this.RFDevice.Gain_dB = value;

                FirePropertyChanged();
                FirePropertyChanged("HumanGain");
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
            get => this.RFDevice.CenterFrequency_Hz;
            set
            {
                this.RFDevice.CenterFrequency_Hz = value;

                FirePropertyChanged();
                FirePropertyChanged("HumanCenterFrequency");
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
            get => this.RFDevice.Bandwidth_Hz;
            set
            {
                this.RFDevice.Bandwidth_Hz = value;

                FirePropertyChanged();
                FirePropertyChanged("HumanBandwidth");
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
            get => this.RFDevice.SignalToNoiseRatio_dB;
            set
            {
                this.RFDevice.SignalToNoiseRatio_dB = value;

                FirePropertyChanged();
                FirePropertyChanged("HumanSignalToNoiseRatio");
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
            get => this.RFDevice.XPos;
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
            get => this.RFDevice.YPos;
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
            get => this.RFDevice.ZPos;
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
            get => this.RFDevice.Remark;
            set
            {
                this.RFDevice.Remark = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the technical parameters.
        /// </summary>
        /// <value>
        /// The technical parameters.
        /// </value>
        public string TechnicalParameters
        {
            get => this.RFDevice.TechnicalParameters;
            set
            {
                this.RFDevice.TechnicalParameters = value;

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
            get => this._Marker;
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
            get => this.bIsMarked;
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
            get => this.bIsSelected;
            set
            {
                this.bIsSelected = value;

                UpdateSelectionChanged();

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsVisible
        {
            get { return ((AbstractMarker)this.Marker.Shape).Visibility != Visibility.Hidden; }
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
        /// Gets the human latitude.
        /// </summary>
        /// <value>
        /// The human latitude.
        /// </value>
        public string HumanLatitude => string.Format("{0:F4}", this.RFDevice.Latitude.Value);

        /// <summary>
        /// Gets the latitude as geo angle.
        /// </summary>
        /// <value>
        /// The latitude as geo angle.
        /// </value>
        public string LatitudeAsGeoAngle => GeoAngle.FromDouble(this.RFDevice.Latitude.Value).ToString();

        /// <summary>
        /// Gets the human longitude.
        /// </summary>
        /// <value>
        /// The human longitude.
        /// </value>
        public string HumanLongitude => string.Format("{0:F4}", this.RFDevice.Longitude.Value);

        /// <summary>
        /// Gets the longitude as geo angle.
        /// </summary>
        /// <value>
        /// The longitude as geo angle.
        /// </value>
        public string LongitudeAsGeoAngle => GeoAngle.FromDouble(this.RFDevice.Longitude.Value).ToString();

        /// <summary>
        /// Gets the human center frequency.
        /// </summary>
        /// <value>
        /// The human center frequency.
        /// </value>
        public string HumanCenterFrequency => Tool.GetHumanSizeForPhysics(this.RFDevice.CenterFrequency_Hz, "Hz");

        /// <summary>
        /// Gets the human bandwidth.
        /// </summary>
        /// <value>
        /// The human bandwidth.
        /// </value>
        public string HumanBandwidth => Tool.GetHumanSizeForPhysics(this.RFDevice.Bandwidth_Hz, "Hz");

        /// <summary>
        /// Gets the human gain.
        /// </summary>
        /// <value>
        /// The human gain.
        /// </value>
        public string HumanGain => Tool.GetHumanSizeForPhysics(this.RFDevice.Gain_dB, "dB");

        /// <summary>
        /// Gets the human signal to noise ratio.
        /// </summary>
        /// <value>
        /// The human signal to noise ratio.
        /// </value>
        public string HumanSignalToNoiseRatio => Tool.GetHumanSizeForPhysics(this.RFDevice.SignalToNoiseRatio_dB, "dB");

        /// <summary>
        /// Gets the human altitude.
        /// </summary>
        /// <value>
        /// The human altitude.
        /// </value>
        public string HumanAltitude => Tool.GetHumanDistance(this.RFDevice.Altitude);

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The string first validation hint
        /// </summary>
        private string strFirstValidationHint = "";

        /// <summary>
        /// Gets or sets the first validation hint.
        /// </summary>
        /// <value>
        /// The first validation hint.
        /// </value>
        public string FirstValidationHint
        {
            get => this.strFirstValidationHint;
            set
            {
                this.strFirstValidationHint = value;

                FirePropertyChanged();
            }
        }


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
            get => this.strValidationHint;
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
            get => this.bValidationBackground;
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
            Models.Validation.ValidationResultList validation = this.RFDevice.Validate();

            //-----------------------------------------------------------------

            this.sbValidationHint.Clear();

            int iCounter = 0;

            foreach (var v in from val in validation orderby val.Servity descending select val)
            {
                if (iCounter++ == 0)
                {
                    this.FirstValidationHint = string.Format("[{0}]: {1}", v.Servity, v.Message);
                }

                this.sbValidationHint.AppendLine("[{0}]: {1}", v.Servity, v.Message);
            }

            this.ValidationHint = this.sbValidationHint.ToString().Trim();

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
        public enum SimulationState : byte { None, Current }

        /// <summary>
        /// The simsa
        /// </summary>
        private SimulationState simsa = SimulationState.None;

        /// <summary>
        /// Gets or sets the state of the current simulation.
        /// </summary>
        /// <value>
        /// The state of the current simulation.
        /// </value>
        public SimulationState CurrentSimulationState
        {
            get => this.simsa;
            set
            {
                this.simsa = value;

                FirePropertyChanged();
                FirePropertyChanged("CurrentSimulationStateBrush");
            }
        }

        /// <summary>
        /// Gets the current simulation state brush.
        /// </summary>
        /// <value>
        /// The current simulation state brush.
        /// </value>
        public Brush CurrentSimulationStateBrush
        {
            get
            {
                switch (this.simsa)
                {
                    case SimulationState.Current:
                        return Brushes.Green;

                    default:
                        return Brushes.LightGray;
                }
            }
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
            //this.mcMapControl = mcMapControl ?? throw new ArgumentNullException(nameof(mcMapControl));
            this.mcMapControl = mcMapControl;
            this.RFDevice = device ?? throw new ArgumentNullException(nameof(device));

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
        /// The sb tooltip
        /// </summary>
        private static readonly StringBuilder sbTooltip = new StringBuilder(512);


        /// <summary>
        /// Gets the tool tip.
        /// </summary>
        /// <returns></returns>
        private string GetToolTip()
        {
            sbTooltip.Clear();

            sbTooltip.AppendLine("{0} ({1})", this.DeviceType, this.DeviceSource);
            sbTooltip.AppendLine("{0} ({1})", this.RFDevice.Name, this.RFDevice.Id);
            sbTooltip.AppendLine("{0} / {1}", this.RFDevice.RxTxType, this.RFDevice.AntennaType);
            sbTooltip.AppendLine("{0,1:00.########}", (double)this.RFDevice.Latitude);
            sbTooltip.AppendLine("{0,1:00.########}", (double)this.RFDevice.Longitude);

            //return string.Format( "- {0} -\n{1} ({2})\n{3,1:00.########}\n{4,1:00.########}" , this.DeviceType , this.RFDevice.Name , this.RFDevice.Id , this.RFDevice.Latitude , this.RFDevice.Longitude );
            //return $"- {this.DeviceType} -\n{this.RFDevice.Name} ({this.RFDevice.Id})\n{( double ) this.RFDevice.Latitude,1:00.########}\n{( double ) this.RFDevice.Longitude,1:00.########}";

            return sbTooltip.ToString();
        }


        /// <summary>
        /// Updates the marker tooltip.
        /// </summary>
        private void UpdateMarkerTooltip()
        {
            ((AbstractMarker)this.Marker.Shape).MarkerToolTip = GetToolTip();
        }


        /// <summary>
        /// Updates the marker shape.
        /// </summary>
        private void UpdateMarkerShape()
        {
            if (this.Marker.Shape != null)
            {
                ((AbstractMarker)this.Marker.Shape).OnPositionChanged -= Marker_OnPositionChanged;
                ((AbstractMarker)this.Marker.Shape).OnSelectionChanged -= Marker_OnSelectionChanged;

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

            //#if DEBUG
            //            if (this.RFDevice.Id == 42)
            //            {
            //                marker = new DiamondMarker(this.mcMapControl, this.Marker, GetToolTip());
            //            }
            //#endif

            marker.OnPositionChanged += Marker_OnPositionChanged;
            marker.OnSelectionChanged += Marker_OnSelectionChanged;

            this.Marker.Shape = marker;

            // Das können wir direkt mal aktualisieren da es ja noch nicht gesetzt wurde ...
            UpdateYaw();
            UpdatePitch();
        }


        /// <summary>
        /// Updates the selection changed.
        /// </summary>
        private void UpdateSelectionChanged()
        {
            ((AbstractMarker)this.Marker.Shape).IsSelected = this.IsSelected;
        }


        /// <summary>
        /// Updates the marker position.
        /// </summary>
        private void UpdateMarkerPosition()
        {
            this.Marker.Position = new PointLatLng(this.RFDevice.Latitude, this.RFDevice.Longitude);
        }


        /// <summary>
        /// Updates the yaw.
        /// </summary>
        private void UpdateYaw()
        {
            // Remark: In the Simulation the Angle 0 starts at 3'Clock ...
            const int iYawCorrection = 90;

            //if (this.Marker.Shape is RectangleMarker rm)
            //{
            //    rm.Yaw = this.Yaw + iYawCorrection;
            //}

            //if (this.Marker.Shape is TriangleMarker tm)
            //{
            //    tm.Yaw = this.Yaw + iYawCorrection;
            //}

            if (this.Marker.Shape is AbstractMarker am)
            {
                am.Yaw = this.Yaw + iYawCorrection;
            }
        }


        /// <summary>
        /// Updates the pitch.
        /// </summary>
        private void UpdatePitch()
        {
            // Remark: In the Simulation the Angle 0 starts at 3'Clock ...
            const int iPitchCorrection = 90;

            //if (this.Marker.Shape is RectangleMarker rm)
            //{
            //    rm.Pitch = this.Pitch + iPitchCorrection;
            //}

            //if (this.Marker.Shape is TriangleMarker tm)
            //{
            //    tm.Pitch = this.Pitch + iPitchCorrection;
            //}

            if (this.Marker.Shape is AbstractMarker am)
            {
                am.Pitch = this.Pitch + iPitchCorrection;
            }
        }


        /// <summary>
        /// Determines whether the specified b is filtered is filtered.
        /// </summary>
        /// <param name="bIsVisible">if set to <c>true</c> [b is visible].</param>
        public void SetVisible(bool bIsVisible)
        {
            this.Marker.Shape.Visibility = bIsVisible ? Visibility.Visible : Visibility.Hidden;
            //((AbstractMarker)this.Marker.Shape).Visibility = bIsVisible ? Visibility.Visible : Visibility.Hidden;
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

            // ReSharper disable ExplicitCallerInfoArgument
            FirePropertyChanged("Latitude");
            FirePropertyChanged("Longitude");
        }


        /// <summary>
        /// Markers the on selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="bIsSelected">if set to <c>true</c> [b is selected].</param>
        // ReSharper disable once ParameterHidesMember
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
        public static implicit operator RFDevice(RFDeviceViewModel model)
        {
            return model.RFDevice;
        }

        //public override string ToString() => $"[{this.Id}]:{this.Name}";

    } // end public sealed class RFDeviceViewModel



    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{RFDeviceViewModel}" />
    public sealed class RFDeviceViewModelCollection : ObservableCollection<RFDeviceViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RFDeviceViewModelCollection"/> class.
        /// </summary>
        public RFDeviceViewModelCollection()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RFDeviceViewModelCollection"/> class.
        /// </summary>
        /// <param name="dvmc">The DVMC.</param>
        public RFDeviceViewModelCollection(RFDeviceViewModelCollection dvmc) :
            base(dvmc)
        {
        }

    } // end public sealed class RFDeviceViewModelCollection
}
