using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UrlShortnerApps.Entities.Concrate
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("username")]
        [Required(ErrorMessage ="Lütfen Kullanici Adinizi Giriniz")]
        public string username { get; set; }
        [BsonElement("userpassword")]
        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz")]
        public string userpassword { get; set; }

        [BsonElement("useremail")]
        [Required(ErrorMessage = "Lütfen Email Adresinisizi Giriniz")]
        public string useremail { get; set; }

        [BsonElement("isadmin")]
        public bool isadmin { get; set; } = false;

        [BsonElement("createdate")]
        public string createdate { get; set; }
        [BsonElement("updateddate")]
        public string updateddate { get; set; }


    }
}
