using System;

using System.Reflection;
using System.Windows.Controls;



namespace SIGENCEScenarioTool.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataGridExtension
    {
        /// <summary>
        /// Gets the focused value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dg">The dg.</param>
        /// <returns></returns>
        public static Tuple<PropertyInfo, object> GetFocusedItem(this DataGrid dg)
        {
            var currentcell = dg.CurrentCell;

            PropertyInfo pi = currentcell.Item.GetType().GetProperty(currentcell.Column.SortMemberPath);

            object oValue = pi.GetValue(currentcell.Item);

            return new Tuple<PropertyInfo, object>(pi, oValue);
        }

    } // end static public class DataGridExtension
}
