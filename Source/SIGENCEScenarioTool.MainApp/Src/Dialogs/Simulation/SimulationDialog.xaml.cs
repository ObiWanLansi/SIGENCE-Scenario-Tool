using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;

using SIGENCEScenarioTool.Extensions;
using SIGENCEScenarioTool.ViewModels;



/*
 * Bei der Simluation müssen nachher nur die Sender berücksichtigt werden, die Receiver empfangen ja einfach nur ... ?
*/
namespace SIGENCEScenarioTool.Dialogs.Simulation
{
    /// <summary>
    /// Interaktionslogik für SimulationDialog.xaml
    /// </summary>
    public partial class SimulationDialog : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// The dt timer
        /// </summary>
        private readonly DispatcherTimer dtTimer = new DispatcherTimer();

        /// <summary>
        /// The i start time
        /// </summary>
        private int iStartTime = 0;

        /// <summary>
        /// The device time cache
        /// </summary>
        private readonly SortedDictionary<int, List<Tuple<double, RFDeviceViewModel>>> sdDeviceTimeCache = new SortedDictionary<int, List<Tuple<double, RFDeviceViewModel>>>();

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets or sets the RFDevice collection.
        /// </summary>
        /// <value>
        /// The RFDevice collection.
        /// </value>
        public RFDeviceViewModelCollection RFDeviceViewModelCollection { get; set; } = new RFDeviceViewModelCollection();


        /// <summary>
        /// The i unique device count
        /// </summary>
        private int iUniqueDeviceCount = 0;

        /// <summary>
        /// Gets the unique device count.
        /// </summary>
        /// <value>
        /// The unique device count.
        /// </value>
        public int UniqueDeviceCount
        {
            get { return this.iUniqueDeviceCount; }
            set
            {
                this.iUniqueDeviceCount = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The i current time
        /// </summary>
        private double iCurrentTimeSeconds;

        /// <summary>
        /// Gets or sets the current time.
        /// </summary>
        /// <value>
        /// The current time.
        /// </value>
        public double CurrentTimeSeconds
        {
            get { return this.iCurrentTimeSeconds; }
            set
            {
                this.iCurrentTimeSeconds = value;
                this.CurrentTimeAsString = TimeSpan.FromSeconds(value).ToShortString();

                UpdateHmi();
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// </summary>
        private double iMinTimeSeconds = 0;

        /// <summary>
        /// </summary>
        /// <value>
        /// </value>
        public double MinTimeSeconds
        {
            get { return this.iMinTimeSeconds; }
            set
            {
                this.iMinTimeSeconds = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The i maximum time
        /// </summary>
        private double iMaxTimeSeconds = 0;

        /// <summary>
        /// Gets or sets the maximum time.
        /// </summary>
        /// <value>
        /// The maximum time.
        /// </value>
        public double MaxTimeSeconds
        {
            get { return this.iMaxTimeSeconds; }
            set
            {
                this.iMaxTimeSeconds = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The string current time
        /// </summary>
        private string strCurrentTime = "88:88:88.888";

        /// <summary>
        /// Gets or sets the current time as string.
        /// </summary>
        /// <value>
        /// The current time as string.
        /// </value>
        public string CurrentTimeAsString
        {
            get { return this.strCurrentTime; }
            set
            {
                this.strCurrentTime = value;

                FirePropertyChanged();
            }
        }


        /// <summary>
        /// The b is running
        /// </summary>
        private bool bIsRunning = false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is runnig.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is runnig; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunning
        {
            get { return this.bIsRunning; }
            set
            {
                this.bIsRunning = value;

                FirePropertyChanged();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// The b MQTT is enabled
        /// </summary>
        private bool bIsMQTTEnabled = Properties.Settings.Default.EnableMQTT;

        /// <summary>
        /// Gets or sets a value indicating whether [MQTT is enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [MQTT is enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool IsMQTTEnabled
        {
            get { return this.bIsMQTTEnabled; }
            set
            {
                this.bIsMQTTEnabled = value;

                //EnableOrDisableMQTT();
                FirePropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the MQTT status brush.
        /// </summary>
        /// <value>
        /// The MQTT status brush.
        /// </value>
        public Brush MQTTStatusBrush { get; set; } = Brushes.Gray;

        /// <summary>
        /// Gets or sets the MQTT status message.
        /// </summary>
        /// <value>
        /// The MQTT status message.
        /// </value>
        public string MQTTStatusMessage { get; set; } = "";

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="System.Collections.IComparer" />
        private class Comparer : IComparer
        {
            /// <summary>
            /// Vergleicht zwei Objekte und gibt einen Wert zurück, der angibt, ob ein Wert niedriger, gleich oder größer als der andere Wert ist.
            /// </summary>
            /// <param name="x">Das erste zu vergleichende Objekt.</param>
            /// <param name="y">Das zweite zu vergleichende Objekt.</param>
            /// <returns>
            /// Eine ganze Zahl mit Vorzeichen, die die relativen Werte von <paramref name="x" /> und <paramref name="y" /> angibt, wie in der folgenden Tabelle veranschaulicht.Wert Bedeutung Kleiner als 0 <paramref name="x" /> ist kleiner als <paramref name="y" />. Zero <paramref name="x" /> ist gleich <paramref name="y" />. Größer als 0 (null) <paramref name="x" /> ist größer als <paramref name="y" />.
            /// </returns>
            public int Compare(object x, object y)
            {
                if ((x as RFDeviceViewModel).StartTime == (y as RFDeviceViewModel).StartTime)
                {
                    return (x as RFDeviceViewModel).Id.CompareTo((y as RFDeviceViewModel).Id);
                }

                return (x as RFDeviceViewModel).StartTime.CompareTo((y as RFDeviceViewModel).StartTime);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Initializes a new instance of the <see cref="SimulationDialog" /> class.
        /// </summary>
        /// <param name="_RFDeviceViewModelCollection">The rf device view model collection.</param>
        /// <param name="mcSourceMapControl">The mc source map control.</param>
        public SimulationDialog(RFDeviceViewModelCollection _RFDeviceViewModelCollection, GMapControl mcSourceMapControl)
        {
            this.DataContext = this;

            this.RFDeviceViewModelCollection = new RFDeviceViewModelCollection(_RFDeviceViewModelCollection);

            //-----------------------------------------------------------------

            if (CollectionViewSource.GetDefaultView(this.RFDeviceViewModelCollection) is ListCollectionView lcvRFDevices)
            {
                lcvRFDevices.IsLiveSorting = false;
                lcvRFDevices.CustomSort = new Comparer();
            }

            //-----------------------------------------------------------------

            InitializeComponent();

            //-----------------------------------------------------------------

            CreateDeviceTimeCache();
            InitMapControl(mcSourceMapControl);
            InitSlider();

            //-----------------------------------------------------------------

            //ConnectMQTTAsync();

            //-----------------------------------------------------------------

            this.dtTimer.Interval = TimeSpan.FromMilliseconds(100);

            this.dtTimer.Tick += (s, args) =>
            {
                UpdateTime();
            };
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Creates the device time cache.
        /// </summary>
        private void CreateDeviceTimeCache()
        {
            foreach (var device in from dev in this.RFDeviceViewModelCollection orderby dev.StartTime descending select dev)
            {
                if (this.sdDeviceTimeCache.ContainsKey(device.Id))
                {
                    this.sdDeviceTimeCache[device.Id].Add(new Tuple<double, RFDeviceViewModel>(device.StartTime, device));
                }
                else
                {
                    this.sdDeviceTimeCache.Add(device.Id, new List<Tuple<double, RFDeviceViewModel>> { new Tuple<double, RFDeviceViewModel>(device.StartTime, device) });
                }
            }

            this.UniqueDeviceCount = this.sdDeviceTimeCache.Count;
        }


        /// <summary>
        /// Initializes the map control.
        /// </summary>
        /// <param name="mcSourceMapControl">The mc source map control.</param>
        private void InitMapControl(GMapControl mcSourceMapControl)
        {
            Properties.Settings settings = Properties.Settings.Default;

            GMapProvider.WebProxy = WebRequest.DefaultWebProxy;
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;

            this.mcMapControl.DragButton = MouseButton.Left;
            this.mcMapControl.Manager.Mode = AccessMode.ServerAndCache;
            this.mcMapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            this.mcMapControl.ShowCenter = false;
            this.mcMapControl.MinZoom = 2;
            this.mcMapControl.MaxZoom = 22;
            //this.mcMapControl.Position = new PointLatLng(settings.InitialLatitude, settings.InitialLongitude);
            this.mcMapControl.Position = mcSourceMapControl.Position;
            //this.mcMapControl.Zoom = settings.InitialZoom;
            this.mcMapControl.Zoom = mcSourceMapControl.Zoom;
            //this.mcMapControl.MapProvider = MainWindow.GetProviderFromString(settings.InitialMap);
            this.mcMapControl.MapProvider = mcSourceMapControl.MapProvider;

            foreach (var device in this.RFDeviceViewModelCollection)
            {
                this.mcMapControl.Markers.Add(device.Marker);
            }
        }


        /// <summary>
        /// Initializes the slider.
        /// </summary>
        private void InitSlider()
        {
            this.MinTimeSeconds = 0;
            this.CurrentTimeSeconds = 0;

            this.MaxTimeSeconds = this.RFDeviceViewModelCollection.Count > 0 ? Math.Ceiling(this.RFDeviceViewModelCollection.Max(d => d.StartTime)) : 0;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Starts the stop.
        /// </summary>
        private void StartStop()
        {
            if (this.IsRunning)
            {
                this.dtTimer.Stop();
                this.CurrentTimeSeconds = 0;
            }
            else
            {
                this.dtTimer.Start();
                this.iStartTime = Environment.TickCount - ((int)(this.iCurrentTimeSeconds * 1000));
            }

            this.IsRunning = !this.IsRunning;

            this.btnPause.IsEnabled = this.bIsRunning;
            this.sCurrentTime.IsEnabled = !this.bIsRunning;
            this.btnPlayStop.Content = this.Resources[this.bIsRunning ? "STOP" : "PLAY"];
        }


        /// <summary>
        /// Pauses this instance.
        /// </summary>
        private void Pause()
        {
            if (this.dtTimer.IsEnabled)
            {
                this.dtTimer.Stop();
            }
            else
            {
                this.iStartTime = Environment.TickCount - ((int)(this.iCurrentTimeSeconds * 1000));
                this.dtTimer.Start();
            }
        }


        /// <summary>
        /// Updates the time.
        /// </summary>
        private void UpdateTime()
        {
            int iMilliSecondsSinceStart = Environment.TickCount - this.iStartTime;
            double dSecondsSinceStart = (double)iMilliSecondsSinceStart / 1000;
            this.CurrentTimeSeconds = dSecondsSinceStart;

            if (this.CurrentTimeSeconds >= this.MaxTimeSeconds)
            {
                Pause();
            }
        }


        /// <summary>
        /// Updates the hmi.
        /// </summary>
        private void UpdateHmi()
        {
            foreach (int id in this.sdDeviceTimeCache.Keys)
            {
                bool bIsSet = false;

                foreach (var tuple in this.sdDeviceTimeCache[id])
                {
                    if ((this.iCurrentTimeSeconds > tuple.Item2.StartTime) && bIsSet == false)
                    {
                        tuple.Item2.SetVisible(true);
                        tuple.Item2.CurrentSimulationState = RFDeviceViewModel.SimulationState.Current;

                        bIsSet = true;
                    }
                    else
                    {
                        tuple.Item2.SetVisible(false);
                        tuple.Item2.CurrentSimulationState = RFDeviceViewModel.SimulationState.None;
                    }
                }
            }
        }


        /// <summary>
        /// Sets the device visibility.
        /// </summary>
        /// <param name="bVisible">if set to <c>true</c> [b visible].</param>
        private void SetDeviceVisibility(bool bVisible = true)
        {
            foreach (var device in this.RFDeviceViewModelCollection)
            {
                device.SetVisible(bVisible);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ///// <summary>
        ///// The mqttclient
        ///// </summary>
        //private IMqttClient mqttclient = null;


        ///// <summary>
        ///// Connects the MQTT.
        ///// </summary>
        //private void ConnectMQTTAsync()
        //{
        //    string strHost = Properties.Settings.Default.MQTTHost;
        //    int iPort = Properties.Settings.Default.MQTTPort;

        //    MqttConfiguration configuration = new MqttConfiguration
        //    {
        //        //BufferSize = 128 * 1024,
        //        Port = iPort,
        //        //KeepAliveSecs = 10,
        //        //WaitTimeoutSecs = 2,
        //        //MaximumQualityOfService = MqttQualityOfService.AtLeastOnce,
        //        AllowWildcardsInTopicFilters = true
        //    };

        //    try
        //    {
        //        Task<IMqttClient> tClient = MqttClient.CreateAsync(strHost, configuration);
        //        tClient.Wait();

        //        this.mqttclient = tClient.Result;
        //        //this.mqttclient.ConnectAsync(new MqttClientCredentials(Tool.ProductName)).Wait();

        //        ////Task<SessionState> session = mqttclient.ConnectAsync(new MqttClientCredentials(Guid.NewGuid().ToString()));
        //        //session.Wait();

        //        this.MQTTStatusBrush = Brushes.Lime;
        //        this.MQTTStatusMessage = $"Connected To MQTTT Broker ({strHost})";
        //    }
        //    catch (Exception ex)
        //    {
        //        this.MQTTStatusBrush = Brushes.Red;
        //        this.MQTTStatusMessage = $"Not Connected To MQTTT Broker ({strHost}):\n{ex.Message}";
        //    }
        //}


        ///// <summary>
        ///// Disconnects the MQTT.
        ///// </summary>
        //private void DisconnectMQTT()
        //{
        //    if (this.mqttclient != null)
        //    {
        //        this.mqttclient.DisconnectAsync().Wait();
        //    }

        //    this.MQTTStatusBrush = Brushes.Gray;
        //    this.MQTTStatusMessage = "Not Connected To An MQTTT Broker";
        //}


        ///// <summary>
        ///// Sends the MQTT message.
        ///// </summary>
        ///// <param name="strTopic">The string topic.</param>
        ///// <param name="strMessage">The string message.</param>
        //private void SendMQTTMessage(string strTopic, string strMessage)
        //{
        //    //MqttApplicationMessage message = new MqttApplicationMessage( "foo/bar/topic1" , Encoding.UTF8.GetBytes( "Foo Message 1" ) );
        //    //client.Result.PublishAsync( message , MqttQualityOfService.AtLeastOnce , true );
        //}


        ///// <summary>
        ///// Enables the or disable MQTT.
        ///// </summary>
        //private void EnableOrDisableMQTT()
        //{
        //    if (this.bIsMQTTEnabled == true)
        //    {
        //        ConnectMQTTAsync();
        //        //this.MQTTStatusBrush = Brushes.Lime;
        //        //this.MQTTStatusMessage = $"Connected To MQTTT Broker ({Properties.Settings.Default.MQTTBrokerURL})";
        //    }
        //    else
        //    {
        //        DisconnectMQTT();
        //        //this.MQTTStatusBrush = Brushes.Gray;
        //        //this.MQTTStatusMessage = "Not Connected To An MQTTT Broker";
        //    }

        //    FirePropertyChanged("MQTTStatusBrush");
        //    FirePropertyChanged("MQTTStatusMessage");
        //}

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Handles the Click event of the Button_PlayStop control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_PlayStop_Click(object sender, RoutedEventArgs e)
        {
            StartStop();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Click event of the Button_Pause control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Pause_Click(object sender, RoutedEventArgs e)
        {
            Pause();

            e.Handled = true;
        }


        /// <summary>
        /// Handles the Closed event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (this.IsRunning)
            {
                this.dtTimer.Stop();
            }

            SetDeviceVisibility();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Tritt ein, wenn sich ein Eigenschaftswert ändert.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Fires the property changed.
        /// </summary>
        /// <param name="oSource">The o source.</param>
        /// <param name="strPropertyName">Name of the string property.</param>
        private void FirePropertyChanged([CallerMemberName]string strPropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strPropertyName));
        }

    } // end public partial class SimulationDialog
}
