using AssociationBids.Portal.Controllers;
using AssociationBids.Portal.Repository.Base;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Repository.Base.Interface;
using AssociationBids.Portal.Repository.ChangePassword;
using AssociationBids.Portal.Repository.Login;
using AssociationBids.Portal.Repository.Login.Code;
using AssociationBids.Portal.Service.Base;
using AssociationBids.Portal.Service.Base.Code;
using AssociationBids.Portal.Service.Base.Interface;
using AssociationBids.Portal.Service.Login;
using Autofac;
using Autofac.Integration.Mvc;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

using System.Web.Mvc;

namespace AssociationBids.Portal.App_Start
{
    public class IOCConfig
    {
        public static void RegisterDependencies()
        {
            #region Create Builder
            var builder = new ContainerBuilder();
            #endregion

            #region Setup a common pattern

            builder.RegisterAssemblyTypes().Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes().Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();

            #endregion

            #region Register all controllers 

            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest(); //p
           
            #endregion


            #region Inject HTTP Abstractions

            builder.RegisterModule<AutofacWebTypesModule>();

            #endregion

            #region Changes Goes Here

            builder.RegisterType<LoginController>().InstancePerDependency();
            builder.RegisterType<LoginRepository>().As<ILoginRepository>().InstancePerRequest();
            builder.RegisterType<LoginService>().As<ILoginService>().InstancePerRequest();


            builder.RegisterType<ChangePasswordController>().InstancePerDependency();
            builder.RegisterType<ChangePasswordRepository>().As<IChangePasswordRepository>().InstancePerRequest();
            builder.RegisterType<ChangePasswordService>().As<IChangePasswordService>().InstancePerRequest();


            builder.RegisterType<PMPropertiesController>().InstancePerDependency();
            builder.RegisterType<PMPropertiesRepository>().As<IPMPropertiesRepository>().InstancePerRequest();
            builder.RegisterType<PMPropertiesService>().As<IPMPropertiesService>().InstancePerRequest();


            builder.RegisterType<StaffDirectoryController>().InstancePerDependency();
            builder.RegisterType<StaffDirectoryRepository>().As<IStaffDirectoryRepository>().InstancePerRequest();
            builder.RegisterType<StaffDirectoryServices>().As<IStaffDirectoryServices>().InstancePerRequest();


            builder.RegisterType<AStaffDirectoryController>().InstancePerDependency();
            builder.RegisterType<AStaffDirectoryRepository>().As<IAStaffDirectoryRepository>().InstancePerRequest();
            builder.RegisterType<AStaffDirectoryService>().As<IAStaffDirectoryService>().InstancePerRequest();

            builder.RegisterType<VendorInvoiceController>().InstancePerDependency();
            builder.RegisterType<VendorInvoiceRepositery>().As<IVendorInvoiceRepositery>().InstancePerRequest();
            builder.RegisterType<VendorInvoiceService>().As<IVendorInvoiceService>().InstancePerRequest();


            builder.RegisterType<UserController>().InstancePerDependency();
            builder.RegisterType<ResourceRepository>().As<IResourceRepository>().InstancePerRequest();
            builder.RegisterType<ResourceService>().As<IResourceService>().InstancePerRequest();

            builder.RegisterType<PMVendorController>().InstancePerDependency();
            builder.RegisterType<PMVendorRepository>().As<IPMVendorRepository>().InstancePerRequest();
            builder.RegisterType<PMVendorService>().As<IPMVendorService>().InstancePerRequest();

            builder.RegisterType<ACompanyManagementController>().InstancePerDependency();
            builder.RegisterType<ACompanymangRepository>().As<IACompanymangRepository>().InstancePerRequest();
            builder.RegisterType<ACompanymangService>().As<IACompanymangService>().InstancePerRequest();

            builder.RegisterType<RegistrationController>().InstancePerDependency();
            builder.RegisterType<RegistrationRepository>().As<IRegistrationRepository>().InstancePerRequest();
            builder.RegisterType<RegistrationService>().As<IRegistrationService>().InstancePerRequest();


            builder.RegisterType<EmailTemplateController>().InstancePerDependency();
            builder.RegisterType<EmailTemplateRepository>().As<IEmailTemplateRepository>().InstancePerRequest();
            builder.RegisterType<EmailTemplateService>().As<IEmailTemplateService>().InstancePerRequest();



            builder.RegisterType<NotificationTemplateController>().InstancePerDependency();
            builder.RegisterType<NotificationTemplateRepository>().As<INotificationTemplateRepository>().InstancePerRequest();
            builder.RegisterType<NotificationTemplateServices>().As<INotificationTemplateServices>().InstancePerRequest();



            builder.RegisterType<PMBidRequestsController>().InstancePerDependency();
            builder.RegisterType<BidRequestRepository>().As<IBidRequestRepository>().InstancePerRequest();
            builder.RegisterType<BidRequestService>().As<IBidRequestService>().InstancePerRequest();





            builder.RegisterType<PMBidRequestsController>().InstancePerDependency();
            builder.RegisterType<BidRequestRepository>().As<IBidRequestRepository>().InstancePerRequest();
            builder.RegisterType<BidRequestService>().As<IBidRequestService>().InstancePerRequest();


            builder.RegisterType<APISController>().InstancePerDependency();
            builder.RegisterType<APIRepositery>().As<IAPIRepositery>().InstancePerRequest();
            builder.RegisterType<APIService>().As<IAPIService>().InstancePerRequest();



            builder.RegisterType<ServiceTmpController>().InstancePerDependency();
            builder.RegisterType<ServiceRepository>().As<IServiceRepository>().InstancePerRequest();
            builder.RegisterType<ServiceService>().As<IServiceService>().InstancePerRequest();


            builder.RegisterType<AgreementController>().InstancePerDependency();
            builder.RegisterType<AgreementRepository>().As<IAgreementRepository>().InstancePerRequest();
            builder.RegisterType<AgreementService>().As<IAgreementService>().InstancePerRequest();



            builder.RegisterType<vProfileController>().InstancePerDependency();
            builder.RegisterType<ResourceRepository>().As<IResourceRepository>().InstancePerRequest();
            builder.RegisterType<ResourceService>().As<IResourceService>().InstancePerRequest();

            builder.RegisterType<NewPricingController>().InstancePerDependency();
            builder.RegisterType<NewPricingRepository>().As<INewPricingRepository>().InstancePerRequest();
            builder.RegisterType<NewPricingService>().As<INewPricingService>().InstancePerRequest();

            builder.RegisterType<vBillingController>().InstancePerDependency();
            builder.RegisterType<vBillingRepository>().As<IvBillingRepository>().InstancePerRequest();
            builder.RegisterType<vBillingService>().As<IvBillingService>().InstancePerRequest();

            builder.RegisterType<VendorManagerController>().InstancePerDependency();
            builder.RegisterType<VendorManagerRepository>().As<IVendorManagerRepository>().InstancePerRequest();
            builder.RegisterType<VendorManagerService>().As<IVendorManagerService>().InstancePerRequest();
            builder.RegisterType<VendorServiceService>().As<IVendorServiceService>().InstancePerRequest();


            builder.RegisterType<VendorMembershipController>().InstancePerDependency();
            builder.RegisterType<MembershipRepository>().As<IMembershipRepository>().InstancePerRequest();
            builder.RegisterType<MembershipService>().As<IMembershipService>().InstancePerRequest();

            builder.RegisterType<ForgotPasswordController>().InstancePerDependency();
            builder.RegisterType<ForgotPasswordRepository>().As<IForgotPasswordRepository>().InstancePerRequest();
            builder.RegisterType<ForgotPasswordServices>().As<IForgotPasswordServices>().InstancePerRequest();

            builder.RegisterType<AcitivitybyManagementCompanyController>().InstancePerDependency();
            builder.RegisterType<AcitivitybyManagementCompanyRepository>().As<IAcitivitybyManagementCompanyRepository>().InstancePerRequest();
            builder.RegisterType<AcitivitybyManagementCompanyService>().As<IAcitivitybyManagementCompanyService>().InstancePerRequest();

            builder.RegisterType<VendorsWithOpenInvoiceController>().InstancePerDependency();
            builder.RegisterType<VendorwithOpeninvoiceRepository>().As<IVendorswithopeninvoiceRepository>().InstancePerRequest();
            builder.RegisterType<VendorsWithOpeninvoiceServices>().As<IVendorswithopeninvoiceService>().InstancePerRequest();

            builder.RegisterType<DashboardController>().InstancePerDependency();
            builder.RegisterType<DashboardRepository>().As<IDashboardRepository>().InstancePerRequest();
            builder.RegisterType<DashboardService>().As<IDashboardService>().InstancePerRequest();

            builder.RegisterType<VendorPolicyController>().InstancePerDependency();
            builder.RegisterType<VendorPolicyRepository>().As<IVendorPolicyRepository>().InstancePerRequest();
            builder.RegisterType<VendorPolicyService>().As<IVendorPolicyService>().InstancePerRequest();

            builder.RegisterType<ABNotificationController>().InstancePerDependency();
            builder.RegisterType<ABNotificationRepository>().As<IABNotificationRepository>().InstancePerRequest();
            builder.RegisterType<ABNotificationService>().As<IABNotificationService>().InstancePerRequest();

            builder.RegisterType<PushNotificationController>().InstancePerDependency();
            builder.RegisterType<PushNotificationRepository>().As<IPushNotificationRepository>().InstancePerRequest();
            builder.RegisterType<PushNotificationService>().As<IPushNotificationService>().InstancePerRequest();

            builder.RegisterType<BidReportController>().InstancePerDependency();
            builder.RegisterType<BidReportRepository>().As<IBidReportRepository>().InstancePerRequest();
            builder.RegisterType<BidReportService>().As<IBidReportService>().InstancePerRequest();

            builder.RegisterType<SupportController>().InstancePerDependency();
            builder.RegisterType<SupportRepository>().As<ISupportRepository>().InstancePerRequest();
            builder.RegisterType<SupportService>().As<ISupportService>().InstancePerRequest();
            #endregion


            #region Set the MVC dependency resolver to use Autofac
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((Autofac.IContainer)container);
            //GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
            #endregion
        }
    }
}