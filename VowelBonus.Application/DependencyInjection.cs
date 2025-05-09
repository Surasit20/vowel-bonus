using VowelBonus.Application;
using System.Reflection;
using VowelBonus.Application.v1.Users.Profiles;
using VowelBonus.Application.v1.VowelBonusScoreHistories.Profiles;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new UserProfile());
            cfg.AddProfile(new VowelBonusScoreHistoryProfile());
            cfg.AddProfile(new FlowsProfile());
        }).CreateMapper());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
