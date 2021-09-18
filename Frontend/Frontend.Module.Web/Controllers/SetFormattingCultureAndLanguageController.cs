using System.ComponentModel;
using DevExpress.ExpressApp;
using NyTEC.EnergyTrecker.Domain.Utils;

namespace Frontend.Module.Web.Controllers
{
    [DesignerCategory("Code")]
    public class SetFormattingCultureAndLanguageController : WindowController
    {
        public SetFormattingCultureAndLanguageController()
        {
            TargetWindowType = WindowType.Main;
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            var space  = Application.CreateObjectSpace();
            var helper = new EinstellungenHelper(space);

            var culture  = helper.GetFormattingCulture();
            var language = helper.GetLanguage();

            Application.SetLanguage(language);
            Application.SetFormattingCulture(culture);
        }
    }
}