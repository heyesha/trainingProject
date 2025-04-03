using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TrainingProject.Application.Mapping;
using TrainingProject.Application.Services;
using TrainingProject.Application.Validations;
using TrainingProject.Application.Validations.FluentValidations.Report;
using TrainingProject.Domain.Dto.Report;
using TrainingProject.Domain.Interfaces.Services;
using TrainingProject.Domain.Interfaces.Validations;

namespace TrainingProject.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ReportMapping));

            InitServices(services);
        }

        private static void InitServices(this IServiceCollection services)
        {
            services.AddScoped<IReportValidator, ReportValidator>();
            services.AddScoped<IValidator<CreateReportDto>, CreateReportValidator>();
            services.AddScoped<IValidator<UpdateReportDto>, UpdateReportValidator>();

            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
