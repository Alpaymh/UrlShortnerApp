using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrlShortnerApp.Business.Abstract;
using UrlShortnerApps.DataAccess.Abstract;
using UrlShortnerApps.Entities.Concrate;

namespace UrlShortnerApp.Business.Concrate
{
    public class UriDetailService:IUriDetailService
    {
        private readonly IUriRepository _customerRepository;
        public UriDetailService(IUriRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public Task<UriDetails> CreateAsync(UriDetails customer)
        {
            return _customerRepository.CreateAsync(customer);
        }

        public Task DeleteAsync(string id)
        {
            return _customerRepository.DeleteAsync(id);
        }

        public Task<List<UriDetails>> GetAllAsync()
        {
            return _customerRepository.GetAllAsync();
        }

        public Task<UriDetails> GetByIdAsync(string id)
        {

            return _customerRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(string id, UriDetails customer)
        {
            return _customerRepository.UpdateAsync(id, customer);
        }
    }
}
