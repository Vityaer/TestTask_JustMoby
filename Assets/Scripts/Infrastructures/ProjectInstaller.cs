using Infrastructures.JsonConverts;
using Models.Services;
using Models.Services.Localizations;
using Zenject;

namespace Infrastructures
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMainServices();
        }

        private void BindMainServices()
        {
            Container.BindInterfacesTo<LocalizationService>().AsSingle();
            Container.BindInterfacesTo<LoadSaveService>().AsSingle();
        }
    }
}
