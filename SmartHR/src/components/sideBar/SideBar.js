import React, { useEffect, useState, useCallback, useContext } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { Sidebar, Menu, MenuItem, SubMenu } from 'react-pro-sidebar'
import MenuOutlinedIcon from '@mui/icons-material/MenuOutlined'
import GroupsIcon from '@mui/icons-material/Groups'
import GroupAddIcon from '@mui/icons-material/GroupAdd'
import ManageAccountsIcon from '@mui/icons-material/ManageAccounts'
import BubbleChartIcon from '@mui/icons-material/BubbleChart'
import HowToRegIcon from '@mui/icons-material/HowToReg'
import InsertChartIcon from '@mui/icons-material/InsertChart'
import NotificationsNoneIcon from '@mui/icons-material/NotificationsNone'
import Avatar from '@mui/material/Avatar'
import Badge from '@mui/material/Badge'
import Box from '@mui/material/Box'
import Typography from '@mui/material/Typography'
import IconButton from '@mui/material/IconButton'
import Tooltip from '@mui/material/Tooltip'
import MenuUI from '@mui/material/Menu'
import MenuItemUI from '@mui/material/MenuItem'
import { styled } from '@mui/material/styles'
import * as signalR from '@microsoft/signalr'
import axios from 'axios'
import Cookies from 'js-cookie'
import PlaylistAddCheckIcon from '@mui/icons-material/PlaylistAddCheck'
import TaskAltIcon from '@mui/icons-material/TaskAlt'
import AssignmentTurnedInIcon from '@mui/icons-material/AssignmentTurnedIn'
import CalendarTodayOutlinedIcon from '@mui/icons-material/CalendarTodayOutlined' // ← Thêm icon cho Schedule

import '../../assets/style/sidebar/SideBar.css'
import eventBus, { EVENT_BUS_KEY } from '../../services/eventBus'
import useGetUserInformation from '../../pages/setting/requests/getUserProfile'
import { baseUrl } from '../../api/rootApi'
import { UserContext } from '../../context/UserContext'

// ---------- styled ----------
const StyledLink = styled(Link)({
  textDecoration: 'none',
  color: 'inherit',
})

const StyledSidebar = styled(Sidebar)({
  color: 'white',
  overflowY: 'auto',
  backgroundColor: '#000000', // ← Sửa lỗi 'Geay' → đen chuẩn
})

const StyledSubMenu = styled(SubMenu)({
  '& .ps-menu-button': {
    backgroundColor: 'black',
    color: 'white',
    '&:hover': { color: 'black', backgroundColor: '#f1f5f9' },
  },
})

const StyledMenuItem = styled(MenuItem)({
  color: 'white',
  backgroundColor: 'black',
  '&:hover': { color: 'black', backgroundColor: '#f1f5f9' },
})

const HeaderArea = styled(Box)({
  position: 'sticky',
  top: 0,
  zIndex: 2,
  backgroundColor: 'black',
  borderBottom: '1px solid #1f2937',
})

const HeaderRow = styled(Box)({
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'space-between',
  padding: '12px 12px',
  gap: 8,
})

const UserRow = styled(Box)({
  display: 'flex',
  alignItems: 'center',
  gap: 8,
})

const NotificationMenu = styled(MenuUI)(({ theme }) => ({
  '& .MuiPaper-root': {
    width: 450,
    maxHeight: 500,
    borderRadius: 10,
    boxShadow: '0px 5px 15px rgba(0,0,0,0.1)',
    transform: 'translate(0px,10px) !important',
  },
}))

const NotificationMenuItem = styled(MenuItemUI)({
  padding: '16px 24px',
  borderBottom: '1px solid #f0f0f0',
  '&:last-child': { borderBottom: 'none' },
})

const NotificationTitle = styled(Typography)({
  fontWeight: 'bold',
  fontSize: 16,
  marginBottom: 8,
})

const NotificationText = styled(Typography)({ color: '#666', fontSize: 14 })
const NotificationTime = styled(Typography)({ color: '#999', fontSize: 12, marginLeft: 'auto', marginBottom: 30 })

// ---------- data ----------
const menuItems = [
  { icon: <InsertChartIcon />, label: 'DashBoard', to: 'dashboard' },
  { icon: <GroupsIcon />, label: 'User Management', to: 'user' },
  {
    icon: <AssignmentTurnedInIcon />,
    label: 'Task Management',
    subMenu: [
      { icon: <TaskAltIcon />, label: 'Daily Task', to: 'okr' },
      { icon: <PlaylistAddCheckIcon />, label: 'Project Task', to: 'okrequest' },
    ],
  },
  // ← Thêm mục Schedule ở đây – vị trí hợp lý, sau Task Management
  { icon: <CalendarTodayOutlinedIcon />, label: 'Lịch Làm Việc', to: 'schedule' },
  {
    icon: <ManageAccountsIcon />,
    label: 'Admin',
    subMenu: [
      { icon: <BubbleChartIcon />, label: 'Department', to: 'department' },
      { icon: <HowToRegIcon />, label: 'Role', to: 'role' },
      { icon: <GroupAddIcon />, label: 'User Group', to: 'userGroup' },
    ],
  },
]

const settings = ['Account', 'Dashboard', 'HomePage', 'Logout']

// ---------- component ----------
const SideBar = () => {
  const navigate = useNavigate()
  const { user, logout } = useContext(UserContext)

  const [collapsed, setCollapsed] = useState(false)
  const [permissionData, setPermissionData] = useState([])
  const hasEmployeeListPermission = permissionData.includes('Employee:List')
  const hasJobPostListPermission = permissionData.includes('JobPost:List')
  const hasCandidateListPermission = permissionData.includes('Candidate:List')
  const hasAdminListPermission = permissionData.includes('Admin')
  const hasOkrListPermission = permissionData.includes('Okr:List')
  const hasOkrRequestPermission = permissionData.includes('OkrRequest:List')

  const [anchorElUser, setAnchorElUser] = useState(null)
  const [anchorElNotification, setAnchorElNotification] = useState(null)
  const [notifications, setNotifications] = useState([])
  const [unreadCount, setUnreadCount] = useState(0)

  const userId = user?.userId
  const { data: userInfo } = useGetUserInformation({ userId })

  // ---- permissions via event bus ----
  useEffect(() => {
    const checkPermissionsSide = (permissions) => {
      setPermissionData(permissions)
    }
    eventBus.on(EVENT_BUS_KEY.DATA_PERMISSIONS, checkPermissionsSide)
    return () => {
      eventBus.off(EVENT_BUS_KEY.DATA_PERMISSIONS, checkPermissionsSide)
    }
  }, [])

  // ---- notifications (moved from Header) ----
  const fetchNotifications = useCallback(() => {
    if (!userId) return
    axios
      .get(baseUrl + `/api/Notifications?userId=${userId}`)
      .then((res) => {
        const list = res?.data ?? []
        setNotifications(list)
        setUnreadCount(list.filter((n) => !n.isRead).length)
      })
      .catch((err) => console.error('Error fetching notifications:', err))
  }, [userId])

  useEffect(() => {
    if (!userId) return
    fetchNotifications()

    const connection = new signalR.HubConnectionBuilder()
      .withUrl(baseUrl + '/notificationHub', {
        accessTokenFactory: () => localStorage.getItem('accessToken'),
        transport: signalR.HttpTransportType.WebSockets,
        skipNegotiation: true,
      })
      .build()

    connection.on('ReceiveNotification', (uid, message) => {
      if (userId === uid) {
        setNotifications((prev) => [message, ...prev])
        setUnreadCount((c) => c + 1)
      }
    })

    connection.start().catch((err) => console.error('SignalR failed: ', err))

    return () => { connection.stop() }
  }, [userId, fetchNotifications])

  const handleClickNotification = () => {
    axios
      .put(baseUrl + `/api/Notifications`)
      .then(() => {
        setUnreadCount(0)
        setNotifications((prev) => prev.map((n) => ({ ...n, isRead: true })))
      })
      .catch((err) => console.error('Error marking read:', err))
  }

  const handleCollapseSidebar = () => setCollapsed((v) => !v)
  const handleOpenUserMenu = (e) => setAnchorElUser(e.currentTarget)
  const handleCloseUserMenu = () => setAnchorElUser(null)
  const handleMenuOpenNotification = (e) => setAnchorElNotification(e.currentTarget)
  const handleMenuCloseNotification = () => setAnchorElNotification(null)
  const handleLogoutClick = () => logout()

  const userAuth = Cookies.get('userAuth')
  if (!userAuth) return null

  return (
    <StyledSidebar collapsed={collapsed}>
      {/* Header inside Sidebar */}
      <HeaderArea>
        <HeaderRow>
          <UserRow>
            <IconButton size="small" onClick={handleCollapseSidebar} sx={{ color: 'white' }}>
              <MenuOutlinedIcon />
            </IconButton>
          </UserRow>
          <UserRow>
            {user?.auth ? (
              <Typography sx={{ color: 'white', fontSize: 14 }}>
                <span style={{ color: '#d1d5db' }}> </span>
                <span style={{ color: '#60a5fa' }}>{userInfo?.data?.fullName}</span>
              </Typography>
            ) : (
              <Typography sx={{ color: 'white' }}>Anonymous</Typography>
            )}

            <Tooltip title="Notifications">
              <IconButton size="small" onClick={handleMenuOpenNotification} sx={{ color: 'white' }}>
                <Badge badgeContent={unreadCount} color="error">
                  <NotificationsNoneIcon />
                </Badge>
              </IconButton>
            </Tooltip>

            <Tooltip title="Account">
              <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                <Avatar alt="Profile Picture" src={baseUrl + `${userInfo?.data?.avatar}`} />
              </IconButton>
            </Tooltip>
          </UserRow>
        </HeaderRow>
      </HeaderArea>

      {/* Menus */}
      <Menu>
        {menuItems.map((item, index) => {
          if (item.subMenu) {
            const filteredSubMenu = item.subMenu.filter((subItem) => {
              return (
                (subItem.label === 'User Management' && hasEmployeeListPermission) ||
                (subItem.label === 'Candidate Management' && hasCandidateListPermission) ||
                (subItem.label === 'Job Post Management' && hasJobPostListPermission) ||
                (subItem.label === 'Department' && hasAdminListPermission) ||
                (subItem.label === 'Role' && hasAdminListPermission) ||
                (subItem.label === 'User Group' && hasAdminListPermission) ||
                (subItem.label === 'Home Page Setting' && hasAdminListPermission) ||
                (subItem.label === 'Daily Task' && hasOkrListPermission) ||
                (subItem.label === 'Home Page Reason' && hasAdminListPermission) ||
                (subItem.label === 'Project Task' && hasOkrRequestPermission)
              )
            })

            return (
              filteredSubMenu.length > 0 && (
                <StyledSubMenu key={index} icon={item.icon} label={item.label}>
                  {filteredSubMenu.map((subItem, subIndex) => (
                    <Tooltip title={subItem.label} key={subIndex} placement="right" arrow>
                      <StyledLink to={subItem.to}>
                        <StyledMenuItem icon={subItem.icon}>{subItem.label}</StyledMenuItem>
                      </StyledLink>
                    </Tooltip>
                  ))}
                </StyledSubMenu>
              )
            )
          }

          // Các menu đơn (không có subMenu)
          const hasPermission =
            item.label === 'DashBoard' ||
            (item.label === 'User Management' && hasEmployeeListPermission) ||
            item.label === 'Lịch Làm Việc' // ← Schedule luôn hiển thị (không cần permission riêng)

          return (
            hasPermission && (
              <StyledLink to={item.to} key={index}>
                <StyledMenuItem icon={item.icon}>{item.label}</StyledMenuItem>
              </StyledLink>
            )
          )
        })}
      </Menu>

      {/* Notification menu */}
      <NotificationMenu
        anchorEl={anchorElNotification}
        keepMounted
        open={Boolean(anchorElNotification)}
        onClick={handleClickNotification}
        onClose={handleMenuCloseNotification}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
      >
        <NotificationMenuItem>
          <NotificationTitle variant="h6">
            General <Badge badgeContent={unreadCount} color="primary" sx={{ ml: 3 }} />
          </NotificationTitle>
        </NotificationMenuItem>
        {notifications.length > 0 ? (
          notifications.map((notification, index) => (
            <NotificationMenuItem key={index} sx={{ opacity: notification.isRead ? 0.5 : 1 }}>
              <Box sx={{ display: 'flex', flexDirection: 'column', flexGrow: 1 }}>
                <NotificationTitle>New Notification</NotificationTitle>
                <NotificationText>{notification.message}</NotificationText>
              </Box>
              <NotificationTime>
                {new Date(notification.createdAt).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })} ago
              </NotificationTime>
            </NotificationMenuItem>
          ))
        ) : (
          <NotificationMenuItem onClick={handleMenuCloseNotification}>
            <NotificationText>No new notifications</NotificationText>
          </NotificationMenuItem>
        )}
      </NotificationMenu>

      {/* User menu */}
      <MenuUI
        sx={{ mt: '45px' }}
        id="menu-appbar"
        anchorEl={anchorElUser}
        keepMounted
        anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
        open={Boolean(anchorElUser)}
        onClose={handleCloseUserMenu}
      >
        {settings.map((setting) => (
          setting.toLowerCase() === 'logout' ? (
            <MenuItemUI key={setting} onClick={handleLogoutClick}>
              <Typography textAlign="center">{setting}</Typography>
            </MenuItemUI>
          ) : (
            <StyledLink to={setting.toLowerCase()} key={setting}>
              <MenuItemUI onClick={handleCloseUserMenu}>
                <Typography textAlign="center">{setting}</Typography>
              </MenuItemUI>
            </StyledLink>
          )
        ))}
      </MenuUI>
    </StyledSidebar>
  )
}

export default SideBar