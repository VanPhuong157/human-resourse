import React from 'react'
import {
  Box,
  Button,
  Select,
  MenuItem,
  Typography,
  CircularProgress,
} from '@mui/material'
import FilterListIcon from '@mui/icons-material/FilterList'
import AddIcon from '@mui/icons-material/Add'
import {useEffect} from 'react'
import {useState} from 'react'
import useGetUserPermissions from '../../../pages/admin/requests/getUserPermissions'

const HeaderOkr = ({
  selectedDepartment,
  handleDepartmentChange,
  departmentsData,
  isLoading,
  handleClickFilter,
  handleClickAdd,
  isOkr,
}) => {
  const [permissionData, setPermissionData] = useState([])
  const hasOkrCreatePermission = permissionData.includes('OkrRequest:Create')
  const [loading, setLoading] = useState(true)
  const userId = localStorage.getItem('userId')
  const role = localStorage.getItem('role')
  const {data: permissions} = useGetUserPermissions({
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
      <Box
        sx={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          height: '100vh',
        }}
      >
        <CircularProgress />
      </Box>
    )
  }
  return (
    <Box className="header" sx={{padding: 5}}>
      <Box display={'flex'}>
        <Box className="header-left">
          <Typography variant="h4" sx={{fontWeight: 'bold', color: '#1976d2'}}>
            {isOkr ? 'Okr Management' : 'Okr Request Management'}
          </Typography>
        </Box>
      </Box>
      <Box className="header-buttons">
        {role === 'BOD' && (
          <Select
            value={selectedDepartment}
            onChange={handleDepartmentChange}
            sx={{
              width: '160px',
              '& .MuiOutlinedInput-notchedOutline': {
                border: 'none',
              },
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
          className="header-button"
          startIcon={<FilterListIcon />}
          sx={{textTransform: 'none', color: 'black'}}
          onClick={handleClickFilter}
        >
          Filter
        </Button>
        {hasOkrCreatePermission && (
          <Button
            className="header-button"
            variant="contained"
            startIcon={<AddIcon />}
            sx={{
              textTransform: 'none',
              borderColor: 'gray',
              borderRadius: '15px solid gray',
              color: 'white',
              ml: '8px',
            }}
            onClick={handleClickAdd}
          >
            Add Okr Request
          </Button>
        )}
      </Box>
    </Box>
  )
}

export default HeaderOkr
