using Libs.Contracts;
using Libs.DTOs;
using Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories.Interfaces
{
    public interface ITokenRepository : IRepositoryBase<RefreshToken>
    {
        Task AddTokenAsync(RefreshToken token);
        Task SaveTokenAsync();
        Task UpdateTokenAsync(RefreshToken token);
        Task<RefreshToken> StoredToken(TokenRequest tokenRequest);
    }
}
