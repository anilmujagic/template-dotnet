using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Common.Extensions;
using MyApp.Domain.Common;

namespace MyApp.Domain.DI;

public class DomainModule
{
    public void Load(IServiceCollection serviceCollection)
    {
        var domainAssembly = typeof(DataService).Assembly;

        domainAssembly.GetTypes()
            .Where(t => !t.GetTypeInfo().IsAbstract && t.Name.EndsWith("Service"))
            .Each(implementationType =>
                implementationType.GetInterfaces().Each(interfaceType =>
                    serviceCollection.AddTransient(interfaceType, implementationType)));
    }
}
