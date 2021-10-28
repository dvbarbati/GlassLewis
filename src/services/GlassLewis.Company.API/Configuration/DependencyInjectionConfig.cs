using FluentValidation.Results;
using GlassLewis.Company.API.Application.Commands;
using GlassLewis.Company.API.Application.Events;
using GlassLewis.Company.API.Application.Handller;
using GlassLewis.Company.Domain.Interfaces.Repository;
using GlassLewis.Company.Infrastructure.Data.Repository;
using GlassLewis.Core.Mediator;
using GlassLewis.Core.Mediator.Interface;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GlassLewis.Company.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddRegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<CreateCompanyCommand, ValidationResult>, CompanyCommandHandler>();

            services.AddScoped<INotificationHandler<RegisteredCompanyEvent>, CompanyEventHandler>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
        }
    }
}
