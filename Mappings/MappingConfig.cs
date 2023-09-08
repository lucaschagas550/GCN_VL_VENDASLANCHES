using AutoMapper;
using VL_VendasLanches.Models;
using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Mappings
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Produto, ProdutoViewModel>()
                .ForMember(pvm => pvm.ComplementosSelecionados, o => o.MapFrom(p => p.Complementos.Select(s => s.Id.ToString())))
                .ForMember(pvm => pvm.TamanhosSelecionados, o => o.MapFrom(p => p.Tamanhos.Select(s => s.Tamanho.Id.ToString())));

                config.CreateMap<ProdutoViewModel, Produto>();
                config.CreateMap<InformacoesUsuarios, LoginViewModel>().ReverseMap();
                config.CreateMap<InformacoesUsuarios, EditLoginViewModel>().ReverseMap();
                config.CreateMap<Pedido, CheckoutViewModel>().ReverseMap();
                config.CreateMap<Categoria, ListaProdutosHomeViewModel>().ReverseMap();
                
            });

            return mappingConfig;
        }
    }
}
