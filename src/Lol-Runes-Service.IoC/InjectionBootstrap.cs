using Lol_Runes_Service.App;
using Lol_Runes_Service.Domain;
using Lol_Runes_Service.Domain.Repositories;
using Lol_Runes_Service.Infra;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Lol_Runes_Service.IoC;

public static class InjectionBootstrap
{
    private static readonly Assembly[] assemblies =
    {
        typeof(AppEntryPoint).Assembly,
        typeof(DomainEntryPoint).Assembly,
        typeof(InfraEntryPoint).Assembly
    };

    public static void AddManagers(this IServiceCollection container)
    {
        AddContextManagers(typeof(IRepository), container);
    }

    private static void AddContextManagers(Type managerType, IServiceCollection container)
    {
        var typesToScan = assemblies.SelectMany(a => a.GetTypes())
                                          .Where(t => t.IsAssignableTo(managerType));

        var typesToRegister = typesToScan.Select(t => t.GetTypeInfo())
                                         .Select(t => new
                                         {
                                             InterfaceType = t.ImplementedInterfaces.FirstOrDefault(i => i.Name == $"I{t.Name}"),
                                             ClassType = t as Type,
                                             t.IsClass,
                                             t.IsGenericType,
                                         })
                                         .Where(info => info.InterfaceType is not null &&
                                                        info.IsClass &&
                                                        info.IsGenericType is false)
                                         .Select(info => (info.InterfaceType!, info.ClassType));


        foreach (var (interfaceType, classType) in typesToRegister)
            container.AddScoped(interfaceType, classType);
    }
}