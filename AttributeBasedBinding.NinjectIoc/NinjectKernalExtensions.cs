using Ninject;
using System;
using System.Linq;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;

namespace AttributeBasedBinding.NinjectIoc
{
    public static class NinjectKernalExtensions
    {
        public static void UseAttributeBasedBindings(this IKernel kernel)
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

            // implementing interface and request scope
            kernel.Bind(x => x.From(assembliesToScan)
                                .SelectAllClasses()
                                .Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(BindPerRequestAttribute)))
                                .BindSingleInterface()
                                .Configure(b => b.InRequestScope()));

            // self binding and request scope
            kernel.Bind(x => x.From(assembliesToScan)
                                .SelectAllClasses()
                                .Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(BindToSelfPerRequestAttribute)))
                                .BindToSelf()
                                .Configure(b => b.InRequestScope()));
        }
    }
}
