using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlassLewis.Core.Data.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
