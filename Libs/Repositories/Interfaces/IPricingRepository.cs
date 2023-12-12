using Libs.Contracts;
using Libs.Dtos;
using Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories.Interfaces
{
    public interface IPricingRepository : IRepositoryBase<Pricing>
    {
        void Insert(Pricing data);
        void Update(Pricing data);
        void Delete(Pricing data);
        List<Pricing> getAll();
        IEnumerable<Pricing> GetAll(Expression<Func<Pricing, bool>> filter = null, Func<IQueryable<Pricing>, IOrderedQueryable<Pricing>> oderBy = null, int skip = 0, int take = 0);
        Pricing GetById(object id);
        void Save();
        List<UserPricing> GetAllUserPricings();
        int CountUserPricing();
        bool CheckPricing(User user);
        ListUserPricingDTO GetPricingByUser(User user);
    }
}
