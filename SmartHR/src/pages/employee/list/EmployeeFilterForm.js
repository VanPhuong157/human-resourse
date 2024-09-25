import {AdapterDayjs} from '@mui/x-date-pickers/AdapterDayjs'
import {LocalizationProvider} from '@mui/x-date-pickers/LocalizationProvider'
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
import {useGetDepartments} from '../../../pages/recruitment/job/request/PostJobRequest'
import SelectComponent from '../../../components/select'
const TypeOfWorkOptions = [
  {
    id: 'FullTime',
    value: 'FullTime',
  },
  {
    id: 'PartTime',
    value: 'PartTime',
  },
]
const StatusOptions = [
  {
    label: 'Active',
    value: 'Active',
  },
  {
    label: 'Deactive',
    value: 'Deactive',
  },
]

const EmployeeFilterForm = (props) => {
  const {data} = props
  const [name, setName] = useState('')
  const [department, setDepartment] = useState('')
  const [departmentList, setDepartmentList] = useState([])
  const [role, setRole] = useState('')
  const [roleList, setRoleList] = useState([])
  const [status, setStatus] = useState('')

  const {
    data: dataDepartment,
    isLoading: isLoadingDepartment,
    error: errorDepartment,
  } = useGetDepartments()
  useEffect(() => {
    if (isLoadingDepartment) {
      console.log('Loading data...')
    } else if (errorDepartment) {
      console.error('Error loading data:', errorDepartment)
    } else if (dataDepartment) {
      setDepartmentList(dataDepartment?.data?.items)
    }
  }, [dataDepartment, isLoadingDepartment, errorDepartment])

  const {data: roles, isLoading, error} = useGetRoles(1, 1000)
  useEffect(() => {
    if (isLoading) {
      console.log('Loading data...')
    } else if (error) {
      console.error('Error loading data:', error)
    } else if (roles) {
      setRoleList(roles?.data?.items)
    }
  }, [roles, isLoading, error])
  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <TextField
            name="name"
            label="Name"
            value={name}
            onChange={(event) => setName(event.target.value)}
            fullWidth
          />
        </Grid>
        <Grid item xs={12}>
          <FormControl fullWidth>
            <InputLabel id="department-label">Department</InputLabel>
            <Select
              labelId="department-label"
              name="department"
              label="Department"
              value={department}
              onChange={(event) => setDepartment(event.target.value)}
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
          <FormControl fullWidth>
            <InputLabel id="role-label">Role</InputLabel>
            <Select
              labelId="role-label"
              name="role"
              label="Role"
              value={role}
              onChange={(event) => setRole(event.target.value)}
            >
              {roleList.map((roleOption) => (
                <MenuItem key={roleOption.id} value={roleOption.id}>
                  {roleOption.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>

        <Grid item xs={6}>
          <FormControl fullWidth>
            <InputLabel id="status-label">Status</InputLabel>
            <Select
              labelId="status-label"
              name="status"
              label="Status"
              value={status}
              onChange={(event) => setStatus(event.target.value)}
            >
              {StatusOptions.map((statusOption) => (
                <MenuItem key={statusOption.value} value={statusOption.value}>
                  {statusOption.label}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={6}>
          <FormControl fullWidth>
            <InputLabel id="type-label">Type Of Work</InputLabel>
            <SelectComponent
              dataSelect={TypeOfWorkOptions}
              label="Type Of Work"
              name="type"
              value={data?.type}
            />
          </FormControl>
        </Grid>
      </Grid>
    </LocalizationProvider>
  )
}

export default EmployeeFilterForm
