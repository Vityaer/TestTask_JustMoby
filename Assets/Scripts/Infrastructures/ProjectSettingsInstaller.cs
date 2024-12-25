using Infrastructures.Settings;
using UnityEngine;
using Zenject;

namespace Infrastructures
{
    [CreateAssetMenu(fileName = nameof(ProjectSettingsInstaller),
        menuName = "Custom Installers/Settings/" + nameof(ProjectSettingsInstaller))]
    public class ProjectSettingsInstaller : ScriptableObjectInstaller<ProjectSettingsInstaller>
    {
        [SerializeField] private GameSettingSo _gameSetting;

        public override void InstallBindings()
        {
            Container.Bind<IGameSetting>().FromInstance(_gameSetting).AsSingle();
        }
    }
}
