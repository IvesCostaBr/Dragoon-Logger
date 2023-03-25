using MongoDB.Driver;

namespace Dragoon_Log.Logger
{
    public class Connection
    {
        private IMongoClient _client;
        public IMongoDatabase database;
        
        public Connection()
        {
            try
            {
                _client = new MongoClient(Config.DATABASE_URI);
                database = _client.GetDatabase(Config.DATABASE);
            }
            catch(Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o servidor.", ex);
            }

        }
    }
}