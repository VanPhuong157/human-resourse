import {AdapterDayjs} from '@mui/x-date-pickers/AdapterDayjs'
import {LocalizationProvider} from '@mui/x-date-pickers/LocalizationProvider'
import {useGetDepartments} from '../../../pages/recruitment/job/request/PostJobRequest'
import {
  TextField,
  InputLabel,
  FormControl,
  Select,
  MenuItem,
  Grid,
} from '@mui/material'
import {useState, useEffect} from 'react'
import {useGetRoles} from '../request/EmployeeRequest'
import {showError} from '../../../components/notification'

const AddUserForm = () => {
  const userRole = localStorage.getItem('role')
  const [departmentId, setDepartmentId] = useState('')
  const [departmentList, setDepartmentList] = useState([])
  const [roleId, setRoleId] = useState('')
  const [typeOfWork, setTypeOfWork] = useState('')
  const [roleList, setRoleList] = useState([])
  const [errors, setErrors] = useState({
    userName: '',
    email: '',
    fullname: '',
    typeOfWork: '',
    departmentId: '',
    roleId: '',
  })

  const {
    data: dataDepartment,
    isLoading: isLoadingDepartment,
    error: errorDepartment,
  } = useGetDepartments()

  useEffect(() => {
    if (errorDepartment) {
      showError('Failed to get Departments')
    } else if (dataDepartment) {
      setDepartmentList(dataDepartment?.data?.items)
    }
  }, [dataDepartment, isLoadingDepartment, errorDepartment])

  const {data: roles, isLoading, error} = useGetRoles(1, 1000)

  useEffect(() => {
    if (error) {
      showError('Failed to get Roles')
    } else if (roles) {
      if (userRole === 'HR') {
        const hrRoles = roles.data.items.filter(
          (role) => role.name === 'Employee' || role.name === 'HR',
        )

        setRoleList((prevRoleList) => [
          ...prevRoleList,
          ...hrRoles.filter(
            (hrRole) => !prevRoleList.some((r) => r.id === hrRole.id),
          ),
        ])
      } else {
        setRoleList(roles.data.items)
      }
    }
  }, [roles, isLoading, error, userRole])

  const TypeOfWorkOptions = [
    {
      id: 'Fulltime',
      name: 'Fulltime',
    },
    {
      id: 'Parttime',
      name: 'Parttime',
    },
  ]

  const handleInputChange = (e) => {
    const {name, value} = e.target
    let error = ''

    if (name === 'userName') {
      if (value.length < 6 || value.length > 50) {
        error = 'UserName must be between 6 and 50 characters'
      }
    }

    if (name === 'fullname') {
      if (value.length < 6 || value.length > 100) {
        error = 'FullName must be between 6 and 100 characters'
      }
    }
    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: error,
    }))
  }

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <TextField
            required
            name="userName"
            label="UserName"
            fullWidth
            error={!!errors.userName}
            helperText={errors.userName}
            onChange={handleInputChange}
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            required
            type="email"
            name="email"
            label="Email"
            fullWidth
            error={!!errors.email}
            helperText={errors.email}
            onChange={handleInputChange}
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            required
            name="fullname"
            label="FullName"
            fullWidth
            error={!!errors.fullname}
            helperText={errors.fullname}
            onChange={handleInputChange}
          />
        </Grid>
        <Grid item xs={12}>
          <FormControl required fullWidth error={!!errors.typeOfWork}>
            <InputLabel id="type-of-work-label">Type of Work</InputLabel>
            <Select
              labelId="type-of-work-label"
              name="typeOfWork"
              label="Type of Work"
              value={typeOfWork}
              onChange={(event) => setTypeOfWork(event.target.value)}
            >
              {TypeOfWorkOptions.map((typeOfWorkOption) => (
                <MenuItem key={typeOfWorkOption.id} value={typeOfWorkOption.id}>
                  {typeOfWorkOption.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <FormControl required fullWidth error={!!errors.departmentId}>
            <InputLabel id="department-label">Department</InputLabel>
            <Select
              labelId="department-label"
              name="departmentId"
              label="Department"
              value={departmentId}
              onChange={(event) => setDepartmentId(event.target.value)}
            >
              {departmentList.map((departmentOption) => (
                <MenuItem key={departmentOption.id} value={departmentOption.id}>
                  {departmentOption.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <FormControl required fullWidth error={!!errors.roleId}>
            <InputLabel id="role-label">Role</InputLabel>
            <Select
              labelId="role-label"
              name="roleId"
              label="Role"
              value={roleId}
              onChange={(event) => setRoleId(event?.target?.value)}
            >
              {roleList?.map((roleOption) => (
                <MenuItem key={roleOption?.id} value={roleOption?.id}>
                  {roleOption?.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
      </Grid>
    </LocalizationProvider>
  )
}

export default AddUserForm
