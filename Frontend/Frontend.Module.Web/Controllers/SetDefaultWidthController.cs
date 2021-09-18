using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;

namespace Frontend.Module.Web.Controllers
{
    [DesignerCategory("Code")]
    public class SetDefaultWidthController : WindowController
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            ((WebWindow)Frame).RegisterStartupScript("SetApplicationWidth", "uiCustomization.SetWidthInPercent(100);");
        }
    }
}