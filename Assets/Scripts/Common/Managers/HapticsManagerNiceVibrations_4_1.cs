#if !NICE_VIBRATIONS_3_9
using System;
using Common.Exceptions;
using Common.Helpers;
using Common.Settings;
using Zenject;
using Lofelt.NiceVibrations;

namespace Common.Managers
{
    public class HapticsManagerNiceVibrations_4_1 : InitBase, IHapticsManager
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
        
        public override void Init()
        {
            if (Initialized)
                return;
            HapticController.Init();
            Setting.ValueSet += EnableHaptics;
            EnableHaptics(Setting.Get());
            base.Init();
        }
        
        public void PlayPreset(EHapticsPresetType _Preset)
        {
            if (!Setting.Get())
                return;
            HapticController.Reset();
            HapticPatterns.PresetType presetType;
            switch (_Preset)
            {
                case EHapticsPresetType.Selection:
                    presetType = HapticPatterns.PresetType.Selection; break;
                case EHapticsPresetType.Success:
                    presetType = HapticPatterns.PresetType.Success; break;
                case EHapticsPresetType.Warning:
                    presetType = HapticPatterns.PresetType.Warning; break;
                case EHapticsPresetType.Failure:
                    presetType = HapticPatterns.PresetType.Failure; break;
                case EHapticsPresetType.LightImpact:
                    presetType = HapticPatterns.PresetType.LightImpact; break;
                case EHapticsPresetType.MediumImpact:
                    presetType = HapticPatterns.PresetType.MediumImpact; break;
                case EHapticsPresetType.HeavyImpact:
                    presetType = HapticPatterns.PresetType.HeavyImpact; break;
                case EHapticsPresetType.RigidImpact:
                    presetType = HapticPatterns.PresetType.RigidImpact; break;
                case EHapticsPresetType.SoftImpact:
                    presetType = HapticPatterns.PresetType.SoftImpact; break;
                case EHapticsPresetType.None:
                    presetType = HapticPatterns.PresetType.None; break;
                default: throw new SwitchCaseNotImplementedException(_Preset);
            }

            try
            {
                HapticPatterns.PlayPreset(presetType);
            }
            catch(Exception ex)
            {
                Dbg.Log(ex);
                HapticPatterns.PlayEmphasis(0f, 0f);
            }
        }

        public void Play(float _Amplitude, float _Frequency, float? _Duration = null)
        {
            if (!Setting.Get())
                return;
            if(_Duration.HasValue)
                HapticPatterns.PlayConstant(_Amplitude, _Frequency, _Duration.Value);
            else
                HapticPatterns.PlayEmphasis(_Amplitude, _Frequency);
        }

        #endregion

        #region nonpublic methods

        private static void EnableHaptics(bool _Enable)
        {
            HapticController.hapticsEnabled = _Enable;
        }

        #endregion
    }
}
#endif
