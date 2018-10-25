using System;



namespace SIGENCEScenarioTool.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// Check if the class is derived from a other class.
        /// </summary>
        /// <param name="tClass">The t class.</param>
        /// <param name="tBase">The t base.</param>
        /// <returns></returns>
        public static bool DerivedFromType(this Type tClass, Type tBase)
        {
            if (tClass.BaseType == null || tClass.BaseType.FullName == null)
            {
                return false;
            }

            return tClass.BaseType.FullName == tBase.FullName || DerivedFromType(tClass.BaseType, tBase);
        }


        /// <summary>
        /// Check if the class implements the interface.
        /// </summary>
        /// <param name="tClass">The t class.</param>
        /// <param name="tInterface">The t interface.</param>
        /// <returns></returns>
        public static bool ImplementsInterface(this Type tClass, Type tInterface)
        {
            foreach (Type t in tClass.GetInterfaces())
            {
                if (t == tInterface)
                {
                    return true;
                }
            }

            return false;
        }

    } // end static public class TypeExtension
}
