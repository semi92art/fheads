namespace Mono_Installers
{
    public class PreloadMonoInstaller : MonoInstallerImplBase
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.Bind<ApplicationInitializer>().FromComponentInHierarchy().AsSingle();
        }
    }
}