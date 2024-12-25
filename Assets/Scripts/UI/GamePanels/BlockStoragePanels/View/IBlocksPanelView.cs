using Models.Datas;
using Models.Services;
using System.Collections.Generic;
using UI.Abstractions.View;

namespace UI.GamePanels.BlockPanels.View
{
    public interface IBlocksPanelView : IUiView
    {
        void CreateBlocks(List<GameBlock> gameBlocks);
    }
}
