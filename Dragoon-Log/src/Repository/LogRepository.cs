using MongoDB.Driver;
using Dragoon_Log.DTO;
using Dragoon_Log.Logger;
using Dragoon_Log.Repository.Interfaces;

namespace Dragoon_Log.Repository;

public class LogRepository: ILogRepository
{
    private Connection _conn = new Connection();
    private IMongoCollection<ReceiverLog> _defaultCollection;
    
    public LogRepository()
    {
        _defaultCollection = _conn.database.GetCollection<ReceiverLog>(
            Config.COLLECTION);
    }
    
    public async Task<bool> Save(ReceiverLog data)
    {
        try
        {
            if (data != null)
            {
                await _defaultCollection.InsertOneAsync(data);
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

    public IMongoCollection<ReceiverLog> List()
    {
        return _defaultCollection;
    }

    public async Task<List<ReceiverLog>> ListFilter(Dictionary<string, string> filter)
    {
        {
            String keyUpper = filter["key"][0].ToString().ToUpper() + filter["key"].Substring(1);
            var customFilter = Builders<ReceiverLog>.Filter.Eq(
                keyUpper, DateTime.Parse(filter["value"]));
            var query = await _defaultCollection.Find(customFilter).ToListAsync();
            return query;
        }
    }
}

