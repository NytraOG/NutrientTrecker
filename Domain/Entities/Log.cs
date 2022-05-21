using System;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using NyTEC.EnergyTrecker.Domain.Interfaces;

namespace NyTEC.EnergyTrecker.Domain.Entities
{
    public class Log : BaseEntity
    {
        private double    carbs;
        private double    fett;
        private double    kcal;
        private double    menge;
        private INahrhaft objektDesKonsums;
        private double    protein;
        private Tag       tag;
        private DateTime  zeitpunkt;

        public Log(Session session) : base(session) { }

        public DateTime Zeitpunkt
        {
            get => zeitpunkt;
            set => SetPropertyValue(nameof(Zeitpunkt), ref zeitpunkt, value);
        }

        [ModelDefault("AllowEdit", "false")]
        public INahrhaft ObjektDesKonsums
        {
            get => objektDesKonsums;
            set => SetPropertyValue(nameof(ObjektDesKonsums), ref objektDesKonsums, value);
        }

        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("DisplayFormat", "{0:n0} g")]
        public double Menge
        {
            get => menge;
            set => SetPropertyValue(nameof(Menge), ref menge, value);
        }

        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("DisplayFormat", "{0:n0}")]
        public double Kcal
        {
            get => kcal;
            set => SetPropertyValue(nameof(Kcal), ref kcal, value);
        }

        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("DisplayFormat", "{0:n0} g")]
        public double Protein
        {
            get => protein;
            set => SetPropertyValue(nameof(Protein), ref protein, value);
        }

        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("DisplayFormat", "{0:n0} g")]
        public double Fett
        {
            get => fett;
            set => SetPropertyValue(nameof(Fett), ref fett, value);
        }

        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("DisplayFormat", "{0:n0} g")]
        public double Carbs
        {
            get => carbs;
            set => SetPropertyValue(nameof(Carbs), ref carbs, value);
        }

        [Association]
        public Tag Tag
        {
            get => tag;
            set => SetPropertyValue(nameof(Tag), ref tag, value);
        }
    }
}