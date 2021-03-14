import axios from 'axios'
import router from './src/router'
import { Message } from 'element-ui'
const base = "/api"

const http = axios.create({

    baseURL: base,
    headers: { "vary": "Access-Control-Request-Headers", "Content-Type": "application/json" }
})

http.baseURL = base

// 请求
http.interceptors.request.use(function (config) {
    if (localStorage.getItem('token')) {
        config.headers.Authorization = 'Bearer ' + localStorage.getItem('token')
    }else{ // 如果获取不到token值，则返回到登录页面
        router.push("/login")
    }
    return config;
}, function (error) {
    return Promise.reject(error);
});

// 响应
http.interceptors.response.use(response => {
    if ((20000 <= response.data.code && response.data.code <= 30000) || response.data.code >= 70003) {
        Message(response.data.message)
        router.push("/login")
        return Promise.reject('error')
    } else {
        return response;
    }
}, error => {
    let code = 0
    try {
        code = error.response.data.status
    } catch (e) {
        if (error.toString().indexOf('Error: timeout') !== -1) {
            Message("网络请求超时")
            return Promise.reject(error)
        }
    }
});

export default http