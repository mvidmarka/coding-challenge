using Customers.Common.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Common.Interfaces
{
    public interface ICustomerService<CustomerBindingModel> : IBaseService<CustomerBindingModel>
    {
        //TODO implement custom business stuff
        Task<List<CustomerBindingModel>> SearchCustomers(string query);
    }
}
