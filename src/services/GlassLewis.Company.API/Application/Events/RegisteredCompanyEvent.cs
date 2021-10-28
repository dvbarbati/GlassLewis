using GlassLewis.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlassLewis.Company.API.Application.Events
{
    public class RegisteredCompanyEvent : Event
    {
        public RegisteredCompanyEvent(Guid id, string name, string stockTicker, string exchange, string isin, string website)
        {
            Id = id;
            Name = name;
            StockTicker = stockTicker;
            Exchange = exchange;
            ISIN = isin;
            Website = website;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string StockTicker { get; private set; }
        public string Exchange { get; private set; }
        public string ISIN { get; private set; }
        public string Website { get; private set; }

    }
}
