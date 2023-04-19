using Common.Helpers;
using mazing.common.Runtime.Helpers;
using UnityEngine.Events;

namespace Common.Managers.PlatformGameServices.GameServiceAuth
{
    public class PlatformGameServiceAuthenticatorFake : InitBase, IPlatformGameServiceAuthenticator
    {
        public bool IsAuthenticated => false;
        
        public void AuthenticatePlatformGameService(UnityAction<bool> _OnFinish) { }
    }
}