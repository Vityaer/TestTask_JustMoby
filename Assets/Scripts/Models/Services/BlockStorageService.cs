using Models.Services.Common;
using System;
using System.Collections.Generic;
using UniRx;
using Zenject;

namespace Models.Services
{
    public class BlockStorageService : IBlockStorageService, IInitializable, IDisposable
    {
        private readonly ICommonGameData _commonGameData;

        private readonly ReactiveCollection<List<GameBlock>> _blocksChanges = new();
        private List<GameBlock> _gameBlocks;

        private IDisposable _loadGameDisposable;

        public IReactiveCollection<List<GameBlock>> BlocksChanges => _blocksChanges;

        public BlockStorageService(ICommonGameData gameData)
        {
            _commonGameData = gameData;
        }

        public void Initialize()
        {
            if (_commonGameData.IsLoaded)
            {
                CreateBlocks();
            }
            else
            {
                _loadGameDisposable = _commonGameData.OnLoadGameData.Subscribe(_ => CreateBlocks());
            }
        }

        private void CreateBlocks()
        {
            _gameBlocks = new(_commonGameData.BlockModels.Count);
            foreach (var model in _commonGameData.BlockModels)
            {
                var gameBlock = new GameBlock(model.Key, model.Value);
                _gameBlocks.Add(gameBlock);
            }
            _blocksChanges.Add(_gameBlocks);
        }

        public void Dispose()
        {
            _loadGameDisposable?.Dispose();
        }
    }
}
