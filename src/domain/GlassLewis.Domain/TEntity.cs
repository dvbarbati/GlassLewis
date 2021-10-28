using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassLewis.Company.Domain
{
    public abstract class TEntity
    {
        public Guid Id { get; private set; }

    }
}
