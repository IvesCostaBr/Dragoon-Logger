using Dragoon_Log.DTO;
using MongoDB.Driver;

namespace Dragoon_Log.Repository.Interfaces;

public interface IAuthRepository
{
    Task<bool> Save(Client data);
    IMongoCollection<Client> List();
    Task<bool> Delete(string id);
    Task<List<Client>> Filter(string clientId, string ClientSecret);
    Task<bool> DeleteAll();
}