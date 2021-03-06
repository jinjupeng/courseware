import http from './http.js'
import {cwUrl} from '@/utils/env.js'
//user-section
export const login = function(data) {
	return http('/sysuser/login', 'post', data)
}
export const getUserInfo = function(data) {
	return http('/sysuser/userinfo', 'get', data)
}
export const updateUserInfo = function(data) {
	return http('/sysuser/updateUser', 'post', data)
}
//user-section
//upload-section

export const upload = (filePath) => {
	let token = "Bearer ";
	token += uni.getStorageSync('token');
	const then= uni.uploadFile({
		url: cwUrl + "/upload/upload",
		filePath: filePath,
		name:'file',
		header: {
			'Authorization': token
		}
	})
	return then
}
//