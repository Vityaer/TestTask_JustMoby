using Models.Datas;
using Models.Services;
using UI.GamePanels.BlockTowerPanels.Blocks;
using UnityEngine;

namespace UI.GamePanels.Blocks
{
    public class BlockContainerView : MonoBehaviour
    {
        [SerializeField] private BlockView _blockView;

        private GameBlock _block;

        public GameBlock Block => _block;

        public virtual void SetData(GameBlock block)
        {
            _block = block;
            _blockView.SetData(block.Model);
        }
    }
}
