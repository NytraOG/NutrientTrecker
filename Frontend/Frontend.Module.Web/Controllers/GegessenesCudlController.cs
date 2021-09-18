using CoOrga.XAF.Modules.CoreExtensions.Controllers;
using CoOrga.XAF.Modules.CoreExtensions.Extensions;
using NyTEC.EnergyTrecker.Domain.Entities;

namespace Frontend.Module.Web.Controllers
{
    public class GegessenesCudlController : DisableCudlActionsController<Gegessenes>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            
            this.SetNewActionStatus(Frame, true);
            this.SetDeleteActionStatus(Frame, true);
        }
    }
}