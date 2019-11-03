using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using K.Core.AOP;
using K.Core.AuthHelper;
using K.Core.Common;
using K.Core.Common.HttpContextUser;
using K.Core.Common.LogHelper;
using K.Core.Common.MemoryCache;
using K.Core.Services;
using K.Core.Hubs;
using K.Core.IServices;
using K.Core.Log;
using K.Core.Middlewares;
using K.Core.Model;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using StackExchange.Profiling.Storage;
using Swashbuckle.AspNetCore.Swagger;
using static K.Core.SwaggerHelper.CustomApiVersion;

namespace K.Core
{
    public class Startup
    {

        /// <summary>
        /// log4net 仓储库
        /// </summary>
        public static ILoggerRepository Repository { get; set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;

            #region  我觉得这块的代码是设置 log4net的配置且启动
            //log4net
            Repository = LogManager.CreateRepository(Configuration["Logging:Log4Net:Name"]);
            //指定配置文件，如果这里你遇到问题，应该是使用了InProcess模式，请查看K.Core.csproj,并删之
            var contentPath = env.ContentRootPath;//获取或设置包含应用程序内容文件的目录的绝对路径
            var log4Config = Path.Combine(contentPath, "log4net.config");
            XmlConfigurator.Configure(Repository, new FileInfo(log4Config));
            #endregion
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; }
        private const string ApiName = "K.Core";

        // This method gets called by the runtime. Use this method to add services to the container.
        //注册
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region 部分服务注入-netcore自带方法
            #region MemoryCaching 类生成实例 ， 用于 注入CacheAOP 类中
            // 缓存注入
            services.AddScoped<ICaching, MemoryCaching>();//下方生成的MemoryCache实例  需要注入到MemoryCaching类中，所以需要下方代码
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());//创建一个新的MemoryCache实例。内置缓存
                return cache;
            });
            #endregion

            #region RedisCacheManager 类生成实例  ，用于注入 RedisCacheAop类中
            // Redis注入
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>();
            #endregion

            #region LogHelper 类生成实例   现用于全局异常拦截类
            // log日志注入   现用于全局异常拦截类
            services.AddSingleton<ILoggerHelper, LogHelper>();
            #endregion

            services.AddSingleton(new Appsettings(Env));// 实例化操作appsettings.json 类
            services.AddSingleton(new LogLock(Env));//写日志，读日志， 好像主要服务于SignalR 去发送日志的
            #endregion

            #region 初始化DB   orm sugar框架 根据model 层类生成对应的数据库表和数据 ， 可以在项目第一次运行时 打开，等待数据库有数据时注释掉
            services.AddScoped<K.Core.Model.Models.DBSeed>();
            services.AddScoped<K.Core.Model.Models.MyContext>();
            #endregion

            #region Automapper    实体转换关系的模型，AutoMapper是一个.NET的对象映射工具。主要作用是进行领域对象与模型（DTO）之间的转换、数据库查询结果映射至实体对象
            //先配置 映射文件 CustomProfile 类  ，见使用如在 BlogArticleServices类的 方法里 
            services.AddAutoMapper(typeof(Startup));//这是AutoMapper的2.0新特性   自动找到所有继承了Profile的类然后进行配置
            #endregion
            #region CORS    两种cors:一种 所有； 另一种 特殊站点可以访问
            //跨域第二种方法，声明策略，记得下边app中配置
            services.AddCors(c =>
            {
                //↓↓↓↓↓↓↓注意正式环境不要使用这种全开放的处理↓↓↓↓↓↓↓↓↓↓
                c.AddPolicy("AllRequests", policy =>
                {
                    policy
                    .AllowAnyOrigin()//允许任何源
                    .AllowAnyMethod()//允许任何方式
                    .AllowAnyHeader()//允许任何头
                    .AllowCredentials();//允许cookie
                });
                //↑↑↑↑↑↑↑注意正式环境不要使用这种全开放的处理↑↑↑↑↑↑↑↑↑↑


                //一般采用这种方法
                c.AddPolicy("LimitRequests", policy =>
                {
                    // 支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                    // 注意，http://127.0.0.1:1818 和 http://localhost:1818 是不一样的，尽量写两个
                    policy
                    .WithOrigins("http://127.0.0.1:1818", "http://localhost:8080", "http://localhost:8021", "http://localhost:8081", "http://localhost:1818")
                    .AllowAnyHeader()//Ensures that the policy allows any header.
                    .AllowAnyMethod();
                });
            });

            //跨域第一种办法，注意下边 Configure 中进行配置
            //services.AddCors();
            #endregion

            #region MiniProfiler  接口执行时间分析——MiniProfiler   //见https://www.cnblogs.com/laozhang-is-phi/p/10287023.html
            //配置服务
            services.AddMiniProfiler(options =>
                {
                    options.RouteBasePath = "/profiler"; //注意这个路径要和core项目根路径下的 index.html 脚本配置中的一致，
                    //(options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(10);
                    options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.Left;//显示位置
                    options.PopupShowTimeWithChildren = true;//指示默认情况下是否显示“Time with Children”列 ；默认false

                    // 可以增加权限
                    //options.ResultsAuthorize = request => request.HttpContext.User.IsInRole("Admin");
                    //options.UserIdProvider = request => request.HttpContext.User.Identity.Name;
                }
            );

            //关于代码使用 可见  如 LogAOP 类  MiniProfiler.Current.Step($"执行Service方法：{invocation.Method.Name}() -> ");
            #endregion

            #region Swagger UI Service  //见https://www.cnblogs.com/laozhang-is-phi/p/9507387.html

            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;//获取项目路径
            services.AddSwaggerGen(c =>
            {
                //遍历出全部的版本，做文档信息展示
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new Info
                    {
                        // {ApiName} 定义成全局变量，方便修改
                        Version = version,
                        Title = $"{ApiName} 接口文档",
                        Description = $"{ApiName} HTTP API " + version,
                        TermsOfService = "None",
                        Contact = new Contact { Name = "K.Core", Email = "K.Core@xxx.com", Url = "https://www.jianshu.com/u/94102b59cc2a" }
                    });
                    // 按相对路径排序，作者：Alby
                    c.OrderActionsBy(o => o.RelativePath);
                });


                //就是这里
                var xmlPath = Path.Combine(basePath, "K.Core.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改

                var xmlModelPath = Path.Combine(basePath, "K.Core.Model.xml");//这个就是Model层的xml文件名
                c.IncludeXmlComments(xmlModelPath);

                #region Token绑定到ConfigureServices

                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();

                // 发行人
                var IssuerName = (Configuration.GetSection("JWT"))["Issuer"];
                var security = new Dictionary<string, IEnumerable<string>> { { IssuerName, new string[] { } }, };
                c.AddSecurityRequirement(security);

                //方案名称“K.Core”可自定义，上下一致即可
                c.AddSecurityDefinition(IssuerName, new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
                #endregion



            //小知识：如果不想显示某些接口，直接在controller 上，或者action 上，增加特性 [ApiExplorerSettings(IgnoreApi = true)]
            });

            #endregion

            #region MVC + GlobalExceptions     //权限+数据库认证  这里可以多思考

            //注入全局异常捕获
            services.AddMvc(o =>
            {
                // 全局异常过滤
                o.Filters.Add(typeof(GlobalExceptionsFilter));
                // 全局路由权限公约
                o.Conventions.Insert(0, new GlobalRouteAuthorizeConvention());//可以判断进入的方法是否具有某个属性
                // 全局路由前缀，统一修改路由
                o.Conventions.Insert(0, new GlobalRoutePrefixFilter(new RouteAttribute(RoutePrefix.Name)));//给url上加某个前缀
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)//兼容版本
            // 取消默认驼峰
            .AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });


            #endregion

            #region TimedJob   定时任务  ：现阶段我发现没有什么实际的作用

            //services.AddHostedService<Job1TimedService>();
            //services.AddHostedService<Job2TimedService>();

            #endregion

            #region Httpcontext

            // Httpcontext 注入
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//注入到AspNetUser
            services.AddScoped<IUser, AspNetUser>();//实例化 AspNetUser ,可注入到 userController

            #endregion

            #region SignalR 通讯
            services.AddSignalR();
            #endregion

            #region Authorize 权限认证三步走

            //Tips：注释中的中括号【xxx】的内容，与下边region中的模块，是一一匹配的

            /*
             * 如果不想走数据库，仅仅想在代码里配置授权，这里可以按照下边的步骤：
             * 1步、【1、基于角色的API授权】
             * 很简单，只需要在指定的接口上，配置特性即可，比如：[Authorize(Roles = "Admin,System,Others")]
             * 
             * 但是如果你感觉"Admin,System,Others"，这样的字符串太长的话，可以把这个融合到简单策略里          
             * 具体的配置，看下文的Region模块【2、基于策略的授权（简单版）】 ，然后在接口上，配置特性：[Authorize(Policy = "A_S_O")]
             * 
             * 
             * 2步、配置Bearer认证服务，具体代码看下文的 region 【第二步：配置认证服务】
             * 
             * 3步、开启中间件
             */



            /*
             * 如果想要把权限配置到数据库，步骤如下：
             * 1步、【3复杂策略授权】
             * 具体的查看下边 region 内的内容
             * 
             * 2步、配置Bearer认证服务，具体代码看下文的 region 【第二步：配置认证服务】
             * 
             * 3步、开启中间件
             */



            //3、综上所述，设置权限，必须要三步走，授权 + 配置认证服务 + 开启授权中间件，只不过自定义的中间件不能验证过期时间，所以我都是用官方的。

            #region 【第一步：授权】

            #region 1、基于角色的API授权 

            // 1【授权】、这个很简单，其他什么都不用做， 只需要在API层的controller上边，增加特性即可，注意，只能是角色的:
            // [Authorize(Roles = "Admin,System")]


            #endregion

            #region 2、基于策略的授权（简单版）

            // 1【授权】、这个和上边的异曲同工，好处就是不用在controller中，写多个 roles 。
            // 然后这么写 [Authorize(Policy = "Admin")]
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
            //    options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
            //    options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
            //    options.AddPolicy("A_S_O", policy => policy.RequireRole("Admin", "System", "Others"));
            //});


            #endregion

            #region 【3、复杂策略授权】   不是很懂哦

            #region 参数
            //读取配置文件
            //var JWTConfig = Configuration.GetSection("JWT");
            //var symmetricKeyAsBase64 = JWTConfig["Secret"];
            //var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            //var signingKey = new SymmetricSecurityKey(keyByteArray);


            //var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            //// 如果要数据库动态绑定，这里先留个空，后边处理器里动态赋值
            //var permission = new List<PermissionItem>();

            //// 角色与接口的权限要求参数
            //var permissionRequirement = new PermissionRequirement(
            //    "/api/denied",// 拒绝授权的跳转地址（目前无用）
            //    permission,
            //    ClaimTypes.Role,//基于角色的授权
            //    JWTConfig["Issuer"],//发行人
            //    JWTConfig["Audience"],//听众
            //    signingCredentials,//签名凭据
            //    expiration: TimeSpan.FromSeconds(60 * 60)//接口的过期时间
            //    );
            #endregion

            //【授权】
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(Permissions.Name,
            //             policy => policy.Requirements.Add(permissionRequirement));
            //});


            #endregion


            #endregion





            #region 【第二步：配置认证服务】

            // 令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                SaveSigninToken = true,//保存token,后台验证token是否生效(重要)
                ValidateIssuer = true,//是否验证Issuer
                ValidateAudience = true,//是否验证Audience
                ValidateLifetime = true,//是否验证失效时间
                ValidateIssuerSigningKey = true,//是否验证SecurityKey
                ValidAudience = Appsettings.app(new String[] { "JWT", "Audience" }),//Audience //订阅人
                ValidIssuer = Appsettings.app(new String[] { "JWT", "Issuer" }),//Issuer，这两项和前面签发jwt的设置一致 //发行人
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Appsettings.app(new String[] { "JWT", "Secret" }))),

                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true,//是否需要过期时间

                AudienceValidator = (IEnumerable<string> audiences, SecurityToken securityToken,
                  TokenValidationParameters validationParameters) =>
                {
                    bool audienceValidator = true;
                    return audienceValidator;
                }//不是很懂，好像可以去掉
            };

            //2.1【认证】、core自带官方JWT认证
            // 开启Bearer认证
            services.AddAuthentication("Bearer")
                // 添加JwtBearer服务
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = tokenValidationParameters;
                 o.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         // 如果过期，则把<是否过期>添加到，返回头信息中
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.Headers.Add("Token-Expired", "true");
                         }
                         return Task.CompletedTask;
                     }
                 };
             });


            //2.2【认证】、IdentityServer4 认证 (暂时忽略)
            //services.AddAuthentication("Bearer")
            //  .AddIdentityServerAuthentication(options =>
            //  {
            //      options.Authority = "http://localhost:5002";
            //      options.RequireHttpsMetadata = false;
            //      options.ApiName = "k.core.api";
            //  });


            // 注入权限处理器   好像是配合上面的  复杂策略授权 使用的，不是很懂
            //services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            //services.AddSingleton(permissionRequirement);
            #endregion

            #endregion

            #region AutoFac DI : 实例化了service、Repository层， 使用了autofac的aop拦截器（注册）
            //实例化 AutoFac  容器   
            var builder = new ContainerBuilder();
            //注册要通过反射创建的组件
            //builder.RegisterType<AdvertisementServices>().As<IAdvertisementServices>();
            builder.RegisterType<CacheAOP>();//可以直接替换其他拦截器           a.一定要把拦截器进行注册
            builder.RegisterType<RedisCacheAOP>();//可以直接替换其他拦截器
            builder.RegisterType<LogAOP>();//这样可以注入第二个

            // ※※★※※ 如果你是第一次下载项目，请先F6编译，然后再F5执行，※※★※※

            #region 带有接口层的服务注入

            #region Service.dll 注入，有对应接口
            //获取项目绝对路径，请注意，这个是实现类的dll文件，不是接口 IService.dll ，注入容器当然是Activatore
            try
            {
                var servicesDllFile = Path.Combine(basePath, "K.Core.Services.dll");
                var assemblysServices = Assembly.LoadFrom(servicesDllFile);//直接采用加载文件的方法  ※※★※※ 如果你是第一次下载项目，请先F6编译，然后再F5执行，※※★※※

                //builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();//指定已扫描程序集中的类型注册为提供所有其实现的接口。


                ////知识点：
                ////注册要通过反射创建的组件
                //builder.RegisterType<AdvertisementServices>().As<IAdvertisementServices>();
                ////二者的区别
                //Assembly.LoadFile只载入相应的dll文件，比如Assembly.LoadFile("a.dll")，则载入a.dll，假如a.dll中引用了b.dll的话，b.dll并不会被载入。
                //Assembly.LoadFrom则不一样，它会载入dll文件及其引用的其他dll，比如上面的例子，b.dll也会被载入。

                
                // AOP 开关，如果想要打开指定的功能，只需要在 appsettigns.json 对应对应 true 就行。
                var cacheType = new List<Type>();
                if (Appsettings.app(new string[] { "AppSettings", "RedisCachingAOP", "Enabled" }).ObjToBool())
                {
                    cacheType.Add(typeof(RedisCacheAOP));
                }
                if (Appsettings.app(new string[] { "AppSettings", "MemoryCachingAOP", "Enabled" }).ObjToBool())
                {
                    cacheType.Add(typeof(CacheAOP));
                }
                if (Appsettings.app(new string[] { "AppSettings", "LogAOP", "Enabled" }).ObjToBool())
                {
                    cacheType.Add(typeof(LogAOP));
                }

                builder.RegisterAssemblyTypes(assemblysServices)
                          .AsImplementedInterfaces()
                          .InstancePerLifetimeScope()
                          .EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;   //对目标类型启用接口拦截
                                                        // 如果你想注入两个，就这么写  InterceptedBy(typeof(BlogCacheAOP), typeof(BlogLogAOP));
                                                        // 如果想使用Redis缓存，请必须开启 redis 服务，端口号我的是6319，如果不一样还是无效，否则请使用memory缓存 BlogCacheAOP
                          .InterceptedBy(cacheType.ToArray());//允许将拦截器服务的列表分配给注册。      b.上方a之后才能将拦截器添加到要注入容器的接口或者类之上
                #endregion

                #region Repository.dll 注入，有对应接口
                var repositoryDllFile = Path.Combine(basePath, "K.Core.Repository.dll");
                var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
                builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();
            }
            catch (Exception ex)
            {
                throw new Exception("※※★※※ 如果你是第一次下载项目，请先对整个解决方案dotnet build（F6编译），然后再对api层 dotnet run（F5执行），\n因为解耦了，如果你是发布的模式，请检查bin文件夹是否存在Repository.dll和service.dll ※※★※※" + ex.Message + "\n" + ex.InnerException);
            }
            #endregion
            #endregion


            #region 没有接口层的服务层注入

            ////因为没有接口层，所以不能实现解耦，只能用 Load 方法。
            ////注意如果使用没有接口的服务，并想对其使用 AOP 拦截，就必须设置为虚方法
            ////var assemblysServicesNoInterfaces = Assembly.Load("K.Core.Services");
            ////builder.RegisterAssemblyTypes(assemblysServicesNoInterfaces);

            #endregion

            #region 没有接口的单独类 class 注入    这里只是举个例子  无作用
            ////只能注入该类中的虚方法    
            //builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Love)))
            //    .EnableClassInterceptors()
            //    .InterceptedBy(typeof(LogAOP));

            #endregion


            //将services填充到Autofac容器生成器中
            builder.Populate(services);

            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();

            #endregion

            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            #region ReuestResponseLog       使用自定义中间件  ：记录下请求路径、参数和返回参数

            if (Appsettings.app("AppSettings", "Middleware_RequestResponse", "Enabled").ObjToBool())
            {
                app.UseReuestResponseLog();//记录请求与返回数据 
            }

            #endregion

            #region Environment
            if (env.IsDevelopment())
            {
                // 在开发环境中，使用异常页面，这样可以暴露错误堆栈信息，所以不要放在生产环境。
                app.UseDeveloperExceptionPage();

                //app.Use(async (context, next) =>
                //{
                //    //这里会多次调用，这里测试一下就行，不要打开注释
                //    //var blogs =await _blogArticleServices.GetBlogs();
                //    var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                //    Console.WriteLine(processName);
                //    await next();
                //});
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // 在非开发环境中，使用HTTP严格安全传输(or HSTS) 对于保护web安全是非常重要的。
                // 强制实施 HTTPS 在 ASP.NET Core，配合 app.UseHttpsRedirection
                //app.UseHsts();

            }
            #endregion

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //之前是写死的
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");

                //根据版本名称倒序 遍历展示
                typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName} {version}");
                });

                #region 在 Swagger 中配置 MiniProfiler
                //将swagger首页，设置成我们自定义的页面，记得这个字符串的写法：解决方案名.index.html       这个页面index.html  在根目录下
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("K.Core.index.html");//这里是配合MiniProfiler进行性能监控的，《文章：完美基于AOP的接口性能分析》，如果你不需要，可以暂时先注释掉，不影响大局。
                #endregion
                c.RoutePrefix = ""; //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉，如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "doc";
            });
            #endregion

            #region MiniProfiler   
            app.UseMiniProfiler();//调用MiniProfiler中间件
            #endregion

            #region 第三步：开启认证中间件

            //此授权认证方法已经放弃，请使用下边的官方验证方法。但是如果你还想传User的全局变量，还是可以继续使用中间件，第二种写法//app.UseMiddleware<JwtTokenAuth>(); 
            //app.UseJwtTokenAuth();

            //如果你想使用官方认证，必须在上边ConfigureService 中，配置JWT的认证服务 (.AddAuthentication 和 .AddJwtBearer 二者缺一不可)
            app.UseAuthentication();
            #endregion

            #region CORS
            //跨域第二种方法，使用策略，详细策略信息在ConfigureService中
            app.UseCors("AllRequests");//将 CORS 中间件添加到 web 应用程序管线中, 以允许跨域请求。


            #region 跨域第一种版本
            //跨域第一种版本，请要ConfigureService中配置服务 services.AddCors();
            //    app.UseCors(options => options.WithOrigins("http://localhost:8021").AllowAnyHeader()
            //.AllowAnyMethod());  
            #endregion

            #endregion


            // 跳转https
            //app.UseHttpsRedirection();
            // 使用静态文件
            app.UseStaticFiles();//在swagger 中配置 MiniProfiler 后 ，需要将这个写上
            // 使用cookie
            app.UseCookiePolicy();
            // 返回错误码
            app.UseStatusCodePages();//把错误码返回前台，比如是404


            app.UseMvc();


            app.UseSignalR(routes =>
            {
                //这里要说下，为啥地址要写 /api/xxx 
                //因为我前后端分离了，而且使用的是代理模式，所以如果你不用/api/xxx的这个规则的话，会出现跨域问题，毕竟这个不是我的controller的路由，而且自己定义的路由
                routes.MapHub<ChatHub>("/api2/chatHub");
            });
        }

    }
}
