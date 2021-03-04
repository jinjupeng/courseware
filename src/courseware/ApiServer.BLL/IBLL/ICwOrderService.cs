using ApiServer.Model.Entity;

namespace ApiServer.BLL.IBLL
{
    public interface ICwOrderService
    {

        JSONObject CreateOrder(cw_order cwOrder);
        bool CallBack(JSONObject callback, HttpServletRequest request);
    }
}
