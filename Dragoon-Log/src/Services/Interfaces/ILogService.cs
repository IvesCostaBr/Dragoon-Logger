using Dragoon_Log.DTO;

namespace Dragoon_Log.service.Interfaces;

public interface ILogService
{
    Task<List<ReceiverLog>> GetAllAsync();
    Task<bool> SaveLog(ReceiverLog data);
    Task<List<ReceiverLog>> ListFilter(Dictionary<String, String> filter);
}