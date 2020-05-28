using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Customers.Common.Interfaces;
using Customers.Common.BindingModels;
using Customers.DAL.Services;

namespace Customers.Controllers
{
    [Authorize]
    public class CompaniesController : Controller
    {
        private readonly ICompanyService<CompanyBindingModel> _companyService;

        public CompaniesController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: Companires
        public async  Task<ActionResult> Index()
        {
            var items = await _companyService.GetAllAsync();
            return View(items);
        }

        public async Task<ActionResult> GetAll()
        {
            var companies = await _companyService.GetAllAsync();
            return View("_companiesGrid", companies);
        }

        public async Task<ActionResult> GetCompany(Guid id)
        {
            try
            {
                var model = await _companyService.GetByIdAsync(id);
                return View("_company", model);
            }
            catch
            {
                //TODO implement error log and handle exception or throw 
                var model = new CompanyBindingModel();
                model.ActionMessage = "There was an error getting item!";
                return View("_company", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Insert(CompanyBindingModel model)
        {
            try
            {
                await _companyService.InsertAsync(model);
                return View("_company", model);
            } 
            catch
            {
                //TODO implement error log and handle exception or throw 
                model.ActionMessage = "There was an error with insert";
                return View("_company", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(CompanyBindingModel model)
        {
            try
            {
                await _companyService.UpdateAsync(model);
                return View("_company", model);
            }
            catch
            {
                //TODO implement error log and handle exception or throw 
                model.ActionMessage = "There was an error with update";
                return View("_company", model);
            }

        }

    }
}