using Customers.Common.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Common.Interfaces
{
    public interface IPersonService<PersonBindingModel> : IBaseService<PersonBindingModel>
    {

       //TODO add other business stuff for person

    }
}
