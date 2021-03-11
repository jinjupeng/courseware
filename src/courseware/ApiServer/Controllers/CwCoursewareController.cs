using ApiServer.BLL.IBLL;
using ApiServer.BLL.JWT;
using ApiServer.Model.Entity;
using ApiServer.Model.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiServer.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CwCoursewareController : ControllerBase
    {
        private readonly ICwCoursewareService _cwCoursewareService;
        private readonly IBaseService<cw_courseware> _baseService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cwCoursewareService"></param>
        /// <param name="baseService"></param>
        public CwCoursewareController(ICwCoursewareService cwCoursewareService, IBaseService<cw_courseware> baseService)
        {
            _cwCoursewareService = cwCoursewareService;
            _baseService = baseService;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="courseware"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "管理员")]
        public async Task<IActionResult> AddCourseware([FromBody] cw_courseware courseware)
        {
            var result = Result.SUCCESS(_cwCoursewareService.AddCW(courseware));
            return Ok(await Task.FromResult(result));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="courseware"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        [Authorize(Roles = "管理员")]
        public async Task<IActionResult> UpdateCourseware([FromBody] cw_courseware courseware)
        {
            var result = Result.SUCCESS(_cwCoursewareService.UpdateCW(courseware));
            return Ok(await Task.FromResult(result));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delete")]
        [Authorize(Roles = "管理员")]
        public async Task<IActionResult> DeleteCourseware([FromQuery] int id)
        {
            var result = Result.SUCCESS(_cwCoursewareService.DeleteCW(id));
            return Ok(await Task.FromResult(result));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetCourseware([FromQuery] int id)
        {
            var result = Result.SUCCESS(_cwCoursewareService.GetCW(id));
            return Ok(await Task.FromResult(result));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> ListCourseware([FromQuery] int pageIndex = 0, int pageSize = 10)
        {
            var pageModel = _baseService.QueryByPage(pageIndex, pageSize, _ => true);
            pageModel.List.ForEach(a => { a.url = null; });
            var result = Result.SUCCESS(pageModel);
            return Ok(await Task.FromResult(result));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("listForAdmin")]
        public async Task<IActionResult> ListCoursewareByAdmin([FromQuery] int pageIndex = 0, int pageSize = 10)
        {
            var pageModel = _baseService.QueryByPage(pageIndex, pageSize, _ => true);
            var result = Result.SUCCESS(pageModel);
            return Ok(await Task.FromResult(result));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getCarousel")]
        public async Task<IActionResult> GetCarousel()
        {
            var result = Result.SUCCESS(_cwCoursewareService.GetCarousel());
            return Ok(await Task.FromResult(result));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getCarouselForAdmin")]
        public async Task<IActionResult> GetCarouselForAdmin()
        {
            var cwCoursewares = _cwCoursewareService.GetCarousel();
            // 如果当前登录用户的角色不是管理员，则将url数据置为null
            var userId = JwtHelper.LoginUserId(HttpContext);

            var result = Result.SUCCESS(cwCoursewares);
            return Ok(await Task.FromResult(result));
        }
    }
}
