﻿using ApiServer.Cache.MemoryCache;
using ApiServer.Common;
using ApiServer.Exception;
using ApiServer.JWT;
using ApiServer.Mapping;
using ApiServer.Model.Model.MsgModel;
using AspNetCoreRateLimit;
using Autofac;
using Item.ApiServer.BLL.BLLModule;
using Item.ApiServer.DAL.DALModule;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ApiServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // 使用DI将服务注入到容器中
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(
                options =>
                {
                    // 序列化时忽略循环
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    // 使用驼峰命名
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    // Enum转换为字符串
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    // Int64转换为字符串
                    options.SerializerSettings.Converters.Add(new Int64ToStringConvert());
                    options.SerializerSettings.Converters.Add(new NullableInt64ToStringConvert());
                    // 序列化时是否忽略空值
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    // 序列化时的时间格式
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = int.MaxValue;
                options.ValueLengthLimit = int.MaxValue;
                options.MemoryBufferThreshold = int.MaxValue;
            });

            #region MemoryCache缓存

            services.AddMemoryCache(options =>
            {
                // SizeLimit缓存是没有大小的，此值设置缓存的份数
                // 注意：netcore中的缓存是没有单位的，缓存项和缓存的相对关系
                options.SizeLimit = 2;
                // 缓存满的时候压缩20%的优先级较低的数据
                options.CompactionPercentage = 0.2;
                // 两秒钟查找一次过期项
                options.ExpirationScanFrequency = TimeSpan.FromSeconds(2);
            });
            // 内置缓存注入
            services.AddTransient<MemoryCacheService>();

            // Redis缓存注入
            services.AddSingleton(new RedisCacheService(new RedisCacheOptions()
            {
                InstanceName = Configuration.GetSection("Redis:InstanceName").Value,
                Configuration = Configuration.GetSection("Redis:Connection").Value
            }));

            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region JwtSetting类注入
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            JwtSettings setting = new JwtSettings();
            Configuration.Bind("JwtSettings", setting);
            JwtHelper.Settings = setting;
            #endregion

            #region 基于策略模式的授权
            // jwt服务注入
            services.AddAuthorization(options =>
            {
                // 增加定义策略
                options.AddPolicy("Permission", policy => policy.Requirements.Add(new PermissionRequirement()));

            })

            #region JWT认证，core自带官方jwt认证
            // 开启Bearer认证
            // .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddAuthentication(s =>
            {
                //添加JWT Scheme
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // 添加JwtBearer验证服务：
            .AddJwtBearer(config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,// 是否验证Issuer
                    ValidateAudience = true,// 是否验证Audience
                    ValidateLifetime = true,// 是否验证失效时间
                    ClockSkew = TimeSpan.FromSeconds(30),
                    ValidateIssuerSigningKey = true,// 是否验证SecurityKey
                    ValidAudience = setting.Audience,// Audience
                    ValidIssuer = setting.Issuer,// Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.SecretKey))// 拿到SecurityKey
                };
                config.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        // 如果token过期，则把<是否过期>添加到返回头信息中
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    },
                    //此处为权限验证失败后触发的事件
                    OnChallenge = context =>
                    {
                        //此处代码为终止.Net Core默认的返回类型和数据结果，这个很重要哦，必须
                        context.HandleResponse();
                        if (!context.Response.HasStarted)
                        {
                            //自定义自己想要返回的数据结果，我这里要返回的是Json对象，通过引用Newtonsoft.Json库进行转换
                            // var payload = JsonConvert.SerializeObject(new { Code = 0, Message = "很抱歉，您无权访问该接口!" });
                            var payload = JsonConvert.SerializeObject(MsgModel.Error(new CustomException(403, "很抱歉，您无权访问该接口!")));
                            //自定义返回的数据类型
                            context.Response.ContentType = "application/json";
                            //自定义返回状态码，默认为401 我这里改成 200
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            //输出Json数据结果
                            context.Response.WriteAsync(payload);
                        }

                        return Task.FromResult(0);
                    }
                };
            });
            #endregion



            #endregion

            #region Cors 跨域
            services.AddCors(options =>
            {
                // 浏览器会发起2次请求,使用OPTIONS发起预检请求，第二次才是api请求
                options.AddPolicy("cors", policy =>
                {
                    policy
                    .SetIsOriginAllowed(origin => true)
                    .SetPreflightMaxAge(new TimeSpan(0, 10, 0))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials(); //指定处理cookie
                });
            });
            #endregion

            #region 注入自定义策略

            //services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            services.AddMapster();

            #endregion

            #region Swagger UI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API171",
                    Version = "v1",
                    Description = "基于.NET Core 3.1 的JWT 身份验证",
                    Contact = new OpenApiContact
                    {
                        Name = "jinjupeng",
                        Email = "im.jp@outlook.com.com",
                        Url = new Uri("http://cnblogs.com/jinjupeng"),
                    },
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
                //获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath, "ApiServer.xml");
                c.IncludeXmlComments(xmlPath);
            });
            #endregion


            services.AddMvc(options =>
            {
                // 注册全局过滤器
                options.Filters.Add<GlobalExceptionFilter>();
            });

            #region IP限流
            // https://marcus116.blogspot.com/2019/06/netcore-aspnet-core-webapi-aspnetcoreratelimit-throttle.html

            // 将速限计数器资料储存在 Memory 中
            services.AddMemoryCache();

            // 从 appsettings.json 读取 IpRateLimiting 设置
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));

            // 从 appsettings.json 读取 Ip Rule 设置
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));

            // 注入 counter and IP Rules
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            // the clientId/ clientIp resolvers use it.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Rate Limit configuration 设置
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // 配置HTTP请求管道
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // 启用限流,需在UseMvc前面
            app.UseIpRateLimiting();

            // 允许跨域
            app.UseCors("cors");

            // app.UseMiddleware<RefererMiddleware>(); // 判断Referer请求来源是否合法
            // app.UseMiddleware<ExceptionMiddleware>(); // 全局异常过滤
            app.UseRouting();

            // 先开启认证
            app.UseAuthentication();

            // 后开启授权
            app.UseAuthorization();

            // 添加请求日志中间件
            app.UseSerilogRequestLogging();

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //要在应用的根(http://localhost:<port>/) 处提供 Swagger UI，请将 RoutePrefix 属性设置为空字符串
                c.RoutePrefix = string.Empty;
                //swagger集成auth验证
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// 自动注册
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new BllModule());
            builder.RegisterModule(new DalModule());

        }
    }
}