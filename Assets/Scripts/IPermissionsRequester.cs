using mazing.common.Runtime.Entities;

namespace RMAZOR.Helpers
{
    public interface IPermissionsRequester
    {
        Entity<bool> RequestPermissions();
    }

    public class FakePermissionsRequester : IPermissionsRequester
    {
        public Entity<bool> RequestPermissions()
        {
            return new Entity<bool> {Result = EEntityResult.Success};
        }
    }
}