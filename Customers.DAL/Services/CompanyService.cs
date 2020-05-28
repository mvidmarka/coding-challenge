using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customers.Common.Interfaces;
using Customers.Common.BindingModels;
using Customers.DAL.Context;
using Omu.ValueInjecter;
using Customers.DAL.Models;

namespace Customers.DAL.Services
{
    public class CompanyService : ICompanyService<CompanyBindingModel>
    {
        CompanyBindingModel item = new CompanyBindingModel();

        public async Task<CompanyBindingModel> DeleteAsync(Guid id)
        {
            using (Database database = new Database())
            {
                var company = database.Companies.FirstOrDefault(x => x.Id == id);

                if (company != null)
                {
                    database.Companies.Remove(company);
                    await database.SaveChangesAsync();
                    item.ActionMessage = "Company successfully deleted";
                }
                else
                {
                    item.ActionMessage = "No company found to be deleted";
                }
            }

            return item;
        }

        public async Task<IEnumerable<CompanyBindingModel>> GetAllAsync()
        {
            var results = new List<CompanyBindingModel>();

            await Task.Run(() =>
            {
                using (Database database = new Database())
                {
                    var dbModels = database.Companies.ToList();
                    results = dbModels
                    .Select(x => new CompanyBindingModel().InjectFrom(x)).Cast<CompanyBindingModel>()
                    .ToList();

                }
            });

            return results;
        }

        public async Task<CompanyBindingModel> GetByIdAsync(Guid id)
        {
            var result = new CompanyBindingModel();

            await Task.Run(() =>
            {
                using (Database database = new Database())
                {
                    var entity = database.Companies.FirstOrDefault(x => x.Id == id);
                    result.InjectFrom(entity);

                }
            });

            return result;
        }

        public async Task<CompanyBindingModel> InsertAsync(CompanyBindingModel item)
        {
            await Task.Run(() =>
            {
                using (Database database = new Database())
                {
                    var entity = new Company();
                    entity.DateEdited = DateTime.UtcNow;
                    entity.InjectFrom(item);

                    database.Companies.Add(entity);
                    database.SaveChanges();

                    item.InjectFrom(entity);
                    item.ActionMessage = $"{item.CompanyName} company added";
                }
            });

            return item;
        }

        public async Task<CompanyBindingModel> UpdateAsync(CompanyBindingModel item)
        {
            await Task.Run(() =>
            {
                using (Database database = new Database())
                {
                    var entity = database.Companies.FirstOrDefault(x => x.Id == item.Id);
                    entity.InjectFrom(item);

                    database.Companies.Attach(entity);
                    database.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    database.SaveChanges();

                    item.InjectFrom(entity);
                    item.ActionMessage = $"{item.CompanyName} company updated";
                }
            });
            
            return item;
        }
    }
}
