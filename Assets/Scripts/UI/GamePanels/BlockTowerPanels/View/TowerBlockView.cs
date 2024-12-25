using DG.Tweening;
using System;
using UI.GamePanels.Blocks;
using UnityEngine;

namespace Models.Services.Towers
{
    public class TowerBlockView : InteractableBlockContainerView
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private TowerBlockModel _model;
        [SerializeField] private float _animationSpeed;
        [SerializeField] private Ease _animationEase;

        [Header("Visual")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _changeVisualAnimationTime;
        [SerializeField] private float _regularCanvasAlpha;
        [SerializeField] private float _ghostCanvasAlpha;
        [SerializeField] private float _showErrorAnimationTime;

        private Tween _moveTween;
        private Tween _changeVisualStateTween;
        
        public TowerBlockModel Model => _model;
        public Vector2 Size => _rectTransform.sizeDelta;

        public void SetData(TowerBlockModel model, Vector2 position)
        {
            _model = model;
            _rectTransform.anchoredPosition = position;
            _canvasGroup.alpha = 1f;
            base.SetData(model.GameBlock);
        }

        public void SetPosition(Vector2 position)
        {
            _moveTween.Kill();
            _moveTween = _rectTransform.DOAnchorPos(position, _animationSpeed)
                .SetSpeedBased(true)
                .SetEase(_animationEase);
        }

        public void ShowGhostState()
        {
            _changeVisualStateTween.Kill();
            _changeVisualStateTween = _canvasGroup
                .DOFade(_ghostCanvasAlpha, _changeVisualAnimationTime);
        }

        public void ShowRegularState()
        {
            _changeVisualStateTween.Kill();
            _changeVisualStateTween = _canvasGroup
                .DOFade(_regularCanvasAlpha, _changeVisualAnimationTime);
        }

        public void ClearData()
        {
            _model = null;
        }

        public void ShowError(Vector2 startPosition, Action<TowerBlockView> callBack)
        {
            _rectTransform.anchoredPosition = startPosition;
            _canvasGroup.alpha = 1f;
            _changeVisualStateTween.Kill();
            _changeVisualStateTween = _canvasGroup
                .DOFade(0f, _showErrorAnimationTime)
                .OnComplete(() => callBack?.Invoke(this));
        }

        private void OnDestroy()
        {
            _moveTween.Kill();
        }
    }
}
