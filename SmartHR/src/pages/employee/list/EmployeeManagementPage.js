import React, {useState, useEffect, useCallback} from 'react'
import Box from '@mui/material/Box'
import Paper from '@mui/material/Paper'
import FilterListIcon from '@mui/icons-material/FilterList'
import {Button, Typography} from '@mui/material'
import ComponentTable from '../../../components/table'
import AddIcon from '@mui/icons-material/Add'
import '../../../assets/style/candidate/candidatePage/Header.css'
import {
  useGetEmployees,
  useUpdatePositionEmployee,
  useUpdateStatusEmployee,
} from '../request/EmployeeRequest'
import {useNavigate} from 'react-router-dom'
import FormDialog from '../../../components/formDialog/FormDialog'
import AddUserForm from '../create/AddUserForm'
import {showError, showSuccess} from '../../../components/notification'
import EmployeeFilterForm from './EmployeeFilterForm'
import UpdateStatusEmployee from './UpdateStatusEmployee'
import {useAddEmployee} from '../request/addEmployee'
import useGetUserPermissions from '../../../pages/admin/requests/getUserPermissions'
import UpdatePositionEmployee from './UpdatePositionEmployee'
import {StatusCodes} from 'http-status-codes'

const getColumns = (
  hasEditEmployee,
  hasUpdateStatusEmployee,
  hasUpdatePostionEmployee,
) => [
  {
    id: 'fullName',
    label: 'Name',
    width: '20%',
  },
  {
    id: 'code',
    label: 'Code',
    width: '10%',
  },
  {
    id: 'departmentName',
    label: 'Department',
    width: '10%',
  },
  {
    id: 'email',
    label: 'Email',
    width: '20%',
  },
  {
    id: 'roleNames',
    label: 'Role',
    width: '10%',
  },
  {
    id: 'typeOfWork',
    label: 'Type Of Work',
    width: '20%',
  },
  {
    id: 'status',
    label: 'Status',
    width: '5%',
  },
  {
    id: 'action',
    label: 'Action',
    width: '5%',
    update_status: hasUpdateStatusEmployee,
    update_position: hasUpdatePostionEmployee,
    edit: hasEditEmployee,
  },
]
const EmployeeManagementPageHeader = ({
  handleClickAddUser,
  handleClickFilter,
  perm,
}) => {
  const hasCreateEmployee = perm.includes('Employee:Create')

  return (
    <Box className="header" sx={{padding: 5}}>
      <Box display={'flex'}>
        <Box className="header-left">
          <Typography variant="h4" sx={{fontWeight: 'bold', color: '#1976d2'}}>
            User Management
          </Typography>
        </Box>
      </Box>
      <Box className="header-buttons">
        <Button
          className="header-button"
          startIcon={<FilterListIcon />}
          sx={{textTransform: 'none', color: 'black'}}
          onClick={handleClickFilter}
        >
          Filter
        </Button>
        {hasCreateEmployee && (
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
            onClick={handleClickAddUser}
          >
            Add User
          </Button>
        )}
      </Box>
    </Box>
  )
}
export default function EmployeeManagementPage() {
  const navigate = useNavigate()
  const [page, setPage] = useState(0)
  const [rowsPerPage, setRowsPerPage] = useState(10)
  const [listEmployee, setListEmployee] = useState([])
  const [totalItems, setTotalItems] = React.useState('')
  const [openAddUserDialog, setOpenAddUserDialog] = useState(false)
  const [openUpdateStatusDialog, setOpenUpdateStatusDialog] = useState(false)
  const [openUpdatePositionDialog, setOpenUpdatePositionDialog] =
    useState(false)
  const [openFilterDialog, setOpenFilterDialog] = useState(false)
  const [dataFilterDialog, setDataFilterDialog] = useState()
  const [userInformation, setUserInformation] = useState(null)
  const [loading, setLoading] = useState(true) // Thêm state loading
  const userId = localStorage.getItem('userId')
  const role = localStorage.getItem('role')
  const [permissionData, setPermissionData] = useState([])
  const hasEditEmployee = permissionData.includes('Employee:Edit')
  const hasUpdateStatusEmployee = permissionData.includes(
    'Employee:UpdateStatus',
  )
  const hasUpdatePostionEmployee = permissionData.includes(
    'Employee:UpdatePosition',
  )

  const columns = getColumns(
    hasEditEmployee,
    hasUpdateStatusEmployee,
    hasUpdatePostionEmployee,
  )
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
  const onChangePage = (newPage) => {
    setPage(newPage)
  }
  const onChangeRowPerPage = (rowsPerPage) => {
    setRowsPerPage(rowsPerPage)
    setPage(0)
  }
  const handleClickAddUser = () => {
    setOpenAddUserDialog(true)
  }
  const handleCancelAddUserDialog = () => {
    setOpenAddUserDialog(false)
  }
  const {mutateAsync: addUserAsync} = useAddEmployee()
  const handleSubmitAddUserDialog = (formData) => {
    addUserAsync(formData)
      .then((response) => {
        if (response?.data?.code === StatusCodes.BAD_REQUEST) {
          showError(response?.data?.message)
          return
        }
        showSuccess({message: response.data?.message})
        fetchDataEmployee()
        setOpenAddUserDialog(false)
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

  const handleClickFilter = () => {
    setOpenFilterDialog(true)
  }
  const handleCancelFilterDialog = () => {
    setOpenFilterDialog(false)
  }
  const handleConfirmFilterDialog = (formData) => {
    setDataFilterDialog(formData)
    setOpenFilterDialog(false)
  }

  const handleCancelUpdateStatusDialog = () => {
    setOpenUpdateStatusDialog(false)
  }
  const handleClickOpenUpdateStatusDialog = () => {
    setOpenUpdateStatusDialog(true)
  }
  const {mutateAsync} = useUpdateStatusEmployee({
    userId: userInformation?.userId,
  })
  const handleConfirmUpdateStatusDialog = (formData) => {
    setOpenUpdateStatusDialog(false)
    const {status} = formData
    const dataRequest = {
      newStatus: status,
    }
    mutateAsync(dataRequest)
      .then((response) => {
        showSuccess({message: response.data?.message})
        fetchDataEmployee()
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

  const handleCancelUpdatePositionDialog = () => {
    setOpenUpdatePositionDialog(false)
  }
  const handleClickOpenUpdatePositionDialog = () => {
    setOpenUpdatePositionDialog(true)
  }
  const {mutateAsync: mutateRole} = useUpdatePositionEmployee()
  const handleConfirmUpdatePositionDialog = (formData) => {
    const {roleId, departmentId} = formData
    const dataRequest = {
      roleId: roleId,
      departmentId: departmentId,
      userId: userInformation.userId,
    }
    mutateRole(dataRequest)
      .then((response) => {
        if (
          response?.data?.code === StatusCodes.BAD_REQUEST ||
          response?.data?.code === StatusCodes.NOT_FOUND
        ) {
          showError(response?.data?.message)
          return
        }
        setOpenUpdatePositionDialog(false)
        fetchDataEmployee()
        showSuccess({message: response.data?.message})
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

  const {refetch} = useGetEmployees(page + 1, rowsPerPage, dataFilterDialog)
  const fetchDataEmployee = useCallback(async () => {
    try {
      const response = await refetch()
      if (response && Array.isArray(response?.data?.data?.items)) {
        const listEmployee = response?.data?.data?.items
        if (role != 'Admin') {
          const filteredList = listEmployee.filter(
            (employee) => !employee.roleNames.includes('Admin'),
          )
          setListEmployee(filteredList)
          setTotalItems(filteredList?.length)

        } else {
          setListEmployee(response.data.data.items)
          setTotalItems(response?.data?.data?.totalCount)
        }
      }
    } catch (error) {
      console.error('Error fetching posts:', error)
    }
  }, [refetch])

  useEffect(() => {
    fetchDataEmployee()
  }, [page, rowsPerPage, dataFilterDialog, refetch, fetchDataEmployee])

  const onActionEdit = (data) => {
    navigate(`edit/${data.userId}`)
  }

  const onActionUpdateStatus = (data) => {
    setUserInformation(data)
    handleClickOpenUpdateStatusDialog()
  }
  const onActionUpdatePosition = (data) => {
    setUserInformation(data)
    handleClickOpenUpdatePositionDialog()
  }
  if (loading) {
    return (
      <>
        <div>Loading....</div>
      </>
    )
  }
  return (
    <Box sx={{width: '100%'}}>
      <Paper sx={{width: '100%', mb: 2}}>
        <EmployeeManagementPageHeader
          perm={permissionData}
          handleClickAddUser={handleClickAddUser}
          handleClickFilter={handleClickFilter}
        />
        <ComponentTable
          columns={columns}
          data={listEmployee}
          totalItems={totalItems}
          onChangePage={onChangePage}
          onChangeRowPerPage={onChangeRowPerPage}
          onActionEdit={onActionEdit}
          onActionUpdateStatus={onActionUpdateStatus}
          onActionUpdatePosition={onActionUpdatePosition}
          disableColCheckbox={true}
        />
      </Paper>
      <FormDialog
        open={openAddUserDialog}
        onCancel={handleCancelAddUserDialog}
        onConfirm={handleSubmitAddUserDialog}
        title="Add New User"
        actionName="Submit"
        dialogContent={<AddUserForm />}
      />
      <FormDialog
        open={openFilterDialog}
        onCancel={handleCancelFilterDialog}
        onConfirm={handleConfirmFilterDialog}
        title="Filter"
        actionName="Filter"
        dialogContent={<EmployeeFilterForm data={dataFilterDialog} />}
      />
      <FormDialog
        open={openUpdateStatusDialog}
        onCancel={handleCancelUpdateStatusDialog}
        onConfirm={handleConfirmUpdateStatusDialog}
        title="Update status user"
        actionName="Save"
        dialogContent={<UpdateStatusEmployee data={userInformation} />}
      />
      <FormDialog
        open={openUpdatePositionDialog}
        onCancel={handleCancelUpdatePositionDialog}
        onConfirm={handleConfirmUpdatePositionDialog}
        title="Update Position"
        actionName="Save"
        dialogContent={<UpdatePositionEmployee data={userInformation} />}
      />
    </Box>
  )
}
