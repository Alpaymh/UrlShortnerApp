using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrlShortnerApp.Business.Abstract;
using UrlShortnerApps.DataAccess.Abstract;
using UrlShortnerApps.Entities.Concrate;

namespace UrlShortnerApp.Business.Concrate
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _customerRepository;
        public UserServices(IUserRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public Task<Users> CreateAsync(Users customer)
        {
            return _customerRepository.CreateAsync(customer);
        }

        public Task DeleteAsync(string id)
        {
            return _customerRepository.DeleteAsync(id);
        }

        public Task<List<Users>> GetAllAsync()
        {
            return _customerRepository.GetAllAsync();
        }

        public Task<Users> GetByIdAsync(string id)
        {

            return _customerRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(string id, Users customer)
        {
            return _customerRepository.UpdateAsync(id, customer);
        }
    }
}
