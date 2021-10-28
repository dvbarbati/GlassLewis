using GlassLewis.Company.API.Application.Commands;
using GlassLewis.Company.Domain.Interfaces.Repository;
using GlassLewis.Company.Domain.Result;
using GlassLewis.Core.Mediator.Interface;
using GlassLewis.WebAPI.Core.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GlassLewis.Company.API.Controllers
{
    [Authorize]
    public class CompanyController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(IMediatorHandler mediator, ICompanyRepository companyRepository)
        {
            _mediator = mediator;
            _companyRepository = companyRepository;
        }

        [HttpGet]
        [Route("companies")]
        public async Task<PagedResult<Company.Domain.Entities.Company>> GetAll([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            return await _companyRepository.GetAll(ps,page,q);
        }

        [HttpGet]
        [Route("companies/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var company = await _companyRepository.GetById(id);
            return company == null ? NotFound() : CustomResponse(company);
        }

        [HttpGet]
        [Route("companies/isin/{isin}")]
        public async Task<IActionResult> GetByIsin(string isin)
        {
            var company = await _companyRepository.GetByISIN(isin);
            return company == null ? NotFound() : CustomResponse(company);

        }

        [HttpPost]
        [Route("companies")]
        public async Task<IActionResult> Add(CreateCompanyCommand command)
        {
            return CustomResponse(await _mediator.SendCommand(command));
        }

        [HttpPut]
        [Route("companies")]
        public async Task<IActionResult> Update(UpdateCompanyCommand command)
        {
            return CustomResponse(await _mediator.SendCommand(command));
        }
    }
}
