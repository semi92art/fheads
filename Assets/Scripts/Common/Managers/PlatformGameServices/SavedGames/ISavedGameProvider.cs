using System;
using Common.Entities;
using mazing.common.Runtime;
using mazing.common.Runtime.Entities;

namespace Common.Managers.PlatformGameServices.SavedGames
{
    public delegate void GameSavedAction(SavedGameEventArgs _Args);
    
    public interface ISavedGameProvider : IInit
    {
        event GameSavedAction GameSaved;
        
        [Obsolete] Entity<object> GetSavedGameProgress(string _FileName);
        [Obsolete] void           SaveGameProgress<T>(T       _Data) where T : FileNameArgs;

        SavedGameV2 GetSavedGame(string _FileName);
        void        SaveGame(SavedGameV2 _SavedGame);
    }
}