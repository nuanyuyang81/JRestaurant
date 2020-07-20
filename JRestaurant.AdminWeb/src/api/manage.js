import request from '@/utils/request'

export function listAllAdmin() {
  return request({
    url: '/admin/manage/ListAllAdmins',
    method: 'get'
  })
}
export function getAdminTotalCount() {
  return request({
    url: '/admin/manage/GetTotalCount',
    method: 'get'
  })
}
export function addAdmin(data) {
  return request({
    url: '/admin/manage/AddAdmin',
    method: 'post',
    data
  })
}
export function updateAdmin(data) {
  return request({
    url: '/admin/manage/UpdateAdminProfile',
    method: 'post',
    data
  })
}
export function updatePassword(data) {
  return request({
    url: '/admin/manage/UpdatePassword',
    method: 'post',
    data
  })
}
