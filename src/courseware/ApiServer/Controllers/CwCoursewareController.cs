using ApiServer.BLL.IBLL;
using ApiServer.Model.Entity;
using ApiServer.Model.Model;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IBaseService<cw_courseware> baseService;

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="courseware"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/add")]
        public async Task<IActionResult> AddCourseWare([FromBody] cw_courseware courseware)
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
        [Route("/update")]
        public async Task<IActionResult> UpdateCourseWare([FromBody] cw_courseware courseware)
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
        [Route("/delete")]
        public async Task<IActionResult> DeleteCourseWare([FromQuery] int id)
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
        [Route("/get")]
        public async Task<IActionResult> getCourseWare([FromQuery] int id)
        {
            var result = Result.SUCCESS(_cwCoursewareService.GetCW(id));
            return Ok(await Task.FromResult(result));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/list")]
        public async Task<IActionResult> listCourseWare([FromQuery] int start)
        {
            var result = Result.SUCCESS(_cwCoursewareService.listCW(start));
            return Ok(await Task.FromResult(result));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/listForAdmin")]
        public async Task<IActionResult> listCourseWareByAdmin([FromQuery] int start)
        {
            var result = Result.SUCCESS();
            return Ok(await Task.FromResult(result));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/getCarousel")]
        public async Task<IActionResult> getCarousel()
        {
            var result = Result.SUCCESS(_cwCoursewareService.GetCarousel());
            return Ok(await Task.FromResult(result));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/getCarouselForAdmin")]
        public async Task<IActionResult> getCarouselForAdmin()
        {
            var result = Result.SUCCESS(_cwCoursewareService.GetCarousel());
            return Ok(await Task.FromResult(result));
        }
    }
}
