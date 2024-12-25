using Sirenix.OdinInspector;
using UnityEngine;

namespace Infrastructures.Settings
{
    [CreateAssetMenu(fileName = nameof(GameSettingSo),
        menuName = "SO/Settings/" + nameof(GameSettingSo))]
    public class GameSettingSo : SerializedScriptableObject, IGameSetting
    {
        [SerializeField] private float _randomDeltaHorizontalPosition;

        public float RandomDeltaHorizontalPosition => _randomDeltaHorizontalPosition;
    }
}
