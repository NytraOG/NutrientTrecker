using System;
using System.Drawing;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using NyTEC.EnergyTrecker.Domain.Interfaces;
using FileDataHelper = NyTEC.EnergyTrecker.Domain.Utils.FileDataHelper;

namespace NyTEC.EnergyTrecker.Domain.Entities
{
    public class Nahrungsmittel : BaseEntity, INahrhaft
    {
        private FileData datei;
        private double   carbs;
        private double   kcal;
        private double   fett;
        private string   name;
        private double   protein;

        public Nahrungsmittel(Session session) : base(session) { }

        private Lazy<Image> LazyThumbnail => new Lazy<Image>(ThumbnailAusBildErstellen, true);
        private Lazy<Image> LazyBild      => new Lazy<Image>(BildAusDateiErstellen, true);

        [NonPersistent]
        [VisibleInDetailView(false)]
        [XafDisplayName("Thumbnail")]
        public Image Thumbnail => LazyThumbnail.Value;

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(true)]
        [XafDisplayName("Abbildung")]
        public Image Bild => LazyBild.Value;
        
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        [RuleRange(0, 9999)]
        public double Kcal
        {
            get => kcal;
            set => SetPropertyValue(nameof(Kcal), ref kcal, value);
        }

        [RuleRange(0, 999)]
        public double Protein
        {
            get => protein;
            set => SetPropertyValue(nameof(Protein), ref protein, value);
        }

        [RuleRange(0, 999)]
        public double Carbs
        {
            get => carbs;
            set => SetPropertyValue(nameof(Carbs), ref carbs, value);
        }

        [RuleRange(0, 999)]
        public double Fett
        {
            get => fett;
            set => SetPropertyValue(nameof(Fett), ref fett, value);
        }

        [RuleRequiredField]
        public FileData Datei
        {
            get => datei;
            set => SetPropertyValue(nameof(Datei), ref datei, value);
        }

        [Association]
        public XPCollection<Gegessenes> Gegessenes => GetCollection<Gegessenes>(nameof(Gegessenes));

        [Association]
        public XPCollection<Zutat> Zutaten => GetCollection<Zutat>(nameof(Zutaten));

        private Image BildAusDateiErstellen() => FileDataHelper.ErstelleBildAusDatei(Datei);

        private Image ThumbnailAusBildErstellen() => FileDataHelper.ErstelleThumbnailAusBild(LazyBild);
        
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Datei = new FileData(Session);
        }
    }
}