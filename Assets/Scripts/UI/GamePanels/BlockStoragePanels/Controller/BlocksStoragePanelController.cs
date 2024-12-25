using Models.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UI.Abstractions.Controllers;
using UI.GamePanels.BlockPanels.View;
using UI.GamePanels.BlockTowerPanels.Blocks;
using UniRx;
using Zenject;

namespace UI.GamePanels.BlockPanels
{
    public class BlocksStoragePanelController : UiController<BlocksStoragePanelView>, IInitializable, IDisposable
    {
        private readonly IBlockStorageService _blockStorageService;
        private readonly IDragBlockViewManager _dragBlockViewManager;

        private readonly CompositeDisposable _disposables = new(); 

        public BlocksStoragePanelController(IBlockStorageService blockStorageService, IDragBlockViewManager dragBlockViewManager)
        {
            _blockStorageService = blockStorageService;
            _dragBlockViewManager = dragBlockViewManager;
        }

        public void Initialize()
        {
            _blockStorageService.BlocksChanges.ObserveAdd().Subscribe(x => OnAddBlocks(x.Value)).AddTo(_disposables);

            View.OnStartDrag.Subscribe(OnStartDrag).AddTo(_disposables);
            _dragBlockViewManager.OnDrop.Subscribe(_ => OnDrop()).AddTo(_disposables);
        }

        private void OnDrop()
        {
            View.OnDropElement();
        }

        private void OnStartDrag(StorageBlockView view)
        {
            _dragBlockViewManager.StartDrag(
                view.Block.Clone(),
                view.transform.position,
                DragManagers.DragBlockState.StorageToTower
                );
        }

        private void OnAddBlocks(List<GameBlock> value)
        {
            View.CreateBlocks(value);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
