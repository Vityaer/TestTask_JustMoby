using Common;
using Infrastructures.Settings;
using Models.Datas;
using Models.Services;
using Models.Services.Common;
using Models.Services.GameLoggers;
using Models.Services.Towers;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Models
{
    public class TowerBlocksService : ITowerBlocksService, IInitializable, IDisposable
    {
        private readonly IGameSetting _gameSetting;
        private readonly ITowerRulesService _towerRuleService;
        private readonly IGameLogger _gameLogger;
        private readonly ICommonGameData _commonGameData;

        private readonly CompositeDisposable _disposables = new();
        private readonly TowerModel _tower = new();

        private readonly ReactiveCommand<TowerBlockModel> _onAddBlockTower = new();
        private readonly ReactiveCommand<TowerBlockModel> _onRemoveBlockTower = new();
        private readonly ReactiveCommand<GameBlock> _onErrorAddTowerBlock = new();
        private readonly ReactiveCommand<TowerModel> _onLoadTower = new();
        
        public IObservable<TowerBlockModel> OnAddBlockTower => _onAddBlockTower;
        public IObservable<TowerBlockModel> OnRemoveBlockTower => _onRemoveBlockTower;
        public IObservable<GameBlock> OnErrorAddTowerBlock => _onErrorAddTowerBlock;
        public IObservable<TowerModel> OnLoadTower => _onLoadTower;

        public TowerBlocksService(
            IGameSetting gameSetting,
            ITowerRulesService towerRuleService,
            IGameLogger gameLogger,
            ICommonGameData commonGameData
            )
        {
            _gameSetting = gameSetting;
            _towerRuleService = towerRuleService;
            _gameLogger = gameLogger;
            _commonGameData = commonGameData;
        }

        public void Initialize()
        {
            if (_commonGameData.IsLoaded)
            {
                LoadTower();
            }
            else
            {
                _commonGameData.OnLoadGameData.Subscribe(_ => LoadTower()).AddTo(_disposables);
            }
        }

        private void LoadTower()
        {
            foreach (var saveTowerBlock in _commonGameData.SaveData.TowerBlockDatas)
            {
                if (_commonGameData.BlockModels.TryGetValue(saveTowerBlock.Id, out var model))
                {
                    var gameBlock = new GameBlock(saveTowerBlock.Id, model);
                    var towerBlock = new TowerBlockModel(gameBlock, saveTowerBlock.HorizontalPosition);
                    _tower.AddBlock(towerBlock);
                }
                else
                {
                    Debug.LogError($"Block with unknow id: {saveTowerBlock.Id}");
                }
            }

            _onLoadTower.Execute(_tower);
        }

        public void TryAddBlockOnTower(GameBlock block, float mouseHorizontalPosition)
        {
            if (!_towerRuleService.CheckCanAddBlockInTower(block, _tower))
            {
                _onErrorAddTowerBlock.Execute(block);
                _gameLogger.AddMessage(ProjectConstants.Localization.Messages.WRONG_BLOCK_DROP_RULE_KEY);
                return;
            }

            var previousBlockHorizntalPosition = _tower.TryGetLastBlockHorizontalPosition(mouseHorizontalPosition);

            var newBlockPosition = previousBlockHorizntalPosition;
            if (_tower.NeedRandomizePosition())
            {
                var randomizeDelta = UnityEngine.Random.Range(
                    -_gameSetting.RandomDeltaHorizontalPosition,
                    _gameSetting.RandomDeltaHorizontalPosition
                    );

                newBlockPosition += randomizeDelta;
            }

            var towerBlock = new TowerBlockModel(block, newBlockPosition);
            _tower.AddBlock(towerBlock);
            _commonGameData.SaveData.TowerBlockDatas.Add(new BlockData(block.Id, newBlockPosition));
            _gameLogger.AddMessage(ProjectConstants.Localization.Messages.CREATE_BLOCK_ON_TOWER_KEY);
            _onAddBlockTower.Execute(towerBlock);
        }

        public void RemoveBlock(GameBlock block)
        {
            var towerBlock = _tower.Get(block, out var index);
            if (towerBlock == null)
            {
                Debug.LogError("Tower block not found!");
                return;
            }

            _commonGameData.SaveData.TowerBlockDatas.RemoveAt(index);
            _tower.RemoveBlock(towerBlock);
            _onRemoveBlockTower.Execute(towerBlock);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}