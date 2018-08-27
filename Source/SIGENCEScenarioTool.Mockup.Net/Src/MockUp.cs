using System;

using SIGENCEScenarioTool.Tools;



namespace SIGENCEScenarioTool.Mockup
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class MockUp
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            Console.Title = Tool.ProductTitle;

            //SimpleMockup.Main(args);
            ExtendedMockup.Main(args);
        }

    } // end sealed class MockUp
}
