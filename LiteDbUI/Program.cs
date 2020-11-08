using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using DataAccessLibrary;
using DataAccessLibrary.Models;


namespace LiteDbUI
{
    class Program
    {
        private static LiteDbDataAccess database;
        private static readonly string collectionName = "Contacts";


        static void Main(string[] args)
        {
            database = new LiteDbDataAccess(GetConnectionString());
            //AddRecords();

            //var contacts = database.LoadRecords<NoSqlContactModel>(collectionName);
            //var contact = contacts[0];
            //DeleteContact(contact.Id);

            //DeleteAllContacts();

            //UpdateContacts();

            PrintAllContacts();
            Console.WriteLine("Processing LiteDb Finished");
            Console.WriteLine("Press Any Key To exit");
            Console.ReadKey();
        }

        private static void AddRecords()
        {
            for (int index = 0; index < 5; index++)
            {
                NoSqlContactModel contact = new NoSqlContactModel();
                contact.FirstName = "First" + index;
                contact.LastName = "Last" + index;
                contact.EmailAddresses.Add(new NoSqlEmailAddressModel { EmailAddress = "someone@somewhere.universe" });
                contact.EmailAddresses.Add(new NoSqlEmailAddressModel { EmailAddress = "someone@somewhere.universe" });
                contact.EmailAddresses.Add(new NoSqlEmailAddressModel { EmailAddress = "someone@somewhere.universe" });
                contact.EmailAddresses.Add(new NoSqlEmailAddressModel { EmailAddress = "someone@somewhere.universe" });
                database.UpsertRecord(contact, collectionName);
            }
        }

        private static void UpdateContacts()
        {
            var contacts = database.LoadRecords<NoSqlContactModel>(collectionName);
            foreach (var contact in contacts)
            {
                foreach (var email in contact.EmailAddresses)
                {
                    email.EmailAddress = "newEmail@new.place";
                }
                database.UpdateRecord(collectionName, contact);
            }
        }

        private static void DeleteContact(Guid id)
        {
            var contacts = database.LoadRecords<NoSqlContactModel>(collectionName);
            var contact = contacts[0];
            database.DeleteRecord<NoSqlContactModel>(collectionName, contact.Id);
        }

        private static void DeleteAllContacts()
        {
            var contacts = database.LoadRecords<NoSqlContactModel>(collectionName);
            foreach (var contact in contacts)
            {
                database.DeleteRecord<NoSqlContactModel>(collectionName, contact.Id);
            }
        }

        private static void PrintAllContacts()
        {
            var contacts = database.LoadRecords<NoSqlContactModel>(collectionName);
            foreach (var contact in contacts)
            {
                PrintContact(contact);
            }
        }

        private static void PrintContact(NoSqlContactModel contact)
        {
            Console.WriteLine("FirstName: " + contact.FirstName + " LastName: " + contact.LastName);
            foreach (NoSqlEmailAddressModel emailAddress in contact.EmailAddresses)
            {
                Console.WriteLine(emailAddress.EmailAddress);
            }
            foreach (NoSqlPhoneNumberModel phoneNumber in contact.PhoneNumbers)
            {
                Console.WriteLine(phoneNumber.PhoneNumber);
            }
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
