using Cysharp.Threading.Tasks;
using Infrastructures.Settings.GameDatas;
using Models.Datas;
using System;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using Utils.AsyncFuctions;
using Zenject;

namespace Models.Services.Common
{
    public class CommonGameData : ICommonGameData, IInitializable, IDisposable
    {
        private const string BLOCK_ID_PREFIX = "Block_";

        private readonly IGameDataLoader _gameDataLoader;
        private readonly ILoadSaveService _loadSaveService;

        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private readonly Dictionary<string, BlockModel> _blockModels = new();
        private readonly ReactiveCommand _onLoadGameData = new();

        private SaveData _saveData;
        private bool _isLoaded;

        public ReactiveCommand OnLoadGameData => _onLoadGameData;
        public IReadOnlyDictionary<string, BlockModel> BlockModels => _blockModels;
        public bool IsLoaded => _isLoaded;
        public SaveData SaveData => _saveData;

        public CommonGameData(IGameDataLoader gameDataLoader, ILoadSaveService loadSaveService)
        {
            _gameDataLoader = gameDataLoader;
            _loadSaveService = loadSaveService;
        }

        public void Initialize()
        {
            WaitLoadGameData(_cancellationTokenSource.Token).Forget();
        }

        private async UniTaskVoid WaitLoadGameData(CancellationToken token)
        {
            _isLoaded = false;
            var gameData = await _gameDataLoader.LoadData(token);
            _saveData = await _loadSaveService.LoadData(token);

            for (var i = 0; i < gameData.BlockStorageCount && i < gameData.Colors.Count; i++)
            {
                var blockModel = new BlockModel(gameData.Colors[i]);
                _blockModels.Add(CreateBlockId(i), blockModel);
            }

            _isLoaded = true;
            _onLoadGameData.Execute();
        }

        private string CreateBlockId(int i)
        {
            return $"{BLOCK_ID_PREFIX}{i}";
        }

        public void Dispose()
        {
            _loadSaveService.SaveData(_saveData);
            _cancellationTokenSource.TryCancel();
        }
    }
}
