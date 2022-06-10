using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Common.Extensions;
using MyApp.Infrastructure.Data;

namespace MyApp.Infrastructure.DI;

public class InfrastructureModule
{
    public void Load(IServiceCollection serviceCollection)
    {
        var infrastructureAssembly = typeof(UnitOfWork).Assembly;

        infrastructureAssembly.GetTypes()
            .Where(t => !t.GetTypeInfo().IsAbstract && t.Name.EndsWith("Factory"))
            .Each(implementationType =>
                implementationType.GetInterfaces().Each(interfaceType =>
                    serviceCollection.AddTransient(interfaceType, implementationType)));
    }
}
