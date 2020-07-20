import request from '@/utils/request'

export function listFTByWeek(week) {
  return request({
    url: '/admin/foodsturnover/ListFoodsTurnOverByWeek',
    method: 'get',
    params: { week }
  })
}
export function listFTToday() {
  return request({
    url: '/admin/foodsturnover/ListFoodsTurnOverToday',
    method: 'get'
  })
}
export function addFT(data) {
  return request({
    url: '/admin/foodsturnover/AddFoodsTurnOver',
    method: 'post',
    data
  })
}
export function updateFT(data) {
  return request({
    url: '/admin/foodsturnover/UpdateFoodsTurnOver',
    method: 'post',
    data
  })
}
export function deleteFT(id) {
  return request({
    url: '/api/foodsturnover/' + id,
    method: 'delete'
  })
}
