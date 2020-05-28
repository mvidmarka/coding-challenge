using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customers.Common.BindingModels;
using Customers.Common.Interfaces;
using Customers.DAL.Context;
using Customers.DAL.Models;
using Omu.ValueInjecter;

namespace Customers.DAL.Services
{
    public class PersonService : IPersonService<PersonBindingModel>
    {
        public async Task<PersonBindingModel> DeleteAsync(PersonBindingModel item)
        {
            using(Database database = new Database())
            {
                var person = database.Persons.FirstOrDefault(x => x.Id == item.Id);

                if(person != null)
                {
                    database.Persons.Remove(person);
                    await database.SaveChangesAsync();
                    item.ActionMessage = "Item successfully deleted";
                }
                else
                {
                    item.ActionMessage = "No person found to be deleted";
                }
            }

            return item;
        }

        public async Task<IEnumerable<PersonBindingModel>> GetAllAsync()
        {
            var results = new List<PersonBindingModel>();

            await Task.Run(() =>
            {
                using (Database database = new Database())
                {
                    var dbModels = database.Persons.ToList();
                    results = dbModels
                    .Select(x => new PersonBindingModel().InjectFrom(x)).Cast<PersonBindingModel>()
                    .ToList();

                }
            });

            return results;

        }

        public async Task<PersonBindingModel> GetByIdAsync(Guid id)
        {
            var result = new PersonBindingModel();

            await Task.Run(() =>
            {
                using (Database database = new Database())
                {
                    var entity = database.Persons.FirstOrDefault(x => x.Id == id);
                    result.InjectFrom(entity);

                }
            });

            return result;
        }


        public async Task<PersonBindingModel> InsertAsync(PersonBindingModel item)
        {
            await Task.Run(() =>
            {
                using (Database database = new Database())
                {
                    var entity = new Person();
                    entity.InjectFrom(item);

                    database.Persons.Add(entity);
                    database.SaveChanges();

                    item.InjectFrom(entity);
                    item.ActionMessage = $"{item.FirstName} {item.SurName} person added";
                }
            });

            return item;
        }

        public async Task<PersonBindingModel> UpdateAsync(PersonBindingModel item)
        {
            await Task.Run(() =>
            {
                using (Database database = new Database())
                {
                    var entity = database.Persons.FirstOrDefault(x => x.Id == item.Id);
                    entity.InjectFrom(item);

                    database.Persons.Attach(entity);
                    database.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    database.SaveChanges();

                    item.InjectFrom(entity);
                    item.ActionMessage = $"{item.FirstName} {item.SurName} person updated";
                }
            });

            return item;
        }
    }
    
}
