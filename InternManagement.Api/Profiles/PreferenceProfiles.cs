using System.Linq;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Models;

namespace InternManagement.Api.Profiles
{
    public class PreferenceProfile : Profile
    {
        public PreferenceProfile()
        {
            CreateMap<PreferencesDto, Preferences>();
            CreateMap<Preferences, PreferencesDto>();
        }
    }
}