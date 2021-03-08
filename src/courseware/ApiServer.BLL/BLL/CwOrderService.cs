using ApiServer.BLL.IBLL;
using ApiServer.Model.Entity;
using Microsoft.AspNetCore.Http;
using System;

namespace ApiServer.BLL.BLL
{
    public class CwOrderService : ICwOrderService
    {
        public bool CallBack(JsonObject callback, HttpContext request)
        {
            throw new NotImplementedException();
        }

        public JsonObject CreateOrder(cw_order cwOrder)
        {
            throw new NotImplementedException();
        }
    }
}
