using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrlShortnerApps.Entities.Concrate;

namespace UrlShortnerApp.Business.Abstract
{
    public interface IUserService
    {
        Task<List<Users>> GetAllAsync();
        Task<Users> GetByIdAsync(string id);
        Task<Users> CreateAsync(Users customer);
        Task UpdateAsync(string id, Users customer);
        Task DeleteAsync(string id);
    }
}
