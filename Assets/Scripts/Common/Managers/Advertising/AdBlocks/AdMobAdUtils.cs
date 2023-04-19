#if ADMOB_API && !UNITY_WEBGL
using System.Text;
using GoogleMobileAds.Api;
using mazing.common.Runtime;

namespace Common.Managers.Advertising.AdBlocks
{
    public static class AdMobAdUtils
    {
        public static void LogAdError(AdError _Error)
        {
            var sb = new StringBuilder();
            AppendValue(sb, "Code",    _Error.GetCode());
            AppendValue(sb, "Message", _Error.GetMessage());
            AppendValue(sb, "Domain",  _Error.GetDomain());
            if (_Error is LoadAdError loadError)
            {
                try
                {
                    var responseInfo = loadError.GetResponseInfo();
                    AppendValue(sb, "Response", responseInfo);
                }
                catch
                {
                    AppendValue(sb, "Response", "null");
                }
            }
            Dbg.LogWarning(sb);
        }
        
        public static void   AppendValue(StringBuilder _Sb, params object[] _Args) => _Sb.AppendLine(Join(_Args));
        private static string Join(params object[]      _Args)                      => string.Join(": ", _Args);
    }
}
#endif