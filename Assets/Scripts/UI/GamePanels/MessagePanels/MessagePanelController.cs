using Models.Services.GameLoggers;
using Models.Services.Localizations;
using System;
using UI.Abstractions.Controllers;
using UniRx;
using Zenject;

namespace UI.GamePanels.MessagePanels
{
    public class MessagePanelController : UiController<MessagePanelView>, IInitializable, IDisposable
    {
        private readonly ILocalizationService _localizationService;
        private readonly IGameLogger _gameLogger;

        private readonly CompositeDisposable _disposables = new();
        
        public MessagePanelController(ILocalizationService localizationService, IGameLogger gameLogger)
        {
            _localizationService = localizationService;
            _gameLogger = gameLogger;
        }

        public void Initialize()
        {
            _gameLogger.OnAddMessage.Subscribe(ShowMessage).AddTo(_disposables);
        }

        private void ShowMessage(string obj)
        {
            var localizeStringContainer = _localizationService.GetLocalizedContainer(obj);
            View.ShowMessage(localizeStringContainer);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
