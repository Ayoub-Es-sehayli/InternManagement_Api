using System;
using InternManagement.Api.Enums;
using Microsoft.AspNetCore.Authorization;

namespace InternManagement.Api.Helpers
{
  public static class AuthorizationPolicies
  {
    public const string Admin = "Admin";
    public const string Supervisor = "Supervisor";
    public static AuthorizationPolicy AdminPolicy()
    {
      return new AuthorizationPolicyBuilder().RequireRole(Admin).Build();
    }

    public static AuthorizationPolicy SupervisorPolicy()
    {
      return new AuthorizationPolicyBuilder().RequireRole(Supervisor).Build();
    }
  }
}