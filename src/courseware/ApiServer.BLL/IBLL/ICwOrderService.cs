using ApiServer.Model.Entity;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ApiServer.BLL.IBLL
{
    public interface ICwOrderService
    {

        Dictionary<string, string> CreateOrder(cw_order cwOrder);
        bool CallBack(Dictionary<string, string> callback, HttpContext request);
    }
}
