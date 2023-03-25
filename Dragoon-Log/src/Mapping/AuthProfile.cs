using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Dragoon_Log.DTO;

public class AuthProfile: Profile
{
    static UInt64 CalculateHash(string read, bool lowTolerance)
    {
        UInt64 hashedValue = 0;
        int i = 0;
        while (i < read.Length)
        {
            hashedValue += read.ElementAt(i) * (UInt64)Math.Pow(31, i);
            if (lowTolerance) i += 2;
            else i++;
        }
        return hashedValue;
    }
    
    public AuthProfile()
    {

        CreateMap<RegisterClient, Client>()
            .ForMember(dest => dest.ClientSecret, 
                opt => opt.MapFrom(src => CalculateHash(src.ClientName, false).ToString()))
            .ForMember(dest => dest.ClientId,
                opt => opt.MapFrom(src => CalculateHash(src.Description, false).ToString()))
            .ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ClientName,
                opt => opt.MapFrom(src => src.ClientName
                ))
            .ForMember(dest => dest.ModifiedAt,
                opt => opt.MapFrom(src => DateTime.Now));
        CreateMap<Client, ResponseCreateAuth>().ForMember(dest => dest.ClientSecret,
                opt => opt.MapFrom(src => src.ClientSecret))
            .ForMember(dest => dest.ClientId, opt 
                => opt.MapFrom(src => src.ClientId));
    }
}