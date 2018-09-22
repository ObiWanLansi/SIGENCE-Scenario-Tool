using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;

using GMap.NET;
using GMap.NET.MapProviders;

using ICSharpCode.TextEditor.Document;

using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //---------------------------------------------------------------------------------------------------------

            GMapProvider.WebProxy = WebRequest.DefaultWebProxy;
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;

            this.mcMapControl.DragButton = MouseButton.Left;
            this.mcMapControl.Manager.Mode = AccessMode.ServerAndCache;
            this.mcMapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            this.mcMapControl.ShowCenter = false;
            this.mcMapControl.MinZoom = 2;
            this.mcMapControl.MaxZoom = 22;
            this.mcMapControl.Position = new PointLatLng(47.667898, 9.384733);
            this.mcMapControl.Zoom = 18;
            this.mcMapControl.MapProvider = GMapProviders.OpenStreetMap;

            //---------------------------------------------------------------------------------------------------------

            this.tecTextEditorControl.HideMouseCursor = true;
            this.tecTextEditorControl.LineViewerStyle = LineViewerStyle.FullRow;
            this.tecTextEditorControl.ConvertTabsToSpaces = true;

            this.tecTextEditorControl.ShowSpaces = true;
            this.tecTextEditorControl.ShowTabs = true;
            //this.tecTextEditorControl.ShowSpaces = false;
            //this.tecTextEditorControl.ShowTabs = false;

            this.tecTextEditorControl.ShowEOLMarkers = false;
            this.tecTextEditorControl.ShowLineNumbers = true;

            this.tecTextEditorControl.IsIconBarVisible = true;
            this.tecTextEditorControl.AllowCaretBeyondEOL = true;
            this.tecTextEditorControl.AllowDrop = true;
            this.tecTextEditorControl.VRulerRow = 80;


            HighlightingManager.Manager.AddSyntaxModeFileProvider(new PythonSyntaxModeFileProvider());
            this.tecTextEditorControl.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("Python");

            string strFilename = string.Format("{0}\\HelloWorld.py", Tool.StartupPath);
            this.tecTextEditorControl.Text = File.ReadAllText(strFilename);
        }

    } //end public partial class MainWindow
}
