using System;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using NyTEC.EnergyTrecker.Domain.Entities;
using NyTEC.EnergyTrecker.Domain.Entities.Security;

namespace Frontend.Module.Web.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
                base(objectSpace, currentDBVersion) { }

        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();

            CustomApplicationRole defaultRole = CreateDefaultRole();

            CustomApplicationUser userAdmin = ObjectSpace.FindObject<CustomApplicationUser>(new BinaryOperator("UserName", "Admin"));

            if (userAdmin == null)
            {
                userAdmin          = ObjectSpace.CreateObject<CustomApplicationUser>();
                userAdmin.UserName = "Admin";
                // Set a password if the standard authentication type is used
                userAdmin.SetPassword("");
            }

            // If a role with the Administrators name doesn't exist in the database, create this role
            CustomApplicationRole adminRole = ObjectSpace.FindObject<CustomApplicationRole>(new BinaryOperator("Name", SecurityStrategy.AdministratorRoleName));

            if (adminRole == null)
            {
                adminRole      = ObjectSpace.CreateObject<CustomApplicationRole>();
                adminRole.Name = SecurityStrategy.AdministratorRoleName;
            }

            adminRole.IsAdministrative = true;
            userAdmin.Roles.Add(adminRole);

            SeedDefaultApplicationSettings();

            ObjectSpace.CommitChanges();
        }

        private void SeedDefaultApplicationSettings()
        {
            var formattingCultureOption = AppConfigOptions.FormattingCulture;
            var formattingCulture       = ObjectSpace.GetObjectsQuery<AppConfig>().FirstOrDefault(a => a.Einstellung == formattingCultureOption);

            if (formattingCulture == null)
            {
                formattingCulture             = ObjectSpace.CreateObject<AppConfig>();
                formattingCulture.Einstellung = formattingCultureOption;
                formattingCulture.Wert        = "de";

                formattingCulture.Beschreibung =
                        "Setzt die Formatierung für Währungen und Dezimaltrennzeichen (z.B. 'de' für 1.000,00 € oder 'en' für $1,000.00";
            }

            var languageOption = AppConfigOptions.Language;
            var language       = ObjectSpace.GetObjectsQuery<AppConfig>().FirstOrDefault(l => l.Einstellung == languageOption);

            if (language == null)
            {
                language              = ObjectSpace.CreateObject<AppConfig>();
                language.Einstellung  = languageOption;
                language.Wert         = "de";
                language.Beschreibung = "Setzt die Sprache der Anwendung ('de' oder 'en')";
            }
        }

        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }

        private CustomApplicationRole CreateDefaultRole()
        {
            CustomApplicationRole defaultRole = ObjectSpace.FindObject<CustomApplicationRole>(new BinaryOperator("Name", "Default"));

            if (defaultRole == null)
            {
                defaultRole      = ObjectSpace.CreateObject<CustomApplicationRole>();
                defaultRole.Name = "Default";

                defaultRole.AddObjectPermission<CustomApplicationUser>(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermission<CustomApplicationUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermission<CustomApplicationUser>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<CustomApplicationRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
            }

            return defaultRole;
        }
    }
}