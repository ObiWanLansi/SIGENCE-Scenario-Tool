using System.ComponentModel;
using System.Runtime.CompilerServices;



namespace SIGENCEScenarioTool.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    public abstract class AbstractModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Fires the property changed.
        /// </summary>
        /// <param name="strPropertyName">Name of the string property.</param>
        protected void FirePropertyChanged( [CallerMemberName]string strPropertyName = null )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( strPropertyName ) );
        }

    } // end public abstract class AbstractModelBase
}
