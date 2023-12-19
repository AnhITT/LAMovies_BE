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
    public class UserInRoomRepository : RepositoryBase<UserInRoom>, IUserInRoomRepository
    {
        public UserInRoomRepository(ApplicationDbContext dBContext) : base(dBContext) { }

        public List<UserInRoom> getAll()
        {
            return _dbContext.UserInRooms.ToList();
        }

        public void Insert(UserInRoom data)
        {
            _dbContext.UserInRooms.Add(data);
        }
        public void Delete(int roomId, string userId)
        {
            var userInRoom = _dbContext.UserInRooms.FirstOrDefault(uir => uir.RoomId == roomId && uir.UserId == userId);
            _dbContext.Remove(userInRoom);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public List<UserInRoom> getUserInRoomList(int roomId)
        {
            return _dbContext.UserInRooms.Where(s => s.RoomId == roomId).ToList();
        }
    }
}
