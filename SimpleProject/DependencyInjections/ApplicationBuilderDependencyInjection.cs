using Microsoft.Extensions.Options;

namespace SimpleProject.DependencyInjections
{
    public static class ApplicationBuilderDependencyInjection
    {
        public static IApplicationBuilder UseApplicationBuilderDependencyInjection(this IApplicationBuilder app , IServiceProvider service)
        {
            var options = service.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options!.Value);
            return app;
        }
    }
}
