using MetaGame.Inventory.WrapperPools;
using Models.Services;
using UI.Abstractions.Views;
using UI.GamePanels.Blocks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GamePanels.BlockGarbagePanels.Views
{
    public class BlockGarbagePanelView : UiView
    {
        [SerializeField] private RectTransform _garbageAreaRect;
        [SerializeField] private Image _garbageArea;
        [SerializeField] private GarbageBlockView _garbageBlockViewPrefab;
        [SerializeField] private float _alphaHitTestMinimumThreshold;

        private WrapperPool<GarbageBlockView> _poolBlocks;
        private CompositeDisposable _disposables = new();
        private bool _mouseCorrectInnerStatus;

        protected override void Awake()
        {
            _poolBlocks = new(_garbageBlockViewPrefab, OnCreateGarbageBlock, _garbageArea.transform);
            _garbageArea.alphaHitTestMinimumThreshold = _alphaHitTestMinimumThreshold;

            _garbageArea.OnPointerEnterAsObservable()
                .Subscribe(_ => SetMouseStatusCorrectPosition(true))
                .AddTo(_disposables);

            _garbageArea.OnPointerExitAsObservable()
                .Subscribe(_ => SetMouseStatusCorrectPosition(false))
                .AddTo(_disposables);
        }

        public bool CheckCorrectMousePosition()
        {
            return _mouseCorrectInnerStatus;
        }

        public void CreateGarbage(GameBlock block)
        {
            var garbageBlock = _poolBlocks.Get();
            RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                _garbageAreaRect,
                Input.mousePosition,
                null,
                out var position
            );

            garbageBlock.SetData(block, position);
        }

        private void OnCreateGarbageBlock(GarbageBlockView view)
        {
            view.OnFinishAnimation.Subscribe(ReturnToPool).AddTo(_disposables);
        }

        private void ReturnToPool(GarbageBlockView view)
        {
            _poolBlocks.Release(view);
        }

        private void SetMouseStatusCorrectPosition(bool status)
        {
            _mouseCorrectInnerStatus = status;
        }
    }
}
