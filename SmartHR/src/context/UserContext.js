import React, {useEffect} from 'react' // @function  UserContext
import {decodeJWT} from '../assets/utils/TokenUtils'
import useRefreshToken from '../pages/auth/request/refreshTokenRequest'
import {useNavigate} from 'react-router-dom'
import {deleteAuthCookie} from '../components/utils'
import {useCallback} from 'react'
const UserContext = React.createContext({
  username: '',
  userId: '',
  role: '',
  departmentId: '',
  expDateAccessToken: '',
  auth: false,
})

// @function  UserProvider
// Create function to provide UserContext
const UserProvider = ({children}) => {
  const navigate = useNavigate()

  const [user, setUser] = React.useState({
    username: '',
    userId: '',
    role: '',
    departmentId: '',
    expDateAccessToken: '',
    auth: false,
  })

  const [tokenExpiry, setTokenExpiry] = React.useState(null) // Thêm state để theo dõi thời gian hết hạn token
  const loginContext = useCallback(
    (username, accessToken) => {
      const decodedToken = decodeJWT(accessToken)
      const userId =
        decodedToken[
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
        ]
      const expDate = decodedToken['exp'] * 1000
      const role =
        decodedToken[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ]
      const departmentId =
        decodedToken[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid'
        ]
      setUser((user) => ({
        username: username,
        userId: userId,
        role: role,
        expDateAccessToken: expDate,
        auth: true,
        departmentId: departmentId,
        expDateAccessToken: expDate,
      }))
      localStorage.setItem('userId', userId)
      localStorage.setItem('accessToken', accessToken)
      localStorage.setItem('username', username)
      localStorage.setItem('role', role)

      localStorage.setItem('departmentId', departmentId)
      setTokenExpiry(expDate) // Cập nhật thời gian hết hạn token
    },
    [setUser, setTokenExpiry],
  )
  const logout = useCallback(() => {
    localStorage.removeItem('userId')
    localStorage.removeItem('accessToken')
    localStorage.removeItem('username')
    localStorage.removeItem('refreshToken')
    localStorage.removeItem('departmentId')
    localStorage.removeItem('role')

    setUser({
      username: '',
      userId: '',
      departmentId: '',
      role: '',
      expDateAccessToken: '',
      auth: false,
    })
    deleteAuthCookie()
    navigate('/homepage')
  }, [navigate])

  // const startAccessTokenTimer = (expDate) => {
  //   const currentTime = new Date().getTime()
  //   const timeUntilExpire = expDate - currentTime
  //   const tenMinutesInMilliseconds = 10 * 60 * 1000
  //   const refreshTime = new Date(localStorage.getItem('refreshTokenExpires'))
  //   const timestamp = refreshTime.getTime() - currentTime
  //   // const tenMinutesBeforeExpiration = expDate - 10 * 60 * 1000; // 10 minutes in milliseconds before expiration
  //   if (timestamp < 0) {
  //     logout()
  //   } else if (timeUntilExpire <= 0) {
  //     logout()
  //     //het han
  //   } else if (timeUntilExpire < tenMinutesInMilliseconds) {
  //     handleRefreshToken()
  //   }
  // }

  const {mutateAsync} = useRefreshToken({
    token: localStorage.getItem('refreshToken'),
  })

  const handleRefreshToken = useCallback(async () => {
    try {
      const refreshToken = localStorage.getItem('refreshToken')
      if (!refreshToken) throw new Error('No refresh token found')
      const data = await mutateAsync()

      const decodedToken = decodeJWT(data.data.accessToken)
      const newExpDate = decodedToken['exp'] * 1000
      localStorage.setItem('accessToken', data.data.accessToken)
      localStorage.setItem('refreshToken', data.data.refreshToken)
      setUser((prevUser) => ({
        ...prevUser,
        expDateAccessToken: newExpDate,
      }))
    } catch (err) {
      console.log('error')
      logout()
    }
  }, [logout, mutateAsync])

  useEffect(() => {
    const interval = setInterval(() => {
      if (tokenExpiry) {
        const currentTime = new Date().getTime()
        const timeUntilExpire = tokenExpiry - currentTime

        if (timeUntilExpire <= 0) {
          logout()
        } else if (timeUntilExpire < 10 * 60 * 1000) {
          // 10 phút trước khi hết hạn
          handleRefreshToken()
        }
      }
    }, 300000) // Kiểm tra mỗi 5 phút
    return () => clearInterval(interval) // Xóa interval khi component bị unmount
  }, [tokenExpiry, logout, handleRefreshToken])

  // Theo dõi sự thay đổi của user
  useEffect(() => {
    console.log('User state updated:', user)
  }, [user])

  // useEffect(() => {
  //   if (permissionsData) {
  //     setUser((prevUser) => ({
  //       ...prevUser,
  //       permissions: permissionsData.data.items,
  //     }))
  //   }
  // }, [permissionsData])

  // useEffect(() => {
  //   if (user.userId) {
  //     refetchPermissions()
  //   }
  // }, [user.userId, refetchPermissions])

  return (
    <UserContext.Provider value={{user, loginContext, logout}}>
      {children}
    </UserContext.Provider>
  )
}

export {UserContext, UserProvider}
// useEffect(() => {
//   const accessToken = localStorage.getItem('accessToken')
//   const username = localStorage.getItem('username')
//   if (accessToken && username) {
//     const decodedToken = decodeJWT(accessToken)
//     const expDate = decodedToken['exp'] * 1000
//     const currentTime = new Date().getTime()
//     const timeUntilExpire = expDate - currentTime
//     if(timeUntilExpire <= 0){
//       logout();
//     }else{
//       loginContext(username, accessToken)
//     }
//   }
// }, [localStorage.getItem('accessToken')])
