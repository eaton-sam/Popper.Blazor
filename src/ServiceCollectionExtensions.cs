using BlazorPortals;
using Microsoft.Extensions.DependencyInjection;

namespace Popper.Blazor;

public static class ServiceCollectionExtensions
{
    public static void AddPoppers(this IServiceCollection services) => services.AddPortals();
}