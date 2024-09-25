/* eslint-disable no-param-reassign */
/* eslint-disable no-use-before-define */
import * as messErr from '../constants/MessageError'

export function parseResponseError(err) {
  let code = 200
  let message = []
  try {
    code = err.response.status
    const {error} = err.response.data
    const mesResponse = error.cause
    if (mesResponse) {
      message.push(mesResponse)
    } else {
      message = error.map((item) => item.cause)
    }
  } catch (error) {
    message.push(messErr.SERVER_INTERNAL_ERROR)
    code = 500
  }
  return {
    message,
    code,
  }
}

export function buildListingUrl(url, data = {}) {
  url = buildBaseUrl(url, data)
  const params = []
  if (data.page) params.push(`page=${data.page}`)
  if (data.pageSize) params.push(`page_size=${data.pageSize}`)
  if (data.query) params.push(`query=${data.query}`)
  if (data.join) params.push(`join=${encodeURIComponent(data.join)}`)
  if (data.sortBy) params.push(`sort_by=${encodeURIComponent(data.sortBy)}`)

  const paramStr = params.join('&')
  return url.includes('?') ? `${url}&${paramStr}` : `${url}?${paramStr}`
}

export function buildGetOneUrl(url, data) {
  // add this (eslint)
  return this.buildBaseUrl(url, data)
}

export function buildBaseUrl(url, data) {
  const params = []
  if (data.regionId)
    params.push(`region_id=${encodeURIComponent(data.regionId)}`)
  if (data.zoneId) params.push(`zone_id=${encodeURIComponent(data.zoneId)}`)
  if (data.fields) params.push(`fields=${encodeURIComponent(data.fields)}`)
  if (data.exFields)
    params.push(`ex_fields=${encodeURIComponent(data.exFields)}`)

  const paramStr = params.join('&')
  return url.includes('?') ? `${url}&${paramStr}` : `${url}?${paramStr}`
}

export function filterUserInfo(data) {
  const userFields = [
    'id',
    'user_name',
    'email',
    'role',
    'token_type',
    'access_token',
  ]
  try {
    const user = {}
    userFields.forEach((key) => {
      user[key] = data[key]
    })
    return user
  } catch (e) {
    return null
  }
}
