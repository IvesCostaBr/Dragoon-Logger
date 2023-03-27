using MongoDB.Driver;
using Dragoon_Log.DTO;
using Dragoon_Log.Filter;
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

    public async Task<List<ReceiverLog>> ListFilter(
        Dictionary<string, string> queryData,
        String collectionName,
        PaginationFilter filter)
    {
        {
            object data;
            String keyUpper = queryData["key"][0].ToString().ToUpper() + queryData["key"].Substring(1);
            if (keyUpper == "Date")
                data = DateTime.Parse(queryData["value"]); else data = queryData["value"];
            var customFilter = Builders<ReceiverLog>.Filter.Eq(
                keyUpper, data);
               
            var query = await GetCollection(collectionName)
                .Find(customFilter).SortByDescending(e => e.Date)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Limit(filter.PageSize).ToListAsync();
            return query;
        }
    }
}

