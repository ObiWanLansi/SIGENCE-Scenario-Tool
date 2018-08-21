using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

using QRCoder;

using SIGENCEScenarioTool.ViewModels;



namespace SIGENCEScenarioTool.Dialogs
{
    /// <summary>
    /// Interaktionslogik für QRCodeDialog.xaml
    /// </summary>
    public partial class QRCodeDialog : Window
    {
        /// <summary>
        /// The qr generator
        /// </summary>
        static private readonly QRCodeGenerator qrGenerator = new QRCodeGenerator();


        /// <summary>
        /// Gets or sets the rf device.
        /// </summary>
        /// <value>
        /// The rf device.
        /// </value>
        private RFDeviceViewModel RFDevice { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="QRCodeDialog" /> class.
        /// </summary>
        /// <param name="rfdefvice">The rfdefvice.</param>
        public QRCodeDialog(RFDeviceViewModel rfdefvice)
        {
            InitializeComponent();

            RFDevice = rfdefvice;
            DataContext = rfdefvice;

            CreateQRCode();
        }


        /// <summary>
        /// Creates the qr code.
        /// </summary>
        private void CreateQRCode()
        {
            string strQRCodeData = new PayloadGenerator.Geolocation(RFDevice.Latitude.ToString(), RFDevice.Longitude.ToString()).ToString();

            QRCode qrCode = new QRCode(qrGenerator.CreateQrCode(strQRCodeData, QRCodeGenerator.ECCLevel.Q));
            Bitmap bmp = qrCode.GetGraphic(5, Color.Black, Color.White, false);

            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();

            imgQRCode.Source = bi;
        }

    } // end public partial class QRCodeDialog
}
