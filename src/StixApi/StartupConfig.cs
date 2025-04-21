using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

public static class StartupConfig
{
    public static void AddScopeAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("read:vuln", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "read:vuln");
            });
            options.AddPolicy("create:vuln", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "create:vuln");
            });
            options.AddPolicy("update:vuln", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "update:vuln");
            });
            options.AddPolicy("delete:vuln", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "delete:vuln");
            });
        });
    }


    public static void AddFeatureControllers(this IServiceCollection services)
    {
        services.AddControllers()
            .AddControllersAsServices()
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new ControllerFeatureProvider());
            });
    }
}

public class ControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
{
    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
    {
        var assembly = typeof(Program).Assembly;
        var types = assembly.GetTypes().Where(t => typeof(Controller).IsAssignableFrom(t));
        foreach (var type in types)
        {
            feature.Controllers.Add(type.GetTypeInfo());
        }
    }
}