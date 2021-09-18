using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.Security;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace NyTEC.EnergyTrecker.Domain.Entities.Security
{
    // https://docs.devexpress.com/eXpressAppFramework/113452/task-based-help/security/how-to-implement-a-custom-security-system-user-based-on-an-existing-business-class
    [NavigationItem(Konstanten.MENU_ADMINISTRATION)]
    [ImageName("custom_applicationuser.svg")]
    [XafDisplayName("Benutzer")]
    public class CustomApplicationUser : BaseEntity,
                                         ISecurityUser,
                                         IAuthenticationStandardUser,
                                         IAuthenticationActiveDirectoryUser,
                                         ISecurityUserWithRoles,
                                         IPermissionPolicyUser,
                                         ICanInitialize
    {
        public CustomApplicationUser(Session session) : base(session) { }

        [Association]
        public XPCollection<Tag> Tage => GetCollection<Tag>(nameof(Tage));
        
        #region ICanInitialize Members

        public void Initialize(IObjectSpace objectSpace, SecurityStrategyComplex security)
        {
            var defaultRole = objectSpace.GetObjectsQuery<CustomApplicationRole>().FirstOrDefault(r => r.Name == security.NewUserRoleName);

            if (defaultRole == null)
            {
                defaultRole                  = objectSpace.CreateObject<CustomApplicationRole>();
                defaultRole.Name             = security.NewUserRoleName;
                defaultRole.IsAdministrative = true;
            }

            IsActive = true;
            defaultRole.Users.Add(this);
        }

        #endregion

        #region IAuthenticationStandardUser Members

        private bool changePasswordOnFirstLogon;

        public bool ChangePasswordOnFirstLogon
        {
            get => changePasswordOnFirstLogon;
            set => SetPropertyValue(nameof(ChangePasswordOnFirstLogon), ref changePasswordOnFirstLogon, value);
        }

        [Browsable(false)]
        [Size(SizeAttribute.Unlimited)]
        [Persistent]
        [SecurityBrowsable]
        protected string StoredPassword { get; set; }

        public bool ComparePassword(string password)
        {
            return PasswordCryptographer.VerifyHashedPasswordDelegate(StoredPassword, password);
        }

        public void SetPassword(string password)
        {
            StoredPassword = PasswordCryptographer.HashPasswordDelegate(password);
            OnChanged(nameof(StoredPassword));
        }

        #endregion

        #region ISecurityUser Members

        private string userName;

        [RuleRequiredField]
        [RuleUniqueValue]
        public string UserName
        {
            get => userName;
            set => SetPropertyValue(nameof(UserName), ref userName, value);
        }

        private bool isActive;

        public bool IsActive
        {
            get => isActive;
            set => SetPropertyValue(nameof(IsActive), ref isActive, value);
        }

        #endregion

        #region ISecurityUserWithRoles Members

        [Association]
        [XafDisplayName("Rollen")]
        public XPCollection<CustomApplicationRole> Roles => GetCollection<CustomApplicationRole>(nameof(Roles));

        IList<ISecurityRole> ISecurityUserWithRoles.Roles
        {
            get
            {
                IList<ISecurityRole> result = new List<ISecurityRole>();

                foreach (var role in Roles)
                    result.Add(role);

                return result;
            }
        }

        IEnumerable<IPermissionPolicyRole> IPermissionPolicyUser.Roles => Roles.OfType<IPermissionPolicyRole>();

        #endregion
    }
}