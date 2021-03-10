using ApiServer.Model.Entity;
using ApiServer.Model.Model.Dto;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ApiServer.Mapping
{
    /// <summary>
    /// Mapster注入
    /// </summary>
    public static class MapsterMap
    {
        /// <summary>
        /// 自定义扩展service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMapster(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;

            #region 返回前端实体类映射

            config.NewConfig<sys_user, UserDto>().NameMatchingStrategy(NameMatchingStrategy.ToCamelCase);
            config.NewConfig<cw_user_courseware, CwUserCoursewareDto>().NameMatchingStrategy(NameMatchingStrategy.ToCamelCase);
            #endregion

            #region 接收前端实体类映射

            //config.NewConfig<SysRole, Sys_Role>().NameMatchingStrategy(NameMatchingStrategy.FromCamelCase);

            #endregion

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
