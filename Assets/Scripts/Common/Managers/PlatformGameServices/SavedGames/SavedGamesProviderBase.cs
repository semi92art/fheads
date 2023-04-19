using System;
using System.Linq;
using Common.Entities;
using Common.Utils;
using mazing.common.Runtime;
using mazing.common.Runtime.Entities;
using mazing.common.Runtime.Helpers;
using mazing.common.Runtime.Network;
using mazing.common.Runtime.Network.DataFieldFilters;
using mazing.common.Runtime.Utils;
using Newtonsoft.Json;

namespace Common.Managers.PlatformGameServices.SavedGames
{
    public class SavedGamesProvider : InitBase, ISavedGameProvider
    {
        #region inject

        private IGameClient GameClient { get; }

        protected SavedGamesProvider(
            IGameClient _GameClient)
        {
            GameClient = _GameClient;
        }

        #endregion

        #region api
        
        public event GameSavedAction GameSaved;

        public void SaveGame(SavedGameV2 _SavedGame)
        {
            SaveGameProgressToCache(_SavedGame);
        }
        
        public SavedGameV2 GetSavedGame(string _FileName)
        {
            return GetSavedGameFromCacheSync(_FileName);
        }

        public void SaveGameProgress<T>(T _Data) where T : FileNameArgs
        {
            SaveGameProgressToCache(_Data);
        }
        
        public Entity<object> GetSavedGameProgress(string _FileName)
        {
            return GetSavedGameProgressFromCache(_FileName);
        }

        #endregion

        #region nonpublic methods
        
        protected void SaveGameProgressToCache(object _Data)
        {
            FileNameArgs fileNameData;
            try
            {
                string ser = JsonConvert.SerializeObject(_Data);
                fileNameData = JsonConvert.DeserializeObject<FileNameArgs>(ser);
            }
            catch (InvalidCastException)
            {
                Dbg.Log(nameof(SaveGameProgressToCache) + ": " + JsonConvert.SerializeObject(_Data));
                throw;
            }
            var gdff = new GameDataFieldFilter(
                GameClient, 
                GameClientUtils.AccountId, 
                CommonData.GameId,
                (ushort)MazorCommonUtils.StringToHash(fileNameData!.FileName)) 
                {OnlyLocal = true};
            gdff.Filter(_Fields =>
            {
                var field = _Fields.FirstOrDefault();
                if (field == null)
                {
                    Dbg.LogError($"Failed to save game with file name {fileNameData.FileName} to cache.");
                }
                else
                {
                    field.SetValue(_Data).Save(true);
                    GameSaved?.Invoke(new SavedGameEventArgs(_Data));
                }
            });
        }

        private Entity<object> GetSavedGameProgressFromCache(string _FileName)
        {
            var entity = new Entity<object>();
            var gdff = new GameDataFieldFilter(
                GameClient,
                GameClientUtils.AccountId, 
                CommonData.GameId,
                (ushort)MazorCommonUtils.StringToHash(_FileName))
                {OnlyLocal = true};
            gdff.Filter(_Fields =>
            {
                var field = _Fields.FirstOrDefault();
                if (field == null)
                {
                    Dbg.LogError($"Failed to load saved game with file name {_FileName} from cache.");
                    entity.Result = EEntityResult.Fail;
                }
                else
                {
                    entity.Value = field.GetValue();
                    entity.Result = EEntityResult.Success;
                }
            });
            return entity;
        }
        
        private SavedGameV2 GetSavedGameFromCacheSync(string _FileName)
        {
            var gdff = new GameDataFieldFilter(
                    GameClient,
                    GameClientUtils.AccountId, 
                    CommonData.GameId,
                    (ushort)MazorCommonUtils.StringToHash(_FileName))
                {OnlyLocal = true};
            var res = gdff.Filter();
            return res.FirstOrDefault()?.GetValue() as SavedGameV2;
        }

        #endregion
    }
}