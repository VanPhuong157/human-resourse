import React, {useEffect, useState, useContext} from 'react'
import {useNavigate} from 'react-router-dom'
import {baseUrl} from '../../api/rootApi'
import {Link} from 'react-router-dom'
import AppBar from '@mui/material/AppBar'
import Box from '@mui/material/Box'
import Toolbar from '@mui/material/Toolbar'
import IconButton from '@mui/material/IconButton'
import Typography from '@mui/material/Typography'
import Menu from '@mui/material/Menu'
import MenuIcon from '@mui/icons-material/Menu'
import Container from '@mui/material/Container'
import Avatar from '@mui/material/Avatar'
import Button from '@mui/material/Button'
import Tooltip from '@mui/material/Tooltip'
import MenuItem from '@mui/material/MenuItem'
import Badge from '@mui/material/Badge'
import NotificationsNoneIcon from '@mui/icons-material/NotificationsNone'
import {styled} from '@mui/material/styles'
import * as signalR from '@microsoft/signalr'
import axios from 'axios'
import {UserContext} from '../../context/UserContext'
import Cookies from 'js-cookie'
import {useCallback} from 'react'
// import {deleteAuthCookie} from '../../components/utils'
import useGetUserInformation from '../../pages/setting/requests/getUserProfile'
const NotificationMenu = styled(Menu)(({theme}) => ({
  '& .MuiPaper-root': {
    width: '450px',
    maxHeight: '500px',
    borderRadius: '10px',
    boxShadow: '0px 5px 15px rgba(0, 0, 0, 0.1)',
    transform: `translate(0px,10px) !important`,
  },
}))

const NotificationMenuItem = styled(MenuItem)(({theme}) => ({
  padding: '16px 24px',
  borderBottom: '1px solid #f0f0f0',
  '&:last-child': {
    borderBottom: 'none',
  },
}))

const NotificationTitle = styled(Typography)(({theme}) => ({
  fontWeight: 'bold',
  fontSize: '16px',
  marginBottom: '8px',
}))

const NotificationText = styled(Typography)(({theme}) => ({
  color: '#666',
  fontSize: '14px',
}))

const NotificationTime = styled(Typography)(({theme}) => ({
  color: '#999',
  fontSize: '12px',
  marginLeft: 'auto',
  marginBottom: '30px',
}))

const StyledContainer = styled(Container)({
  width: '100%',
  backgroundColor: '#F4F5FA',
})

const LogoTypography = styled(Typography)({
  mr: 2,
  fontFamily: 'monospace',
  fontWeight: 700,
  letterSpacing: '.3rem',
  color: 'inherit',
  textDecoration: 'none',
})

const CustomLink = styled(Link)({
  textDecoration: 'none',
  color: 'inherit',
})

const pages = ['Products', 'Pricing', 'Blog']
const settings = ['Account', 'Dashboard', 'HomePage', 'Logout']

function Header() {
  const navigate = useNavigate()
  const {user, logout} = useContext(UserContext)
  const [anchorElNav, setAnchorElNav] = useState(null)
  const [anchorElUser, setAnchorElUser] = useState(null)
  const [anchorElNotification, setAnchorElNotification] = useState(null)
  const [notifications, setNotifications] = useState([])
  const [unreadCount, setUnreadCount] = useState(0)
  const userId = user?.userId

  const {data: userInfo} = useGetUserInformation({
    userId: userId,
  })

  const fetchNotifications = useCallback(() => {
    if (!user?.userId) return

    axios
      .get(baseUrl + `/api/Notifications?userId=${user?.userId}`)
      .then((response) => {
        console.log('data_noti', response?.data)
        setNotifications(response.data)
        const unreadCount = response.data.filter(
          (notification) => !notification.isRead,
        ).length
        setUnreadCount(unreadCount)
      })
      .catch((error) => {
        console.error('Error fetching notifications:', error)
      })
  }, [user?.userId])

  useEffect(() => {
    if (!user?.userId) return

    // Fetch initial notifications via API
    fetchNotifications()

    // Setup SignalR connection for real-time updates
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(baseUrl + '/notificationHub', {
        accessTokenFactory: () => localStorage.getItem('accessToken'), // Use access token if needed
        transport: signalR.HttpTransportType.WebSockets,
        skipNegotiation: true, // Skip negotiation since it's done by API endpoint
        logger: signalR.LogLevel.Information,
      })
      .build()

    connection.on('ReceiveNotification', (userId, message) => {
      if (user?.userId === userId) {
        setNotifications((prevNotifications) => [message, ...prevNotifications])
        setUnreadCount((prevCount) => prevCount + 1)
      }
    })

    connection
      .start()
      .then(() => console.log('Connected to SignalR'))
      .catch((err) => console.error('Connection failed: ', err))

    connection.onclose((error) => {
      console.error('Connection closed: ', error)
    })

    connection.onreconnecting((error) => {
      console.warn('Reconnecting: ', error)
    })

    connection.onreconnected((connectionId) => {
      console.log('Reconnected: ', connectionId)
    })

    // Polling API every 5 seconds

    // Cleanup connection and interval on component unmount
    return () => {
      connection.stop()
    }
  }, [user?.userId, fetchNotifications])

  const handleMenuOpenNotification = (event) => {
    setAnchorElNotification(event.currentTarget)
  }

  const handleMenuCloseNotification = () => {
    setAnchorElNotification(null)
  }
  const handleClickNotification = () => {
    // Kiểm tra nếu isRead là false
    axios
      .put(baseUrl + `/api/Notifications`)
      .then((response) => {
        setUnreadCount(0) // Giảm số lượng thông báo chưa đọc
        setNotifications((prevNotifications) =>
          prevNotifications.map((notification) => ({
            ...notification,
            isRead: true,
          })),
        )
      })
      .catch((error) => {
        console.error('Error marking notification as read:', error)
      })
  }

  const handleOpenNavMenu = (event) => {
    setAnchorElNav(event.currentTarget)
  }
  const handleOpenUserMenu = (event) => {
    setAnchorElUser(event.currentTarget)
  }

  const handleCloseNavMenu = () => {
    setAnchorElNav(null)
  }

  const handleCloseUserMenu = () => {
    setAnchorElUser(null)
  }

  const handleLogoutClick = () => {
    logout()
  }

  const userAuth = Cookies.get('userAuth')
  if (!userAuth) return

  return (
    <AppBar position="static">
      <StyledContainer maxWidth="">
        <Toolbar disableGutters>
          <Box sx={{flexGrow: 1, display: {xs: 'flex', md: 'none'}}}>
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={handleOpenNavMenu}
              color="inherit"
            >
              <MenuIcon />
            </IconButton>
            <Menu
              id="menu-appbar"
              anchorEl={anchorElNav}
              anchorOrigin={{
                vertical: 'bottom',
                horizontal: 'left',
              }}
              keepMounted
              transformOrigin={{
                vertical: 'top',
                horizontal: 'left',
              }}
              open={Boolean(anchorElNav)}
              onClose={handleCloseNavMenu}
              sx={{
                display: {xs: 'block', md: 'none'},
              }}
            ></Menu>
          </Box>
          <Box sx={{flexGrow: 1, display: {xs: 'none', md: 'flex'}}}></Box>
          <Box
            sx={{
              flexGrow: 0,
              display: 'flex',
              alignItems: 'center',
              gap: '5px',
            }}
          >
            {user && user.auth === true ? (
              <Typography>
                <span style={{color: 'black'}}>Hi </span>
                <span style={{color: 'royalblue'}}>
                  {userInfo?.data.fullName}!
                </span>
              </Typography>
            ) : (
              <Typography sx={{color: 'black'}}>Anonymous</Typography>
            )}
            <IconButton
              size="large"
              aria-label="show new notifications"
              color="black"
              onClick={handleMenuOpenNotification}
            >
              <Badge badgeContent={unreadCount} color="error">
                <NotificationsNoneIcon />
              </Badge>
            </IconButton>
            <NotificationMenu
              anchorEl={anchorElNotification}
              keepMounted
              open={Boolean(anchorElNotification)}
              onClick={handleClickNotification}
              onClose={handleMenuCloseNotification}
              anchorOrigin={{
                vertical: 'bottom',
                horizontal: 'right',
              }}
              transformOrigin={{
                vertical: 'top',
                horizontal: 'right',
              }}
            >
              <NotificationMenuItem>
                <NotificationTitle variant="h6">
                  General
                  <Badge
                    badgeContent={unreadCount}
                    color="primary"
                    sx={{marginLeft: 3}}
                  />
                </NotificationTitle>
              </NotificationMenuItem>
              {notifications.length > 0 ? (
                notifications.map((notification, index) => (
                  <NotificationMenuItem
                    key={index}
                    sx={{
                      opacity: notification.isRead ? 0.5 : 1, // Làm mờ nếu isRead là false
                    }}
                  >
                    <Box
                      sx={{
                        display: 'flex',
                        flexDirection: 'column',
                        flexGrow: 1,
                      }}
                    >
                      <NotificationTitle>New Notification</NotificationTitle>
                      <NotificationText>
                        {notification.message}
                      </NotificationText>
                    </Box>
                    <NotificationTime>
                      {new Date(notification.createdAt).toLocaleTimeString([], {
                        hour: '2-digit',
                        minute: '2-digit',
                      })}{' '}
                      ago
                    </NotificationTime>
                  </NotificationMenuItem>
                ))
              ) : (
                <NotificationMenuItem onClick={handleMenuCloseNotification}>
                  <NotificationText>No new notifications</NotificationText>
                </NotificationMenuItem>
              )}
            </NotificationMenu>
          </Box>

          <Box>
            <Tooltip title="Open settings">
              <IconButton onClick={handleOpenUserMenu} sx={{p: 0}}>
                <Avatar
                  alt="Profile Picture"
                  src={baseUrl + `${userInfo?.data?.avatar}`}
                />
              </IconButton>
            </Tooltip>
            <Menu
              sx={{mt: '45px'}}
              id="menu-appbar"
              anchorEl={anchorElUser}
              keepMounted
              anchorOrigin={{
                vertical: 'top',
                horizontal: 'right',
              }}
              transformOrigin={{
                vertical: 'top',
                horizontal: 'right',
              }}
              open={Boolean(anchorElUser)}
              onClose={handleCloseUserMenu}
            >
              {settings.map((setting) => {
                if (setting.toLowerCase() === 'logout') {
                  return (
                    <MenuItem key={setting} onClick={handleLogoutClick}>
                      <Typography textAlign="center">{setting}</Typography>
                    </MenuItem>
                  )
                } else {
                  return (
                    <CustomLink to={setting.toLowerCase()} key={setting}>
                      <MenuItem onClick={handleCloseUserMenu}>
                        <Typography textAlign="center">{setting}</Typography>
                      </MenuItem>
                    </CustomLink>
                  )
                }
              })}
            </Menu>
          </Box>
        </Toolbar>
      </StyledContainer>
    </AppBar>
  )
}

export default Header
