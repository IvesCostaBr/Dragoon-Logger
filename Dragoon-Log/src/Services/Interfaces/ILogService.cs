using Dragoon_Log.DTO;
using Dragoon_Log.Filter;

namespace Dragoon_Log.service.Interfaces;

public interface ILogService
{
    Task<List<ReceiverLog>> GetAllAsync(String collectionName, PaginationFilter filter);
    Task<bool> SaveLog(ReceiverLog data);
    Task<List<ReceiverLog>> ListFilter(
        Dictionary<String,String> query,
        String collectionName, 
        PaginationFilter filter);
}