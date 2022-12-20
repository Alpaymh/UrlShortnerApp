using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UrlShortnerApps.DataAccess.Abstract;
using UrlShortnerApps.Entities.Concrate;

namespace UrlShortnerApps.DataAccess.Concrate
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private const string Alphabet = "23456789bcdfghjkmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ-_";
        private static readonly int Base = Alphabet.Length;

        //Tablolara erişim için
        private readonly IMongoCollection<UriDetails> _mongoCollection;
        public ShortUrlService(IConfiguration configuration)
        {
            _configuration = configuration;
            //Veri Tabanı erişim işlemleri
            _mongoClient = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            var _MongoDatabase = _mongoClient.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            _mongoCollection = _MongoDatabase.GetCollection<UriDetails>(_configuration["DatabaseSettings:OtherCollectionName"]);
        }

        public Task<GetAllRecordResponse> GetAllRecord()
        {
            throw new NotImplementedException();
        }

        public async Task<GetRecordByIdResponse> GetById(string id)
        {
            GetRecordByIdResponse response = new GetRecordByIdResponse();
            response.IsSucces = true;
            response.Message = "Id bulundu.";
            try
            {
                response.data = await _mongoCollection.Find(x => (x.shortnerurl == id.ToString())).FirstOrDefaultAsync();
                if (response.data == null)
                {
                    response.Message = "Geçersiz id girildi lütfen geçerli id giriniz";
                }
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = "Id bulunamadı = " + ex.Message;
            }
            return response;
        }
        public static int Decode(string str)
        {
            var num = 0;
            for (var i = 0; i < str.Length; i++)
            {
                num = num * Base + Alphabet.IndexOf(str.ElementAt(i));
            }
            return num;
        }
        public async Task<GetRecordByIdResponse> GetByPath(string name)
        {
            GetRecordByIdResponse response = new GetRecordByIdResponse();
            response.IsSucces = true;
            response.Message = "Id bulundu.";
            try
            {
                int number = Decode(name);
                response.data = await _mongoCollection.Find(x => (x.randomusernumber == number.ToString())).FirstOrDefaultAsync();
                if (response.data == null)
                {
                    response.Message = "Geçersiz id girildi lütfen geçerli id giriniz";
                }
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = "Id bulunamadı = " + ex.Message;
            }
            return response;
        }
        public string Encode(int num)
        {
            var sb = new StringBuilder();
            while (num > 0)
            {
                sb.Insert(0, Alphabet.ElementAt(num % Base));
                num = num / Base;
            }
            return sb.ToString();
        }
        public async Task<InsertRecordResponse> Save(UriDetails request)
        {
            InsertRecordResponse response = new InsertRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Kayıdı Başarılı";
            try
            {
                //Hazır datetime işlemleri
                request.createdate = DateTime.Now.ToString();
                request.updateddate = String.Empty;
                request.randomusernumber = "5";
                SelectedIdQuerry.SelectedId = "5";
                request.shortnerurl = " http://localhost:5000//ShortUrls//RedirectTo//" + SelectedIdQuerry.SelectedId;
                await _mongoCollection.InsertOneAsync(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Data kayıdı hatalıdır = " + ex.Message;
            }
            return response;
        }
    }
}
