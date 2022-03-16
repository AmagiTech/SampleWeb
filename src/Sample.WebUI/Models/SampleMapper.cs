using Sample.Data.Models;
using Sample.Data.Models.DTOs;
using AutoMapper;

namespace Sample.WebUI.Models
{
    public class SampleMapper : Profile
    {
        public SampleMapper()
        {

            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<Item, CreateOrUpdateItemDto>().ReverseMap()
                .ForMember(x => x.Category, opt => opt.Ignore());
            CreateMap<Category, CategoryDto>()
                .ForMember(x => x.Category, opt => opt.MapFrom(y => y.Name))
                .ReverseMap()
                .ForMember(y => y.Name, opt => opt.MapFrom(x => x.Category));
            CreateMap<CategoryDetail, CategoryDetailDto>()
                .ForMember(x => x.Color, opt => opt.MapFrom(y => y.ColorName))
                .ForMember(x => x.Value, opt => opt.MapFrom(y => y.ColorValue))
                .ReverseMap()
                .ForMember(y => y.ColorValue, opt => opt.MapFrom(x => x.Value))
                .ForMember(y => y.ColorName, opt => opt.MapFrom(x => x.Color));
        }
    }
}
