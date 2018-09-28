using System;

using Android.Runtime;

using Esri.ArcGISRuntime;

namespace ArcGISApp
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Android.App.Application" />
    [Android.App.Application]
    internal class Application : Android.App.Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="transfer">The transfer.</param>
        public Application(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {

        }

        /// <summary>
        /// Called when the application is starting, before any activity, service,
        /// or receiver objects (excluding content providers) have been created.
        /// </summary>
        /// <remarks>
        /// <para tool="javadoc-to-mdoc">Called when the application is starting, before any activity, service,
        /// or receiver objects (excluding content providers) have been created.
        /// Implementations should be as quick as possible (for example using
        /// lazy initialization of state) since the time spent in this function
        /// directly impacts the performance of starting the first activity,
        /// service, or receiver in a process.
        /// If you override this method, be sure to call super.onCreate().
        /// </para>
        /// <para tool="javadoc-to-mdoc">
        ///   <format type="text/html">
        ///     <a href="http://developer.android.com/reference/android/app/Application.html#onCreate()" target="_blank">[Android Documentation]</a>
        ///   </format>
        /// </para>
        /// </remarks>
        /// <since version="Added in API level 1" />
        public override void OnCreate()
        {
            base.OnCreate();

            // Deployed applications must be licensed at the Lite level or greater. 
            // See https://developers.arcgis.com/licensing for further details.

            // Initialize the ArcGIS Runtime before any components are created.
            ArcGISRuntimeEnvironment.Initialize();
        }
    }
}