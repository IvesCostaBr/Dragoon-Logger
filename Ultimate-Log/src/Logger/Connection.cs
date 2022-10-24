using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Ultimate_Log.DTO;

namespace Ultimate_Log.Logger
{
    public class Connection
    {
        private IMongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<ReceiverLog> colNews;


        public Connection()
        {
            client = new MongoClient(Environment.GetEnvironmentVariable("DATABASE_URI"));
            database = client.GetDatabase(Environment.GetEnvironmentVariable("DATABASE"));            
            colNews = database.GetCollection<ReceiverLog>(Environment.GetEnvironmentVariable("COLLECTION"));
        }

         public bool SaveLogInDatabse(ReceiverLog data)
        {
            try
            {
                if (data != null)
                {
                    colNews.InsertOneAsync(data);
                    Console.WriteLine(data.ToString());
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}