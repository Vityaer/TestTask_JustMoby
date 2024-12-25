using Infrastructures.Settings;
using Models;
using Models.Services;
using Models.Services.Common;
using Models.Services.GameLoggers;
using UnityEngine;
using Zenject;

namespace Infrastructures
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameDataSo _gameDataSo;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameDataSo>().FromInstance(_gameDataSo).AsSingle();
            Container.BindInterfacesTo<CommonGameData>().AsSingle();
            Container.BindInterfacesTo<GameLogger>().AsSingle();
            Container.BindInterfacesTo<TowerRulesService>().AsSingle();
            Container.BindInterfacesTo<TowerBlocksService>().AsSingle();
            Container.BindInterfacesTo<BlockStorageService>().AsSingle();
            Container.BindInterfacesTo<GameEntryPoint>().AsSingle();
        }
    }
}
