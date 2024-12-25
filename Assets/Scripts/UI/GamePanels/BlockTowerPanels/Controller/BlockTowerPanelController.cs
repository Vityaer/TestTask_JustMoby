using Common;
using Models;
using Models.Services;
using Models.Services.GameLoggers;
using Models.Services.Towers;
using System;
using UI.Abstractions.Controllers;
using UI.DragManagers;
using UI.GamePanels.BlockTowerPanels.View;
using UniRx;
using Zenject;

namespace UI.GamePanels.BlockTowerPanels.Controller
{
    public class BlockTowerPanelController : UiController<BlockTowerPanelView>, IInitializable, IDisposable
    {
        private readonly ITowerBlocksService _blockTowerService;
        private readonly IDragBlockViewManager _dragBlockViewManager;
        private readonly IGameLogger _gameLogger;

        private readonly CompositeDisposable _disposables = new();
        private IDisposable _dropDisposable;

        public BlockTowerPanelController(
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
            _blockTowerService.OnLoadTower.Subscribe(OnLoadTower).AddTo(_disposables);
            _blockTowerService.OnErrorAddTowerBlock.Subscribe(OnErrorAddTowerBlock).AddTo(_disposables);
            _blockTowerService.OnAddBlockTower.Subscribe(AddBlockTower).AddTo(_disposables);
            _blockTowerService.OnRemoveBlockTower.Subscribe(RemoveBlockTower).AddTo(_disposables);
            _dragBlockViewManager.OnChangeDragType.Subscribe(OnChangeDragType).AddTo(_disposables);

            View.OnReachTowerLimit.Subscribe(_ => OnReachTowerLimit()).AddTo(_disposables);
            View.OnStartDrag.Subscribe(OnStartDrag).AddTo(_disposables);
        }

        private void OnReachTowerLimit()
        {
            _gameLogger.AddMessage(ProjectConstants.Localization.Messages.TOWER_HEIGHT_LIMIT_KEY);
        }

        private void OnErrorAddTowerBlock(GameBlock block)
        {
            View.ShowErrorDropPosition(block);
        }

        private void OnLoadTower(TowerModel tower)
        {
            View.CreateBlocks(tower.Blocks);
        }

        private void OnChangeDragType(DragBlockState state)
        {
            DisposaDropAction();
            if (state == DragBlockState.StorageToTower)
            {
                _dropDisposable = _dragBlockViewManager.OnDrop.Subscribe(OnDropBlock);
            }
            else if (state == DragBlockState.TowerToGarbage)
            {
                _dropDisposable = _dragBlockViewManager.OnDrop.Subscribe(OnDropBlockGarbage);
            }
        }

        private void OnDropBlockGarbage(GameBlock block)
        {
            View.ShowRegularVisual(block);
        }

        private void OnDropBlock(GameBlock block)
        {
            DisposaDropAction();
            var correctPositionStatus = View.CheckBlockDropCorrectPosition();
            if (!correctPositionStatus)
            {
                View.ShowErrorDropPosition(block);
                _gameLogger.AddMessage(ProjectConstants.Localization.Messages.WRONG_BLOCK_DROP_POSITION_KEY);
                return;
            }

            var mouseHorizontalPosition = View.GetMouseHorizontalPosition();
            _blockTowerService.TryAddBlockOnTower(block, mouseHorizontalPosition);
        }

        private void DisposaDropAction()
        {
            _dropDisposable?.Dispose();
            _dropDisposable = null;
        }

        private void OnStartDrag(TowerBlockView view)
        {
            if (view.Model != null)
            {
                _gameLogger.AddMessage(ProjectConstants.Localization.Messages.GET_BLOCK_FROM_TOWER_KEY);
                view.ShowGhostState();
                _dragBlockViewManager.StartDrag(
                    view.Block,
                    view.transform.position,
                    DragBlockState.TowerToGarbage
                    );
            }
        }

        private void AddBlockTower(TowerBlockModel model)
        {
            View.AddBlock(model);
        }

        private void RemoveBlockTower(TowerBlockModel model)
        {
            View.RemoveBlock(model);
        }

        public void Dispose()
        {
            _disposables.Dispose();
            DisposaDropAction();
        }
    }
}