import React, {useEffect, useState} from 'react'
import {Sidebar, Menu, MenuItem, SubMenu} from 'react-pro-sidebar'
import MenuOutlinedIcon from '@mui/icons-material/MenuOutlined'
import AssignmentIndIcon from '@mui/icons-material/AssignmentInd'
import GroupsIcon from '@mui/icons-material/Groups'
import QueryStatsIcon from '@mui/icons-material/QueryStats'
import InsightsIcon from '@mui/icons-material/Insights'
import TimelineIcon from '@mui/icons-material/Timeline'
import GroupAddIcon from '@mui/icons-material/GroupAdd'
import AddHomeWorkIcon from '@mui/icons-material/AddHomeWork'
import HomeWorkIcon from '@mui/icons-material/HomeWork'
import ManageAccountsIcon from '@mui/icons-material/ManageAccounts'
import BubbleChartIcon from '@mui/icons-material/BubbleChart'
import HowToRegIcon from '@mui/icons-material/HowToReg'
import PersonSearchIcon from '@mui/icons-material/PersonSearch'
import DescriptionIcon from '@mui/icons-material/Description'
import InsertChartIcon from '@mui/icons-material/InsertChart'
import {Link} from 'react-router-dom'
import {styled} from '@mui/material/styles'
import '../../assets/style/sidebar/SideBar.css'
import Cookies from 'js-cookie'
import eventBus, {EVENT_BUS_KEY} from '../../services/eventBus'
import {Tooltip} from '@mui/material'
const StyledLink = styled(Link)({
  textDecoration: 'none',
  color: 'inherit',
})

const StyledSidebar = styled(Sidebar)({
  color: 'white',
  overflowY: 'auto',
})

const StyledSubMenu = styled(SubMenu)({
  '& .ps-menu-button': {
    backgroundColor: 'black',
    color: 'white',
    '&:hover': {
      color: 'black',
    },
  },
})

const StyledMenuItem = styled(MenuItem)({
  color: 'white',
  backgroundColor: 'black',
  '&:hover': {
    color: 'black',
  },
})

const menuItems = [
  {icon: <InsertChartIcon />, label: 'DashBoard', to: 'dashboard'},
  {icon: <GroupsIcon />, label: 'User Management', to: 'user'},
  {
    icon: <AssignmentIndIcon />,
    label: 'Recruitment Management',
    subMenu: [
      {
        icon: <PersonSearchIcon />,
        label: 'Candidate Management',
        to: 'candidate',
      },
      {
        icon: <DescriptionIcon />,
        label: 'Job Post Management',
        to: 'jobpost',
      },
    ],
  },
  {
    icon: <QueryStatsIcon />,
    label: 'Okr Management',
    subMenu: [
      {
        icon: <InsightsIcon />,
        label: 'Okr Public Management',
        to: 'okr',
      },
      {
        icon: <TimelineIcon />,
        label: 'Okr Request Management',
        to: 'okrequest',
      },
    ],
  },
  {
    icon: <ManageAccountsIcon />,
    label: 'Admin',
    subMenu: [
      {
        icon: <BubbleChartIcon />,
        label: 'Department',
        to: 'department',
      },
      {
        icon: <HowToRegIcon />,
        label: 'Role',
        to: 'role',
      },
      {
        icon: <GroupAddIcon />,
        label: 'User Group',
        to: 'userGroup',
      },
      {
        icon: <AddHomeWorkIcon />,
        label: 'Home Page Setting',
        to: 'homepagesetting',
      },
      {
        icon: <HomeWorkIcon />,
        label: 'Home Page Reason',
        to: 'homepagereason',
      },
    ],
  },
]

const SideBar = () => {
  const [collapsed, setCollapsed] = useState(false)
  const [permissionData, setPermissionData] = useState([])
  const hasEmployeeListPermission = permissionData.includes('Employee:List')
  const hasJobPostListPermission = permissionData.includes('JobPost:List')
  const hasCandidateListPermission = permissionData.includes('Candidate:List')
  const hasAdminListPermission = permissionData.includes('Admin')
  const hasOkrListPermission = permissionData.includes('Okr:List')
  const hasOkrRequestPermission = permissionData.includes('OkrRequest:List')
  const handleCollapseSidebar = () => {
    setCollapsed(!collapsed)
  }
  useEffect(() => {
    const checkPermissionsSide = (permissions) => {
      permissions.current = permissions
      setPermissionData(permissions)
    }
    eventBus.on(EVENT_BUS_KEY.DATA_PERMISSIONS, checkPermissionsSide)
    return () => {
      eventBus.off(EVENT_BUS_KEY.DATA_PERMISSIONS, checkPermissionsSide)
    }
  }, [])
  const userAuth = Cookies.get('userAuth')
  if (!userAuth) return

  return (
    <StyledSidebar collapsed={collapsed}>
      <Menu>
        <StyledMenuItem
          icon={<MenuOutlinedIcon />}
          onClick={handleCollapseSidebar}
        >
          <h2 style={{color: 'white'}}>SHRS</h2>
        </StyledMenuItem>
        {menuItems.map((item, index) => {
          if (item.subMenu) {
            const filteredSubMenu = item.subMenu.filter((subItem) => {
              // Kiểm tra quyền cho từng mục con
              return (
                (subItem.label === 'User Management' &&
                  hasEmployeeListPermission) ||
                (subItem.label === 'Candidate Management' &&
                  hasCandidateListPermission) ||
                (subItem.label === 'Job Post Management' &&
                  hasJobPostListPermission) ||
                (subItem.label === 'Department' && hasAdminListPermission) ||
                (subItem.label === 'Role' && hasAdminListPermission) ||
                (subItem.label === 'User Group' && hasAdminListPermission) ||
                (subItem.label === 'Home Page Setting' &&
                  hasAdminListPermission) ||
                (subItem.label === 'Okr Public Management' &&
                  hasOkrListPermission) ||
                (subItem.label === 'Home Page Reason' &&
                  hasAdminListPermission) ||
                (subItem.label === 'Okr Request Management' &&
                  hasOkrRequestPermission)
              )
            })

            return (
              filteredSubMenu.length > 0 && (
                <StyledSubMenu key={index} icon={item.icon} label={item.label}>
                  {filteredSubMenu.map((subItem, subIndex) => (
                    <Tooltip
                      title={subItem.label}
                      key={subIndex}
                      placement="right"
                      arrow
                    >
                      <StyledLink to={subItem.to}>
                        <StyledMenuItem icon={subItem.icon}>
                          {subItem.label}
                        </StyledMenuItem>
                      </StyledLink>
                    </Tooltip>
                  ))}
                </StyledSubMenu>
              )
            )
          }

          // Kiểm tra quyền cho mục menu không có submenu
          const hasPermission =
            item.label === 'DashBoard' ||
            (item.label === 'User Management' && hasEmployeeListPermission)

          return (
            hasPermission && (
              <StyledLink to={item.to} key={index}>
                <StyledMenuItem icon={item.icon}>{item.label}</StyledMenuItem>
              </StyledLink>
            )
          )
        })}
      </Menu>
    </StyledSidebar>
  )
}

export default SideBar
