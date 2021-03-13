import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const routes = [
    {
        path: '/',
        name: 'Home',
        component: Home,
        redirect: '/courseware',
        children: [
            {
                path: '/courseware',
                name: 'courseware',
                component: () => import('../views/courseware/courseware')
            },
            {
                path: '/key',
                name:'key',
                component:()=>import('../views/key/Key')
            },
        ]
    },
    {
        path: '/login',
        name: 'login',
        component: () => import('../views/Login')
    },
]

const router = new VueRouter({
    routes
})

// 这里判断用户是否需要登录，判断token是否失效

export default router
