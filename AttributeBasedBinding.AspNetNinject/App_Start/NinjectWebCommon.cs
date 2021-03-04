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
                .Where(a => a.FullName.StartsWith("AttributeBasedBinding.")).ToArray();

            // implementing interface and transient scope
            kernel.Bind(x => x.From(assembliesToScan)
                                .SelectAllClasses()
                                .Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(BindAttribute)))
                                .BindSingleInterface());

            // self binding transient scope
            kernel.Bind(x => x.From(assembliesToScan)
                                .SelectAllClasses()
                                .Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(BindToSelfAttribute)))
                                .BindToSelf());

            // implementing interface and singleton scope
            kernel.Bind(x => x.From(assembliesToScan)
                                .SelectAllClasses()
                                .Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(BindAsSingletonAttribute)))
                                .BindSingleInterface()
                                .Configure(b => b.InSingletonScope()));

            // self binding and singleton scope
            kernel.Bind(x => x.From(assembliesToScan)
                                .SelectAllClasses()
                                .Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(BindToSelfAsSingletonAttribute)))
                                .BindToSelf()
                                .Configure(b => b.InSingletonScope()));
        }
    }
}