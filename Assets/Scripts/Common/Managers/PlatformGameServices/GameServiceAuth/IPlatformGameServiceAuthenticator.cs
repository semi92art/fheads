using mazing.common.Runtime;
using UnityEngine.Events;

namespace Common.Managers.PlatformGameServices.GameServiceAuth
{
    public interface IPlatformGameServiceAuthenticator : IInit
    {
        void AuthenticatePlatformGameService(UnityAction<bool> _OnFinish);
        bool IsAuthenticated { get; }
    }
}