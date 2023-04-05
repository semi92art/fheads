#if UNITY_IOS
using System.Collections;
using Common;
using Common.Entities;
using Common.Utils;
using mazing.common.Runtime;
using mazing.common.Runtime.Entities;
using mazing.common.Runtime.Utils;
using SA.iOS.AppTrackingTransparency;
using Unity.Notifications.iOS;


namespace RMAZOR.Helpers
{
    public class IosPermissionsRequester : IPermissionsRequester
    {
        private bool m_NotificationsAuthorized;
        private bool m_AdvertisingIdTrackingRequested;
        
        public Entity<bool> RequestPermissions()
        {
            var entity = new Entity<bool>();
            RequestAdvertisingIdTracking();
            Cor.Run(RequestNotifications());
            Cor.Run(Cor.WaitWhile(
                () => !m_NotificationsAuthorized  || !m_AdvertisingIdTrackingRequested,
                () => entity.Result = EEntityResult.Success));
            return entity;
        }

        private void RequestAdvertisingIdTracking()
        {
            ISN_ATTrackingManager.RequestTrackingAuthorization(_Status =>
            {
                Dbg.Log($"Tracking authorization status: {_Status}");
                m_AdvertisingIdTrackingRequested = true;
            });
        }

        private IEnumerator RequestNotifications()
        {
            const AuthorizationOption authorizationOption = AuthorizationOption.Alert 
                                                            | AuthorizationOption.Badge 
                                                            | AuthorizationOption.Sound;
            using var req = new AuthorizationRequest(authorizationOption, true);
            while (!req.IsFinished)
                yield return null;
            m_NotificationsAuthorized = true;

            string res = "\n RequestAuthorization:";
            res += "\n finished: " + req.IsFinished;
            res += "\n granted :  " + req.Granted;
            res += "\n error:  " + req.Error;
            res += "\n deviceToken:  " + req.DeviceToken;
            Dbg.Log(res);
        }
    }
}
#endif