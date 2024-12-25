using Cysharp.Threading.Tasks;
using Models.Datas;
using System.Threading;

namespace Infrastructures.Settings.GameDatas
{
    public interface IGameDataLoader
    {
        UniTask<GameData> LoadData(CancellationToken token);
    }
}
