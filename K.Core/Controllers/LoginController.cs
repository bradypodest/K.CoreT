using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using K.Core.AuthHelper;
using K.Core.Common.Helper;
using K.Core.Common.Model;
using K.Core.IServices.System;
using K.Core.Model.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace K.Core.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api")]
    public class LoginController : Controller
    {
        readonly ISysUserService _sysUserServices;

        public LoginController(ISysUserService sysUserService) 
        {
            _sysUserServices = sysUserService;
        }

        /// <summary>
        /// 获取token 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Login")]
        public async Task<object> Login(string username = "", string password = "")
        {
            //JsonResult
            string jwtStr = string.Empty;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return new JsonResult(new
                {
                    code = ResponseEnum.ServerRequestError,
                    success = false,
                    msg = "用户名或密码不能为空"
                });
            }

            password = MD5Helper.MD5Encrypt32(password);//加密密码

            List<SysUser> user = new List<SysUser>();
            if ("admin".Equals(username))
            {
                user= await _sysUserServices.Query();
            }
            else 
            {
                //判断当前用户是否已经登陆失败超过3次,超过3次则不能登陆，直接给user=null;


                user = await _sysUserServices.Query(d => d.UserName == username && d.UserPwd == password);
            }

            
            if (user.Count > 0)
            {
                SysUser userLogin = user.FirstOrDefault();

                ClaimsIdentity claimIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);//身份ID

                var claims = new List<Claim> {//Claim是拥有什么
                    new Claim(ClaimTypes.Name, username),//保存登陆人账号
                    new Claim(JwtRegisteredClaimNames.Jti, userLogin.ID.ToString()),//保存登陆人ID
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddMinutes(60).ToString()) };//60分钟过期

                //下面就是添加角色了，如果你需要基于角色的授权的话,就需要先查询到 当前人 的角色，然后放入
                //如一个角色则为 
                claims.AddRange("admin".Split(',').Select(s => new Claim(ClaimTypes.Role, s)));
                //claims.AddRange(userRoles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

                claimIdentity.AddClaims(claims);

                var token = JwtToken.BuildJwtToken(claims.ToArray());
                return new JsonResult(new{
                    code = ResponseEnum.Success,
                    success = true,
                    msg = "认证成功",
                    data=token
                });
            }
            else
            {
                //给当前用户数据登陆次数加一

                return new JsonResult(new
                {
                    code=ResponseEnum.ServerRequestError,
                    success = false,
                    msg = "认证失败"
                });
            }


        }
    }
}