using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlassLewis.WebApp.MVC.Models
{
    public class PagedViewModel<T> : IPagedList where T : class
    {
        public PagedViewModel()
        {
            List = new List<T>();
        }

        public string ReferenceAction { get; set; }
        public IEnumerable<T> List { get; set; }
        public int PageIndex { get; set ; }
        public int PageSize { get; set; }
        public string Query { get; set; }
        public int TotalResults { get; set; }
        public double TotalPages => Math.Ceiling((double)TotalResults / PageSize);

       
    }
}
