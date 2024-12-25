using UI.Abstractions.Windows.Imps;
using UnityEngine;
using Zenject;

namespace Infrastructures
{
    public class GameEntryPoint : IInitializable
    {
        private readonly MainGameWindow _mainGameWindow;

        public GameEntryPoint(MainGameWindow mainGameWindow)
        {
            _mainGameWindow = mainGameWindow;
        }

        public void Initialize()
        {
            _mainGameWindow.Open();
        }
    }
}
