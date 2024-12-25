using Models.Services;
using Models.Services.Towers;
using System;

namespace Models
{
    public interface ITowerBlocksService
    {
        IObservable<TowerBlockModel> OnAddBlockTower { get; }
        IObservable<TowerBlockModel> OnRemoveBlockTower { get; }
        IObservable<GameBlock> OnErrorAddTowerBlock { get; }
        IObservable<TowerModel> OnLoadTower { get; }

        void TryAddBlockOnTower(GameBlock block, float mouseHorizontalPosition);
        void RemoveBlock(GameBlock block);
    }
}