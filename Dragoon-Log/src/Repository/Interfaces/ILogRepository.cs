using MongoDB.Driver;
using Dragoon_Log.DTO;

namespace Dragoon_Log.Repository.Interfaces;

public interface ILogRepository
{
     Task<bool> Save(ReceiverLog data);
     IMongoCollection<ReceiverLog> List(String collectionName);
     Task<List<ReceiverLog>> ListFilter(Dictionary<string, string> filter, String collectionName);
}