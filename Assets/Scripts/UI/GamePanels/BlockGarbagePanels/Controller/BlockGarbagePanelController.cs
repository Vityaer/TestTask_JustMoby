using Common;
using Models;
using Models.Services;
using Models.Services.GameLoggers;
using Models.Services.Towers;
using System;
using UI.Abstractions.Controllers;
using UI.DragManagers;
using UI.GamePanels.BlockGarbagePanels.Views;
using UniRx;
using Zenject;

namespace UI.GamePanels.BlockGarbagePanels.Controller
{
    public class BlockGarbagePanelController : UiController<BlockGarbagePanelView>, IInitializable, IDisposable
    {
        private readonly ITowerBlocksService _blockTowerService;
        private readonly IDragBlockViewManager _dragBlockViewManager;
        private readonly IGameLogger _gameLogger;

        private readonly CompositeDisposable _disposables = new();
        private IDisposable _dropDisposable;

        public BlockGarbagePanelController(
            ITowerBlocksService blockTowerService,
            IDragBlockViewManager dragBlockViewManager,
            IGameLogger gameLogger
            )
        {
            _blockTowerService = blockTowerService;
            _dragBlockViewManager = dragBlockViewManager;
            _gameLogger = gameLogger;
        }

        public void Initialize()
        {
            _dragBlockViewManager.OnChangeDragType.Subscribe(OnChangeDragType).AddTo(_disposables);
            _blockTowerService.OnRemoveBlockTower.Subscribe(CreateGarbage).AddTo(_disposables);
        }

        private void OnChangeDragType(DragBlockState state)
        {
            DisposeDropAction();
            if (state == DragBlockState.TowerToGarbage)
            {
                _dropDisposable = _dragBlockViewManager.OnDrop.Subscribe(OnDropBlock);
            }
        }

        private void OnDropBlock(GameBlock block)
        {
            if (!View.CheckCorrectMousePosition())
                return;

            _gameLogger.AddMessage(ProjectConstants.Localization.Messages.THROW_BLOCK_IN_GARBAGE_KEY);
            _blockTowerService.RemoveBlock(block);
        }

        private void CreateGarbage(TowerBlockModel towerBlock)
        {
            View.CreateGarbage(towerBlock.GameBlock);
        }

        private void DisposeDropAction()
        {
            _dropDisposable?.Dispose();
            _dropDisposable = null;
        }

        public void Dispose()
        {
            _disposables.Dispose();
            DisposeDropAction();
        }
    }
}
