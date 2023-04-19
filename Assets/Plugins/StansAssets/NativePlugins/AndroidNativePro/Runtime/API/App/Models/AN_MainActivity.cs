using System;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Android.App
{
    /// <summary>
    /// Unity Android application main activity.
    /// </summary>
    [Serializable]
    public class AN_MainActivity : AN_Activity
    {
        static AN_MainActivity s_Instance = null;

        //--------------------------------------
        // Initialization
        //--------------------------------------

        /// <summary>
        /// Returns a singleton class instance
        /// </summary>
        public static AN_MainActivity Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new AN_MainActivity();
                    s_Instance.m_classId = AN_ActivityId.Main;
                }

                return s_Instance;
            }
        }
    }
}
