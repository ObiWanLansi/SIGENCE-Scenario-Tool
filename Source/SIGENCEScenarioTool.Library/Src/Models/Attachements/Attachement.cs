//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Windows.Interop;
//using System.Windows.Media.Imaging;

//using SIGENCEScenarioTool.Extensions;
//using SIGENCEScenarioTool.Tools;



//namespace SIGENCEScenarioTool.Models.Attachements
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public enum AttachementType : byte
//    {
//        /// <summary>
//        /// The link
//        /// </summary>
//        Link,

//        /// <summary>
//        /// The embedded
//        /// </summary>
//        Embedded

//    } // end public enum AttachementType



//    /// <summary>
//    /// 
//    /// </summary>
//    public sealed class Attachement
//    {
//        /// <summary>
//        /// Gets or sets the source.
//        /// </summary>
//        /// <value>
//        /// The source.
//        /// </value>
//        public object Source { get; set; }

//        /// <summary>
//        /// Gets or sets the type.
//        /// </summary>
//        /// <value>
//        /// The type.
//        /// </value>
//        public AttachementType Type { get; set; }

//        /// <summary>
//        /// Gets or sets the display name.
//        /// </summary>
//        /// <value>
//        /// The display name.
//        /// </value>
//        public string DisplayName { get; set; }

//        /// <summary>
//        /// Gets or sets the size.
//        /// </summary>
//        /// <value>
//        /// The size.
//        /// </value>
//        public string FileSize { get; set; }

//        /// <summary>
//        /// Gets or sets the file timestamp.
//        /// </summary>
//        /// <value>
//        /// The file timestamp.
//        /// </value>
//        public DateTime FileTimestamp { get; set; }

//        /// <summary>
//        /// Gets or sets the added timestamp.
//        /// </summary>
//        /// <value>
//        /// The added timestamp.
//        /// </value>
//        public DateTime AddedTimestamp { get; set; }

//        /// <summary>
//        /// Gets or sets the added username.
//        /// </summary>
//        /// <value>
//        /// The added username.
//        /// </value>
//        public string AddedUsername { get; set; }

//        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


//        /// <summary>
//        /// Gets or sets the bitmap.
//        /// </summary>
//        /// <value>
//        /// The bitmap.
//        /// </value>
//        public BitmapSource FileIcon { get; set; }


//        /// <summary>
//        /// Gets the timestamp.
//        /// </summary>
//        /// <value>
//        /// The timestamp.
//        /// </value>
//        public string Timestamp
//        {
//            get
//            {
//                return this.FileTimestamp.Fmt_DD_MM_YYYY_HH_MM();
//            }
//        }

//        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


//        /// <summary>
//        /// Initializes a new instance of the <see cref="Attachement" /> class.
//        /// </summary>
//        /// <param name="fiSource">The fi source.</param>
//        /// <param name="at">At.</param>
//        public Attachement(FileInfo fiSource, AttachementType at = AttachementType.Link)
//        {
//            this.Source = fiSource;
//            this.Type = at;
//            this.DisplayName = fiSource.Name;
//            this.AddedTimestamp = DateTime.Now;
//            this.AddedUsername = Environment.UserName;

//            if (fiSource.Exists == true)
//            {
//                using (Icon icon = fiSource.GetAssociatedIcon())
//                {
//                    this.FileIcon = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
//                }

//                this.FileSize = fiSource.GetFileSize();
//                this.FileTimestamp = fiSource.LastWriteTime;
//            }
//            else
//            {
//                using (Icon icon = ExtractIcon.GetIcon("shell32.dll", 0, true))
//                {
//                    this.FileIcon = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
//                }
//            }
//        }

//    } // end public sealed class Attachement



//    /// <summary>
//    /// 
//    /// </summary>
//    /// <seealso cref="List{SIGENCEScenarioTool.Models.Attachements.Attachement}" />
//    public sealed class AttachementList : List<Attachement>
//    {
//    } // end public sealed class AttachementList 
//}
