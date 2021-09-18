using System.Globalization;
using System.Linq;
using DevExpress.ExpressApp;
using NyTEC.EnergyTrecker.Domain.Entities;

namespace NyTEC.EnergyTrecker.Domain.Utils
{
    public class EinstellungenHelper
    {
        private readonly IObjectSpace space;

        public EinstellungenHelper(IObjectSpace space)
        {
            this.space = space;
        }

        public string GetFormattingCulture()
        {
            var setting = space.GetObjectsQuery<AppConfig>().FirstOrDefault(x => x.Einstellung == AppConfigOptions.FormattingCulture);
            var currentCulture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            if (setting == null || string.IsNullOrWhiteSpace(setting.Wert)) return currentCulture;
            return setting.Wert;
        }

        public string GetLanguage()
        {
            var setting = space.GetObjectsQuery<AppConfig>().FirstOrDefault(x => x.Einstellung == AppConfigOptions.Language);
            if (setting == null || string.IsNullOrWhiteSpace(setting.Wert)) return "de";
            return setting.Wert;
        }
    }
}
