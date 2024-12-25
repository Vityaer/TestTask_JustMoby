using UnityEngine.Localization;

namespace Models.Services.Localizations
{
    public class LocalizationService : ILocalizationService
    {
        private readonly LocalizationUiContainer _localizationUiContainer;

        public LocalizationService()
        {
            _localizationUiContainer = new();
        }

        public LocalizedString GetLocalizedContainer(string key)
        {
            return _localizationUiContainer.GetLocalizedContainer(key);
        }
    }
}
