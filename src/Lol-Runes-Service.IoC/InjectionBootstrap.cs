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
        AddContextManagers(container, typeof(IRepository<>));
    }

    private static void AddContextManagers(IServiceCollection container, Type managerType, Type? baseManagerType = null)
    {
        var adsad = assemblies[2].ExportedTypes.Select(t => t.GetTypeInfo());
        //var typesToScan12 = assemblies[0].GetTypes();
        var typesToScan2 = assemblies[2].GetTypes();

        var typesToScan = assemblies.SelectMany(a => a.GetTypes())
                                          .Where(t => t.GetInterfaces().Contains(managerType));
                                          //.Where(t => t.IsAssignableTo(managerType));

        var typesToRegister = adsad.Select(t => t.GetTypeInfo())
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