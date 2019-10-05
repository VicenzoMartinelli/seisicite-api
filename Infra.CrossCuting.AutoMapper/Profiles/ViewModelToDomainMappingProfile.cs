using Application.Api.Commands;
using AutoMapper;
using Domains.Article;

namespace Infra.CrossCuting.AutoMapper.Profiles
{
  public class ViewModelToDomainMappingProfile : Profile
  {
    public ViewModelToDomainMappingProfile()
    {
      CreateMap<UpdateArticleCommand, Article>()
        .ForMember(x => x.ApresentationType, (src) => src.MapFrom(s => s.Type));
    }
  }
}
