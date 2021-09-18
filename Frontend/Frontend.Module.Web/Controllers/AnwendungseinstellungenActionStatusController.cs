using System.ComponentModel;
using CoOrga.XAF.Modules.CoreExtensions.Controllers;
using CoOrga.XAF.Modules.CoreExtensions.Extensions;
using NyTEC.EnergyTrecker.Domain.Entities;

namespace Frontend.Module.Web.Controllers
{
    [DesignerCategory("Code")]
    public class AnwendungseinstellungenActionStatusController : DisableCudlActionsController<AnwendungseinstellungenActionStatusController>
    {
        public AnwendungseinstellungenActionStatusController()
        {
            TargetObjectType = typeof(AppConfig);
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            this.SetEditActionStatus(Frame, true);
            this.SetDeleteActionStatus(Frame, true);
        }
    }
}