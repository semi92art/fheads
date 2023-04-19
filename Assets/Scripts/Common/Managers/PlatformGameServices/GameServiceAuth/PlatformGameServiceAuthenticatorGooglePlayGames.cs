#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using mazing.common.Runtime;
using mazing.common.Runtime.Ticker;
using UnityEngine.Events;

namespace Common.Managers.PlatformGameServices.GameServiceAuth
{
    public class PlatformGameServiceAuthenticatorGooglePlayGames :
        PlatformGameServiceAuthenticatorBase,
        IApplicationPause
    {
        private ICommonTicker Ticker { get; }

        public PlatformGameServiceAuthenticatorGooglePlayGames(ICommonTicker _Ticker)
        {
            Ticker = _Ticker;
        }

        public override void Init()
        {
            Ticker.Register(this);
            base.Init();
        }

        public override bool IsAuthenticated => PlayGamesPlatform.Instance.IsAuthenticated() && base.IsAuthenticated;

        public override void AuthenticatePlatformGameService(UnityAction<bool> _OnFinish)
        {
            AuthenticateAndroid(_OnFinish);
        }
        
        private void AuthenticateAndroid(UnityAction<bool> _OnFinish)
        {
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.Authenticate(
                _Status =>
                {
                    if (_Status == SignInStatus.Success)
                    {
                        Dbg.Log(AuthMessage(true, string.Empty));
                        base.AuthenticatePlatformGameService(_OnFinish);
                    }
                    else
                    {
                        Dbg.LogWarning(AuthMessage(false, _Status.ToString()));
                        _OnFinish?.Invoke(false);
                    }
                });
        }

        public void OnApplicationPause(bool _Pause)
        {
            throw new System.NotImplementedException();
        }
    }
}
#endif