using System.Collections.Generic;

namespace Models.Services.Towers
{
    public class TowerModel
    {
        private List<TowerBlockModel> _blocks = new();

        public IReadOnlyList<TowerBlockModel> Blocks => _blocks;

        public void AddBlock(TowerBlockModel blockTowerModel)
        {
            _blocks.Add(blockTowerModel);
        }

        public void RemoveBlock(TowerBlockModel blockTowerModel)
        {
            _blocks.Remove(blockTowerModel);
        }

        public TowerBlockModel Get(GameBlock block, out int index)
        {
            index = _blocks.FindIndex(towerBlock => towerBlock.GameBlock.Equals(block));
            return _blocks[index];
        }

        public float TryGetLastBlockHorizontalPosition(float mouseHorizontalPosition)
        {
            var result = mouseHorizontalPosition;
            if (_blocks.Count > 0)
            {
                result = _blocks[_blocks.Count - 1].PositionHorizontal;
            }

            return result;
        }

        public bool NeedRandomizePosition()
        {
            return _blocks.Count > 0;
        }
    }
}
