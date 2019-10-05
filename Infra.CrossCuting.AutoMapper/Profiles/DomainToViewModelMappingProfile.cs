using Application.Api.ViewModels;
using AutoMapper;
using Domains.Article;

namespace Infra.CrossCuting.AutoMapper.Profiles
{
  public class DomainToViewModelMappingProfile : Profile
  {
    public DomainToViewModelMappingProfile()
    {
      CreateMap<Article, ArticleViewModel>()
        .ForMember(x => x.Type, (src) => src.MapFrom(x => x.ApresentationType));
    }
  }
}
