import request from '@/utils/request'

export function listDTByWeek(week) {
  return request({
    url: '/admin/drinksturnover/ListDrinksTurnOverByWeek',
    method: 'get',
    params: { week }
  })
}
export function listDTToday() {
  return request({
    url: '/admin/drinksturnover/ListDrinksTurnOverToday',
    method: 'get'
  })
}
export function addDT(data) {
  return request({
    url: '/admin/drinksturnover/AddDrinksTurnOver',
    method: 'post',
    data
  })
}
export function updateDT(data) {
  return request({
    url: '/admin/drinksturnover/UpdateDrinksTurnOver',
    method: 'post',
    data
  })
}
export function deleteDT(id) {
  return request({
    url: '/api/drinksturnover/' + id,
    method: 'delete'
  })
}
