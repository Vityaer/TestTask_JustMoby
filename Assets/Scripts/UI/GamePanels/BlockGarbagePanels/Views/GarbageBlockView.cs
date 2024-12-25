using DG.Tweening;
using Models.Services;
using System;
using UniRx;
using UnityEngine;

namespace UI.GamePanels.Blocks
{
    public class GarbageBlockView : BlockContainerView
    {
        private const float CANVAS_GROUP_FULL_ALPHA = 1f; 
        private const float CANVAS_GROUP_EMPTY_ALPHA = 0f;

        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _animationTime;
        [SerializeField] private Vector2 _throwDirrection;

        private Vector3 _animationRotation = new Vector3(0, 0, 360);
        private ReactiveCommand<GarbageBlockView> _onFinishAnimation = new();
        private Tween _tween;

        public IObservable<GarbageBlockView> OnFinishAnimation => _onFinishAnimation;

        public void SetData(GameBlock gameBlock, Vector2 startPosition)
        {
            _rectTransform.anchoredPosition = startPosition;
            _rectTransform.localRotation = Quaternion.identity;
            _canvasGroup.alpha = CANVAS_GROUP_FULL_ALPHA;
            _rectTransform.localScale = Vector3.one;

            base.SetData(gameBlock);
            StartAnimation();
        }

        private void StartAnimation()
        {
            var targetPosition = _rectTransform.anchoredPosition + _throwDirrection;
            _tween.Kill();
            _tween = DOTween.Sequence()
                .Append(_rectTransform.DOAnchorPos(targetPosition, _animationTime))
                .Join(_canvasGroup.DOFade(CANVAS_GROUP_EMPTY_ALPHA, _animationTime))
                .Join(_rectTransform.DOScale(Vector3.zero, _animationTime))
                .Join(_rectTransform.DORotate(_animationRotation, _animationTime, RotateMode.FastBeyond360))
                .OnComplete(() => _onFinishAnimation.Execute(this));
        }

        private void OnDestroy()
        {
            _tween.Kill();
        }
    }
}
