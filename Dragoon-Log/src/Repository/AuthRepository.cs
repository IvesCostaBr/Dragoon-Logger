using Dragoon_Log.DTO;
using Dragoon_Log.Logger;
using Dragoon_Log.Repository.Interfaces;
using MongoDB.Driver;

namespace Dragoon_Log.Repository;

public class AuthRepository : IAuthRepository
{
    private Connection _conn = new Connection();
    private IMongoCollection<Client> defaultCollection;
    
    public AuthRepository()
    {
        defaultCollection = _conn.database.GetCollection<Client>("auth");
    }

    public async Task<bool> Save(Client data)
    {
        try
        {
            var collection = _conn.database.GetCollection<Client>("auth");
            await collection.InsertOneAsync(data);
            return true;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }

    }

    public IMongoCollection<Client> List()
    {
        return defaultCollection;
    }

    public async Task<bool> Delete(String id)
    {
        try
        {
            await defaultCollection.DeleteOneAsync(obj => obj.Id == id);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    public async Task<bool> DeleteAll()
    {
        try
        {
           var collection = _conn.database.GetCollection<Client>("auth");
           await collection.DeleteManyAsync(_ => true);
           return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    public async Task<List<Client>> Filter(string clientId, string clientSecret)
    {
        var filter = Builders<Client>.Filter.Where(s =>
            s.ClientId == clientId && s.ClientSecret == clientSecret);
        var collection = _conn.database.GetCollection<Client>("auth");
        return await collection.Find(filter).ToListAsync();

    }
}