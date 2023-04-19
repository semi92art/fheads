#if NICE_VIBRATIONS_3_9
using Common.Helpers;
using mazing.common.Runtime.Exceptions;
using mazing.common.Runtime.Helpers;
using mazing.common.Runtime.Settings;
using MoreMountains.NiceVibrations;
using Zenject;

namespace Common.Managers
{
    public class HapticsManagerNiceVibrations_3_9 : InitBase, IHapticsManager
    {
        #region inject
        
        private IHapticsSetting Setting { get; set; }

        [Inject]
        private void Inject(IHapticsSetting _Setting)
        {
            Setting = _Setting;
        }

        #endregion

        #region api

        public void PlayPreset(EHapticsPresetType _Preset)
        {
            if (!Setting.Get())
                return;
            if (!IsHapticsSupported())
                return;
            MMVibrationManager.StopAllHaptics(true);
            HapticTypes type;
            switch (_Preset)
            {
                case EHapticsPresetType.Selection:
                    type = HapticTypes.Selection;
                    break;
                case EHapticsPresetType.Success:
                    type = HapticTypes.Success;
                    break;
                case EHapticsPresetType.Warning:
                    type = HapticTypes.Warning;
                    break;
                case EHapticsPresetType.Failure:
                    type = HapticTypes.Failure;
                    break;
                case EHapticsPresetType.LightImpact:
                    type = HapticTypes.LightImpact;
                    break;
                case EHapticsPresetType.MediumImpact:
                    type = HapticTypes.MediumImpact;
                    break;
                case EHapticsPresetType.HeavyImpact:
                    type = HapticTypes.HeavyImpact;
                    break;
                case EHapticsPresetType.RigidImpact:
                    type = HapticTypes.RigidImpact;
                    break;
                case EHapticsPresetType.SoftImpact:
                    type = HapticTypes.SoftImpact;
                    break;
                case EHapticsPresetType.None:
                    type = HapticTypes.None;
                    break;
                default:
                    throw new SwitchCaseNotImplementedException(_Preset);
            }
            MMVibrationManager.Haptic(type);
        }

        public void Play(float  _Amplitude, float _Frequency, float? _Duration = null)
        {
            if (!Setting.Get())
                return;
            MMVibrationManager.StopAllHaptics(true);
            if (!_Duration.HasValue)
                MMVibrationManager.TransientHaptic(_Amplitude, _Frequency);
            else 
                MMVibrationManager.ContinuousHaptic(_Amplitude, _Frequency, _Duration.Value);
        }

        public bool IsHapticsSupported()
        {
            return MMVibrationManager.HapticsSupported();
        }

        #endregion
    }
}
#endif