using Libs.Contracts;
using Libs.Data;
using Libs.Models;
using Libs.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public class PricingRepository : RepositoryBase<Pricing>, IPricingRepository
    {
        public PricingRepository(ApplicationDbContext dBContext) : base(dBContext) { }
        public void Delete(Pricing data)
        {
            _dbContext.Remove(data);
        }

        public List<Pricing> getAll()
        {
            return _dbContext.Pricings.ToList();
        }


        public void Insert(Pricing data)
        {
            _dbContext.Pricings.Add(data);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Pricing data)
        {
            _dbContext.Pricings.Update(data);
        }
        public Pricing GetById(object id)
        {
            Pricing data = _dbContext.Pricings.Find(id);
            if (data != null)
                return data;
            throw new Exception("Pricing not found");
        }

        public IEnumerable<Pricing> GetAll(Expression<Func<Pricing, bool>> filter = null, Func<IQueryable<Pricing>, IOrderedQueryable<Pricing>> oderBy = null, int skip = 0, int take = 0)
        {
            throw new NotImplementedException();
        }
    }
}
