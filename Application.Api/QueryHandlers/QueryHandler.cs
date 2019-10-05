using AutoMapper;
using Domain.Core.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.CommandHandlers
{
  public abstract class QueryHandler
  {
    protected readonly IMapper _mapper;

    public QueryHandler(IMapper mapper)
    {
      _mapper = mapper;
    }

    public IEnumerable<TDestinyType> Project<T, TDestinyType>(IQueryable<T> projectList) => _mapper.ProjectTo<TDestinyType>(projectList);
  }
}
