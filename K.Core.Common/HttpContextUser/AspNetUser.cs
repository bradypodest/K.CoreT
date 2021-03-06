﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace K.Core.Common.HttpContextUser
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        //public int ID => GetClaimValueByType("jti").FirstOrDefault().ObjToInt();

        //public int ID => GetClaimValueByType(JwtRegisteredClaimNames.Jti).FirstOrDefault().ObjToInt(); 
        public string ID => GetClaimValueByType("jti").FirstOrDefault().ToString();

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public List<string> GetClaimValueByType(string ClaimType)
        {
            var s = GetClaimsIdentity();

            return (from item in GetClaimsIdentity()
                    where item.Type == ClaimType
                    select item.Value).ToList();

        }
    }
}
