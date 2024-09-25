import {useParams} from 'react-router-dom'
import ComponentTable from '../../../../components/table'
import React, {useEffect, useState} from 'react'
import Box from '@mui/material/Box'
import Paper from '@mui/material/Paper'
import useGetEmployeeFamily from '../../request/getEmployeeFamily'
import {Button, CircularProgress, Typography} from '@mui/material'
import AddIcon from '@mui/icons-material/Add'
import FormDialog from '../../../../components/formDialog/FormDialog'
import {useAddEmployeeFamily} from '../../request/addEmployeeFamily'
import {showError, showSuccess} from '../../../../components/notification'
import AddUserFamilyForm from './create/AddUserFamilyForm'
import {useEditEmployeeFamily} from '../../../../pages/employee/request/editmployeeFamily'
import EditUserFamilyForm from './edit/EditUserFamilyForm'
import {StatusCodes} from 'http-status-codes'
import ConfirmDialog from '../../../../components/confirmDialog'
import {useDeleteEmployeeFamily} from '../../../../pages/employee/request/deleteEmployeeFamily'
import useGetUserPermissions from '../../../../pages/admin/requests/getUserPermissions'
import {convertDateFormat} from '../../../../components/utils'

const getColumns = (hasEditEmployeeFamily, hasDeleteEmployeeFamily) => [
  {
    id: 'fullName',
    label: 'FullName',
    width: '20%',
  },
  {
    id: 'relationship',
    label: 'Relationship',
    width: '20%',
  },
  {
    id: 'dateOfBirth',
    label: 'DateOfBirth',
    width: '15%',
    formatDatetime: 'dd-MM-yyyy',
  },
  {
    id: 'job',
    label: 'Job',
    width: '20%',
  },
  {
    id: 'phoneNumber',
    label: 'PhoneNumber',
    width: '20%',
  },
  {
    id: 'action',
    label: 'Action',
    width: '5%',
    edit: hasEditEmployeeFamily,
    delete: hasDeleteEmployeeFamily,
  },
]

const EmployeeFamilyHeaderTab = ({
  handleClickAddUserFamily,
  hasCreateEmployeeFamily,
}) => {
  return (
    <Box className="header">
      <Box display={'flex'}>
        <Box className="header-left"></Box>
      </Box>
      <Box className="header-buttons">
        {hasCreateEmployeeFamily && (
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
            onClick={handleClickAddUserFamily}
          >
            Add user family
          </Button>
        )}
      </Box>
    </Box>
  )
}

const EmployeeFamilyTab = () => {
  const {userId} = useParams()
  const [familyData, setFamilyData] = useState(null)

  const {data, refetch} = useGetEmployeeFamily({userId})
  const [formErrors, setFormErrors] = useState({})

  const [permissionData, setPermissionData] = useState([])
  const userIdPermission = localStorage.getItem('userId')

  const hasEditEmployeeFamily = permissionData.includes('EmployeeFamily:Edit')
  const hasDeleteEmployeeFamily = permissionData.includes(
    'EmployeeFamily:Delete',
  )
  const hasCreateEmployeeFamily = permissionData.includes(
    'EmployeeFamily:Create',
  )
  const columns = getColumns(hasEditEmployeeFamily, hasDeleteEmployeeFamily)

  const {data: permissions, isLoading: loadingPermission} =
    useGetUserPermissions({
      userId: userIdPermission,
      pageIndex: 1,
      pageSize: 1000,
    })
  useEffect(() => {
    if (data) {
      setFamilyData(data?.data?.familyMembers)
    }
    if (permissions) {
      const newPerm = permissions?.data?.items.map((perm) => perm.name)
      setPermissionData(newPerm)
    }
  }, [permissions, data])

  const [openAddUserFamilyDialog, setOpenAddUserFamilyDialog] = useState(false)
  const [openEditUserFamilyDialog, setOpenEditUserFamilyDialog] =
    useState(false)
  const [openDeleteUserFamilyDialog, setOpenDeleteUserFamilyDialog] =
    useState(false)
  const [selectedFamilyMember, setSelectedFamilyMember] = useState(null)

  const handleClickAddUserFamily = () => {
    setOpenAddUserFamilyDialog(true)
  }

  const handleCancelAddUserFamilyDialog = () => {
    setFormErrors({})
    setOpenAddUserFamilyDialog(false)
  }

  const {mutateAsync: addUserFamilyAsync} = useAddEmployeeFamily({userId})

  const handleSubmitAddUserFamilyDialog = (data) => {
    const errors = {}
    if (!data.fullname) {
      errors.fullname = 'FullName is required'
    }
    if (!data.relationship) {
      errors.relationship = 'Relationship is required'
    }
    if (!data.dateOfBirth) {
      errors.dateOfBirth = 'DateOfBirth is required'
    }
    if (!data.job) {
      errors.job = 'Job is required'
    }
    if (!data.phoneNumber) {
      errors.phoneNumber = 'PhoneNumber is required'
    }

    if (Object.keys(errors).length > 0) {
      setFormErrors(errors)
      return
    }
    addUserFamilyAsync(data)
      .then((response) => {
        showSuccess({message: response.data?.message})
        setOpenAddUserFamilyDialog(false)
        setFormErrors({})
        refetch()
      })
      .catch((err) => {
        const badRequestMessage = err.response?.data?.message
        showError({
          message: badRequestMessage || 'An unexpected error occurred',
        })
      })
  }

  const handleCancelEditUserFamilyDialog = () => {
    setFormErrors({})

    setOpenEditUserFamilyDialog(false)
  }

  const onActionEdit = (data) => {
    setOpenEditUserFamilyDialog(true)
    setSelectedFamilyMember(data)
  }

  const {mutateAsync: editUserFamilyAsync} = useEditEmployeeFamily({
    userId,
    memberId: selectedFamilyMember?.id,
  })

  const handleSubmitEditUserFamilyDialog = (data) => {
    const errors = {}
    if (!data.fullName) {
      errors.fullName = 'FullName is required'
    }
    if (!data.relationship) {
      errors.relationship = 'Relationship is required'
    }
    if (!data.dateOfBirth) {
      errors.dateOfBirth = 'DateOfBirth is required'
    }
    if (!data.job) {
      errors.job = 'Job is required'
    }
    if (!data.phoneNumber) {
      errors.phoneNumber = 'PhoneNumber is required'
    }

    if (Object.keys(errors).length > 0) {
      setFormErrors(errors)
      return
    }
    editUserFamilyAsync(data)
      .then((response) => {
        showSuccess({message: response.data?.message})
        setOpenEditUserFamilyDialog(false)
        setFormErrors({})

        refetch()
      })
      .catch((err) => {
        const badRequestMessage =
          err.response?.data?.message || err.response?.data?.title
        showError({
          message: badRequestMessage,
        })
      })
  }

  const handleCancelDeleteUserFamilyDialog = () => {
    setOpenDeleteUserFamilyDialog(false)
  }

  const onActionDelete = (data) => {
    setOpenDeleteUserFamilyDialog(true)
    setSelectedFamilyMember(data)
  }

  const {mutateAsync: deleteUserFamilyAsync} = useDeleteEmployeeFamily({
    memberId: selectedFamilyMember?.id,
  })

  const handleSubmitDeleteUserFamilyDialog = () => {
    deleteUserFamilyAsync()
      .then((response) => {
        showSuccess({message: response.data?.message})
        setOpenDeleteUserFamilyDialog(false)
        refetch()
      })
      .catch((err) => {
        const badRequestMessage = err.response?.data?.message
        showError({
          message: badRequestMessage || 'An unexpected error occurred',
        })
      })
  }

  if (loadingPermission) {
    return (
      <Box
        display="flex"
        justifyContent="center"
        alignItems="center"
        minHeight="100vh"
      >
        <CircularProgress />
      </Box>
    )
  }

  return (
    <Box sx={{width: '100%'}}>
      <Paper sx={{width: '100%', mb: 2}}>
        <EmployeeFamilyHeaderTab
          handleClickAddUserFamily={handleClickAddUserFamily}
          hasCreateEmployeeFamily={hasCreateEmployeeFamily}
        />
        <ComponentTable
          columns={columns}
          data={familyData}
          disableColCheckbox={true}
          disablePaginationTable={true}
          onActionEdit={onActionEdit}
          onActionDelete={onActionDelete}
        />
        <FormDialog
          open={openAddUserFamilyDialog}
          onCancel={handleCancelAddUserFamilyDialog}
          onConfirm={handleSubmitAddUserFamilyDialog}
          title="Add New UserFamily"
          actionName="Submit"
          dialogContent={<AddUserFamilyForm formErrors={formErrors} />}
        />
        <FormDialog
          open={openEditUserFamilyDialog}
          onCancel={handleCancelEditUserFamilyDialog}
          onConfirm={handleSubmitEditUserFamilyDialog}
          title="Edit UserFamily"
          actionName="Save"
          dialogContent={
            <EditUserFamilyForm
              data={selectedFamilyMember}
              formErrors={formErrors}
            />
          }
        />
        <ConfirmDialog
          open={openDeleteUserFamilyDialog}
          onCancel={handleCancelDeleteUserFamilyDialog}
          onConfirm={handleSubmitDeleteUserFamilyDialog}
          title="Delete UserFamily"
          actionName="Delete"
          dialogContent={<Typography>Are you sure delete?</Typography>}
        />
      </Paper>
    </Box>
  )
}

export default EmployeeFamilyTab
