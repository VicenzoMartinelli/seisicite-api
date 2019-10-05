using MediatR;

namespace Application.Api.Queries
{
  public class CanEvaluateArticleQuery : IRequest<bool>
  {
    public string Id { get; set; }
    public string EvaluatorId { get; set; }

    public CanEvaluateArticleQuery SetId(string id)
    {
      Id = id;

      return this;
    }
  }
}
