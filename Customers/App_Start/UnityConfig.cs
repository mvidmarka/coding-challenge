using System;

using Unity;
using Customers.Common.Interfaces;
using Customers.DAL.Services;
using Customers.Common.BindingModels;
using Customers.Models;
using Microsoft.Owin.Security;
using Unity.Injection;
using Microsoft.AspNet.Identity;
using Customers.Controllers;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Unity.Lifetime;

namespace Customers
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {

            //Identity injection
            container.RegisterType<ApplicationDbContext>();
            container.RegisterType<ApplicationSignInManager>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<EmailService>();
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new InjectionConstructor(typeof(ApplicationDbContext)));
            container.RegisterType<AccountController>(new InjectionConstructor());

            //Identity / Unity stuff below to fix No IUserToken Issue  
            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());

            // TODO: Register your type's mappings here.
            container.RegisterType<IPersonService<PersonBindingModel>, PersonService>();
            container.RegisterType<ICompanyService<CompanyBindingModel>, CompanyService>();
            container.RegisterType<ICustomerService<CustomerBindingModel>, CustomerService>();
        }
    }
}