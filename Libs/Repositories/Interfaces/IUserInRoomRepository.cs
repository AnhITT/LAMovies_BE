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
    public interface IUserInRoomRepository : IRepositoryBase<UserInRoom>
    {
        void Insert(UserInRoom data);
        void Delete(int roomId, string userId);
        List<UserInRoom> getAll();
        void Save();
        List<UserInRoom> getUserInRoomList(int roomId);
    }
}
