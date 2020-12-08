using System;
using System.IO;
using System.Linq;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace MongoDbUI
{
    class Program
    {
        private static MongoDbDataAccess db;
        private static readonly string tableName = "Contacts";
        
        static void Main(string[] args)
        {
            db = new MongoDbDataAccess("TimsDataAccessCourse", GetConnectionString());
            NoSqlContactModel user = new NoSqlContactModel
            {
                FirstName = "Charity",
                LastName = "Corey"
            };
            user.EmailAddresses.Add(new NoSqlEmailAddressModel {EmailAddress = "nope@aol.com"});
            user.EmailAddresses.Add(new NoSqlEmailAddressModel {EmailAddress = "me@iamtimcorey.com"});
            user.PhoneNumbers.Add(new NoSqlPhoneNumberModel{PhoneNumber = "555-1212"});
            user.PhoneNumbers.Add(new NoSqlPhoneNumberModel{PhoneNumber = "555-9876"});
            //CreateContact(user);
            GetAllContacts();
            //GetContactById("36ee2c20-ce43-4a63-99fa-fd145254362d");
            //UpdateContactsFirstName("Timothy", "36ee2c20-ce43-4a63-99fa-fd145254362d");
            //RemovePhoneNumberFromUser("555-1212","36ee2c20-ce43-4a63-99fa-fd145254362d");
            //RemoveUser("36ee2c20-ce43-4a63-99fa-fd145254362d");
            Console.WriteLine("Done Processing MongoDb");
            Console.WriteLine("Press Any Key To Continue");
            Console.ReadKey();
        }

        public static void RemoveUser(string id)
        {
            Guid guid = new Guid(id);
            db.DeleteRecord<NoSqlContactModel>(tableName, guid);
        }

        public static void RemovePhoneNumberFromUser(string phoneNumber, string id)
        {
            Guid guid = new Guid(id);
            var contact = db.LoadRecordById<NoSqlContactModel>(tableName, guid);
            contact.PhoneNumbers = contact.PhoneNumbers.Where(x => x.PhoneNumber != phoneNumber).ToList();
            db.UpsetRecord(tableName, contact.Id, contact);
        }

        private static void UpdateContactsFirstName(string firstName, string id)
        {
            Guid guid = new Guid(id);
            var contact = db.LoadRecordById<NoSqlContactModel>(tableName, guid);
            contact.FirstName = firstName;
            db.UpsetRecord(tableName, contact.Id, contact);
        }

        private static void GetContactById(string id)
        {
            Guid guid = new Guid(id);
            var contact = db.LoadRecordById<NoSqlContactModel>(tableName, guid);
            Console.WriteLine($"{contact.Id}: {contact.FirstName} {contact.LastName}");
        }

        private static void GetAllContacts()
        {
            var contacts = db.LoadRecords<NoSqlContactModel>(tableName);
            foreach (var contact in contacts)
            {
                Console.WriteLine($"{contact.Id}: {contact.FirstName} {contact.LastName}");
            }
        }

        private static void CreateContact(NoSqlContactModel contact)
        {
            db.UpsetRecord(tableName, contact.Id, contact);
        }

        private static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            output = config.GetConnectionString(connectionStringName);
            return output;
        }
    }
}