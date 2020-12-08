using System.Collections.Generic;

namespace DataAccessLibrary.Models
{
    public class SqlFullContactModel
    {
        public SqlBasicContactModel BasicInfo { get; set; }
        public List<SqlEmailAddressModel> EmailAddresses { get; set; } = new List<SqlEmailAddressModel>();
        public List<SqlPhoneNumberModel> PhoneNumbers { get; set; } = new List<SqlPhoneNumberModel>();
    }
}