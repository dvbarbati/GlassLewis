using GlassLewis.Core.DomainObjetcs.Interfaces;
using System;
using System.Threading.Tasks;

namespace GlassLewis.Core.Data.Interfaces
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
