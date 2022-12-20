using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrlShortnerApps.DataAccess.Abstract;
using UrlShortnerApps.Entities.Concrate;

namespace UrlShortnerApps.DataAccess.Concrate
{
    public class UriRepository : IUriRepository
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;

        //Tablolara erişim için
        private readonly IMongoCollection<UriDetails> _mongoCollection;
        public UriRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            //Veri Tabanı erişim işlemleri
            _mongoClient = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            var _MongoDatabase = _mongoClient.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            _mongoCollection = _MongoDatabase.GetCollection<UriDetails>(_configuration["DatabaseSettings:OtherCollectionName"]);
        }
        public async Task<DeleteAllRecordResponse> DeleteAllRecord()
        {
            DeleteAllRecordResponse response = new DeleteAllRecordResponse();
            response.IsSuccess = true;
            response.Message = "Toplu Silme işlemi başarıyla gerçekleşmiştir.";
            try
            {
                var result = await _mongoCollection.DeleteManyAsync(x => true);
                if (!result.IsAcknowledged)
                {
                    response.Message = "Üyeler bulunamadı";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hatalı id = " + ex.Message;
            }
            return response;
        }

        public async Task<DeleteRecordByIdResponse> DeletedRecordById(DeleteRecordByIdRequest request)
        {
            DeleteRecordByIdResponse response = new DeleteRecordByIdResponse();
            response.IsSuccess = true;
            response.Message = "Silme işlemi başarıyla gerçekleşmiştir.";
            try
            {
                var result = await _mongoCollection.DeleteOneAsync(x => x.shortnerurl == request.Id);
                if (!result.IsAcknowledged)
                {
                    response.Message = "Girilen id bulunamadı lütfen geçerli bir id giriniz";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hatalı id = " + ex.Message;
            }
            return response;
        }

        public async Task<GetAllRecordResponse> GetAllRecord()
        {
            GetAllRecordResponse response = new GetAllRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data başarıyla gelmiştir.";

            try
            {
                response.data = new List<UriDetails>();
                response.data = await _mongoCollection.Find(x => true).ToListAsync();
                if (response.data.Count == 0)
                {
                    response.Message = "Data bulunamadı";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Data hatalı gelmiştir = " + ex.Message;
            }
            return response;
        }

        public async Task<GetRecordByIdResponse> GetRecordById(string id)
        {
            GetRecordByIdResponse response = new GetRecordByIdResponse();
            response.IsSucces = true;
            response.Message = "Id bulundu.";
            try
            {
                response.data = await _mongoCollection.Find(x => (x.shortnerurl == id)).FirstOrDefaultAsync();
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


        public async Task<GetRecordByNameResponse> GetRecordByName(string name)
        {
            GetRecordByNameResponse response = new GetRecordByNameResponse();
            response.IsSucces = true;
            response.Message = "İsim bulundu.";
            try
            {
                response.data = new List<UriDetails>();
                response.data = await _mongoCollection.Find(x => (x.originalurl == name) || (x.shortnerurl == name)).ToListAsync();

                if (response.data.Count == 0)
                {
                    response.Message = "Geçersiz isim girildi lütfen geçerli isim giriniz";
                }
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = "Id bulunamadı = " + ex.Message;
            }
            return response;
        }
        public async Task<GetRecordByNameResponse> GetRecordByUserId(string name)
        {
            GetRecordByNameResponse response = new GetRecordByNameResponse();
            response.IsSucces = true;
            response.Message = "İsim bulundu.";
            try
            {
                response.data = new List<UriDetails>();
                response.data = await _mongoCollection.Find(x => (x.ticketcount == name)).ToListAsync();

                if (response.data.Count == 0)
                {
                    response.Message = "Geçersiz isim girildi lütfen geçerli isim giriniz";
                }
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = "Id bulunamadı = " + ex.Message;
            }
            return response;
        }

        public async Task<InsertRecordResponse> InsertRecord(UriDetails request)
        {
            InsertRecordResponse response = new InsertRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Kayıdı Başarılı";
            try
            {
                string[] myNumber = { "A", "B", "C", "D", "E", "F", "G", "1", "2", "3", "4", "" };
                Guid myİd = new Guid();
                Random myTempRandom = new Random(0);
                string myGuidCreated = myİd.ToString();
                foreach (var item in myNumber)
                {
                    myGuidCreated += myNumber[myTempRandom.Next(myNumber.Length)];
                }
                //Hazır datetime işlemleri
                request.createdate = DateTime.Now.ToString();
                request.updateddate = String.Empty;
                request.randomusernumber = myGuidCreated;
                SelectedIdQuerry.SelectedId = myGuidCreated;
                await _mongoCollection.InsertOneAsync(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Data kayıdı hatalıdır = " + ex.Message;
            }
            return response;
        }

        public async Task<UpdateRecordByIdResponse> UpdateRecordById(UriDetails request)
        {
            UpdateRecordByIdResponse response = new UpdateRecordByIdResponse();
            response.IsSucces = true;
            response.Message = "Id başarıyla güncellenmiştir";
            try
            {
                GetRecordByIdResponse response1 = await GetRecordById(request.id);
                request.createdate = response1.data.createdate;
                request.updateddate = DateTime.Now.ToString();

                var result = await _mongoCollection.ReplaceOneAsync(x => x.id == request.id, request);

                if (!result.IsAcknowledged)
                {
                    response.Message = "Girilen id bulunamadı";
                }
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = "Hatalı id girildi = " + ex.Message;
            }
            return response;
        }

        public async Task<UpdateSalaryByIdResponse> UpdateSalaryById(UpdateSalaryByIdRequest request)
        {
            UpdateSalaryByIdResponse response = new UpdateSalaryByIdResponse();
            response.IsSuccess = true;
            response.Message = "Id başarıyla güncellenmiştir";
            try
            {
                var Filter = new BsonDocument().Add("Salary", request.Salary).Add("UpdatedDate", DateTime.Now.ToString());

                var UpdateDate = new BsonDocument("$set", Filter);

                var Result = await _mongoCollection.UpdateOneAsync(x => x.id == request.Id, UpdateDate);



                if (!Result.IsAcknowledged)
                {
                    response.Message = "Girilen id bulunamadı";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hatalı id girildi = " + ex.Message;
            }
            return response;
        }
    }
}
