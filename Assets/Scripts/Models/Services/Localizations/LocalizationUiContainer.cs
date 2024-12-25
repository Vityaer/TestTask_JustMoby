using Common;

namespace Models.Services.Localizations
{
    public class LocalizationUiContainer : LocalizationContainer
    {
        protected override string TableName => ProjectConstants.Localization.UI_TABLE_NAME;

        public LocalizationUiContainer()
        {
            WaitLoadLocalization().Forget();
        }
    }
}
