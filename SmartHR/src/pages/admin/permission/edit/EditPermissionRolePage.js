import React, { useState, useEffect, useCallback } from 'react'
import {
  Box,
  Paper,
  Button,
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Checkbox,
} from '@mui/material'
import { showSuccess, showError } from '../../../../components/notification'
import { useNavigate, useParams } from 'react-router-dom'
import useGetPermissions from '../../requests/getPermission'
import AddIcon from '@mui/icons-material/Add'
import useGetRolePermissions from '../../requests/getRolePermissions'
import { useEditRolePermissions } from '../../requests/editRolePermissions'
import FilterListIcon from '@mui/icons-material/FilterList'
import FormDialog from '../../../../components/formDialog/FormDialog'
import FilterPermission from './FilterPermission'

const EditPermissionRolePage = () => {
  const navigate = useNavigate()
  const { roleId } = useParams()
  
  const [permissions, setPermissions] = useState([])
  const [selectedIds, setSelectedIds] = useState([])
  const [dataFilter, setDataFilter] = useState('')
  const [openFilterPermission, setOpenFilterPermission] = useState(false)

  const { refetch } = useGetPermissions(1, 1000, dataFilter)
  const { data: rolePermissions, refetch: refetchRolePermissions } =
    useGetRolePermissions({
      roleId: roleId,
      pageIndex: 1,
      pageSize: 1000,
    })

  useEffect(() => {
    if (rolePermissions?.data?.items) {
      const ids = rolePermissions.data.items.map((item) => item.id)
      setSelectedIds(ids)
    }
  }, [rolePermissions])

  const fetchData = useCallback(async () => {
    try {
      const response = await refetch()
      if (response && response.data?.data?.items) {
        setPermissions(response.data.data.items)
      }
    } catch (error) {
      console.error('Error fetching permissions:', error)
      showError({ message: 'Cannot load permissions list' })
    }
  }, [refetch])

  useEffect(() => {
    fetchData()
  }, [fetchData, dataFilter])

  const { mutateAsync: editRolePermissionAsync } = useEditRolePermissions({
    roleId,
  })

  const handleSelectOne = (id) => {
    const selectedIndex = selectedIds.indexOf(id)
    let newSelected = []

    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selectedIds, id)
    } else {
      newSelected = selectedIds.filter(item => item !== id)
    }
    setSelectedIds(newSelected)
  }

  const handleSelectAll = (event) => {
    if (event.target.checked) {
      const newSelecteds = permissions.map((n) => n.id)
      setSelectedIds(newSelecteds)
      return
    }
    setSelectedIds([])
  }

  const onSave = () => {
    editRolePermissionAsync(selectedIds)
      .then((response) => {
        showSuccess({ message: response.data?.message || 'Update successfully!' })
        refetchRolePermissions()
      })
      .catch((err) => {
        showError({ message: err.response?.data?.message || 'Update failed' })
      })
  }

  return (
    /* Paper ngoài cùng: m: 0 và width: 100% để tràn màn hình */
    <Paper sx={{ width: '100%', minHeight: '90vh', boxShadow: 'none', backgroundColor: '#f4f6f8', p: 2 }}>
      
      {/* Header Container */}
      <Box sx={{ 
          display: 'flex', 
          justifyContent: 'space-between', 
          alignItems: 'center', 
          p: 2, 
          bgcolor: 'white', 
          borderRadius: '8px',
          mb: 2,
          boxShadow: '0px 2px 4px rgba(0,0,0,0.05)'
      }}>
        <Typography variant="h5" sx={{ fontWeight: 'bold', color: '#1976d2' }}>
          Role Permission Management
        </Typography>
        <Box>
          <Button
            startIcon={<FilterListIcon />}
            sx={{ textTransform: 'none', color: 'black', mr: 2 }}
            onClick={() => setOpenFilterPermission(true)}
          >
            Filter
          </Button>
          <Button
            variant="contained"
            startIcon={<AddIcon />}
            sx={{ textTransform: 'none', px: 3, borderRadius: '6px' }}
            onClick={onSave}
          >
            Save Changes
          </Button>
        </Box>
      </Box>

      {/* Table Container: Chiếm hết chiều cao còn lại và có thanh cuộn dọc */}
      <TableContainer component={Paper} sx={{ 
          width: '100%', 
          maxHeight: 'calc(100vh - 200px)', // Tự động tính toán chiều cao trừ đi phần Header
          borderRadius: '8px',
          boxShadow: '0px 4px 10px rgba(0,0,0,0.05)'
      }}>
        <Table stickyHeader aria-label="sticky table">
          <TableHead>
            <TableRow>
              <TableCell padding="checkbox" sx={{ bgcolor: '#f8f9fa', width: '50px' }}>
                <Checkbox
                  indeterminate={selectedIds.length > 0 && selectedIds.length < permissions.length}
                  checked={permissions.length > 0 && selectedIds.length === permissions.length}
                  onChange={handleSelectAll}
                  color="primary"
                />
              </TableCell>
              <TableCell sx={{ bgcolor: '#f8f9fa', fontWeight: 'bold', fontSize: '1rem' }}>
                Permission Name
              </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {permissions.map((row) => {
              const isItemSelected = selectedIds.indexOf(row.id) !== -1
              return (
                <TableRow
                  hover
                  key={row.id}
                  onClick={() => handleSelectOne(row.id)}
                  role="checkbox"
                  selected={isItemSelected}
                  sx={{ cursor: 'pointer', '&.Mui-selected': { bgcolor: '#e3f2fd' } }}
                >
                  <TableCell padding="checkbox">
                    <Checkbox checked={isItemSelected} color="primary" />
                  </TableCell>
                  <TableCell sx={{ fontSize: '0.95rem' }}>
                    {row.name}
                  </TableCell>
                </TableRow>
              )
            })}
          </TableBody>
        </Table>
      </TableContainer>

      <FormDialog
        open={openFilterPermission}
        onCancel={() => setOpenFilterPermission(false)}
        onConfirm={(formData) => {
          setDataFilter(formData)
          setOpenFilterPermission(false)
        }}
        title="Filter Permission"
        actionName="Filter"
        dialogContent={<FilterPermission data={dataFilter} />}
      />
    </Paper>
  )
}

export default EditPermissionRolePage