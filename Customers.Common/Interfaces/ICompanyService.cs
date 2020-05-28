using Customers.Common.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Common.Interfaces
{
    public interface ICompanyService<CompanyBindingModel> : IBaseService<CompanyBindingModel>
    {
        //TODO add specific business logic for companies
    }
}
