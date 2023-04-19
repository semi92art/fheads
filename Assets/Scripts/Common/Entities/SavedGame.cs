using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Entities
{
    [Serializable]
    public class FileNameArgs
    {
        public string FileName { get; set; }
    }
    
    [Serializable]
    public class SavedGame : FileNameArgs
    {
        public long                       Level        { get; set; }
        public long                       Money        { get; set; }
        public Dictionary<string, object> Args         { get; set; }
    }

    [Serializable]
    public class SavedGameV2 : FileNameArgs
    {
        [JsonProperty("AR")] public Dictionary<string, object> Arguments { get; set; } = new Dictionary<string, object>();

        public SavedGameV2()
        {
            FileName = MazorCommonData.SavedGameFileName;
        }
    }
}