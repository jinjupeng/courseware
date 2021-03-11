import http from "../../http";
//user-section
export const login = function(data) {
    return http.post('/sysuser/login', data)
}
export const getUserInfo = function(data) {
    return http.get('/sysuser/userinfo',{params:data})
}
export const updateUserInfo = function(data) {
    return http.post('/sysuser/updateUser', data)
}
//user-section
//upload-section
export const uploadFile=function (data) {
    return http.post('/upload/upload',data)
}
//upload-section