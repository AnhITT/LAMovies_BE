using Libs.Contracts;
using Libs.Data;
using Libs.Dtos;
using Libs.Models;
using Libs.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
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
        public List<UserPricing> GetAllUserPricings()
        {
            var data = _dbContext.UserPricings.ToList();
            return data;
        }
        public int CountUserPricing()
        {
            return _dbContext.UserPricings.Count();
        }
        public bool CheckPricing(User user)
        {
            var userPricing = _dbContext.UserPricings.First(
                                          up => up.IdUser == user.Id &&
                                          up.StartTime < DateTime.Now &&
                                          up.EndTime > DateTime.Now);
            if (userPricing != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ListUserPricingDTO GetPricingByUser(User user)
        {
            
            var userPricing = _dbContext.UserPricings.FirstOrDefault(up => up.IdUser == user.Id &&
                                          up.StartTime < DateTime.Now &&
                                          up.EndTime > DateTime.Now);
            if (userPricing != null)
            {
                ListUserPricingDTO listPricing = new ListUserPricingDTO();
                var pricing = _dbContext.Pricings.FirstOrDefault(up => up.Id == userPricing.IdPricing);
                listPricing.NamePricing = pricing.Name;
                listPricing.StartTime = userPricing.StartTime;
                listPricing.EndTime = userPricing.EndTime;
                listPricing.RemainingTime = userPricing.EndTime - DateTime.Now;
                return listPricing;
            }
            else
            {
                return null;
            }
        }
    }
}
