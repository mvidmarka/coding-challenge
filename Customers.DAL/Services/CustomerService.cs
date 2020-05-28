using Customers.Common.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customers.Common.Interfaces;
using Customers.DAL.Context;
using Customers.DAL.Models;
using Omu.ValueInjecter;

namespace Customers.DAL.Services
{
    public class CustomerService : ICustomerService<CustomerBindingModel>
    {
        public async Task<CustomerBindingModel> DeleteAsync(CustomerBindingModel item)
        {

            using (Database database = new Database())
            {
                Person person = database.Persons.FirstOrDefault(x => x.Id == item.Id);
                Company company = database.Companies.FirstOrDefault(x => x.Id == item.Id);

                if (person != null)
                {
                    database.Persons.Remove(person);
                    await database.SaveChangesAsync();
                    item.ActionMessage = $"Person {person.FirstName} {person.SurName} successfully deleted";
                }

                if (company != null)
                {
                    database.Companies.Remove(company);
                    await database.SaveChangesAsync();
                    item.ActionMessage = $"Company {company.CompanyName} successfully deleted";
                }

            }

            return item;
        }

        public async Task<IEnumerable<CustomerBindingModel>> GetAllAsync()
        {
            PersonService personService = new PersonService();
            CompanyService companyService = new CompanyService();
            IEnumerable<PersonBindingModel> allPersons = await personService.GetAllAsync();
            IEnumerable<CompanyBindingModel> allCompanies = await companyService.GetAllAsync();
            List<CustomerBindingModel> result = new List<CustomerBindingModel>();

            await Task.Run(() =>
            {
                List<object> merge = allPersons.Cast<object>()
               .Concat(allCompanies)
               .ToList();

                result = merge
                .Select(x => new CustomerBindingModel().InjectFrom(x)).Cast<CustomerBindingModel>()
                .ToList();
            });

            return result;
        }

        public async Task<CustomerBindingModel> GetByIdAsync(Guid id)
        {
            var customers = await GetAllAsync();
            return customers.FirstOrDefault(x => x.Id == id);

        }

        public async Task<CustomerBindingModel> InsertAsync(CustomerBindingModel item)
        {

            using (Database database = new Database())
            {

                if (item.IsCompany)
                {
                    CompanyService companyService = new CompanyService();
                    CompanyBindingModel companyBindingModel = new CompanyBindingModel();
                    companyBindingModel.InjectFrom(item);
                    await companyService.InsertAsync(companyBindingModel);
                }
                else
                {
                    PersonService personService = new PersonService();
                    PersonBindingModel personBinding = new PersonBindingModel();
                    personBinding.InjectFrom(item);
                    await personService.InsertAsync(personBinding);
                }

            }

            return item;

        }

        //not in use added it just server side filter example
        public async Task<List<CustomerBindingModel>> SearchCustomers(string query)
        {
            var customers = await GetAllAsync();
            var result = customers.Where(x =>
            x.CompanyName.ToLower().Contains(query.ToLower()) ||
            x.FirstName.ToLower().Contains(query.ToLower()) ||
            x.SurName.ToLower().Contains(query.ToLower()) ||
            x.Email.ToLower().Contains(query.ToLower()) ||
            x.Position.ToLower().Contains(query.ToLower())
            );

            return result.ToList();
        }

        public async Task<CustomerBindingModel> UpdateAsync(CustomerBindingModel item)
        {

            using (Database database = new Database())
            {

                if (item.IsCompany)
                {
                    CompanyService companyService = new CompanyService();
                    CompanyBindingModel companyBindingModel = new CompanyBindingModel();
                    companyBindingModel.InjectFrom(item);
                    await companyService.UpdateAsync(companyBindingModel);
                }
                else
                {
                    PersonService personService = new PersonService();
                    PersonBindingModel personBinding = new PersonBindingModel();
                    personBinding.InjectFrom(item);
                    await personService.UpdateAsync(personBinding);
                }

            }
            return item;
        }
    }
}
