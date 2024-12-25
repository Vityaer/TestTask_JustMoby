using UI.GamePanels.BlockGarbagePanels.Controller;
using UI.GamePanels.BlockPanels;
using UI.GamePanels.BlockTowerPanels.Controller;
using UI.GamePanels.MessagePanels;
using UnityEngine;

namespace UI.Abstractions.Windows.Imps
{
    public class MainGameWindow : AbstractWindow
    {
        private readonly BlockGarbagePanelController _blockGarbagePanelController;
        private readonly BlocksStoragePanelController _blocksPanelController;
        private readonly BlockTowerPanelController _blockTowerPanelController;
        private readonly MessagePanelController _messagePanelController;

        public MainGameWindow(
            BlockGarbagePanelController blockGarbagePanelController,
            BlocksStoragePanelController blocksPanelController,
            BlockTowerPanelController blockTowerPanelController,
            MessagePanelController messagePanelController
            )
        {
            _blockGarbagePanelController = blockGarbagePanelController;
            _blocksPanelController = blocksPanelController;
            _blockTowerPanelController = blockTowerPanelController;
            _messagePanelController = messagePanelController;
        }

        public override void Initialize()
        {
            AddController(_blockGarbagePanelController);
            AddController(_blocksPanelController);
            AddController(_blockTowerPanelController);
            AddController(_messagePanelController);
        }
    }
}
