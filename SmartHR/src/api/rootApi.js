import axios from 'axios'

const baseUrl = 'http://sep490g49-api.eastasia.cloudapp.azure.com'
const rootApi = axios.create({
  baseURL: `${baseUrl}/api`,
  headers: {
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*',
  },
})

rootApi.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('accessToken')
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`
    }
    return config
  },
  (error) => {
    return Promise.reject(error)
  },
)
export {rootApi, baseUrl}
