using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary
{
    public class LiteDbDataAccess
    {
        private LiteDatabase database;

        public LiteDbDataAccess(string connectionString)
        {
            database = new LiteDatabase(connectionString);
        }

        public void UpsertRecord<T>(T record, string collectionName)
        {
            ILiteCollection<T> collection = database.GetCollection<T>(collectionName);
            collection.Insert(record);
        }

        public List<T> LoadRecords<T>(string collectionName)
        {
            ILiteCollection<T> collection = database.GetCollection<T>(collectionName);
            List<T> records = collection.FindAll().ToList();
            return records;
        }

        public T LoadRecordById<T>(string collectionName, Guid id)
        {
            ILiteCollection<T> collection = database.GetCollection<T>(collectionName);
            var record = collection.FindById(id);
            return record;
        }

        public void UpdateRecord<T>(string collectionName, T record)
        {
            var collection = database.GetCollection<T>(collectionName);
            collection.Update(record);
        }

        public void DeleteRecord<T>(string collectionName, Guid id)
        {
            var collection = database.GetCollection<T>(collectionName);
            collection.Delete(id);
        }
    }
}
