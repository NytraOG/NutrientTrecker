using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Xpo;

namespace Frontend.Module.Web.Editors
{
    [PropertyEditor(typeof(IXPSimpleObject), true)]
    public class ASPXxLookupPropertyEditorNavigateToMainWindow : ASPxLookupPropertyEditor
    {
        public ASPXxLookupPropertyEditorNavigateToMainWindow(Type objectType, IModelMemberViewItem model)
                : base(objectType, model) { }

        protected override void NavigateToObject(ShowViewParameters showViewParameters)
        {
            base.NavigateToObject(showViewParameters);

            if (application.MainWindow.View is DetailView detail && detail.ViewEditMode == ViewEditMode.View)
                application.MainWindow.SetView(showViewParameters.CreatedView);
        }
    }
}