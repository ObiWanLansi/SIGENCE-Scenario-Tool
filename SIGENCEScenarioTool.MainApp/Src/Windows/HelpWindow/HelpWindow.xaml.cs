using System.IO;
using System.Windows;
using System.Windows.Xps.Packaging;



namespace SIGENCEScenarioTool.Windows.HelpWindow
{

    /// <summary>
    /// Interaktionslogik für HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpWindow" /> class.
        /// </summary>
        public HelpWindow()
        {
            InitializeComponent();

            XpsDocument xps = new XpsDocument("CheatSheet.xps", FileAccess.Read);
            documentViewer.Document = xps.GetFixedDocumentSequence();
        }

    } // end public partial class HelpWindow
}
