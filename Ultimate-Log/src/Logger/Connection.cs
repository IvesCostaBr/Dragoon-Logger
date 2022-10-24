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
            client = new MongoClient("mongodb+srv://recharge:recharge%402022@cluster0.ri2aos7.mongodb.net/?retryWrites=true&w=majority");
            database = client.GetDatabase("ultimate-log");            
            colNews = database.GetCollection<ReceiverLog>("general-log");
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