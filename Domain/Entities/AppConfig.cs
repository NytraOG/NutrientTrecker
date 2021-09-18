using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace NyTEC.EnergyTrecker.Domain.Entities
{
    [ImageName("appconfig.svg")]
    [NavigationItem(Konstanten.MENU_ADMINISTRATION)]
    public class AppConfig : BaseEntity
    {
        private string           beschreibung;
        private AppConfigOptions einstellung;
        private string           wert;

        public AppConfig(Session session) : base(session) { }

        [RuleUniqueValue]
        [Indexed(Unique = true)]
        [Appearance(nameof(Einstellung), Enabled = false)]
        public AppConfigOptions Einstellung
        {
            get => einstellung;
            set => SetPropertyValue(nameof(Einstellung), ref einstellung, value);
        }

        [RuleRequiredField]
        public string Wert
        {
            get => wert;
            set => SetPropertyValue(nameof(Wert), ref wert, value);
        }

        [Appearance(nameof(Beschreibung), Enabled = false)]
        [Size(SizeAttribute.Unlimited)]
        [ModelDefault("RowCount", "2")]
        public string Beschreibung
        {
            get => beschreibung;
            set => SetPropertyValue(nameof(Beschreibung), ref beschreibung, value);
        }
    }
}