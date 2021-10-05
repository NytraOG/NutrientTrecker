using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using NyTEC.EnergyTrecker.Domain.Entities;

namespace Frontend.Module.Web.ViewModels
{
    [DomainComponent]
    public class GerichtKonsumierenViewModel
    {
        private readonly IObjectSpace space;

        public GerichtKonsumierenViewModel(IObjectSpace space)
        {
            this.space = space ?? throw new ArgumentNullException(nameof(space));

            Gerichte = this.space
                           .GetObjects<Gericht>()
                           .ToList();
        }

        [RuleRange(0, 100)]
        [ModelDefault("DisplayFormat", "{0:n0} %")]
        public int Menge { get; set; }

        [ImmediatePostData]
        [DataSourceProperty(nameof(Gerichte))]
        public Gericht Gericht { get; set; }

        [ImmediatePostData]
        [MemberDesignTimeVisibility(false)]
        [VisibleInDetailView(false )]
        public List<Gericht> Gerichte { get; set; }
    }
}