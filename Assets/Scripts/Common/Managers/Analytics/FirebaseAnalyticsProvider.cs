#if FIREBASE
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase;
using Firebase.Analytics;
using mazing.common.Runtime;
using mazing.common.Runtime.Constants;
using mazing.common.Runtime.Managers;
using mazing.common.Runtime.Managers.Analytics;
using mazing.common.Runtime.Utils;

namespace Common.Managers.Analytics
{
    public interface IFirebaseAnalyticsProvider : IAnalyticsProvider { }

    public class FirebaseAnalyticsProvider : AnalyticsProviderBase, IFirebaseAnalyticsProvider
    {
        #region inject
        
        private IFirebaseInitializer FirebaseInitializer { get; }

        private FirebaseAnalyticsProvider(IFirebaseInitializer _FirebaseInitializer)
        {
            FirebaseInitializer = _FirebaseInitializer;
        }

        #endregion
        
        #region api
        
        public override void Init()
        {
            Cor.Run(InitializeAnalyticsCoroutine());
            base.Init();
        }

        #endregion

        #region nonpublic methods

        private IEnumerator InitializeAnalyticsCoroutine()
        {
            if (!FirebaseInitializer.Initialized)
                yield return null;
            if (FirebaseInitializer.DependencyStatus != DependencyStatus.Available)
            {
                Dbg.LogError("Failed to initialize Firebase Analytics," +
                             $" dependency status: {FirebaseInitializer.DependencyStatus}");
                yield break;
            }
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            FirebaseAnalytics.SetUserProperty(
                FirebaseAnalytics.UserPropertySignUpMethod,
                "Google");
            base.Init();
        }

        protected override void SendAnalyticCore(
            string                      _AnalyticId, 
            IDictionary<string, object> _EventData = null)
        {
            if (_AnalyticId == "session_start")
                return;
            if (FirebaseInitializer.DependencyStatus != DependencyStatus.Available)
                return;
            if (FirebaseInitializer.FirebaseApp == null)
                return;
            Parameter[] @params = _EventData?.Select(_Kvp =>
            {
                (string key, var value) = _Kvp;
                Parameter p = value switch
                {
                    short shortVal   => new Parameter(key, shortVal),
                    int intVal       => new Parameter(key, intVal),
                    long longVal     => new Parameter(key, longVal),
                    float floatVal   => new Parameter(key, floatVal),
                    double doubleVal => new Parameter(key, doubleVal),
                    string stringVal => new Parameter(key, stringVal),
                    _                => null
                };
                return p;
            }).Where(_P => _P != null).ToArray();
            if (@params == null)
                FirebaseAnalytics.LogEvent(_AnalyticId);
            else
                FirebaseAnalytics.LogEvent(_AnalyticId, @params);
        }

        protected override string GetRealAnalyticId(string _AnalyticId)
        {
            return _AnalyticId switch
            {
                AnalyticIds.LevelStarted  => FirebaseAnalytics.EventLevelStart,
                AnalyticIds.LevelFinished => FirebaseAnalytics.EventLevelEnd,
                _                               => _AnalyticId
            };
        }

        protected override string GetRealParameterId(string _ParameterId)
        {
            return _ParameterId switch
            {
                AnalyticIds.ParameterLevelIndex       => FirebaseAnalytics.ParameterLevel,
                AnalyticIds.Parameter1ForTestAnalytic => FirebaseAnalytics.ParameterIndex,
                _                                     => _ParameterId
            };
        }

        #endregion
    }
}
#endif