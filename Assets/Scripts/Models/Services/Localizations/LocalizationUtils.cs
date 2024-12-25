using Common;
using UnityEngine.Localization;

namespace Models.Services.Localizations
{
    public static class LocalizationUtils
    {
        public static LocalizedString GetUiLocalizedString(string key)
        {
            return GetLocalizedString(ProjectConstants.Localization.UI_TABLE_NAME, key);
        }

        public static LocalizedString GetLocalizedString(string tableName, string key)
        {
            return new LocalizedString(tableName, key);
        }
    }
}
