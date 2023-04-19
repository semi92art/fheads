using System.Collections.Generic;

namespace Common.Managers.Advertising.AdsProviders
{
    public interface IAdsProvidersSet
    {
        List<IAdsProvider> GetProviders();
    }
}