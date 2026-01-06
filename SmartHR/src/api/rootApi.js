import axios from 'axios'

// const baseUrl = 'http://localhost:5158'
const baseUrl = 'https://etm-azcvdeftdeamfsdx.japaneast-01.azurewebsites.net'
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
