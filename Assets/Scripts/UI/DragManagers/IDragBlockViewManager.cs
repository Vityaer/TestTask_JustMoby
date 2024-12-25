using System;
using UI.DragManagers;
using UnityEngine;

namespace Models.Services
{
    public interface IDragBlockViewManager
    {
        IObservable<DragBlockState> OnChangeDragType { get; }
        IObservable<GameBlock> OnDrop { get; }
        Transform Self { get; }

        void StartDrag(GameBlock block, Vector3 position, DragBlockState type);
    }
}