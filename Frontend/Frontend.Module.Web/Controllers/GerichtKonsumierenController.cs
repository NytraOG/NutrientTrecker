using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.Utils.Extensions;
using Frontend.Module.Web.ViewModels;
using NyTEC.EnergyTrecker.Domain.Entities;

namespace Frontend.Module.Web.Controllers
{
    public class GerichtKonsumierenController : ObjectViewController<DetailView, Tag>
    {
        private readonly PopupWindowShowAction action;

        public GerichtKonsumierenController()
        {
            action = new PopupWindowShowAction(this, "PlseMaekActionWork", PredefinedCategory.Edit)
            {
                Caption             = "Gericht konsumieren",
                AcceptButtonCaption = "In das Mundloch rein!"
            };
        }

        private void ActionOnExecute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (e.PopupWindowViewCurrentObject is GerichtKonsumierenViewModel { Gericht: { } } vm &&
                View.CurrentObject is Tag tag)
            {
                if (vm.Menge == 0)
                    throw new UserFriendlyException("Iss mal ein bisschen mehr als 0%");

                vm.Gericht.Zutaten.ForEach(z =>
                {
                    var gegessenes = ObjectSpace.CreateObject<Gegessenes>();
                    gegessenes.Nahrungsmittel = ObjectSpace.GetObject(z.Nahrungsmittel);
                    gegessenes.Menge          = z.Menge * vm.Menge / 100;

                    tag.GegesseneDinge.Add(gegessenes);
                });

                ObjectSpace.CommitChanges();
            }
        }

        private void ActionOnCustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            if (Application.CreateObjectSpace(typeof(GerichtKonsumierenViewModel)) is NonPersistentObjectSpace npSpace)
            {
                var space = Application.CreateObjectSpace(typeof(Gericht));
                npSpace.AdditionalObjectSpaces.Add(space);

                var vm   = new GerichtKonsumierenViewModel(space);
                var view = Application.CreateDetailView(npSpace, vm);
                e.View = view;
            }
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            action.CustomizePopupWindowParams += ActionOnCustomizePopupWindowParams;
            action.Execute                    += ActionOnExecute;
        }

        protected override void OnDeactivated()
        {
            action.CustomizePopupWindowParams -= ActionOnCustomizePopupWindowParams;
            action.Execute                    -= ActionOnExecute;

            base.OnDeactivated();
        }
    }
}