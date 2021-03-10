using ApiServer.BLL.IBLL;
using ApiServer.DAL.IDAL;
using ApiServer.Model.Entity;
using ApiServer.Model.Model.Dto;
using System;

namespace ApiServer.BLL.BLL
{
    public class CwExchangeKeyService : ICwExchangeKeyService
    {
        private readonly IBaseDal<cw_exchange_key> _baseDal;
        private readonly IBaseDal<cw_user_courseware> _userCourseDal;

        public CwExchangeKeyService(IBaseDal<cw_exchange_key> baseDal, IBaseDal<cw_user_courseware> userCourseDal)
        {
            _baseDal = baseDal;
            _userCourseDal = userCourseDal;
        }
        public bool Use(string key)
        {
            var result = false;
            var cwExchangeKey = _baseDal.GetModel(a => a.ex_key == key);
            if(cwExchangeKey != null && !(bool)cwExchangeKey.is_used)
            {
                // todo:根据token获取UserDto
                var userDto = new UserDto();
                var cwUserCourseware = _userCourseDal.GetModel(a => a.cw_id == cwExchangeKey.cw_id && a.user_id == userDto.id);
                if(cwUserCourseware == null)
                {
                    var userCourseware = new cw_user_courseware {
                        cw_id = cwExchangeKey.cw_id,
                        user_id = userDto.id
                    };
                    _userCourseDal.AddAndSave(userCourseware);
                    cwExchangeKey.is_used = true;
                    cwExchangeKey.user_id = userDto.id;
                    cwExchangeKey.use_time = DateTime.Now;
                    _baseDal.ModifyAndSave(cwExchangeKey);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
