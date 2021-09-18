using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;

namespace NyTEC.EnergyTrecker.Domain.Entities.Security
{
    // https://docs.devexpress.com/eXpressAppFramework/113452/task-based-help/security/how-to-implement-a-custom-security-system-user-based-on-an-existing-business-class
    [NavigationItem(Konstanten.MENU_ADMINISTRATION)]
    [ImageName("custom_applicationrole.svg")]
    [XafDisplayName("Rolle")]
    public class CustomApplicationRole : PermissionPolicyRoleBase, IPermissionPolicyRoleWithUsers
    {
        public CustomApplicationRole(Session session) : base(session) { }

        [Association]
        [XafDisplayName("Benutzer")]
        public XPCollection<CustomApplicationUser> Users => GetCollection<CustomApplicationUser>(nameof(Users));

        IEnumerable<IPermissionPolicyUser> IPermissionPolicyRoleWithUsers.Users => Users.OfType<IPermissionPolicyUser>();
    }
}