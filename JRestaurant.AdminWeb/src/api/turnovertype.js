import request from '@/utils/request'

export function listAllTypes() {
  return request({
    url: '/admin/turnovertype/ListAllTurnOverTypes',
    method: 'get'
  })
}

export function listTypesByAim(aim) {
  return request({
    url: '/admin/turnovertype/ListTurnOverTypeByAim',
    method: 'get',
    params: { aim }
  })
}

export function addType(data) {
  return request({
    url: '/admin/turnovertype/AddTurnOverType',
    method: 'post',
    data
  })
}
export function updateType(data) {
  return request({
    url: '/admin/turnovertype/UpdateTurnOverType',
    method: 'post',
    data
  })
}
export function deleteType(id, uid) {
  return request({
    url: '/admin/turnovertype/InActiveTurnOverTypes',
    method: 'get',
    params: { id, uid }
  })
}
