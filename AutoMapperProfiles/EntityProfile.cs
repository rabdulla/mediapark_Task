using AutoMapper;
using CountryHolidays_API.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CountryHolidays_API.AutoMapperProfiles
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<Holiday, Models.Holiday>().ForMember(x => x.Names, opt => opt.MapFrom(src =>
                new List<Models.HolidayName>
                {
                    new Models.HolidayName
                    {
                        Text = src.Name
                    }
                }));

            CreateMap<Models.Holiday, Holiday>().ForMember(x => x.Name,
                opt => opt.MapFrom(
                    src => src.Names.First(x => x.Lang == "eng").Text));

            CreateMap<Models.HolidayType, HolidayType>().ReverseMap();
            CreateMap<Models.Date, Date>().ReverseMap();
            CreateMap<Models.Country, Country>().ReverseMap();
            CreateMap<Models.Country, Country>().ForMember(x => x.FullName, opt => opt.MapFrom(src => src.CountryFullName));
            CreateMap<Models.Country, Country>().ForMember(x => x.HolidayName, opt => opt.MapFrom(src => src.HolidayName.First()));


        }
    }
}
