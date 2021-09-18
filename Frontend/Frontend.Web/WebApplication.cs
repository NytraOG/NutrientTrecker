using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Validation;
using DevExpress.ExpressApp.Validation.Web;
using DevExpress.ExpressApp.ViewVariantsModule;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using Frontend.Module.Web;
using NyTEC.EnergyTrecker.Domain;
using NyTEC.EnergyTrecker.Domain.Entities.Security;

namespace Frontend.Web
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Web.WebApplication
    public class FrontendAspNetApplication : WebApplication
    {
        private AuthenticationActiveDirectory authenticationActiveDirectory1;
        private ConditionalAppearanceModule   conditionalAppearanceModule;
        private DomainModule           domainModule1;
        private SystemModule                  module1;
        private SystemAspNetModule            module2;
        private FrontendAspNetModule          module4;
        private SecurityModule                securityModule1;
        private SecurityStrategyComplex       securityStrategyComplex1;
        private ValidationAspNetModule        validationAspNetModule;
        private ValidationModule              validationModule;
        private DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule fileAttachmentsAspNetModule1;
        private CoOrga.XAF.Modules.CoreExtensions.CoreExtensionsModule coreExtensionsModule1;
        private ViewVariantsModule            viewVariantsModule;

        public FrontendAspNetApplication()
        {
            InitializeComponent();
            InitializeDefaults();
        }

        protected override IViewUrlManager CreateViewUrlManager()
        {
            return new ViewUrlManager();
        }

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
        {
            args.ObjectSpaceProvider = new SecuredObjectSpaceProvider((SecurityStrategyComplex)Security, GetDataStoreProvider(args.ConnectionString, args.Connection), true);
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }

        private IXpoDataStoreProvider GetDataStoreProvider(string connectionString, IDbConnection connection)
        {
            var                   application       = HttpContext.Current != null ? HttpContext.Current.Application : null;
            IXpoDataStoreProvider dataStoreProvider = null;

            if (application != null && application["DataStoreProvider"] != null)
                dataStoreProvider = application["DataStoreProvider"] as IXpoDataStoreProvider;
            else
            {
                dataStoreProvider = XPObjectSpaceProvider.GetDataStoreProvider(connectionString, connection, true);

                if (application != null)
                    application["DataStoreProvider"] = dataStoreProvider;
            }

            return dataStoreProvider;
        }

        private void FrontendAspNetApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e)
        {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if (Debugger.IsAttached)
            {
                e.Updater.Update();
                e.Handled = true;
            }
            else
            {
                var message = "The application cannot connect to the specified database, " +
                              "because the database doesn't exist, its version is older " +
                              "than that of the application or its schema does not match " +
                              "the ORM data model structure. To avoid this error, use one " +
                              "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

                if (e.CompatibilityError != null && e.CompatibilityError.Exception != null)
                    message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
                throw new InvalidOperationException(message);
            }
#endif
        }

        private void InitializeComponent()
        {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule();
            this.conditionalAppearanceModule = new DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule();
            this.validationModule = new DevExpress.ExpressApp.Validation.ValidationModule();
            this.validationAspNetModule = new DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule();
            this.viewVariantsModule = new DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule();
            this.domainModule1 = new NyTEC.EnergyTrecker.Domain.DomainModule();
            this.module4 = new Frontend.Module.Web.FrontendAspNetModule();
            this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.securityStrategyComplex1 = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
            this.authenticationActiveDirectory1 = new DevExpress.ExpressApp.Security.AuthenticationActiveDirectory();
            this.fileAttachmentsAspNetModule1 = new DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule();
            this.coreExtensionsModule1 = new CoOrga.XAF.Modules.CoreExtensions.CoreExtensionsModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // validationModule
            // 
            this.validationModule.AllowValidationDetailsAccess = true;
            this.validationModule.IgnoreWarningAndInformationRules = false;
            // 
            // securityStrategyComplex1
            // 
            this.securityStrategyComplex1.AllowAnonymousAccess = false;
            this.securityStrategyComplex1.Authentication = this.authenticationActiveDirectory1;
            this.securityStrategyComplex1.PermissionsReloadMode = DevExpress.ExpressApp.Security.PermissionsReloadMode.NoCache;
            this.securityStrategyComplex1.RoleType = typeof(NyTEC.EnergyTrecker.Domain.Entities.Security.CustomApplicationRole);
            this.securityStrategyComplex1.SupportNavigationPermissionsForTypes = false;
            this.securityStrategyComplex1.UseOptimizedPermissionRequestProcessor = false;
            this.securityStrategyComplex1.UserType = typeof(NyTEC.EnergyTrecker.Domain.Entities.Security.CustomApplicationUser);
            // 
            // authenticationActiveDirectory1
            // 
            this.authenticationActiveDirectory1.CreateUserAutomatically = true;
            this.authenticationActiveDirectory1.LogonParametersType = null;
            this.authenticationActiveDirectory1.UserLoginInfoType = null;
            // 
            // FrontendAspNetApplication
            // 
            this.ApplicationName = "Frontend";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.validationModule);
            this.Modules.Add(this.validationAspNetModule);
            this.Modules.Add(this.domainModule1);
            this.Modules.Add(this.securityModule1);
            this.Modules.Add(this.conditionalAppearanceModule);
            this.Modules.Add(this.viewVariantsModule);
            this.Modules.Add(this.fileAttachmentsAspNetModule1);
            this.Modules.Add(this.coreExtensionsModule1);
            this.Security = this.securityStrategyComplex1;
            DatabaseVersionMismatch += FrontendAspNetApplication_DatabaseVersionMismatch;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #region Default XAF configuration options (https: //www.devexpress.com/kb=T501418)

        static FrontendAspNetApplication()
        {
            EnableMultipleBrowserTabsSupport                              = true;
            ASPxGridListEditor.AllowFilterControlHierarchy                = true;
            ASPxGridListEditor.MaxFilterControlHierarchyDepth             = 3;
            ASPxCriteriaPropertyEditor.AllowFilterControlHierarchyDefault = true;
            ASPxCriteriaPropertyEditor.MaxHierarchyDepthDefault           = 3;
            PasswordCryptographer.EnableRfc2898                           = true;
            PasswordCryptographer.SupportLegacySha512                     = false;
        }

        private void InitializeDefaults()
        {
            LinkNewObjectToParentImmediately = false;
            OptimizedControllersCreation     = true;
        }

        #endregion
    }
}