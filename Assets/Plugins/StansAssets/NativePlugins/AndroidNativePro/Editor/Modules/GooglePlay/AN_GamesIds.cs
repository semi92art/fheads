using System;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using SA.Android.Utilities;
using SA.Foundation.Utility;

namespace SA.Android.Editor
{
    [Serializable]
    class AN_GamesIds
    {
        [SerializeField]
        string m_rawData;

        //Parsed from games-ids.xml
        [SerializeField]
        string app_id;
        [SerializeField]
        string package_name;

        public AN_GamesIds(string rawData)
        {
            m_rawData = rawData;

            try
            {
                var doc = new XmlDocument();
                doc.Load(SA_PathUtil.ConvertRelativeToAbsolutePath(AN_Settings.AndroidGamesIdsFilePath));
                var xnList = doc.SelectNodes("resources/string");

                foreach (XmlNode node in xnList)
                {
                    var name = node.Attributes["name"].Value;
                    if (name.Equals("app_id"))
                        app_id = node.InnerText;

                    if (name.Equals("package_name"))
                        package_name = node.InnerText;

                    if (name.StartsWith("achievement") && name.Contains("_"))
                    {
                        var key = string.Empty;
                        var words = name.Split('_');

                        for (var i = 1; i < words.Length; i++)
                        {
                            key += UppercaseFirst(words[i]);
                        }

                        var value = node.InnerText;
                        Achievements.Add(new KeyValuePair<string, string>(key, value));
                    }

                    if (name.StartsWith("leaderboard") && name.Contains("_"))
                    {
                        var key = string.Empty;
                        var words = name.Split('_');

                        for (var i = 1; i < words.Length; i++)
                        {
                            key += UppercaseFirst(words[i]);
                        }
                        var value = node.InnerText;
                        Leaderboards.Add(new KeyValuePair<string, string>(key, value));
                    }
                }
            }
            catch (Exception ex)
            {
                AN_Logger.LogError("Error reading AN_GamesIds");
                AN_Logger.LogError(AN_Settings.AndroidGamesIdsFilePath + " filed: " + ex.Message);
            }
        }

        static string UppercaseFirst(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var ch = value.ToCharArray();
            ch[0] = char.ToUpper(ch[0]);
            return new string(ch);
        }

        public string RawData => m_rawData;

        public string AppId => app_id;

        public string PackageName => package_name;

        public List<KeyValuePair<string, string>> Leaderboards { get; } = new List<KeyValuePair<string, string>>();

        public List<KeyValuePair<string, string>> Achievements { get; } = new List<KeyValuePair<string, string>>();
    }
}
