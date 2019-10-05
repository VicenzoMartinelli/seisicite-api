using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Domain.Core.Commands
{
  public abstract class Command<T> : IRequest<T>
  {
    public DateTime Timestamp { get; private set; }

    protected Command()
    {
      Timestamp = DateTime.Now;
    }
  }
}