using ApiServer.BLL.IBLL;
using ApiServer.Common;
using ApiServer.DAL.IDAL;
using ApiServer.Model.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ApiServer.BLL.BLL
{
    public class CwOrderService : ICwOrderService
    {
        private readonly ICommonService _commonService;
        private readonly IBaseDal<cw_user_courseware> _userCoursewareDal;
        private readonly IBaseDal<cw_courseware> _coursewareDal;
        private readonly IBaseDal<cw_order> _orderDal;
        private readonly string mchid = ConfigTool.Configuration["wxpay:mchid"];
        private readonly string key = ConfigTool.Configuration["wxpay:key"];


        public CwOrderService(ICommonService commonService, IBaseDal<cw_user_courseware> userCoursewareDal,
            IBaseDal<cw_courseware> coursewareDal, IBaseDal<cw_order> orderDal)
        {
            _commonService = commonService;
            _userCoursewareDal = userCoursewareDal;
            _coursewareDal = coursewareDal;
            _orderDal = orderDal;
        }

        /// <summary>
        /// 支付回调
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool CallBack(Dictionary<string, string> callback, HttpContext request)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> CreateOrder(cw_order cwOrder)
        {
            var userDto = _commonService.GetUserDto();
            var cwUserCourseware = _userCoursewareDal.GetModel(a => a.user_id == userDto.id && a.cw_id == cwOrder.cw_id);
            if (cwUserCourseware == null)
            {
                cwOrder.is_pay = null;
                cwOrder.create_time = null;
                cwOrder.pay_time = null;
                var cwCourseware = _coursewareDal.GetModel(a => a.id == (int)cwOrder.cw_id);
                cwOrder.order_sn = Guid.NewGuid().ToString();
                cwOrder.price = cwCourseware.price;
                cwOrder.user_id = userDto.id;
                _orderDal.AddAndSave(cwOrder);
                // todo：调用微信支付接口
                // return WxPay.minAppPay(cwOrder.order_sn, "" + cwOrder.price, mchid, "购买课件ID为:" + cwOrder.cw_id, "初七课件", null, "https://xxx/api/cworder/callback", null, null, null, key);
                return null;
            }
            else
            {
                var dict = new Dictionary<string, string>();
                dict.Add("message", "已经购买请勿重复购买");
                return dict;
            }
        }
    }
}
