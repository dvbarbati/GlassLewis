using GlassLewis.Company.Domain.Result;
using GlassLewis.Core.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace GlassLewis.Company.Domain.Interfaces.Repository
{
    public interface ICompanyRepository : IRepository<Domain.Entities.Company>
    {
        Task<Domain.Entities.Company> GetById(Guid id);

        Task<Domain.Entities.Company> GetByISIN(string isin);

        Task<PagedResult<Domain.Entities.Company>> GetAll(int pageSize, int pageIndex, string query = null);

        void Add(Domain.Entities.Company company);

        void Update(Domain.Entities.Company company);
    }
}
