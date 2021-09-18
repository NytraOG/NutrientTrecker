using System.Web.UI;
using DevExpress.ExpressApp.Web.Templates;

namespace Frontend.Web
{
    public partial class LoginPage : BaseXafPage
    {
        public override System.Web.UI.Control InnerContentPlaceHolder
        {
            get
            {
                return Content;
            }
        }
    }
}