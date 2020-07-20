import request from '@/utils/request'

export function getRoutes() {
  return request({
    url: '/admin/routes',
    method: 'get'
  })
}

export function listAllRoles() {
  return request({
    url: '/admin/role/ListAllRoles',
    method: 'get'
  })
}

export function addRole(data) {
  return request({
    url: '/admin/role/AddRole',
    method: 'post',
    data
  })
}

export function updateRole(data) {
  return request({
    url: '/admin/role/UpdateRole',
    method: 'post',
    data
  })
}

export function deleteRole(roleId, ownerId) {
  return request({
    url: `/admin/role/InActiveRole`,
    params: { roleId, ownerId }
  })
}
