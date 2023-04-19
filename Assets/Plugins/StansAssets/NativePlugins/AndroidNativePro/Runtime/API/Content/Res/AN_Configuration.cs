using System;
using SA.Android.Utilities;

namespace SA.Android.Content.Res
{
    /// <summary>
    /// This class describes all device configuration information that can impact the resources the application retrieves.
    /// This includes both user-specified configuration options (locale list and scaling)
    /// as well as device configurations (such as input modes, screen size and screen orientation).
    /// </summary>
    [Serializable]
    public class AN_Configuration : AN_LinkedObject
    {
        /// <summary>
        ///  Added in API level 8
        ///  Constant for <see cref="UIMode"/>: bits that encode the night mode.
        /// </summary>
        public const int UI_MODE_NIGHT_MASK = 48;

        /// <summary>
        /// Added in API level 8
        /// Constant for <see cref="UIMode"/>: a <see cref="UI_MODE_NIGHT_MASK"/> value that corresponds to the notnight resource qualifier.
        /// </summary>
        public const int UI_MODE_NIGHT_NO = 16;

        /// <summary>
        /// Added in API level 8
        ///  Constant for <see cref="UIMode"/>: a <see cref="UI_MODE_NIGHT_MASK"/> value indicating that no mode type has been set.
        /// </summary>
        public const int UI_MODE_NIGHT_UNDEFINED = 0;

        /// <summary>
        /// Added in API level 8
        /// Constant for <see cref="UIMode"/>: a <see cref="UI_MODE_NIGHT_MASK"/> value that corresponds to the night resource qualifier.
        /// </summary>
        public const int UI_MODE_NIGHT_YES = 32;

        const string k_JavaClassName = "com.stansassets.android.content.res.AN_Configuration";

        public int UIMode => AN_Java.Bridge.CallStatic<int>(k_JavaClassName, "GetUIMode", HashCode);
    }
}
