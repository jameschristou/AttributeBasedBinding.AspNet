using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AttributeBasedBinding.NetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseAttributeBasedBindings(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(classes => classes.WithAttribute<BindAttribute>())
                .AsImplementedInterfaces());

            services.Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(classes => classes.WithAttribute<BindToSelfAttribute>())
                .AsSelf());

            services.Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(classes => classes.WithAttribute<BindAsSingletonAttribute>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

            services.Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(classes => classes.WithAttribute<BindToSelfAsSingletonAttribute>())
                .AsSelf()
                .WithSingletonLifetime());

            services.Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(classes => classes.WithAttribute<BindPerRequestAttribute>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(classes => classes.WithAttribute<BindToSelfPerRequestAttribute>())
                .AsSelf()
                .WithScopedLifetime());

            return services;
        }
    }
}
