namespace Mono_Installers
{
    public class LevelMonoInstaller : MonoInstallerImplBase
    {
        public override void InstallBindings()
        {
            Container.Bind<UniversalFunctions>().FromComponentInHierarchy().AsSingle();
        }
    }
}