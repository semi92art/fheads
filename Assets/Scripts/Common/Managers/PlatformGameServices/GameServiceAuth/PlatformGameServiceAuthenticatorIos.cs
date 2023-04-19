using System.Text;
using mazing.common.Runtime;
using SA.iOS.GameKit;
using UnityEngine.Events;

namespace Common.Managers.PlatformGameServices.GameServiceAuth
{
    public class PlatformGameServiceAuthenticatorIos : PlatformGameServiceAuthenticatorBase
    {
        public override bool IsAuthenticated => ISN_GKLocalPlayer.LocalPlayer.Authenticated && base.IsAuthenticated;

        public override void AuthenticatePlatformGameService(UnityAction<bool> _OnFinish)
        {
            AuthenticateIos(_OnFinish);
        }

        private void AuthenticateIos(UnityAction<bool> _OnFinish)
        {
            ISN_GKLocalPlayer.SetAuthenticateHandler(_Result =>
            {
                if (_Result.IsSucceeded)
                {
                    var player = ISN_GKLocalPlayer.LocalPlayer;
                    var sb = new StringBuilder();
                    sb.AppendLine($"player game id: {player.GamePlayerId}");
                    sb.AppendLine($"player team id: {player.TeamPlayerId}");
                    sb.AppendLine($"player Alias: {player.Alias}");
                    sb.AppendLine($"player DisplayName: {player.DisplayName}");
                    sb.AppendLine($"player Authenticated: {player.Authenticated}");
                    sb.AppendLine($"player Underage: {player.Underage}");
                    Dbg.Log(sb.ToString());
                    player.GenerateIdentityVerificationSignatureWithCompletionHandler(_SignatureResult =>
                    {
                        if (_SignatureResult.IsSucceeded)
                        {
                            sb.Clear();
                            sb.AppendLine($"signatureResult.PublicKeyUrl: {_SignatureResult.PublicKeyUrl}");
                            sb.AppendLine($"signatureResult.Timestamp: {_SignatureResult.Timestamp}");
                            sb.AppendLine($"signatureResult.Salt.Length: {_SignatureResult.Salt.Length}");
                            sb.AppendLine($"signatureResult.Signature.Length: {_SignatureResult.Signature.Length}");
                            Dbg.Log(sb.ToString());
                            base.AuthenticatePlatformGameService(_OnFinish);
                        }
                        else
                        {
                            Dbg.LogError("IdentityVerificationSignature has failed: " +
                                         $"{_SignatureResult.Error.FullMessage}");
                            _OnFinish?.Invoke(false);
                        }
                    });
                }
                else
                {
                    Dbg.LogError(AuthMessage(false,
                        $"Error with code: {_Result.Error.Code} and description: {_Result.Error.Message}"));
                }
            });
        }
    }
}