using System;
using InternManagement.Api.Enums;
using Microsoft.AspNetCore.Authorization;

namespace InternManagement.Api.Helpers
{
  public static class AuthorizationPolicies
  {
    public static AuthorizationPolicy AdminPolicy()
    {
      return new AuthorizationPolicyBuilder().RequireRole(Enum.GetName(eUserRole.Admin)).Build();
    }

    public static AuthorizationPolicy SupervisorPolicy()
    {
      return new AuthorizationPolicyBuilder().RequireRole(Enum.GetName(eUserRole.Supervisor)).Build();
    }
  }
}