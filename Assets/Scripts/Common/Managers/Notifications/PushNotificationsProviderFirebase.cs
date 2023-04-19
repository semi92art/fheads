#if FIREBASE && !UNITY_WEBGL
using System.Collections;
using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using Firebase.Messaging;
using mazing.common.Runtime;
using mazing.common.Runtime.Helpers;
using mazing.common.Runtime.Utils;
using UnityEngine;

namespace Common.Managers.Notifications
{
public interface IPushNotificationsProvider : IInit { }
    
    public class PushNotificationsProviderFirebase : InitBase, IPushNotificationsProvider
    {
        #region constants

        private const string Topic = "Topic";
        
        #endregion

        #region inject
        
        private IFirebaseInitializer FirebaseInitializer { get; }

        private PushNotificationsProviderFirebase(IFirebaseInitializer _FirebaseInitializer)
        {
            FirebaseInitializer = _FirebaseInitializer;
        }

        #endregion

        #region api

        public override void Init()
        {
            Cor.Run(InitializeFirebaseMessagingCoroutine());
        }

        #endregion

        #region nonpublic methods

        private IEnumerator InitializeFirebaseMessagingCoroutine()
        {
            if (!FirebaseInitializer.Initialized)
                yield return null;
            if (FirebaseInitializer.DependencyStatus != DependencyStatus.Available)
            {
                Dbg.LogError("Failed to initialize Firebase Messaging," +
                             $" dependency status: {FirebaseInitializer.DependencyStatus}");
                yield break;
            }
            InitMessaging();
        }

        private void InitMessaging()
        {
            if (Application.isEditor)
                return;
            FirebaseMessaging.TokenReceived += OnTokenReceived;
            FirebaseMessaging.MessageReceived += OnMessageReceived;
            SubscribeAndRequestPermissions();
        }

        private void SubscribeAndRequestPermissions()
        {
            FirebaseMessaging.SubscribeAsync(Topic).ContinueWithOnMainThread(_Task => {
                LogTaskCompletion(_Task, "SubscribeAsync");
            });
            base.Init();
            FirebaseMessaging.RequestPermissionAsync().ContinueWithOnMainThread(
                _Task => {
                    LogTaskCompletion(_Task, "RequestPermissionAsync");
                }
            );
        }
        
        private static bool LogTaskCompletion(Task _Task, string _Operation) 
        {
            bool complete = false;
            if (_Task.IsCanceled) 
            {
                Dbg.LogWarning(_Operation + " canceled.");
            } 
            else if (_Task.IsFaulted)
            {
                Dbg.LogError(_Operation + " encounted an error.");
                foreach (System.Exception exception in _Task.Exception!.Flatten().InnerExceptions) 
                {
                    string errorCode = "";
                    if (exception is FirebaseException firebaseEx)
                        errorCode = $"Error.{((Error) firebaseEx.ErrorCode).ToString()}: ";
                    Dbg.LogError(errorCode + exception);
                }
            } else if (_Task.IsCompleted) {
                Dbg.Log(_Operation + " completed");
                complete = true;
            }
            return complete;
        }

        private static void OnTokenReceived(object _Sender, TokenReceivedEventArgs _Args)
        {
            Dbg.Log("Received Registration Token: " + _Args.Token);
        }
        
        private static void OnMessageReceived(object _Sender, MessageReceivedEventArgs _Args)
        {
            Dbg.Log("Received a new message from: " + _Args.Message.From);
        }

        #endregion
    }
}
#endif