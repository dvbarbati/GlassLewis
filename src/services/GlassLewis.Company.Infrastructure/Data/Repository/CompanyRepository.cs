using Dapper;
using GlassLewis.Company.Domain.Interfaces.Repository;
using GlassLewis.Company.Domain.Result;
using GlassLewis.Company.Infrastructure.Data.Context;
using GlassLewis.Core.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlassLewis.Company.Infrastructure.Data.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyContext _context;

        public CompanyRepository(CompanyContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Domain.Entities.Company> GetById(Guid id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<Domain.Entities.Company> GetByISIN(string isin)
        {
            return await _context.Companies.FirstOrDefaultAsync(c=> c.ISIN == isin);
        }

        public async Task<PagedResult<Domain.Entities.Company>> GetAll(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$" SELECT * FROM COMPANIES 
                          WHERE (@Name IS NULL OR NAME LIKE '%' + @Name + '%' OR ISIN LIKE '%' + @Name + '%' OR ID LIKE '%' + @Name + '%' )
                                 ORDER BY [NAME]
                          OFFSET {pageSize * (pageIndex - 1)} ROWS
                          FETCH NEXT {pageSize} ROWS ONLY
                          SELECT COUNT(ID) FROM COMPANIES
                          WHERE (@Name IS NULL OR NAME LIKE '%' + @Name + '%'  OR ISIN LIKE '%' + @Name + '%' OR ID LIKE '%' + @Name + '%' ) ";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Name = query });

            var companies = multi.Read<Domain.Entities.Company>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedResult<Domain.Entities.Company>()
            {
                List = companies,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };
        }

        public void Add(Domain.Entities.Company company)
        {
            _context.Companies.Add(company);
        }

        public void Update(Domain.Entities.Company company)
        {
            _context.Companies.Update(company);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
