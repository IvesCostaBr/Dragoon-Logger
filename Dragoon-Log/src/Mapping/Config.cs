using AutoMapper;

namespace Dragoon_Log.Mapping;

public static class MappingConfig
{
    public static MapperConfiguration InitializeAutoMapper()
    {
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(typeof(AuthProfile));
        });
        return config;
    }
}