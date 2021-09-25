using System.Linq;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;

namespace NyTEC.EnergyTrecker.Domain.Entities
{
    public class Gericht : BaseEntity
    {
        private double gesamtFett;
        private double gesamtKcal;
        private double gesamtProtein;
        private string name;

        public Gericht(Session session) : base(session) { }

        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        [XafDisplayName("Kcal")]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("DisplayFormat", "{0:n0}")]
        public double GesamtKcal
        {
            get => gesamtKcal;
            set => SetPropertyValue(nameof(GesamtKcal), ref gesamtKcal, value);
        }

        [XafDisplayName("Protein")]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("DisplayFormat", "{0:n0} g")]
        public double GesamtProtein
        {
            get => gesamtProtein;
            set => SetPropertyValue(nameof(GesamtProtein), ref gesamtProtein, value);
        }

        [XafDisplayName("Fett")]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("DisplayFormat", "{0:n0} g")]
        public double GesamtFett
        {
            get => gesamtFett;
            set => SetPropertyValue(nameof(GesamtFett), ref gesamtFett, value);
        }

        [Association]
        public XPCollection<Zutat> Zutaten => GetCollection<Zutat>(nameof(Zutaten));

        protected override void OnSaving()
        {
            base.OnSaving();

            GibAlleNährstoffe();
        }

        public void GibAlleNährstoffe()
        {
            GesamtKcal    = GibKcal();
            GesamtProtein = GibProtein();
            GesamtFett    = GibFett();
        }

        private double GibFett() => Zutaten.Sum(gg => gg.Menge / 100 * gg.Nahrungsmittel.Fett);

        private double GibProtein() => Zutaten.Sum(gg => gg.Menge / 100 * gg.Nahrungsmittel.Protein);

        private double GibKcal() => Zutaten.Sum(gg => gg.Menge / 100 * gg.Nahrungsmittel.Kcal);
    }
}