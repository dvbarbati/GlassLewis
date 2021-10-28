using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlassLewis.WebApp.MVC.Models
{
    public interface IPagedList
    {
        public string ReferenceAction { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }
        public int TotalResults { get; set; }
        public double TotalPages { get; }
    }
}
