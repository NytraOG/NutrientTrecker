using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoOrga.XAF.Modules.CoreExtensions.Controllers;
using CoOrga.XAF.Modules.CoreExtensions.Extensions;
using NyTEC.EnergyTrecker.Domain.Entities;

namespace Frontend.Module.Web.Controllers.Cudl
{
    public class GegessenesCudlController : DisableCudlActionsController<Gegessenes>
    {
        protected override void OnActivated()
        {
            base.OnActivated();

            this.SetNewActionStatus(Frame, true);
            this.SetUnlinkActionStatus(Frame, true);
        }
    }
}
