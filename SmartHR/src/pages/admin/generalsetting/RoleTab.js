import ComponentTable from '../../../components/table'
import React, {useState, useEffect, useCallback} from 'react'
import Box from '@mui/material/Box'
import Paper from '@mui/material/Paper'
import {Typography, Button} from '@mui/material'
import AddIcon from '@mui/icons-material/Add'
import FormDialog from '../../../components/formDialog/FormDialog'
import AddRoleForm from '../generalsetting/create/AddRoleForm'
import useGetRoles from '../../../pages/role/request/getRoles'
import {useAddRole} from '../../../pages/role/request/addRole'
import {showSuccess, showError} from '../../../components/notification'
import {useNavigate} from 'react-router-dom'
import {useDeleteRole} from '../../../pages/role/request/deleteRole'
import ConfirmDialog from '../../../components/confirmDialog'
import {StatusCodes} from 'http-status-codes'

const columns = [
  {
    id: 'name',
    label: 'RoleName',
    width: '25%',
  },
  {
    id: 'createdAt',
    label: 'CreateAt',
    width: '25%',
    formatDatetime: 'dd-MM-yyyy',
  },
  {
    id: 'description',
    label: 'Description',
    width: '25%',
  },
  {
    id: 'type',
    label: 'Type',
    width: '25%',
  },
  {
    id: 'action',
    label: 'Action',
    width: '25%',
    edit: true,
    delete: true,
  },
]
const RoleHeader = ({handleClickAddRole}) => {
  return (
    <Box className="header" sx={{padding: 5}}>
      <Box display={'flex'}>
        <Box className="header-left">
          <Typography variant="h4" sx={{fontWeight: 'bold', color: '#1976d2'}}>
            Role Management
          </Typography>
        </Box>
      </Box>
      <Box className="header-buttons">
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
          onClick={handleClickAddRole}
        >
          Add Role
        </Button>
      </Box>
    </Box>
  )
}

const RoleTab = () => {
  const navigate = useNavigate()
  const [page, setPage] = useState(0)
  const [rowsPerPage, setRowsPerPage] = useState(10)
  const [openAddRoleDialog, setOpenAddRoleDialog] = useState(false)
  const [roleList, setRoleList] = useState([])
  const [openDeleteRole, setOpenDeleteRole] = useState(false)
  const [selectedRoleId, setSelectedRoleId] = useState(null)
  const [totalItems, setTotalItems] = useState('')
  const [formErrors, setFormErrors] = useState({})

  const onChangePage = (newPage) => {
    setPage(newPage)
  }
  const onChangeRowPerPage = (rowsPerPage) => {
    setRowsPerPage(rowsPerPage)
    setPage(0)
  }

  const onActionEdit = (data) => {
    console.log('dataRole', data)
    navigate(`rolePermission/${data.id}`, {
      state: {data}, // Truyền dữ liệu ở đây
    })
  }

  const handleClickAddRole = () => {
    setOpenAddRoleDialog(true)
  }

  const handleCancelAddRoleDialog = () => {
    setFormErrors({})

    setOpenAddRoleDialog(false)
  }

  const {mutateAsync: mutateAsyncAdd} = useAddRole()
  const handleSubmitAddRoleDialog = async (formData) => {
    const errors = {}
    if (!formData.name) {
      errors.name = 'RoleName is required'
    }
    if (!formData.type) {
      errors.type = 'Type is required'
    }
    if (Object.keys(errors).length > 0) {
      setFormErrors(errors)
      return
    }
    mutateAsyncAdd(formData).then((response) => {
      if (response?.data.code === StatusCodes.BAD_REQUEST) {
        showError(response?.data?.message)
        setOpenAddRoleDialog(true)
      } else {
        showSuccess({message: response.data?.message})
        fetchData()
        setFormErrors({})
        setOpenAddRoleDialog(false)
      }
    })
  }
  const {mutateAsync: deleteRole} = useDeleteRole({
    roleId: selectedRoleId?.id,
  })

  const handleSubmitDeleteRole = () => {
    deleteRole()
      .then((response) => {
        showSuccess({message: response.data?.message})
        setOpenDeleteRole(false)
        fetchData()
      })
      .catch((err) => {
        if (err.response?.status === '') {
          const badRequestMessage = err.response?.data?.message
          showError({
            message: badRequestMessage,
          })
        } else {
          showError(err.response?.data?.message)
        }
      })
  }

  const handleCancelDelete = () => {
    setOpenDeleteRole(false)
  }

  const onActionDelete = (data) => {
    setOpenDeleteRole(true)
    setSelectedRoleId(data)
  }

  const {refetch} = useGetRoles(page + 1, rowsPerPage)

  const fetchData = useCallback(async () => {
    try {
      const response = await refetch()
      console.log(response.data?.data?.items)
      if (response && Array.isArray(response?.data?.data?.items)) {
        setRoleList(response?.data?.data?.items)
        setTotalItems(response?.data?.data?.totalCount)
      }
    } catch (error) {
      console.error('Error fetching posts:', error)
    }
  }, [refetch])

  useEffect(() => {
    fetchData()
  }, [page, rowsPerPage, refetch, fetchData])

  return (
    <Box sx={{width: '100%'}}>
      <Paper sx={{width: '100%', mb: 2}}>
        <RoleHeader handleClickAddRole={handleClickAddRole}></RoleHeader>
        <ComponentTable
          columns={columns}
          data={roleList}
          totalItems={totalItems}
          onChangePage={onChangePage}
          onChangeRowPerPage={onChangeRowPerPage}
          onActionEdit={onActionEdit}
          disableColCheckbox={true}
          hideDeleteForBasicType={true} // Hoặc false tùy vào yêu cầu
          onActionDelete={onActionDelete}
        />
      </Paper>
      <FormDialog
        open={openAddRoleDialog}
        onCancel={handleCancelAddRoleDialog}
        onConfirm={handleSubmitAddRoleDialog}
        title="Add New Role "
        actionName="Submit"
        dialogContent={<AddRoleForm formErrors={formErrors} />}
      />
      <ConfirmDialog
        open={openDeleteRole}
        onCancel={handleCancelDelete}
        onConfirm={handleSubmitDeleteRole}
        title="Delete Role"
        actionName="Delete"
        dialogContent={<Typography>Are you sure delete?</Typography>}
      />
    </Box>
  )
}

export default RoleTab
