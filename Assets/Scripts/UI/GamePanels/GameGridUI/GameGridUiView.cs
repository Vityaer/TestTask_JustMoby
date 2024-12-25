using Models.Services;
using UI.Abstractions.Views;
using UI.Extentions;
using UI.GamePanels.BlockGarbagePanels.Views;
using UI.GamePanels.BlockPanels.View;
using UI.GamePanels.BlockTowerPanels.View;
using UI.GamePanels.MessagePanels;
using UnityEngine;
using Zenject;

namespace UI.GamePanels.GameGridUI
{
    public class GameGridUiView : UiView
    {
        public RectTransform GarbageContainer;
        public RectTransform StorageContainer;
        public RectTransform TowerContainer;
        public RectTransform DragManagerContainer;
        public RectTransform MessageContainer;

        [Inject]
        public void Construct(
            BlocksStoragePanelView blocksPanelView,
            BlockTowerPanelView blockTowerPanelView,
            BlockGarbagePanelView blockGarbagePanelView,
            MessagePanelView messagePanelView,
            IDragBlockViewManager dragBlockViewManager
            )
        {
            SetContent(blockGarbagePanelView.GetComponent<RectTransform>(), GarbageContainer);
            SetContent(blocksPanelView.GetComponent<RectTransform>(), StorageContainer);
            SetContent(blockTowerPanelView.GetComponent<RectTransform>(), TowerContainer);
            SetContent(messagePanelView.GetComponent<RectTransform>(), MessageContainer);
            dragBlockViewManager.Self.SetParent(DragManagerContainer);
        }

        private void SetContent(RectTransform rectTransform, RectTransform parent)
        {
            rectTransform.SetParent(parent);
            rectTransform.SetAllZero();
        }
    }
}
