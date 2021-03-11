using ApiServer.BLL.IBLL;
using ApiServer.Model.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CwExchangeKeyController : ControllerBase
    {
        private readonly IBaseService<cw_exchange_key> _baseService;
        private readonly ICwExchangeKeyService _cwExchangeKeyService;

        public CwExchangeKeyController(IBaseService<cw_exchange_key> baseService, ICwExchangeKeyService cwExchangeKeyService)
        {
            _baseService = baseService;
            _cwExchangeKeyService = cwExchangeKeyService;
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
            return Ok(await Task.FromResult(""));
        }

        [HttpGet]
        [Route("add")]
        public async Task<IActionResult> Add([FromQuery] int id)
        {
            var cwExchangeKey = new cw_exchange_key { 
                cw_id = id,
                ex_key = Guid.NewGuid().ToString(),
                create_time = DateTime.Now,
                is_used = false,
                //user_id = 1, // 根据token来获取
            };
            return Ok(await Task.FromResult(""));
        }

        [HttpGet]
        [Route("use")]
        public async Task<IActionResult> Use([FromQuery] string key)
        {
            var result = _cwExchangeKeyService.Use(key);
            return Ok(await Task.FromResult(result));
        }


        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> delete([FromQuery] int id)
        {
            var result = _baseService.DelBy(a => a.id == id) > 0;
            return Ok(await Task.FromResult(result));
        }
    }
}
