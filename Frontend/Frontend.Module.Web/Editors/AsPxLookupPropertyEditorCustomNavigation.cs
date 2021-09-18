using CoOrga.XAFModules.CoreExtensions.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using System;

namespace Frontend.Module.Web.Editors
{

    [PropertyEditor(typeof(DevExpress.Xpo.IXPSimpleObject), true)]
    public class ASPxLookupPropertyEditorCustomNavigation : CoOrga.XAFModules.CoreExtensions.Editors.ASPxLookupPropertyEditorCustomNavigation
    {
        public ASPxLookupPropertyEditorCustomNavigation(Type objectType, IModelMemberViewItem model)
                : base(objectType, model)
        {
        }
    }
}
