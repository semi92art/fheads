using mazing.common.Runtime.Exceptions;

namespace Common.Constants
{
    public static class DataFieldIds
    {
        // game field ids
        public const ushort Level    = 2;

        public static string GetDataFieldName(ushort _Id)
        {
            return _Id switch
            {
                Level    => nameof(Level),
                _        => throw new SwitchCaseNotImplementedException(_Id)
            };
        }
    }
}