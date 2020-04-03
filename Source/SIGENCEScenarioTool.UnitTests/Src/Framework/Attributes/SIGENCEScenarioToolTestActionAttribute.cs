using System;

using log4net;

using NUnit.Framework;
using NUnit.Framework.Interfaces;



namespace SIGENCEScenarioTool.UnitTest.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SIGENCEScenarioToolTestActionAttribute : Attribute, ITestAction
    {
        /// <summary>
        /// Logger zum Ausgeben der Protokollierung.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(SIGENCEScenarioToolTestActionAttribute));

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        #region ITestAction Member


        /// <summary>
        /// Executed before each test is run
        /// </summary>
        /// <param name="test">The test that is going to be run.</param>
        public void BeforeTest( ITest test ) => Log.InfoFormat("BeforeTest: [{0}] {1}", DateTime.Now.ToString("dd.MM.yyyy, HH:mm:ss"), test.FullName);


        /// <summary>
        /// Executed after each test is run
        /// </summary>
        /// <param name="test">The test that has just been run.</param>
        public void AfterTest( ITest test ) => Log.InfoFormat("AfterTest: [{0}] {1}", DateTime.Now.ToString("dd.MM.yyyy, HH:mm:ss"), test.FullName);


        /// <summary>
        /// Provides the target for the action attribute
        /// </summary>
        public ActionTargets Targets
        {
            get { return ActionTargets.Suite; }
        }

        #endregion

    } // end sealed public class SIGENCEScenarioToolTestActionAttribute
}
