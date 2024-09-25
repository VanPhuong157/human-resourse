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
import {useEffect, useState} from 'react'
import useGetDepartments from '../../../../pages/department/requests/getDepartment'

const TypeOptions = [
  {
    label: 'None',
    value: '',
  },
  {
    label: 'Full-Time',
    value: 'fullTime',
  },
  {
    label: 'Part-Time',
    value: 'partTime',
  },
]
const StatusOptions = [
  {
    label: 'Recruiting',
    value: 'recruiting',
  },
  {
    label: 'Stop Recruiting',
    value: 'stopRecruiting',
  },
]

const PostFilter = (props) => {
  const [title, setTitle] = useState('')
  const [minSalary, setMinSalary] = useState('')
  const [maxSalary, setMaxSalary] = useState('')
  const [minYear, setMinYear] = useState('')
  const [maxYear, setMaxYear] = useState('')
  const [type, setType] = useState('')
  const [status, setStatus] = useState('')
  const [departmentId, setDepartmentId] = useState('')
  const [departmentList, setDepartmentList] = useState([])

  const {data, isLoading, error} = useGetDepartments()

  useEffect(() => {
    if (isLoading) {
      console.log('Loading data...')
    } else if (error) {
      console.error('Error loading data:', error)
    } else if (data) {
      setDepartmentList(data?.data?.items)
    }
  }, [data, isLoading, error])

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <TextField
            name="title"
            label="Title Infomation"
            value={title}
            onChange={(event) => setTitle(event.target.value)}
            fullWidth
          />
        </Grid>
        <Grid item xs={6}>
          <TextField
            name="minYear"
            label="Min Year"
            value={minYear}
            onChange={(event) => setMinYear(event.target.value)}
            fullWidth
          />
        </Grid>
        <Grid item xs={6}>
          <TextField
            name="maxYear"
            label="Max Year"
            value={maxYear}
            onChange={(event) => setMaxYear(event.target.value)}
            fullWidth
          />
        </Grid>
        <Grid item xs={6}>
          <TextField
            name="minSalary"
            label="Min Salary"
            value={minSalary}
            onChange={(event) => setMinSalary(event.target.value)}
            fullWidth
          />
        </Grid>
        <Grid item xs={6}>
          <TextField
            name="maxSalary"
            label="Max Salary"
            value={maxSalary}
            onChange={(event) => setMaxSalary(event.target.value)}
            fullWidth
          />
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
            <Select
              labelId="type-label"
              name="type"
              label="Type Of Work"
              value={type}
              onChange={(event) => setType(event.target.value)}
            >
              {TypeOptions.map((typeOption) => (
                <MenuItem key={typeOption.value} value={typeOption.value}>
                  {typeOption.label}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <FormControl fullWidth>
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
      </Grid>
    </LocalizationProvider>
  )
}

export default PostFilter
