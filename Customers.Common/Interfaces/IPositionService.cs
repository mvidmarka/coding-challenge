using Customers.Common.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Common.Interfaces
{
    public interface IPositionService<PositionBindingModel> : IBaseService<PositionBindingModel>
    {
        /// <summary>
        /// Get all persons on the position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        Task<List<PersonBindingModel>> GetAllPersonsAsync(PositionBindingModel position);
    }
}
