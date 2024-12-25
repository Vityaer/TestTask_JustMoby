using UnityEngine.Localization;

namespace Models.Services.Localizations
{
    public interface ILocalizationService
    {
        LocalizedString GetLocalizedContainer(string key);
    }
}