using System;
using SA.Android.Utilities;

namespace SA.Android.Content.Res
{
    /// <summary>
    /// Class for accessing an application's resources.
    ///
    /// The Android SDK tools compile your application's resources into the application binary at build time.
    /// To use a resource, you must install it correctly in the source tree (inside your project's res/ directory) and build your application. As part of the build process,
    /// the SDK tools generate symbols for each resource,
    /// which you can use in your application code to access the resources.
    ///
    /// Using application resources makes it easy to update various characteristics
    /// of your application without modifying code, and—by providing sets of alternative resources—enables
    /// you to optimize your application for a variety
    /// of device configurations (such as for different languages and screen sizes).
    /// This is an important aspect of developing Android applications that are compatible
    /// on different types of devices.
    /// </summary>
    [Serializable]
    public class AN_Resources : AN_LinkedObject
    {
        const string k_JavaClassName = "com.stansassets.android.content.res.AN_Resources";

        /// <summary>
        /// Return the current configuration that is in effect for this resource object.
        /// The returned object should be treated as read-only.
        /// </summary>
        /// <returns>The <see cref="AN_Configuration"/> instance.</returns>
        public AN_Configuration GetConfiguration()
        {
            return AN_Java.Bridge.CallStatic<AN_Configuration>(k_JavaClassName, "GetConfiguration", HashCode);
        }
    }
}
