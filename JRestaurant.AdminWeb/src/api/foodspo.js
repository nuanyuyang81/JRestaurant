import request from '@/utils/request'

export function listFoodsPOByWeek(week) {
  return request({
    url: '/admin/foodspo/ListFoodsPOByWeek',
    method: 'get',
    params: { week }
  })
}
export function listFoodsPOToday() {
  return request({
    url: '/admin/foodspo/GetFoodsPOByToDay',
    method: 'get'
  })
}
export function addFoodsPO(data) {
  return request({
    url: '/admin/foodspo/AddFoodsPO',
    method: 'post',
    data
  })
}
export function updateFoodsPO(data) {
  return request({
    url: '/admin/foodspo/UpdateFoodsPO',
    method: 'post',
    data
  })
}
export function deleteFoodsPO(id) {
  return request({
    url: '/api/foodspo/' + id,
    method: 'delete'
  })
}
