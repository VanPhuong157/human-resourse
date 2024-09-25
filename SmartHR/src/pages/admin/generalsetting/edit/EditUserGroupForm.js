import {
  Box,
  Typography,
  DialogContent,
  Dialog,
  DialogTitle,
  IconButton,
  TextField,
  DialogActions,
  Button,
  Grid,
  Autocomplete,
  Chip,
} from '@mui/material'
import {showSuccess, showError} from '../../../../components/notification'
import CloseIcon from '@mui/icons-material/Close'
import {useState, useEffect} from 'react'
import useGetRoles from '../../../../pages/role/request/getRoles'
import {useGetUser} from '../../../../pages/admin/requests/getUser'
import {useEditUserGroup} from '../../../../pages/admin/requests/editUserGroup'
import ConfirmDialog from '../../../../components/confirmDialog'
import {useDeleteUserGroup} from '../../../../pages/admin/requests/deleteUserGroup'
import {StatusCodes} from 'http-status-codes'

const CustomEditUserGroupDialog = ({
  open,
  onClose,
  userGroupData,
  fetchData,
}) => {
  const [errors, setErrors] = useState({
    name: '',
    roleIds: null,
  })
  const [userList, setUserList] = useState([])
  const [roleList, setRoleList] = useState([])
  const [openDeleteUserGroup, setOpenDeleteUserGroup] = useState(false)

  const {mutateAsync: deleteUserGroup} = useDeleteUserGroup({
    userGroupId: userGroupData?.id,
  })

  const [filter, setFilter] = useState({
    type: 'Custom',
  })

  const handleSubmitDeleteUserGroup = () => {
    deleteUserGroup()
      .then((response) => {
        showSuccess({message: response.data?.message})
        fetchData()
        setOpenDeleteUserGroup(false)
      })
      .catch((err) => {
        if (err.response?.status === StatusCodes.BAD_REQUEST) {
          const badRequestMessage = err.response?.data?.title
          showError({
            message: badRequestMessage,
          })
        } else {
          showError(err.response?.data?.message)
        }
      })
  }
  const handleCancelDelete = () => {
    setOpenDeleteUserGroup(false)
  }

  const {data, isLoading, error} = useGetRoles(1, 1000, filter)
  console.log(data?.data?.items)

  useEffect(() => {
    if (isLoading) {
      console.log('Loading data...')
    } else if (error) {
      console.error('Error loading data:', error)
    } else if (data) {
      setRoleList(data?.data?.items)
    }
  }, [data, isLoading, error])

  const {
    data: userData,
    isLoading: userDataLoading,
    error: userDataError,
  } = useGetUser(1, 1000)

  useEffect(() => {
    if (userDataLoading) {
      console.log('Loading data...')
    } else if (userDataError) {
      console.error('Error loading data:', userDataError)
    } else if (userData) {
      setUserList(userData?.data?.items)
    }
  }, [userData, userDataLoading, userDataError])

  const [formData, setFormData] = useState({
    name: '',
    description: '',
    userIds: [],
    roleIds: [],
  })

  const validateInput = (name, value) => {
    let error = ''

    if (name === 'name') {
      if (!value) error = 'Name is required.'
      if (value.length > 50) {
        error = 'Title cannot exceed 50 characters.'
      }
    }
    if (name === 'roleIds') {
      if (!value || value.length === 0) {
        error = 'At least one role is required.'
      }
    }

    return error
  }

  const handleInputChange = (event) => {
    const {name, value} = event.target
    const error = validateInput(name, value)

    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }))
    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: error,
    }))
  }

  const handleUserChange = (event, newValue) => {
    // Chỉ lưu trữ ID của người dùng đã chọn
    const userIds = newValue.map((user) => user.id)
    setFormData((prevData) => ({
      ...prevData,
      userIds: userIds || [],
    }))
  }
  const handleRoleChange = (event, newValue) => {
    // Chỉ lưu trữ ID của vai trò đã chọn
    const roleIds = newValue.map((role) => role.id)
    setFormData((prevData) => ({
      ...prevData,
      roleIds: roleIds,
    }))
    const error = validateInput('roleIds', roleIds)

    // Cập nhật errors state
    setErrors((prevErrors) => ({
      ...prevErrors,
      roleIds: error,
    }))
  }

  const {mutateAsync: mutateAsyncEdit} = useEditUserGroup({
    userGroupId: userGroupData?.id,
  })

  const handleSubmit = async (event) => {
    event.preventDefault()
    const newErrors = {
      name: validateInput('name', formData.name),
      roleIds: validateInput('roleIds', formData.roleIds),
    }
    setErrors(newErrors)

    // Nếu có lỗi, không thực hiện tiếp quy trình gửi form
    if (Object.values(newErrors).some((error) => error !== '')) {
      return
    }
    if (formData && formData?.userIds?.length > 0) {
      try {
        const response = await mutateAsyncEdit(formData)
        if (response?.data?.code === StatusCodes.BAD_REQUEST) {
          const badRequestMessage = response?.data?.message
          showError({
            message: badRequestMessage,
          })
        } else {
          showSuccess({message: response.data?.message})
          fetchData()
          onClose()
        }
      } catch (error) {
        showError(error.response?.data?.message)
      }
    } else {
      setOpenDeleteUserGroup(true)
      onClose()
    }
  }
  const handleClose = () => {
    setErrors({}) // Reset errors về giá trị rỗng
    onClose() // Gọi hàm onClose từ props để đóng dialog
  }

  useEffect(() => {
    if (userGroupData) {
      setFormData({
        name: userGroupData.name,
        description: userGroupData.description,
        userIds: userGroupData.users.map(
          (userName) => userList.find((user) => user.userName === userName)?.id,
        ),
        roleIds: userGroupData.roles.map(
          (roleName) => roleList.find((role) => role.name === roleName)?.id,
        ),
      })
    }
  }, [userGroupData, userList, roleList])

  return (
    <>
      <Dialog
        open={open}
        onClose={onClose}
        aria-labelledby="application-form-dialog"
        maxWidth="sm"
        fullWidth
      >
        <DialogTitle id="application-form-dialog">
          <Box display="flex" justifyContent="flex-end">
            <IconButton
              aria-label="close"
              onClick={handleClose}
              sx={{
                position: 'absolute',
                right: 8,
                top: 8,
                color: (theme) => theme.palette.grey[500],
              }}
            >
              <CloseIcon />
            </IconButton>
          </Box>
          <Box display="flex">
            <Typography
              variant="h4"
              component="h1"
              sx={{
                marginTop: '6px',
              }}
            >
              Edit UserGroup
            </Typography>
          </Box>
        </DialogTitle>

        {/* Dialog Apply CV */}
        <DialogContent>
          <form onSubmit={handleSubmit}>
            <Grid container spacing={2} paddingTop={2}>
              <Grid item xs={12}>
                <TextField
                  required
                  label="Name"
                  name="name"
                  value={formData.name}
                  onChange={handleInputChange}
                  error={!!errors.name}
                  helperText={errors.name}
                  fullWidth
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  label="Description"
                  name="description"
                  value={formData.description}
                  onChange={handleInputChange}
                  fullWidth
                />
              </Grid>
              <Grid item xs={12}>
                <Autocomplete
                  multiple
                  id="tags-outlined"
                  name="users"
                  options={userList}
                  getOptionLabel={(option) => option.userName}
                  filterSelectedOptions
                  value={userList.filter((user) =>
                    formData.userIds?.includes(user.id),
                  )}
                  onChange={handleUserChange} // Gọi hàm handleUserChange
                  renderTags={(selected, getTagProps) =>
                    selected.map((value, index) => (
                      <Chip
                        key={index}
                        label={value.userName}
                        {...getTagProps({index})}
                      />
                    ))
                  }
                  renderInput={(params) => (
                    <TextField
                      {...params}
                      variant="outlined"
                      label="User"
                      placeholder="User"
                    />
                  )}
                />
              </Grid>
              <Grid item xs={12}>
                <Autocomplete
                  multiple
                  id="tags-outlined"
                  name="roles"
                  options={roleList}
                  getOptionLabel={(option) => option.name}
                  filterSelectedOptions
                  value={roleList.filter((role) =>
                    formData.roleIds?.includes(role.id),
                  )}
                  onChange={handleRoleChange} // Gọi hàm handleUserChange
                  renderTags={(selected, getTagProps) =>
                    selected.map((value, index) => (
                      <Chip
                        key={index}
                        label={value.name}
                        {...getTagProps({index})}
                      />
                    ))
                  }
                  renderInput={(params) => (
                    <TextField
                      {...params}
                      variant="outlined"
                      label="Role"
                      error={!!errors.roleIds} // Chỉ hiển thị lỗi nếu errors.roleIds có giá trị
                      helperText={errors.roleIds}
                      placeholder="Role"
                    />
                  )}
                />
              </Grid>
            </Grid>
          </form>
        </DialogContent>
        <DialogActions>
          <Button
            onClick={handleSubmit}
            variant="contained"
            disabled={isLoading}
            type="submit"
            sx={{
              textTransform: 'none',
              backgroundColor: (theme) => theme.palette.primary.main,
              color: (theme) => theme.palette.primary.contrastText,
              '&:hover': {
                backgroundColor: (theme) => theme.palette.primary.dark,
              },
            }}
          >
            Save
          </Button>
        </DialogActions>
      </Dialog>
      <ConfirmDialog
        open={openDeleteUserGroup}
        onCancel={handleCancelDelete}
        onConfirm={handleSubmitDeleteUserGroup}
        title="Delete User Group"
        actionName="Delete"
        dialogContent={
          <Typography>No one in User Group, Are you sure delete?</Typography>
        }
      />
    </>
  )
}

export default CustomEditUserGroupDialog
