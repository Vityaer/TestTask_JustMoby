using DG.Tweening;
using UI.Abstractions.Views;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace UI.GamePanels.MessagePanels
{
    public class MessagePanelView : UiView
    {
        [SerializeField] private LocalizeStringEvent _messageLocalizeString;
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Animation")]
        [SerializeField] private float _animationShowTime;
        [SerializeField] private float _animationDelayTime;
        [SerializeField] private float _animationHideTime;

        private Tween _tweenMessage;

        protected override void Awake()
        {
            _canvasGroup.alpha = 0f;
            base.Awake();
        }

        public void ShowMessage(LocalizedString localizedString)
        {
            _tweenMessage.Kill();
            _messageLocalizeString.StringReference = localizedString;
            _canvasGroup.alpha = 0f;

            _tweenMessage = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(1f, _animationShowTime))
                .AppendInterval(_animationDelayTime)
                .Append(_canvasGroup.DOFade(0f, _animationHideTime));
        }

        protected override void OnDestroy()
        {
            _tweenMessage.Kill();
            base.OnDestroy();
        }
    }
}
