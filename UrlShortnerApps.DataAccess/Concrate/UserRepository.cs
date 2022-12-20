using Microsoft.Extensions.Configuration;
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
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;

        private readonly IMongoCollection<Users> _mongoCollection;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            //Veri Tabanı erişim işlemleri
            _mongoClient = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            var _MongoDatabase = _mongoClient.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            _mongoCollection = _MongoDatabase.GetCollection<Users>(_configuration["DatabaseSettings:CollectionName"]);
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
                var result = await _mongoCollection.DeleteOneAsync(x => x.useremail == request.Id);
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

        public async Task<GetAllRecordUser> GetAllRecord()
        {
            GetAllRecordUser response = new GetAllRecordUser();
            response.IsSuccess = true;
            response.Message = "Data başarıyla gelmiştir.";

            try
            {
                response.data = new List<Users>();
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

        public async Task<GetRecordByIdUser> GetRecordById(string id)
        {
            GetRecordByIdUser response = new GetRecordByIdUser();
            response.IsSucces = true;
            response.Message = "Id bulundu.";
            try
            {
                response.data = await _mongoCollection.Find(x => (x.id == id)).FirstOrDefaultAsync();
                if (response.data == null)
                {
                    response.Message = "Geçersiz id girildi lütfen geçerli id giriniz";
                }
                else
                {
                    response.IsSucces = true;
                }
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = "Id bulunamadı = " + ex.Message;
            }
            return response;
        }

        public async Task<GetRecordByIdUser> GetRecordByName(string name,string password)
        {
            GetRecordByIdUser response = new GetRecordByIdUser();
            response.IsSucces = true;
            response.Message = "İsim bulundu.";
            try
            {
                response.data = new Users();
                response.data = await _mongoCollection.Find(x => (x.useremail == name) &&  (x.userpassword == password)).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = "Id bulunamadı = " + ex.Message;
            }
            return response;
        }

        public async Task<InsertRecordResponse> InsertRecord(Users request)
        {
            InsertRecordResponse response = new InsertRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Kayıdı Başarılı";
            try
            {
                //Hazır datetime işlemleri
                request.createdate = DateTime.Now.ToString();
                request.updateddate = String.Empty;

                await _mongoCollection.InsertOneAsync(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Data kayıdı hatalıdır = " + ex.Message;
            }
            return response;
        }

        public async Task<UpdateRecordByIdResponse> UpdateRecordById(Users request)
        {
            UpdateRecordByIdResponse response = new UpdateRecordByIdResponse();
            response.IsSucces = true;
            response.Message = "Id başarıyla güncellenmiştir";
            try
            {
                GetRecordByIdUser response1 = await GetRecordById(request.id);
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
