using GlassLewis.WebApp.MVC.Extensions;
using GlassLewis.WebApp.MVC.Models;
using GlassLewis.WebApp.MVC.Models.Response;
using GlassLewis.WebApp.MVC.Service.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GlassLewis.WebApp.MVC.Service
{
    public class CompanyService : Service, ICompanyService
    {
        private readonly HttpClient _httpClient;

        public CompanyService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.CompanyApiEndpoint);
            _httpClient = httpClient;
        }

        public async Task<PagedViewModel<CompanyViewModel>> GetAll(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"companies?ps={pageSize}&page={pageIndex}&q={query}");

            ResponseErrorsHandler(response);

            return await DeserealizeObjetResponse<PagedViewModel<CompanyViewModel>>(response);
        }

        public async Task<CompanyViewModel> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/companies/{id}");

            ResponseErrorsHandler(response);

            return await DeserealizeObjetResponse<CompanyViewModel>(response);
        }

        public async Task<CompanyViewModel> GetByIsin(string isin)
        {
            var response = await _httpClient.GetAsync($"/companies/{isin}");

            ResponseErrorsHandler(response);

            return await DeserealizeObjetResponse<CompanyViewModel>(response);
        }

        public async Task<ResponseResult> Add(CompanyViewModel company)
        {
            var companyContent = GetContent(company);

            var response = await _httpClient.PostAsync("/companies/", companyContent);

            if (!ResponseErrorsHandler(response))
                return await DeserealizeObjetResponse<ResponseResult>(response);

            return new ResponseResult { Status = (int)response.StatusCode };
        }

        public async Task<ResponseResult> Update(CompanyViewModel company)
        {
            var companyContent = GetContent(company);

            var response = await _httpClient.PutAsync($"/companies/", companyContent);

            if (!ResponseErrorsHandler(response))
                return await DeserealizeObjetResponse<ResponseResult>(response);

            return new ResponseResult { Status = (int)response.StatusCode};


        }
    }
}
