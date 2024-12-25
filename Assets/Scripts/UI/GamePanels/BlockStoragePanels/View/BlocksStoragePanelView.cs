using MetaGame.Inventory.WrapperPools;
using Models.Services;
using System;
using System.Collections.Generic;
using UI.Abstractions.Views;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GamePanels.BlockPanels.View
{
    public class BlocksStoragePanelView : UiView
    {
        [SerializeField] private ScrollRect _storageScroll;
        [SerializeField] private StorageBlockView _storageBlockViewPrefab;

        private List<StorageBlockView> _workList = new();
        private WrapperPool<StorageBlockView> _blockViews;

        private CompositeDisposable _disposables = new();

        private ReactiveCommand<StorageBlockView> _onStartDrag = new();

        public IObservable<StorageBlockView> OnStartDrag => _onStartDrag;


        private void Awake()
        {
            _blockViews = new(_storageBlockViewPrefab, OnCreateBlockView, _storageScroll.content);
        }

        public void CreateBlocks(List<GameBlock> gameBlocks)
        {
            foreach (var block in gameBlocks)
            {
                var blockView = _blockViews.Get();
                _workList.Add(blockView);
                blockView.SetData(block);
            }
        }

        public void OnDropElement()
        {
            _storageScroll.horizontal = true;
        }

        private void OnCreateBlockView(StorageBlockView view)
        {
            view.OnStartDrag.Subscribe(_ => StartDrag(view)).AddTo(_disposables);
        }

        private void StartDrag(StorageBlockView view)
        {
            _storageScroll.StopMovement();
            _storageScroll.horizontal = false;
            _onStartDrag.Execute(view);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }


    }
}
