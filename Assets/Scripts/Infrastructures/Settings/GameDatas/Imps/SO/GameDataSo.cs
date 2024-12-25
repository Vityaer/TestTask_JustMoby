using Cysharp.Threading.Tasks;
using Infrastructures.Settings.GameDatas;
using Models.Datas;
using Sirenix.OdinInspector;
using System.Threading;
using UnityEngine;

namespace Infrastructures.Settings
{
    [CreateAssetMenu(fileName = nameof(GameDataSo),
    menuName = "SO/Datas/" + nameof(GameDataSo))]
    public class GameDataSo : SerializedScriptableObject, IGameDataLoader
    {
        [SerializeField] private GameData _gameData;

        public async UniTask<GameData> LoadData(CancellationToken token)
        {
            return _gameData;
        }
    }
}
