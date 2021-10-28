using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlassLewis.WebApp.MVC.Models
{
    public class CompanyViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters")]
        public string Name { get;  set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(10, ErrorMessage = "The field {0} must have between {2} and {1} characters")]
        [Display(Name="Stock Ticker")]
        public string StockTicker { get;  set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters")]
        public string Exchange { get;  set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "The field {0} must have 12 characters")]
        public string ISIN { get;  set; }

        [Url(ErrorMessage = "The field {0} is required")]
        public string Website { get;  set; }

    }
}
