using Models.Datas;

namespace Models.Services
{
    public class GameBlock
    {
        private readonly string _id;
        private readonly BlockModel _model;

        public string Id => _id;
        public BlockModel Model => _model;

        public GameBlock(string id, BlockModel blockModel)
        {
            _id = id;
            _model = blockModel;
        }

        public GameBlock Clone()
        {
            return new GameBlock(_id, _model);
        }
    }
}
