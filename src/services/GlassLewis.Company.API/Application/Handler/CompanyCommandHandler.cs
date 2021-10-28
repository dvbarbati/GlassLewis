using FluentValidation.Results;
using GlassLewis.Company.API.Application.Commands;
using GlassLewis.Company.API.Application.Events;
using GlassLewis.Company.Domain.Interfaces.Repository;
using GlassLewis.Core.Messages;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GlassLewis.Company.API.Application.Handller
{
    public class CompanyCommandHandler : CommandHandler, 
        IRequestHandler<CreateCompanyCommand, ValidationResult>,
        IRequestHandler<UpdateCompanyCommand, ValidationResult>
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<ValidationResult> Handle(CreateCompanyCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return message.ValidationResult;

            var company = new Domain.Entities.Company(
                        message.Name, message.StockTicker, message.Exchange, message.ISIN, message.Website);

            var existingCompany = await _companyRepository.GetByISIN(company.ISIN);

            if(existingCompany != null)
            {
                AddError("ISIN is already registered");
                return ValidationResult;
            }

            _companyRepository.Add(company);

            company.AddEvent(new RegisteredCompanyEvent(company.Id, message.Name, message.StockTicker, message.Exchange, message.ISIN, message.Website));

            return await PersistData(_companyRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateCompanyCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return message.ValidationResult;

            var company = new Domain.Entities.Company(message.Id, message.Name, message.StockTicker, message.Exchange, message.ISIN, message.Website);

            var existingCompany = await _companyRepository.GetByISIN(company.ISIN);

            if (existingCompany != null && existingCompany.Id != message.Id)
            {
                AddError("ISIN is already registered");
                return ValidationResult;
            }

            _companyRepository.Update(company);

            company.AddEvent(new RegisteredCompanyEvent(message.Id, message.Name, message.StockTicker, message.Exchange, message.ISIN, message.Website));

            return await PersistData(_companyRepository.UnitOfWork);
        }
    }
}
