using SimpleProject.Repositories.implimintation;
using SimpleProject.Repositories.Interfaces;
using SimpleProject.SharedRepository;
using SimpleProject.UnitOfWorks;

namespace SimpleProject.DependencyInjections
{
    public static class RepositoryDependencyInjection
    {
        public static IServiceCollection AddRepositoryDependencyInjection(
            this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductImagesRepository, ProductImagesRepository>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork,UnitOfWork>();
            return services;
        }
    }
}
