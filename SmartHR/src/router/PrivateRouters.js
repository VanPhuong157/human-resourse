import {Outlet, Navigate, useLocation} from 'react-router-dom'
import Cookies from 'js-cookie'
import {useEffect} from 'react'
// import {UserContext} from 'context/UserContext'
import useGetUserPermissions from '../pages/admin/requests/getUserPermissions'
import {useState} from 'react'
import ForbiddenPage from '../pages/errorPage/ForbiddenPage '
import eventBus, {EVENT_BUS_KEY} from '../services/eventBus'

const PrivateRoutes = ({requiredPermissions}) => {
  const location = useLocation()
  // const {user} = useContext(UserContext)
  const userAuth = Cookies.get('userAuth')
  const [permissions, setPermissions] = useState([])
  const [canNavigate, setCanNavigate] = useState(true)
  const [loadingPermissions, setLoadingPermissions] = useState(true)
  const [isCheckAuth, setIsCheckAuth] = useState(false)
  const [isRender, setIsRender] = useState(false)

  const userId = localStorage.getItem('userId')
  const {data: permissionsData} = useGetUserPermissions({
    userId,
    pageIndex: 1,
    pageSize: 1000,
  })
  useEffect(() => {
    if (userAuth) {
      if (permissionsData) {
        setIsCheckAuth(true)
        const newPerm = permissionsData?.data?.items.map((perm) => perm.name)
        setPermissions(newPerm)
        eventBus.emit(EVENT_BUS_KEY.DATA_PERMISSIONS, newPerm)
        setLoadingPermissions(false) // Đặt loadingPermissions thành false khi có dữ liệu
      } else {
        setPermissions([])
      }
    } else {
      setLoadingPermissions(false) // Đặt loadingPermissions thành false khi có dữ liệu
    }
  }, [permissionsData, location.pathname, userAuth])

  useEffect(() => {
    // Kiểm tra quyền mỗi khi đường dẫn thay đổi
    if (!loadingPermissions) {
      if (requiredPermissions && requiredPermissions.length > 0) {
        const hasRequiredPermissions = requiredPermissions.every((permission) =>
          permissions.includes(permission),
        )
        if (hasRequiredPermissions) {
          setIsRender(true)
        } else {
          setIsRender(false)
        }
        setCanNavigate(hasRequiredPermissions)
      } else {
        setCanNavigate(true)
      }
    }
  }, [
    loadingPermissions,
    location.pathname,
    requiredPermissions,
    permissions,
    canNavigate,
  ])

  if (loadingPermissions) {
    return <div>Loading...</div>
  }
  if (!userAuth && !isCheckAuth && !canNavigate) {
    return <Navigate to="/login" replace />
  }

  if (!canNavigate) {
    return <ForbiddenPage />
  }
  if (isRender) {
    return <Outlet />
  }
}

export default PrivateRoutes
