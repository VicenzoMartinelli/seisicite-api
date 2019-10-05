using AutoMapper;
using Infra.CrossCuting.AutoMapper.Profiles;
using System;

namespace Infra.CrossCuting.AutoMapper
{
  public class AutoMapperConfig
  {
    public static MapperConfiguration RegisterMappings()
    {
      return new MapperConfiguration(cfg =>
      {
        #region Default Value Mappings
        cfg.CreateMap<string, int>().ConvertUsing(s => Convert.ToInt32(s));
        cfg.CreateMap<int, string>().ConvertUsing(i => i.ToString());
        cfg.CreateMap<string, DateTime>().ConvertUsing(s => Convert.ToDateTime(s));
        cfg.CreateMap<DateTime, string>().ConvertUsing(d => d.ToShortDateString());
        cfg.CreateMap<string, char>().ConvertUsing(s => string.IsNullOrEmpty(s) || s.Length == 0 ? ' ' : s[0]);
        cfg.CreateMap<char, string>().ConvertUsing(s => new string(new char[] { s }));
        #endregion

        cfg.AddProfile(new ViewModelToDomainMappingProfile ());
        cfg.AddProfile(new DomainToViewModelMappingProfile ());
      });
    }
  }
}
