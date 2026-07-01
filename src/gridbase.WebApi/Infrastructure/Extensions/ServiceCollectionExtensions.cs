using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace gridbase.WebApi.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJobs(this IServiceCollection services)
    {
        var jobTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(IHostedService).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in jobTypes)
        {
            // Non-generic ekleme
            services.AddSingleton(typeof(IHostedService), type);
        }

        return services;
    }
}