using MongoDB.Driver;
using Dragoon_Log.DTO;
using Dragoon_Log.Logger;
using Dragoon_Log.Repository.Interfaces;

namespace Dragoon_Log.Repository;

public class LogRepository: ILogRepository
{
    private Connection _conn = new Connection();
    private ILogRepository _logRepositoryImplementation;

    private IMongoCollection<ReceiverLog> GetCollection(String collectionName)
    {
        var collection = _conn.database.GetCollection<ReceiverLog>(collectionName);
        return collection;
    }
    
    public async Task<bool> Save(ReceiverLog data)
    {
        try
        {
            if (data != null)
            {
                await GetCollection(data.Collection).InsertOneAsync(data);
                Console.WriteLine(data);
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine($"erro ao salvar - {e}");
            return false;
        }
    }

    public IMongoCollection<ReceiverLog> List(String collectionName)
    {
        return GetCollection(collectionName);
    }

    public async Task<List<ReceiverLog>> ListFilter(Dictionary<string, string> filter, String collectionName)
    {
        {
            String keyUpper = filter["key"][0].ToString().ToUpper() + filter["key"].Substring(1);
            var customFilter = Builders<ReceiverLog>.Filter.Eq(
                keyUpper, DateTime.Parse(filter["value"]));
            var query = await GetCollection(collectionName).Find(customFilter).ToListAsync();
            return query;
        }
    }
}

