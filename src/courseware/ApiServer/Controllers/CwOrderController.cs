using ApiServer.BLL.IBLL;
using ApiServer.Model.Entity;
using ApiServer.Model.Model;
using ApiServer.Model.Model.MsgModel;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CwOrderController : ControllerBase
    {
        private readonly ICwOrderService _cwOrderService;

        public CwOrderController(ICwOrderService cwOrderService)
        {
            _cwOrderService = cwOrderService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateOrder([FromBody] cwOrder cwOrder)
        {
            var cworder = new cw_order();
            cworder = cwOrder.BuildAdapter().AdaptToType<cw_order>();
            var result = Result.SUCCESS(_cwOrderService.CreateOrder(cworder));
            return Ok(await Task.FromResult(result));
        }
    }
}
