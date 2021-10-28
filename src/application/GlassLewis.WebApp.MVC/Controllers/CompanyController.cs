using GlassLewis.WebApp.MVC.Models;
using GlassLewis.WebApp.MVC.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GlassLewis.WebApp.MVC.Controllers
{
    [Authorize]
    public class CompanyController : MainController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var companies = await _companyService.GetAll(ps, page, q);
            ViewBag.Search = q;
            companies.ReferenceAction = "Index";

            return View(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var company = await _companyService.GetById(id);
            return View(company);
        }

        [HttpGet("{isin}")]
        public async Task<IActionResult> GetIsin(string isin)
        {
            var company = await _companyService.GetByIsin(isin);
            return View(company);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CompanyViewModel company)
        {
            if (!ModelState.IsValid)
                return View(company);

            var response = await _companyService.Add(company);

            if (ResponseContainsErrors(response))
                return View(company);

            TempData["Message"] = "The record was created successfully";

            return RedirectToAction("Index", "Company");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var company = await _companyService.GetById(id);
            return View(company);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, CompanyViewModel company)
        {
            if (!ModelState.IsValid)
                return View(company);

            var response = await _companyService.Update(company);

            if (ResponseContainsErrors(response))
                return View(company);

            TempData["Message"] = "The record was updated successfully";

            return RedirectToAction("Index", "Company");
        }

    }
}
