using Libs.Contracts;
using Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories.Interfaces
{
    public interface IOrderRepository : IRepositoryBase<UserPricing>
    {
        void CreateOrder(User user, Pricing Pricing);
    }
}
