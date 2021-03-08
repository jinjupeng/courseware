using ApiServer.Model.Entity;
using Microsoft.AspNetCore.Http;
using System;

namespace ApiServer.BLL.IBLL
{
    public interface ICwOrderService
    {

        JsonObject CreateOrder(cw_order cwOrder);
        bool CallBack(JsonObject callback, HttpContext request);
    }
}
