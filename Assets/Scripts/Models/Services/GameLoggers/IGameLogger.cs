using System;

namespace Models.Services.GameLoggers
{
    public interface IGameLogger
    {
        IObservable<string> OnAddMessage { get; }

        void AddMessage(string stringKey);
    }
}