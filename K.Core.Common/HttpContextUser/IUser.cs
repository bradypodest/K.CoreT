﻿using System.Collections.Generic;
using System.Security.Claims;

namespace K.Core.Common.HttpContextUser
{
    public interface IUser
    {
        string Name { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
        List<string> GetClaimValueByType(string ClaimType);
    }
}
