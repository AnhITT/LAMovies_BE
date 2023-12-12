using Libs.Contracts;
using Libs.Data;
using Libs.Models;
using Libs.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public class OrderRepository : RepositoryBase<UserPricing>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dBContext) : base(dBContext) { }

        public void CreateOrder(User user, Pricing pricing)
        {
            var checkUserPricing = _dbContext.UserPricings.FirstOrDefault(up => up.IdUser == user.Id);
            if (checkUserPricing == null)
            {
                UserPricing userPricing = new UserPricing
                {
                    IdPricing = pricing.Id,
                    IdUser = user.Id,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMonths(pricing.Time),
                    TotalAmount = pricing.Price
                };
                _dbContext.Add(userPricing);
                _dbContext.SaveChanges();
            }
            else
            {
                //Hết hạn
                if (checkUserPricing.EndTime < DateTime.Now)
                {
                    UserPricing updateUserPricing = new UserPricing()
                    {
                        IdPricing = pricing.Id,
                        IdUser = user.Id,
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMonths(pricing.Time),
                        TotalAmount = checkUserPricing.TotalAmount + pricing.Price
                    };
                    _dbContext.Remove(checkUserPricing);
                    _dbContext.Add(updateUserPricing);
                    _dbContext.SaveChanges();
                }
                //Chưa hết hạn, đăng ký thêm
                else
                {
                    UserPricing updateUserPricing = new UserPricing()
                    {
                        IdUser = user.Id,
                        EndTime = checkUserPricing.EndTime.AddMonths(pricing.Time),
                        StartTime = checkUserPricing.StartTime,
                        TotalAmount = checkUserPricing.TotalAmount + pricing.Price
                    };
                    if (pricing.Id > checkUserPricing.IdPricing)
                    {
                        updateUserPricing.IdPricing = pricing.Id;
                    }
                    else
                    {
                        updateUserPricing.IdPricing = checkUserPricing.IdPricing;
                    }
                    _dbContext.Remove(checkUserPricing);
                    _dbContext.Add(updateUserPricing);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
