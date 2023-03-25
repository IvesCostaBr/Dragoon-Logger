using AutoMapper;
using Dragoon_Log.DTO;
using Dragoon_Log.Repository.Interfaces;
using Dragoon_Log.service.Interfaces;
using MongoDB.Driver;

namespace Dragoon_Log.service;

public class AuthService : IAuthService
{
    private IAuthRepository _repository;
    private IMapper _mapper;

    public AuthService(
        IAuthRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseCreateAuth> Create(RegisterClient data)
    {
        var newObj = _mapper.Map<RegisterClient, Client>(data);
        var response = await _repository.Save(newObj);
        if (!response)
        {
            throw new Exception("nao foi possivel cadastrar o client.");
        }
        return _mapper.Map<Client, ResponseCreateAuth>(newObj);
    }

    public async Task<List<Client>> List()
    {
        return await _repository.List().Find(_ => true).ToListAsync();
    }

    public async Task<bool> Delete(String id)
    {
        return await _repository.Delete(id);
    }

    public async Task<bool> DeleteAll()
    {
        return await _repository.DeleteAll();
    }
}