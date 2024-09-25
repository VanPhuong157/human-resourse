import React, {useContext, useEffect} from 'react'
import {useLocation} from 'react-router-dom'
import Header from './components/header/Header'
import SideBar from './components/sideBar/SideBar'
import Routers from './router/Routers'
import {ReactNotifications} from 'react-notifications-component'
import 'react-notifications-component/dist/theme.css'
import 'animate.css'
import './App.css'
import {Worker} from '@react-pdf-viewer/core'
import {UserContext} from './context/UserContext'
import {decodeJWT} from './assets/utils/TokenUtils'

function App() {
  const location = useLocation()
  const {loginContext, logout} = useContext(UserContext)

  // Khôi phục thông tin người dùng từ localStorage khi component được mount
  useEffect(() => {
    const accessToken = localStorage.getItem('accessToken')
    const username = localStorage.getItem('username')

    if (accessToken && username) {
      const decodedToken = decodeJWT(accessToken)
      const expDate = decodedToken['exp'] * 1000
      const currentTime = new Date().getTime()

      if (currentTime < expDate) {
        loginContext(username, accessToken)
      } else {
        logout() // Nếu token hết hạn, logout
      }
    }
  }, [loginContext, logout])
  // const show = location.pathname !== '/homepage'
  const show = ![
    '/homepage',
    '/homepage1',
    '/login',
    '/forgetPassword',
  ].includes(location.pathname)
  return (
    <>
      <ReactNotifications />
      <Worker workerUrl="https://unpkg.com/pdfjs-dist@3.4.120/build/pdf.worker.min.js">
        <div className="App">
          {show && <SideBar />}
          <main className="main">
            {show && <Header />}
            <div className="router">
              <Routers />
            </div>
          </main>
        </div>
      </Worker>
    </>
  )
}

export default App
