using ApiServer.BLL.IBLL;
using ApiServer.Model.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UploadController : ControllerBase
    {
        private readonly IOssService _ossService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ossService"></param>
        public UploadController(IOssService ossService)
        {
            _ossService = ossService;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadImg([FromForm] IFormCollection form)
        {
            var url = _ossService.Upload(form);
            var urlMap = new Dictionary<string, string>();
            urlMap.Add("url", url);
            var result = Result.SUCCESS(urlMap);
            return Ok(await Task.FromResult(result));
        }

        [HttpGet]
        [Route("deleteObject")]
        public async Task<IActionResult> Delete([FromForm] string url)
        {
            var result = Result.SUCCESS(_ossService.Delete(url) > 0);
            return Ok(await Task.FromResult(result));
        }
    }
}
