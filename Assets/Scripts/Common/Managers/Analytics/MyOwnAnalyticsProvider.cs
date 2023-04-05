using System.Collections.Generic;
using Common.Utils;
using mazing.common.Runtime.Entities;
using mazing.common.Runtime.Managers;
using mazing.common.Runtime.Managers.Analytics;
using mazing.common.Runtime.Network;
using mazing.common.Runtime.Network.Packets;
using mazing.common.Runtime.PreciseLocale;
using mazing.common.Runtime.Utils;
using UnityEngine.Device;

namespace Common.Managers.Analytics
{
    public interface IMyOwnAnalyticsProvider : IAnalyticsProvider { }
    
    public class MyOwnAnalyticsProvider : AnalyticsProviderBase, IMyOwnAnalyticsProvider
    {
        private string m_IdfaCached;
        
        private IGameClient GameClient { get; }

        public MyOwnAnalyticsProvider(IGameClient _GameClient)
        {
            GameClient = _GameClient;
        }
        
        protected override void SendAnalyticCore(
            string                      _AnalyticId,
            IDictionary<string, object> _EventData = null)
        {
            if (Application.isEditor)
                return;
            if (_AnalyticId == null)
                return;
            var packetEntity = CreatePacketEntity(_AnalyticId, _EventData);
            Cor.Run(Cor.WaitWhile(
                () => packetEntity.Result == EEntityResult.Pending,
                () => GameClient.Send(packetEntity.Value)));
        }

        protected override string GetRealAnalyticId(string _AnalyticId)
        {
            return _AnalyticId;
        }

        protected override string GetRealParameterId(string _ParameterId)
        {
            return _ParameterId;
        }
        
        private static Entity<GameUserEventPacket> CreatePacketEntity(
            string _AnalyticId,
            IDictionary<string, object> _EventData = null)
        {
            var result = new Entity<GameUserEventPacket>();
            var idfaEntity = MazorCommonUtils.GetIdfa();
            Cor.Run(Cor.WaitWhile(
                () => idfaEntity.Result == EEntityResult.Pending,
                () =>
                {
                    var gameUserDto = new GameUserDto
                    {
                        Idfa       = idfaEntity.Value,
                        Action     = _AnalyticId,
                        Country    = PreciseLocale.GetRegion(),
                        Language   = PreciseLocale.GetLanguage(),
                        AppVersion = Application.version,
                        Platform   = Application.platform.ToString(),
                        EventData  = _EventData
                    };
                    result.Value = new GameUserEventPacket(gameUserDto);
                    result.Result = EEntityResult.Success;
                }));
            return result;
        }
    }
}