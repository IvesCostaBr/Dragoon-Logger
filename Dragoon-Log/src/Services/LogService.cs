using Dragoon_Log.DTO;
using Dragoon_Log.Repository.Interfaces;
using Dragoon_Log.service.Interfaces;
using MongoDB.Driver;

namespace Dragoon_Log.service;

public class LogService: ILogService
{
    private ILogRepository _repository;

    public LogService(ILogRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ReceiverLog>> GetAllAsync(String collectionName)
    {
        return await _repository.List(collectionName).Find(_ => true).ToListAsync();
    }

    public async Task<bool> SaveLog(ReceiverLog data)
    {
        return await _repository.Save(data);
    }

    public async Task<List<ReceiverLog>> ListFilter(Dictionary<String, String> filter, String collectionName)
    {
        return await _repository.ListFilter(filter, collectionName);
    }
}