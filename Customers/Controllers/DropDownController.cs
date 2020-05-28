using Customers.Common.BindingModels;
using Customers.Common.Interfaces;
using Customers.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Customers.Controllers
{
    [Authorize]
    public class DropDownController : Controller
    {
        ICompanyService<CompanyBindingModel> _companyService;

        public DropDownController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<ActionResult> GetCompanies()
        {
            var results = await _companyService.GetAllAsync();
            return Json(results, JsonRequestBehavior.AllowGet);
        }

    }
}