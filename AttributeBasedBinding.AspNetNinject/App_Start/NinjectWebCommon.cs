[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(AttributeBasedBinding.AspNetNinject.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(AttributeBasedBinding.AspNetNinject.App_Start.NinjectWebCommon), "Stop")]

namespace AttributeBasedBinding.AspNetNinject.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using System.Web.Http;
    using Ninject.Web.WebApi;
    using System.Linq;
    using Ninject.Extensions.Conventions;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // only scan our assemblies and not 3rd party assemblies or external assemblies
            var assembliesToScan = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.StartsWith("AttributesBasedBinding.")).ToArray();

            //foreach (var assembly in assembliesToScan)
            //{
            //    var typesToBind = assembly.GetTypes().Where(t => t.CustomAttributes.Any(x => x.AttributeType == typeof(BindAttribute)));

            //    foreach (var typeToBind in typesToBind)
            //    {
            //        // get the bind attribute
            //        var bindAttribute = typeToBind.CustomAttributes.First(x => x.AttributeType == typeof(BindAttribute));

            //        var bindingType = ((BindAttribute)bindAttribute.ConstructorArguments.First().Value).BindingType;

            //        switch (bindingType)
            //        {
            //            case BindingType.Singleton:

            //        }
            //    }
            //}


            // implementing interface and singleton scope
            kernel.Bind(x => x.From(assembliesToScan)
                                .SelectAllClasses()
                                .Where(t => 
                                {
                                    var bindingAttribute = t.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(BindAttribute));

                                    if (bindingAttribute == null) return false;

                                    if (((BindAttribute)bindingAttribute.ConstructorArguments.First().Value).BindingType != BindingType.Singleton) return false;

                                    return true;
                                })
                                .BindSingleInterface()
                                .Configure(b => b.InSingletonScope()));
        }
    }
}