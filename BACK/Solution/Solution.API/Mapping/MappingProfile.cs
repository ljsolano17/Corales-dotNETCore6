using AutoMapper;
using data = Solution.DO.Objects;

namespace Solution.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<data.Articulos,DataModels.Articulos>().ReverseMap();

            CreateMap<data.ArticulosSolicitud,DataModels.ArticulosSolicitud>().ReverseMap();

            CreateMap<data.Categorias,DataModels.Categorias>().ReverseMap();

            CreateMap<data.Solicitudes,DataModels.Solicitudes>().ReverseMap();

        }
    }
}
