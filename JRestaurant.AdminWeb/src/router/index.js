import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

/* Layout */
import Layout from '@/layout'

export const constantRoutes = [
  {
    path: '/redirect',
    component: Layout,
    hidden: true,
    children: [
      {
        path: '/redirect/:path(.*)',
        component: () => import('@/views/redirect/index')
      }
    ]
  },
  {
    path: '/login',
    component: () => import('@/views/login/index'),
    hidden: true
  },
  {
    path: '/auth-redirect',
    component: () => import('@/views/login/auth-redirect'),
    hidden: true
  },
  {
    path: '/404',
    component: () => import('@/views/error-page/404'),
    hidden: true
  },
  {
    path: '/401',
    component: () => import('@/views/error-page/401'),
    hidden: true
  },
  {
    path: '/',
    component: Layout,
    redirect: '/dashboard',
    children: [
      {
        path: 'dashboard',
        component: () => import('@/views/dashboard/index'),
        name: '总览',
        meta: { title: '总览', icon: 'dashboard', affix: true }
      }
    ]
  },
  {
    path: '/foodsturnover',
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/foodsturnover/index'),
        name: '料理营业额',
        meta: { title: '料理营业额', icon: 'money', affix: true }
      }
    ]
  },
  {
    path: '/drinksturnover',
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/drinksturnover/index'),
        name: '酒水营业额',
        meta: { title: '酒水营业额', icon: 'money', affix: true }
      }
    ]
  },
  {
    path: '/foodspo',
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/foodspo/index'),
        name: '料理进货',
        meta: { title: '料理进货', icon: 'shopping', affix: true }
      }
    ]
  },
  {
    path: '/drinkspo',
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/drinkspo/index'),
        name: '酒水进货',
        meta: { title: '酒水进货', icon: 'shopping', affix: true }
      }
    ]
  },
  {
    path: '/vendor',
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/vendor/index'),
        name: '供应商',
        meta: { title: '供应商', icon: 'tree', affix: true }
      }
    ]
  },
  {
    path: '/turnovertype',
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/turnovertype/index'),
        name: '营业额分类',
        meta: { title: '营业额分类', icon: 'list', affix: true }
      }
    ]
  },
  {
    path: '/profile',
    component: Layout,
    redirect: '/profile/index',
    hidden: true,
    children: [
      {
        path: 'index',
        component: () => import('@/views/profile/index'),
        name: '个人信息',
        meta: { title: '个人信息', icon: 'user', noCache: true }
      }
    ]
  }
]

/**
 * asyncRoutes
 * the routes that need to be dynamically loaded based on user roles
 */
export const asyncRoutes = [
  {
    path: '/manage',
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/manage/index'),
        name: '用户管理',
        meta: { title: '用户管理', icon: 'user', affix: true, roles: ['admin'] }
      }
    ]
  },
  {
    path: '/roles',
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/roles/index'),
        name: '权限管理',
        meta: { title: '权限管理', icon: 'lock', affix: true, roles: ['admin'] }
      }
    ]
  },
  // 404 page must be placed at the end !!!
  { path: '*', redirect: '/404', hidden: true }
]

const createRouter = () => new Router({
  // mode: 'history', // require service support
  scrollBehavior: () => ({ y: 0 }),
  routes: constantRoutes
})

const router = createRouter()

// Detail see: https://github.com/vuejs/vue-router/issues/1234#issuecomment-357941465
export function resetRouter() {
  const newRouter = createRouter()
  router.matcher = newRouter.matcher // reset router
}

export default router
