using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Customers.DAL.Services;
using Customers.Common.BindingModels;
using Customers.Common.Interfaces;
using System.Threading.Tasks;

namespace Customers.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly IPersonService<PersonBindingModel> _personService;
        private readonly ICompanyService<CompanyBindingModel> _companyService;

        public CustomersController(PersonService personService, CompanyService companyService)
        {
            _personService = personService;
            _companyService = companyService;
        }

        public async Task<ActionResult> Index()
        {
          var customers = await _personService.GetAllAsync();
           return View(customers);
        }

        public ActionResult AddCustomer()
        {
            PersonBindingModel personBindingModel = new PersonBindingModel();
            personBindingModel.Action = "InsertCustomer";
            personBindingModel.ActionMessage = "Insert new customer";
            return PartialView("_addCustomerModal", personBindingModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertCustomer(PersonBindingModel model)
        {
            await _personService.InsertAsync(model);
            var results = await _personService.GetAllAsync();
            return PartialView("_customersGrid", results);
        }


        public async Task<ActionResult> EditCustomer(Guid Id)
        {
            PersonBindingModel personBindingModel = await _personService.GetByIdAsync(Id);

            personBindingModel.Action = "UpdateCustomer";
            personBindingModel.ActionMessage = $"Edit customer {personBindingModel.FirstName} {personBindingModel.SurName}";
            return PartialView("_addCustomerModal", personBindingModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateCustomer(PersonBindingModel model)
        {
            PersonBindingModel personBindingModel = await _personService.UpdateAsync(model);

            personBindingModel.Action = "UpdateCustomer";

            var results = await _personService.GetAllAsync();
            return PartialView("_customersGrid", results);
        }


        public ActionResult AddCompany()
        {
            CompanyBindingModel companyBindingModel = new CompanyBindingModel();
            companyBindingModel.Action = "InsertCompany";
            return PartialView("_addCompanyModal", companyBindingModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertCompany(CompanyBindingModel model)
        {
            await _companyService.InsertAsync(model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<ActionResult> Search(string query)
        {
            //this can also be done on client side to reduce trafic
            var results = await _personService.SearchPersons(query);
            return PartialView("_customersGrid", results);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCustomer(Guid Id)
        {
            var result = await _personService.DeleteAsync(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}