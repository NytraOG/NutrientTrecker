using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using NyTEC.EnergyTrecker.Domain.Entities;

namespace Frontend.Module.Web.Controllers;

public class GegessenesObjectCreationController : ObjectViewController<ListView, Gegessenes>
{
    protected override void OnActivated()
    {
        base.OnActivated();
        
        ObjectSpace.ObjectSaving += ObjectSpaceOnObjectSaving;
        ObjectSpace.Committing += ObjectSpaceOnCommitting;
    }

    private void ObjectSpaceOnCommitting(object sender, CancelEventArgs e)
    {
    }

    private void ObjectSpaceOnObjectSaving(object sender, ObjectManipulatingEventArgs e)
    {
        if (sender is not XPObjectSpace space)
            return;

        if(e.Object is not Tag tag)
            return;
    }
}