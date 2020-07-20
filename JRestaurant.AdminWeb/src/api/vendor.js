import request from '@/utils/request'

export function listAllVendors() {
  return request({
    url: '/admin/vendor/ListAllVendors',
    method: 'get'
  })
}
export function getVendorTotalCount() {
  return request({
    url: '/admin/vendor/GetTotalCount',
    method: 'get'
  })
}
export function ListVendorByAim(aim) {
  return request({
    url: '/admin/vendor/ListVendorByAim',
    method: 'get',
    params: { aim }
  })
}

export function addVendor(data) {
  return request({
    url: '/admin/vendor/AddVendor',
    method: 'post',
    data
  })
}
export function updateVendor(data) {
  return request({
    url: '/admin/vendor/UpdateVendor',
    method: 'post',
    data
  })
}
export function deleteVendor(id, uid) {
  return request({
    url: '/admin/vendor/InActiveVendor',
    method: 'get',
    params: { id, uid }
  })
}
