using System;
using UniRx;
using UnityEngine.EventSystems;

namespace UI.GamePanels.Blocks
{
    public class InteractableBlockContainerView : BlockContainerView, IBeginDragHandler, IPointerDownHandler, IInitializePotentialDragHandler
    {
        private ReactiveCommand<InteractableBlockContainerView> _onStartDrag = new();

        public IObservable<InteractableBlockContainerView> OnStartDrag => _onStartDrag;

        public void OnBeginDrag(PointerEventData eventData)
        {
            UnityEngine.Debug.Log("OnBeginDrag");
            _onStartDrag.Execute(this);
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            UnityEngine.Debug.Log("OnInitializePotentialDrag");
            _onStartDrag.Execute(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //UnityEngine.Debug.Log("OnPointerDown");
            _onStartDrag.Execute(this);
        }
    }
}
