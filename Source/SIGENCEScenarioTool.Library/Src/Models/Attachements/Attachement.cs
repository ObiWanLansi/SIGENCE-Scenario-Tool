using System;
using System.Drawing;
using System.IO;

using SIGENCEScenarioTool.Extensions;

namespace SIGENCEScenarioTool.Models.Attachements
{
    /// <summary>
    /// 
    /// </summary>
    public enum AttachementType : byte
    {
        /// <summary>
        /// The link
        /// </summary>
        Link,

        /// <summary>
        /// The embedded
        /// </summary>
        Embedded

    } // end public enum AttachementType



    /// <summary>
    /// 
    /// </summary>
    public sealed class Attachement
    {
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public object Source { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public AttachementType Type { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the added timestamp.
        /// </summary>
        /// <value>
        /// The added timestamp.
        /// </value>
        public DateTime AddedTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the added username.
        /// </summary>
        /// <value>
        /// The added username.
        /// </value>
        public string AddedUsername { get; set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the file icon.
        /// </summary>
        /// <value>
        /// The file icon.
        /// </value>
        //public ImageSource FileIcon { get; set; }
        public Image FileIcon { get; set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //private readonly Icon iDefault = ExtractIcon.GetIcon( "shell32.dll", 0, true );


        /// <summary>
        /// Initializes a new instance of the <see cref="Attachement" /> class.
        /// </summary>
        /// <param name="fiSource">The fi source.</param>
        /// <param name="at">At.</param>
        public Attachement( FileInfo fiSource, AttachementType at = AttachementType.Link )
        {
            this.Source = fiSource;
            this.DisplayName = fiSource.Name;
            this.AddedTimestamp = DateTime.Now;
            this.AddedUsername = Environment.UserName;
            //this.FileIcon = new BitmapImage()
            //this.FileIcon = this.iDefault;
            Icon icon = fiSource.GetAssociatedIcon();
            //this.FileIcon = Imaging.CreateBitmapSourceFromHIcon( icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions() );

            //this.FileIcon = Image.FromHbitmap( icon.ToBitmap().GetHbitmap() );
        }

    } // end public sealed class Attachement
}
