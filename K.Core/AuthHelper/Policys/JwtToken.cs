using K.Core.Common;
using K.Core.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace K.Core.AuthHelper
{
    /// <summary>
    /// JWTToken生成类
    /// </summary>
    public class JwtToken
    {
        /// <summary>
        /// 获取基于JWT的Token  
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static dynamic BuildJwtToken(Claim[] claims) 
        {
            var now = DateTime.Now;
            // 实例化JwtSecurityToken
            //(string issuer = null, string audience = null, IEnumerable< Claim > claims = null, DateTime? notBefore = null, DateTime? expires = null, SigningCredentials signingCredentials = null);
            var jwt = new JwtSecurityToken(
                issuer: Appsettings.app(new string[] { "JWT", "Issuer" }),
                audience: Appsettings.app(new string[] { "JWT", "Audience" }),
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(60), 
                signingCredentials: new SigningCredentials(
                                           new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Appsettings.app(new String[] { "JWT", "Secret" })))//与Startup类中 令牌验证参数 IssuerSigningKey 一致
                                           , SecurityAlgorithms.HmacSha256)
            );
            // 生成 Token
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            //打包返回前台
            var responseJson = new
            {
                success = true,
                msg="获取token成功",
                token = encodedJwt,
                expires_in = 60 * 1000,//60*1000秒
                token_type = "Bearer"
            };

            return responseJson;
        }




        ///// <summary>
        ///// 获取基于JWT的Token
        ///// </summary>
        ///// <param name="claims">需要在登陆的时候配置</param>
        ///// <param name="permissionRequirement">在startup中定义的参数</param>
        ///// <returns></returns>
        //public static dynamic BuildJwtToken(Claim[] claims, PermissionRequirement permissionRequirement)
        //{
        //    var now = DateTime.Now;
        //    // 实例化JwtSecurityToken
        //    var jwt = new JwtSecurityToken(
        //        issuer: permissionRequirement.Issuer,
        //        audience: permissionRequirement.Audience,
        //        claims: claims,
        //        notBefore: now,
        //        expires: now.Add(permissionRequirement.Expiration),
        //        signingCredentials: permissionRequirement.SigningCredentials
        //    );
        //    // 生成 Token
        //    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        //    //打包返回前台
        //    var responseJson = new
        //    {
        //        success = true,
        //        token = encodedJwt,
        //        expires_in = permissionRequirement.Expiration.TotalSeconds,
        //        token_type = "Bearer"
        //    };
        //    return responseJson;
        //}
    }
}
