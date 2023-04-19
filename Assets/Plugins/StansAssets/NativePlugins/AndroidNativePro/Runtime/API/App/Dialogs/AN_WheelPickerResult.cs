using UnityEngine;
using SA.Foundation.Templates;
using System;
using StansAssets.Foundation;

namespace SA.Android.App
{
    /// <summary>
    /// Object that contains a result of picking a element from wheel
    /// using the <see cref="AN_WheelPickerDialog"/>
    /// </summary>
    [Serializable]
    public class AN_WheelPickerResult : SA_Result
    {
        [SerializeField]
        string m_Value = default;

        [SerializeField]
        string m_State = default;
        
        /// <summary>
        /// The value that was picked by user.
        /// </summary>
        public string Value => m_Value;
        
        /// <summary>
        /// Picker state, see <see cref="AN_WheelPickerState"/> for more info.
        /// </summary>
        public AN_WheelPickerState State =>
            EnumUtility.TryParseEnum<AN_WheelPickerState>(m_State, out var  state) 
                ? state 
                : AN_WheelPickerState.Canceled;
    }
}
