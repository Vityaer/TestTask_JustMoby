using MetaGame.Inventory.WrapperPools;
using Models.Services;
using Models.Services.Towers;
using System;
using System.Collections.Generic;
using UI.Abstractions.Views;
using UI.Extentions;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GamePanels.BlockTowerPanels.View
{
    public class BlockTowerPanelView : UiView, IBlockTowerPanelView
    {
        [SerializeField] private RectTransform _towerRectContainer;
        [SerializeField] private RectTransform _correctRectTransform;
        [SerializeField] private Image _correctRectTransformImage;
        [SerializeField] private Image _towerImage;
        [SerializeField] private TowerBlockView _towerBlockViewPrefab;

        private CompositeDisposable _disposables = new();
        private List<TowerBlockView> _workList = new();
        private WrapperPool<TowerBlockView> _towerBlockPool;

        private bool _mouseCorrectInnerStatus;
        private ReactiveCommand<TowerBlockView> _onStartDrag = new();

        public ReactiveCommand OnReachTowerLimit = new();
        public IObservable<TowerBlockView> OnStartDrag => _onStartDrag;

        protected override void Awake()
        {
            _towerBlockPool = new(_towerBlockViewPrefab, OnCreateTowerBlock, _towerRectContainer);

            _correctRectTransformImage.OnPointerEnterAsObservable()
                .Subscribe(_ => SetMouseStatusCorrectPosition(true))
                .AddTo(_disposables);

            _correctRectTransformImage.OnPointerExitAsObservable()
                .Subscribe(_ => SetMouseStatusCorrectPosition(false))
                .AddTo(_disposables);

            RecalculateTargetZone();
        }

        public void CreateBlocks(IReadOnlyList<TowerBlockModel> towerBlocks)
        {
            foreach (var towerBlock in towerBlocks)
            {
                var newTowerBlock = _towerBlockPool.Get();
                var blockSize = _towerBlockViewPrefab.Size;
                _workList.Add(newTowerBlock);

                var index = _workList.Count - 1;
                var position = new Vector2
                (
                    towerBlock.PositionHorizontal * blockSize.x,
                    blockSize.y * index
                );
                newTowerBlock.SetData(towerBlock, position);
            }
            RecalculateTargetZone();
        }

        private void SetMouseStatusCorrectPosition(bool status)
        {
            _mouseCorrectInnerStatus = status;
        }

        private void OnCreateTowerBlock(TowerBlockView view)
        {
            view.OnStartDrag.Subscribe(_ => StartDrag(view)).AddTo(_disposables);
        }

        private void StartDrag(TowerBlockView view)
        {
            _onStartDrag.Execute(view);
        }

        public void AddBlock(TowerBlockModel model)
        {
            var newTowerBlock = _towerBlockPool.Get();

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _towerRectContainer,
                Input.mousePosition,
                null,
                out var startPosition
                );

            newTowerBlock.SetData(model, startPosition);
            _workList.Add(newTowerBlock);

            UpdateTowerPositionBlocks();
        }

        private void UpdateTowerPositionBlocks()
        {
            var blockSize = _towerBlockViewPrefab.Size;
            for (var i = 0; i < _workList.Count; i++)
            {
                var position = new Vector2
                (
                    _workList[i].Model.PositionHorizontal * blockSize.x,
                    blockSize.y * i
                );
                _workList[i].SetPosition(position);
            }

            RecalculateTargetZone();
        }

        private void RecalculateTargetZone()
        {
            if (_workList.Count > 0)
            {
                var blockSize = _towerBlockViewPrefab.Size;
                var scale = blockSize.x / _towerRectContainer.rect.width;

                var anchorPosition = _towerRectContainer.pivot.x
                    + scale * _workList[_workList.Count - 1].Model.PositionHorizontal;

                _correctRectTransform.anchorMin = new Vector2(anchorPosition, 0);
                _correctRectTransform.anchorMax = new Vector2(anchorPosition, 1);

                var halfBlockHeight = blockSize.y / 2;
                var bottomOffset = blockSize.y * (_workList.Count) + halfBlockHeight;
                var workAreaHeight = _towerRectContainer.rect.height;
                if (bottomOffset >= workAreaHeight)
                    bottomOffset = workAreaHeight;

                _correctRectTransform.offsetMin = new Vector2(-blockSize.x / 2, bottomOffset);
                _correctRectTransform.offsetMax = new Vector2(blockSize.x / 2, 0);
                var correcthHeight = _correctRectTransform.rect.height;
                
                if (correcthHeight <= 0)
                {
                    OnReachTowerLimit.Execute();
                }
            }
            else
            {
                _correctRectTransform.SetAllZero();
            }
        }

        public void RemoveBlock(TowerBlockModel model)
        {
            var removedBlockIndex = _workList.FindIndex(block => block.Model.Equals(model));
            if (removedBlockIndex < 0)
                return;

            _workList[removedBlockIndex].ClearData();
            _towerBlockPool.Release(_workList[removedBlockIndex]);
            _workList.RemoveAt(removedBlockIndex);
            UpdateTowerPositionBlocks();
        }

        public bool CheckBlockDropCorrectPosition()
        {
            return _mouseCorrectInnerStatus;
        }

        public float GetMouseHorizontalPosition()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                _towerRectContainer,
                Input.mousePosition,
                null,
                out var result
            );

            var normalizePosition = result.x / _towerBlockViewPrefab.Size.x;
            return normalizePosition;
        }


        public void ShowRegularVisual(GameBlock block)
        {
            var towerBlock = _workList
                .Find(towerBlock => towerBlock.Model.GameBlock.Equals(block));

            if (towerBlock == null)
                return;

            towerBlock.ShowRegularState();
        }

        public void ShowErrorDropPosition(GameBlock block)
        {
            var towerBlock = _towerBlockPool.Get();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _towerRectContainer,
                Input.mousePosition,
                null,
                out var startPosition
            );

            startPosition.y -= _towerBlockViewPrefab.Size.y / 2;
            towerBlock.SetData(block);
            towerBlock.ShowError(startPosition, ReleaseErrorBlock);
        }

        private void ReleaseErrorBlock(TowerBlockView towerBlock)
        {
            _towerBlockPool.Release(towerBlock);
        }

        protected override void OnRectTransformDimensionsChange()
        {
            RecalculateTargetZone();
        }

        protected override void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}