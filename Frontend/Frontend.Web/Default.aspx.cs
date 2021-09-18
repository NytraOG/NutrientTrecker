using System.Web.UI;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Web.Templates;

namespace Frontend.Web
{
    public partial class Default : BaseXafPage
    {
        public override Control InnerContentPlaceHolder => Content;

        protected override ContextActionsMenu CreateContextActionsMenu()
        {
            return new ContextActionsMenu(this, "Edit", "RecordEdit", "ObjectsCreation", "ListView", "Reports");
        }
    }
 }