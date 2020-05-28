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
    public class CustomersController : Controller
    {
        ICustomerService<CustomerBindingModel> _customerService;
        IPersonService<PersonBindingModel> _personService;
        ICompanyService<CompanyBindingModel> _companyService;

        public CustomersController()
        {

        }

        public async Task<ActionResult> Index()
        {
          var customers = await  _customerService.GetAllAsync();
           return View(customers);
        }

        public ActionResult AddPerson()
        {
            PersonBindingModel personBindingModel = new PersonBindingModel();
            personBindingModel.Action = "InsertPerson";
            return PartialView("_addPersonModal" , personBindingModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertPerson(PersonBindingModel model)
        {
        //    await _customerService.InsertAsync(model);
            return Json( model, JsonRequestBehavior.AllowGet);
        }



        public ActionResult AddCompany()
        {
            CompanyBindingModel companyBindingModel = new CompanyBindingModel();
            companyBindingModel.Action = "InsertCompany";
            return PartialView("_addCompanyModal", companyBindingModel);
        }
    }
}