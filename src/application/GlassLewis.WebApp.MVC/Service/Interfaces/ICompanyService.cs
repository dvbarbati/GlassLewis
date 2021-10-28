using GlassLewis.WebApp.MVC.Models;
using GlassLewis.WebApp.MVC.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlassLewis.WebApp.MVC.Service.Interfaces
{
    public interface ICompanyService
    {
        Task<PagedViewModel<CompanyViewModel>>GetAll(int pageSize, int pageIndex, string query = null);
        Task<CompanyViewModel> GetById(Guid id);
        Task<CompanyViewModel> GetByIsin(string isin);
        Task<ResponseResult> Add(CompanyViewModel company);
        Task<ResponseResult> Update(CompanyViewModel company);
    }
}
