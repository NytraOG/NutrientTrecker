using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using NyTEC.EnergyTrecker.Domain.Entities;

namespace Frontend.Module.Web.Controllers
{
    public class kekwController : ViewController<ListView>
    {
        private readonly SimpleAction action;

        public kekwController()
        {
            action = new SimpleAction(this, "hsdiogbvsndo", PredefinedCategory.Edit) { Caption = "====" };
        }

        protected override void OnActivated()
        {
            base.OnActivated();
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
    }
}