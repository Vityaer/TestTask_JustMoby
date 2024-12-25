using Cysharp.Threading.Tasks;
using Models.Datas;
using System;
using System.Threading;
using UI.DragManagers;
using UI.GamePanels.Blocks;
using UniRx;
using UnityEngine;
using Utils.AsyncFuctions;

namespace Models.Services
{
    public class DragBlockViewManager : MonoBehaviour, IDragBlockViewManager
    {
        [SerializeField] private DragableBlockView _dragableBlockView;

        private CancellationTokenSource _cancellationTokenSource;

        private GameBlock _currentBlock;
        private DragBlockState _currentDragBlockState;

        private ReactiveCommand<GameBlock> _onDrop = new();
        private ReactiveCommand<DragBlockState> _onChangeDragType = new();

        public IObservable<DragBlockState> OnChangeDragType => _onChangeDragType;
        public IObservable<GameBlock> OnDrop => _onDrop;
        public Transform Self => transform;

        public void StartDrag(GameBlock block, Vector3 position, DragBlockState type)
        {
            _currentDragBlockState = type;
            _currentBlock = block;

            _dragableBlockView.SetData(block, position);

            _cancellationTokenSource.TryCancel();
            _cancellationTokenSource = new();
            Dragging(_cancellationTokenSource.Token).Forget();

            _onChangeDragType.Execute(_currentDragBlockState);
        }

        private async UniTaskVoid Dragging(CancellationToken token)
        {
            _dragableBlockView.gameObject.SetActive(true);
            while (!token.IsCancellationRequested)
            {
                if (Input.GetMouseButton(0))
                {
                    MoveBlock();
                }
                else
                {
                    StopDrag();
                }
                await UniTask.Yield(token);
            }
        }

        private void MoveBlock()
        {
            _dragableBlockView.SetPosition(Input.mousePosition);
        }

        private void StopDrag()
        {
            _onDrop.Execute(_currentBlock);
            _cancellationTokenSource.TryCancel();
            _cancellationTokenSource = null;
            _dragableBlockView.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _cancellationTokenSource.TryCancel();
        }

    }
}
