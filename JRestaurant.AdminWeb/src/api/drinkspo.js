import request from '@/utils/request'

export function listDrinksPOByWeek(week) {
  return request({
    url: '/admin/drinkspo/ListDrinksPOByWeek',
    method: 'get',
    params: { week }
  })
}
export function listDrinksPOToday() {
  return request({
    url: '/admin/drinkspo/GetDrinksPOByToDay',
    method: 'get'
  })
}
export function addDrinksPO(data) {
  return request({
    url: '/admin/drinkspo/AddDrinksPO',
    method: 'post',
    data
  })
}
export function updateDrinksPO(data) {
  return request({
    url: '/admin/drinkspo/UpdateDrinksPO',
    method: 'post',
    data
  })
}
export function deleteDrinksPO(id) {
  return request({
    url: '/api/drinkspo/' + id,
    method: 'delete'
  })
}
