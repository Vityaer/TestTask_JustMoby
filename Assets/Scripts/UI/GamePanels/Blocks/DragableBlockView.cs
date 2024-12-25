using DG.Tweening;
using Models.Datas;
using Models.Services;
using UI.GamePanels.BlockTowerPanels.Blocks;
using UnityEngine;

namespace UI.GamePanels.Blocks
{
    public class DragableBlockView : BlockContainerView
    {
        [SerializeField] private float _dragTime;

        private Transform _transform;
        private Tween _dragTween;

        public void SetData(GameBlock model, Vector3 position)
        {
            transform.position = position;
            base.SetData(model);
        }

        private void Awake()
        {
            _transform = gameObject.transform;
        }

        public void SetPosition(Vector3 newPosition)
        {
            _dragTween.Kill();
            _dragTween = _transform.DOMove(newPosition, _dragTime);
        }

        private void OnDestroy()
        {
            _dragTween.Kill();
        }
    }
}
