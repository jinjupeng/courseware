using ApiServer.BLL.IBLL;
using ApiServer.Model.Entity;
using ApiServer.Model.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CwExchangeKeyController : ControllerBase
    {
        private readonly IBaseService<cw_exchange_key> _baseService;
        private readonly ICwExchangeKeyService _cwExchangeKeyService;
        private readonly ICommonService _commonService;

        public CwExchangeKeyController(IBaseService<cw_exchange_key> baseService, ICwExchangeKeyService cwExchangeKeyService, ICommonService commonService)
        {
            _baseService = baseService;
            _cwExchangeKeyService = cwExchangeKeyService;
            _commonService = commonService;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> ListKey([FromQuery] int pageIndex = 0, int pageSize = 0)
        {
            var pageModel = _baseService.QueryByPage(pageIndex, pageSize, _ => true);
            var result = Result.SUCCESS(pageModel);
            return Ok(await Task.FromResult(result));
        }

        [HttpGet]
        [Route("add")]
        public async Task<IActionResult> Add([FromQuery] int id)
        {
            var cwExchangeKey = new cw_exchange_key
            {
                cw_id = id,
                ex_key = Guid.NewGuid().ToString(),
                create_time = DateTime.Now,
                is_used = false,
                user_id = _commonService.GetUserId()
            };
            var result = Result.SUCCESS(_baseService.AddModel(cwExchangeKey) > 0);
            return Ok(await Task.FromResult(result));
        }

        [HttpGet]
        [Route("use")]
        public async Task<IActionResult> Use([FromQuery] string key)
        {
            var result = Result.SUCCESS(_cwExchangeKeyService.Use(key));
            return Ok(await Task.FromResult(result));
        }


        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> delete([FromQuery] int id)
        {
            var result = Result.SUCCESS(_baseService.DelBy(a => a.id == id) > 0);
            return Ok(await Task.FromResult(result));
        }
    }
}
