using Models.Datas;

namespace Models.Services.Towers
{
    public class TowerBlockModel
    {
        private GameBlock _gameBlock;
        private float _positionHorizontal;

        public GameBlock GameBlock => _gameBlock;
        public float PositionHorizontal => _positionHorizontal;

        public TowerBlockModel(GameBlock gameBlock, float positionHorizontal)
        {
            _gameBlock = gameBlock;
            _positionHorizontal = positionHorizontal;
        }
    }
}
