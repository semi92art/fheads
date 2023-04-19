using System;
using System.Collections.Generic;
using SA.Android.App;
using UnityEngine;
using SA.Android.Content.Pm;
using SA.Android.Content.Res;
using SA.Android.GMS.Games;
using SA.Android.Utilities;

namespace SA.Android.Content
{
    /// <summary>
    /// Interface to global information about an application environment. 
    /// This is an abstract class whose implementation is provided by the Android system. 
    /// It allows access to application-specific resources and classes, as well as up-calls 
    /// for application-level operations such as launching activities, broadcasting and receiving intents, etc.
    /// </summary>
    [Serializable]
    public abstract class AN_Context
    {
#pragma warning disable 414

        [SerializeField]
        protected AN_ActivityId m_classId = AN_ActivityId.Undefined;
        [SerializeField]
        protected string m_instanceId = string.Empty;

#pragma warning restore 414

        const string k_ContextClassName = "com.stansassets.android.content.AN_Context";
        
        /// <summary>
        /// Return PackageManager instance to find global package information.
        /// </summary>
        public abstract AN_PackageManager GetPackageManager();

        public AN_Resources GetResources()
        {
            return AN_Java.Bridge.CallStatic<AN_Resources>(k_ContextClassName, "GetResources", this);
        }
    }
}
