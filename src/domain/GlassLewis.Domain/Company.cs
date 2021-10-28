using System;

namespace GlassLewis.Company.Domain
{
    public class Company : TEntity
    {
       
        public string Name { get; private set; }
        public string StockTicker { get; private set; }
        public int Exchange { get; private set; }
        public string ISIN { get; private set; }
        public string Website { get; private set; }

        private bool ValidateISIN()
        {
            // Dois primeiros caracteres devem ser letras


            // Validar se existe cadastrado 
            return true;
        }

       
    }



//1-Create a Company record specifying the Name, Stock Ticker, Exchange, ISIN, and optionally a website URL.
//You are not allowed create two Companies with the same ISIN.
//The first two characters of an ISIN must be letters / non numeric.
//2-Retrieve an existing Company by Id
//3-Retrieve a Company by ISIN
//4-Retrieve a collection of all Companies
//5-Update an existing Company
}
