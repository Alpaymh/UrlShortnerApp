using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrlShortnerApps.DataAccess.Abstract;
using UrlShortnerApps.Entities.Concrate;

namespace UrlShortnerApps.DataAccess.Concrate
{
    public class UriRepository:IUriRepository
    {
        private readonly IMongoCollection<UriDetails> _collection;
        private readonly DbConfiguration _settings;
        public UriRepository(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _collection = database.GetCollection<UriDetails>(_settings.CollectionName);
        }

        public Task<List<UriDetails>> GetAllAsync()
        {
            return _collection.Find(c => true).ToListAsync();
        }
        public Task<UriDetails> GetByIdAsync(string id)
        {
            return _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public Task UpdateAsync(string id, UriDetails customer)
        {
            return _collection.ReplaceOneAsync(c => c.Id == id, customer);
        }
        public Task DeleteAsync(string id)
        {
            return _collection.DeleteOneAsync(c => c.Id == id);
        }

        public Task<UriDetails> CreateAsync(UriDetails customer)
        {
            throw new NotImplementedException();
        }
    }
}
