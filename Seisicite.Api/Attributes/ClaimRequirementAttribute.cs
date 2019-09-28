using Microsoft.AspNetCore.Mvc;
using Seisicite.Api.Filters;
using System.Security.Claims;

namespace Seisicite.Api.Attributes
{
  public class ClaimRequirementAttribute : TypeFilterAttribute
  {
    public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
    {
      Arguments = new object[] { new Claim(claimType, claimValue) };
    }
  }
}
