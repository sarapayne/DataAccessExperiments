using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLibrary.Models
{
    public class NoSqlContactModel
    {
        [BsonId] public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<NoSqlEmailAddressModel> EmailAddresses { get; set; } = new List<NoSqlEmailAddressModel>();
        public List<NoSqlPhoneNumberModel> PhoneNumbers { get; set; } = new List<NoSqlPhoneNumberModel>();
    }
}
