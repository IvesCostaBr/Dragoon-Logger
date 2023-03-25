using Dragoon_Log.DTO;
using MongoDB.Driver;

namespace Dragoon_Log.service.Interfaces;

public interface IAuthService
{
    Task<ResponseCreateAuth> Create(RegisterClient data);
    Task<List<Client>> List();
    Task<bool> Delete(String id);
    Task<bool> DeleteAll();
}