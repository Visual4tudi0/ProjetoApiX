using AutoMapper;
using ClassLibraryDomain.Entity;
using ClassLibraryDomain.Models;
using ClassLibraryDomain.Models.DTO;



namespace ClassLibraryData1.Mapping { 

        public class MappingProfile : Profile
        {
        public MappingProfile()
        {
            //CreateMap<Produto, ProdutoDto>().ReverseMap();
            //CreateMap<Categoria, CategoriaDto>().ReverseMap();  
            CreateMap<ProdutoEntity, Produto>()
                .ForMember(dest => dest.PrecoReal, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CategoriaEntity, Categoria>()
                .ForMember(e => e.Produtos, opt => opt
                .MapFrom(src => src.Produtos))
                .ReverseMap();

            CreateMap<ProdutoEntity, Produto>()
             .ForMember(dest => dest.PrecoReal, opt => opt.MapFrom(src => src.PrecoBase))
             .ReverseMap();
        }
        }
        
}
