import request from '@/utils/request'

export function login(data) {
  return request({
    url: '/token/login',
    method: 'post',
    data
  })
}

export function getInfo(phone) {
  return request({
    url: '/admin/manage/GetUsrInfo',
    method: 'get',
    params: { phone }
  })
}

export function logout() {
  return request({
    url: '/admin/manage/logout',
    method: 'post'
  })
}
