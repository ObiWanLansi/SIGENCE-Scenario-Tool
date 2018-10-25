using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using log4net;

using NUnit.Framework;
using NUnit.Framework.Internal;

using SIGENCEScenarioTool.UnitTest.Attributes;



namespace SIGENCEScenarioTool.UnitTests
{
    /// <summary>
    /// 
    /// </summary>
    public static class SIGENCEScenarioToolTestCaseHelper
    {
        /// <summary>
        /// Logger zum Ausgeben der Protokollierung.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(SIGENCEScenarioToolTestCaseHelper));

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="desc">The desc.</param>
        /// <returns></returns>
        private static string GetDescription(DescriptionAttribute desc)
        {
            if (desc != null && desc.Properties != null && desc.Properties.ContainsKey(PropertyNames.Description))
            {
                return (string)desc.Properties.Get(PropertyNames.Description);
            }

            return "";
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Validates the test case information.
        /// </summary>
        public static void ValidateTestCaseInformation()
        {
            HashSet<string> hsTestCaseId = new HashSet<string>();

            Assembly a = Assembly.GetCallingAssembly();

            Log.InfoFormat("Validating TestAssembly {0}", a.FullName);

            foreach (Type t in a.GetTypes())
            {
                Log.InfoFormat("    Validating TestClass {0}", t.Name);

                foreach (MethodInfo mi in from mi in t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                                          let itcAttributes = mi.GetCustomAttributes(typeof(TestAttribute), false)
                                          where itcAttributes.Length == 1
                                          select mi)
                {
                    Log.InfoFormat("        Validating TestCase {0}", mi.Name);

                    object[] opeaAttributes = mi.GetCustomAttributes(typeof(SIGENCEScenarioToolTestCaseAttribute), false);
                    object[] descAttributes = mi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (opeaAttributes.Length != 1 || descAttributes.Length != 1)
                    {
                        Assert.Fail(string.Format("The testmethod {0}.{1} is no valid testmethod.", t.FullName, mi.Name));
                    }

                    string strTestCasseId = (opeaAttributes[0] as SIGENCEScenarioToolTestCaseAttribute).Id.ToString();

                    if (hsTestCaseId.Contains(strTestCasseId) == false)
                    {
                        hsTestCaseId.Add(strTestCasseId);
                    }
                    else
                    {
                        string strMessage = string.Format("{0}.{1}: The TestCaseId \"{2}\" is multiple available and therefore inadmissible.", t.FullName, mi.Name, strTestCasseId);

                        Log.Fatal(strMessage);
                        Assert.Fail(strMessage);
                    }
                }
            }
        }


        /// <summary>
        /// Shows the test case information.
        /// </summary>
        public static void ShowTestCaseInformation()
        {
            StackFrame sf = new StackFrame(1, true);

            MethodBase meba = sf.GetMethod();

            object[] itcAttributes = meba.GetCustomAttributes(typeof(SIGENCEScenarioToolTestCaseAttribute), false);
            object[] descAttributes = meba.GetCustomAttributes(typeof(DescriptionAttribute), false);

            SIGENCEScenarioToolTestCaseAttribute tca = itcAttributes[0] as SIGENCEScenarioToolTestCaseAttribute;
            DescriptionAttribute desc = descAttributes[0] as DescriptionAttribute;

            Log.InfoFormat("TestCase   : {0}", tca.Id);
            Log.InfoFormat("Description: {0}", GetDescription(desc));
            Log.InfoFormat("Method     : {0}", meba);
            Log.InfoFormat("Class      : {0}", meba.DeclaringType.FullName);
            Log.InfoFormat("Assembly   : {0}", meba.DeclaringType.AssemblyQualifiedName);
            Log.InfoFormat("Sourcode   : {0}", sf.GetFileName());
        }

    } // end static public class SIGENCEScenarioToolTestCaseHelper
}
