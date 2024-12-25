using Models.Services;
using UI.Abstractions.View;
using UI.Abstractions.Windows.Imps;
using UI.Extentions;
using UI.GamePanels.BlockGarbagePanels.Controller;
using UI.GamePanels.BlockGarbagePanels.Views;
using UI.GamePanels.BlockPanels;
using UI.GamePanels.BlockPanels.View;
using UI.GamePanels.BlockTowerPanels.Controller;
using UI.GamePanels.BlockTowerPanels.View;
using UI.GamePanels.GameGridUI;
using UI.GamePanels.MessagePanels;
using UnityEngine;
using Zenject;

namespace Infrastructures.GameScenes
{
    public class GameUiInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvasPrefab;
        [SerializeField] private BlockGarbagePanelView _blockGarbagePanelView;
        [SerializeField] private BlockTowerPanelView _blockTowerPanelView;
        [SerializeField] private BlocksStoragePanelView _blocksPanelView;
        [SerializeField] private MessagePanelView _messagePanelView;
        [SerializeField] private GameGridUiView _gameGridUiView;
        [SerializeField] private DragBlockViewManager _dragBlockViewManager;

        public override void InstallBindings()
        {
            BindPanels();
            BindWindows();
        }

        private void BindPanels()
        {
            var canvas = Instantiate(_canvasPrefab);
            Container.BindUiView<BlockGarbagePanelController, BlockGarbagePanelView>(
                _blockGarbagePanelView,
                canvas.transform
                );

            Container.BindUiView<BlockTowerPanelController, BlockTowerPanelView>(
                _blockTowerPanelView,
                canvas.transform
                );

            Container.BindUiView<BlocksStoragePanelController, BlocksStoragePanelView>(
                _blocksPanelView,
                canvas.transform
                );

            Container.BindUiView<MessagePanelController, MessagePanelView>(
                _messagePanelView,
                canvas.transform
                );

            Container.Bind<GameGridUiView>()
                .FromComponentInNewPrefab(_gameGridUiView)
                .UnderTransform(canvas.transform)
                .AsSingle()
                .NonLazy();

            Container.Bind<IDragBlockViewManager>()
                .FromComponentInNewPrefab(_dragBlockViewManager)
                .UnderTransform(canvas.transform)
                .AsSingle()
                .NonLazy();
        }

        private void BindWindows()
        {
            Container.BindInterfacesAndSelfTo<MainGameWindow>().AsSingle();
        }
    }
}
