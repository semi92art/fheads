using Common.Helpers;
using mazing.common.Runtime;
using mazing.common.Runtime.Helpers;
using UnityEngine;
using UnityEngine.Events;

namespace Common.Managers.PlatformGameServices.GameServiceAuth
{
    public abstract class PlatformGameServiceAuthenticatorBase : InitBase, IPlatformGameServiceAuthenticator
    {
        public virtual bool IsAuthenticated => Social.localUser.authenticated;
        
        public virtual void AuthenticatePlatformGameService(UnityAction<bool> _OnFinish)
        {
            Social.localUser.Authenticate(_Success =>
            {
                _OnFinish?.Invoke(_Success);
                if (!_Success)
                    Dbg.LogError("Failed to authenticate to Social");
            });
        }
        
        protected static string AuthMessage(bool _Success, string _AddMessage)
        {
            return $"{(_Success ? "Success" : "Fail")} on authentication to game service: {_AddMessage}";
        }
    }
}