using System;

using SIGENCEScenarioTool.Extensions;



namespace SIGENCEScenarioTool.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IEquatable{SIGENCEScenarioTool.Models.RFDevice}" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="System.ICloneable" />
    /// <seealso cref="SIGENCEScenarioTool.Interfaces.IXmlExport" />
    sealed public partial class RFDevice
    {

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            if (PrimaryKey == Guid.Empty)
            {
                return false;
            }

            if (Name.IsEmpty() == true)
            {
                return false;
            }

            //if (DeviceSource == DeviceSource.Unknown)
            //{
            //    return false;
            //}

            return true;
        }

    } // end sealed public partial class RFDevice
}
