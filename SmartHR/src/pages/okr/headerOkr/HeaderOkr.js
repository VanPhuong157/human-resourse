import React, { useEffect, useState } from 'react'
import {
  Box,
  Button,
  Select,
  MenuItem,
  CircularProgress,
} from '@mui/material'
import FilterListIcon from '@mui/icons-material/FilterList'
import AddIcon from '@mui/icons-material/Add'
import GridViewIcon from '@mui/icons-material/GridView'
import FastForwardIcon from '@mui/icons-material/FastForward'
import CheckCircleIcon from '@mui/icons-material/CheckCircle'
import PauseIcon from '@mui/icons-material/Pause'
import ArchiveOutlinedIcon from '@mui/icons-material/ArchiveOutlined'
import useGetUserPermissions from '../../../pages/admin/requests/getUserPermissions'

const HeaderOkr = ({
  selectedDepartment,
  handleDepartmentChange,
  departmentsData,
  isLoading,
  handleClickFilter,
  handleClickAdd,
  isOkr,
  activeTab = 0,
  onTabChange,
  tabLabels = [],
}) => {
  const [permissionData, setPermissionData] = useState([])
  const hasOkrCreatePermission = permissionData.includes('OkrRequest:Create')
  const [loading, setLoading] = useState(true)
  const userId = localStorage.getItem('userId')
  const role = localStorage.getItem('role')
  const { data: permissions } = useGetUserPermissions({
    userId,
    pageIndex: 1,
    pageSize: 1000,
  })

  useEffect(() => {
    if (permissions) {
      const newPerm = permissions?.data?.items.map((perm) => perm.name)
      setPermissionData(newPerm)
      setLoading(false)
    }
  }, [permissions])

  if (loading) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
        <CircularProgress />
      </Box>
    )
  }

  // icon cho từng tab theo thứ tự labels
  const icons = [
    <GridViewIcon fontSize="small" />,
    <FastForwardIcon fontSize="small" />,
    <CheckCircleIcon fontSize="small" />,
    <CheckCircleIcon fontSize="small" sx={{ opacity: .6 }} />,
    <PauseIcon fontSize="small" />,
    <ArchiveOutlinedIcon fontSize="small" />,
    <GridViewIcon fontSize="small" />,
    <GridViewIcon fontSize="small" />,
    <FastForwardIcon fontSize="small" />,
  ]

  return (
    <Box sx={{
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'space-between',
      mb: 2,
    }}>
      {/* Tabs bên trái */}
      <Box sx={{ display: 'flex', alignItems: 'center', gap: 1.5, flexWrap: 'wrap' }}>
        {tabLabels.map((label, idx) => (
          <Button
            key={label}
            startIcon={icons[idx]}
            onClick={() => onTabChange?.(idx)}
            sx={{
              px: 1.5,
              py: 0.5,
              borderRadius: 999,
              fontSize: 14,
              textTransform: 'none',
              bgcolor: activeTab === idx ? '#EFEFEF' : 'transparent', // pill xám nhạt khi active
              color: '#444',                                          // màu xám đậm (không xanh)
              '&:hover': { bgcolor: '#E8E8E8' },
            }}
          >
            {label}
          </Button>
        ))}
      </Box>

      {/* Bộ lọc & nút hành động bên phải */}
      <Box sx={{ display: 'flex', alignItems: 'center', gap: 1.5 }}>
        {role === 'BOD' && (
          <Select
            value={selectedDepartment || 'All Department'}
            onChange={handleDepartmentChange}
            sx={{
              width: 160,
              height: 34,
              '& .MuiOutlinedInput-notchedOutline': { border: 'none' },
              bgcolor: '#F4F4F5',
              borderRadius: 999,
              fontSize: 14,
            }}
          >
            <MenuItem value="All Department">All Department</MenuItem>
            {!isLoading &&
              Array.isArray(departmentsData?.data?.items) &&
              departmentsData.data.items.map((department) => (
                <MenuItem key={department.id} value={department.id}>
                  {department.name}
                </MenuItem>
              ))}
          </Select>
        )}

        <Button
          startIcon={<FilterListIcon />}
          sx={{ textTransform: 'none', color: '#333', minWidth: 0, px: 1 }}
          onClick={handleClickFilter}
        >
          Filter
        </Button>

        {hasOkrCreatePermission && (
          <Button
            variant="contained"
            startIcon={<AddIcon />}
            sx={{
              textTransform: 'none',
              borderRadius: 999,
              px: 1.75,
              py: 0.5,
            }}
            onClick={handleClickAdd}
          >
            Add Okr
          </Button>
        )}
      </Box>
    </Box>
  )
}

export default HeaderOkr
