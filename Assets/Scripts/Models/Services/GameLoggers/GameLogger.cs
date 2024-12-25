using System;
using UniRx;
using UnityEngine;

namespace Models.Services.GameLoggers
{
    public class GameLogger : IGameLogger
    {
        private readonly ReactiveCommand<string> _onAddMessage = new();

        public IObservable<string> OnAddMessage => _onAddMessage;

        public void AddMessage(string stringKey)
        {
            if(string.IsNullOrEmpty(stringKey))
            {
                Debug.LogError("String key in empty!");
                return;
            }

            _onAddMessage.Execute(stringKey);
        }
    }
}
