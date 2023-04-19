using System;
using mazing.common.Runtime;
using Unity.Services.Core;
using UnityEngine;

namespace Common.Helpers
{
    public class UnityGameServicesLauncher : MonoBehaviour
    {
        private async void Start()
        {
            try
            {
                await UnityServices.InitializeAsync();
            }
            catch (Exception exception)
            {
                Dbg.LogError(exception);
            }
        }
    }
}