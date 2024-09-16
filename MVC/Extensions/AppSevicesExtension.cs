using BusinessLayer.Interfaces;
using BusinessLayer.Repository;
using DataAccessLayer.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC.Helpers;


namespace MVC.Extentions
{
    public static class AppSevicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddAutoMapper(p => p.AddProfile(new MappingProfiles()));
            return services;
        }
    }
}
