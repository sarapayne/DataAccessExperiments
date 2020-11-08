using System;
using System.IO;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace SqLiteUI
{
    class Program
    {
        static void Main(string[] args)
        {
            SqliteCrud sql = new SqliteCrud(GetConnectionString());

            ReadAllContacts(sql);

            //ReadContact(sql, 3);

            //CreateNewContact(sql);

            //UpdateContact(sql);

            //RemovePhoneNumberFromContact(sql, 1, 1);

            Console.WriteLine("Done Processing SqLite");

            Console.ReadLine();
        }
        
        private static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = "";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            output = config.GetConnectionString(connectionStringName);

            return output;
        }
        
        private static void RemovePhoneNumberFromContact(SqliteCrud sql, int contactId, int phoneNumberId)
        {
            sql.RemovePhoneNumberFromContact(contactId, phoneNumberId);
        }

        private static void UpdateContact(SqliteCrud sql)
        {
            SqlBasicContactModel contact = new SqlBasicContactModel
            {
                Id = 1,
                FirstName = "Timothy",
                LastName = "Corey"
            };
            sql.UpdateContactName(contact);
        }

        private static void CreateNewContact(SqliteCrud sql)
        {
            SqlFullContactModel user = new SqlFullContactModel
            {
                BasicInfo = new SqlBasicContactModel
                {
                    FirstName = "Charity",
                    LastName = "Corey"
                }
            };

            user.EmailAddresses.Add(new SqlEmailAddressModel { EmailAddress = "nope@aol.com" });
            user.EmailAddresses.Add(new SqlEmailAddressModel { Id = 2, EmailAddress = "me@timothycorey.com" });

            user.PhoneNumbers.Add(new SqlPhoneNumberModel { Id = 1, PhoneNumber = "555-1212" });
            user.PhoneNumbers.Add(new SqlPhoneNumberModel { PhoneNumber = "555-9876" });

            sql.CreateContact(user);
        }

        private static void ReadAllContacts(SqliteCrud sql)
        {
            var rows = sql.GetAllContacts();

            foreach (var row in rows)
            {
                Console.WriteLine($"{ row.Id }: { row.FirstName } { row.LastName }");
            }
        }

        private static void ReadContact(SqliteCrud sql, int contactId)
        {
            var contact = sql.GetFullContactById(contactId);

            Console.WriteLine($"{ contact.BasicInfo.Id }: { contact.BasicInfo.FirstName } { contact.BasicInfo.LastName }");
        }
    }
}