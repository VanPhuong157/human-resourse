import {DatePicker} from '@mui/x-date-pickers/DatePicker'
import {AdapterDayjs} from '@mui/x-date-pickers/AdapterDayjs'
import {LocalizationProvider} from '@mui/x-date-pickers/LocalizationProvider'
import {useGetDepartments} from '../request/PostJobRequest.js'
import {
  TextField,
  InputLabel,
  FormControl,
  Select,
  MenuItem,
  Grid,
  FormHelperText,
} from '@mui/material'
import {useState, useEffect, useCallback} from 'react'
import dayjs from 'dayjs'
const TypeOptions = [
  {
    label: 'Full-Time',
    value: 'FullTime',
  },
  {
    label: 'Part-Time',
    value: 'PartTime',
  },
]
const AddPostJobForm = ({formErrors}) => {
  const [title, setTitle] = useState('')
  const [department, setDepartment] = useState('')
  const [departmentList, setDepartmentList] = useState([])
  const [salary, setSalary] = useState('')
  const [experienceYear, setExperienceYear] = useState('')
  const [numberOfRecruits, setNumberOfRecruits] = useState('')
  const [type, setType] = useState('')
  const [expiryDate, setExpiryDate] = useState()
  const [description, setDescription] = useState('')
  const [requirements, setRequirements] = useState('')
  const [benefits, setBenefits] = useState('')
  const [status, setStatus] = useState('Recruiting')

  const [errors, setErrors] = useState({
    title: '',
    department: '',
    salary: '',
    experienceYear: '',
    numberOfRecruits: '',
    type: '',
    expiryDate: '',
    description: '',
    requirements: '',
    benefits: '',
  })

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
  useEffect(() => {
    if (formErrors) {
      setErrors((prevErrors) => ({
        ...prevErrors,
        ...formErrors,
      }))
    }
  }, [formErrors])

  const handleInputChange = (e) => {
    const {name, value} = e.target
    let error = ''

    if (name === 'title') {
      setTitle(value)
      if (value.length > 100) {
        error = 'Title cannot be longer than 100 characters'
      }
    }

    if (name === 'salary') {
      if (!/^\d+$/.test(value) || parseInt(value, 10) <= 0) {
        error = 'Salary must be an integer greater than 0'
      }
      setSalary(value)
    }

    if (name === 'experienceYear') {
      setExperienceYear(value)
      if (!/^\d+$/.test(value) || parseFloat(value) < 0) {
        error = 'Experience Year must be greater than or equal 0'
      }
    }

    if (name === 'numberOfRecruits') {
      if (!/^\d+$/.test(value) || parseInt(value, 10) <= 0) {
        error = 'Number Of Recruits must be an integer greater than 0'
      }
      setNumberOfRecruits(value)
    }
    if (name === 'description') {
      setDescription(value)
      if (value.length > 3000) {
        error = 'Description cannot be longer than 3000 characters'
      }
    }
    if (name === 'requirements') {
      setRequirements(value)
      if (value.length > 3000) {
        error = 'Requirements cannot be longer than 3000 characters'
      }
    }
    if (name === 'benefits') {
      setBenefits(value)
      if (value.length > 3000) {
        error = 'Benefits cannot be longer than 3000 characters'
      }
    }

    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: error,
    }))
  }

  const handleDateChange = (newValue) => {
    setExpiryDate(newValue)
    let error = ''

    if (!newValue || dayjs(newValue).isBefore(dayjs())) {
      error = 'Application Deadline must be a future date'
    }

    setErrors((prevErrors) => ({
      ...prevErrors,
      expiryDate: error,
    }))
  }

  const handleDepartmentChange = (event) => {
    const {value} = event.target
    setDepartment(value)
    setErrors((prevErrors) => ({
      ...prevErrors,
      department: value ? '' : 'Department is required',
    }))
  }
  const handleTypeChange = (event) => {
    const {value} = event.target
    setType(value)
    setErrors((prevErrors) => ({
      ...prevErrors,
      type: value ? '' : 'Type Of Work is required',
    }))
  }

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <TextField
            name="title"
            label="Title Information *"
            value={title}
            onChange={handleInputChange}
            fullWidth
            error={!!errors.title}
            helperText={errors.title}
          />
        </Grid>
        <Grid item xs={12}>
          <FormControl fullWidth error={!!errors.department}>
            <InputLabel id="department-label">Department *</InputLabel>
            <Select
              labelId="department-label"
              name="department"
              label="Department"
              value={department}
              onChange={handleDepartmentChange}
            >
              {departmentList.map((departmentOption) => (
                <MenuItem key={departmentOption.id} value={departmentOption.id}>
                  {departmentOption.name}
                </MenuItem>
              ))}
            </Select>
            <FormHelperText>{errors.department}</FormHelperText>
          </FormControl>
        </Grid>

        <Grid item xs={12}>
          <TextField
            name="status"
            label="Status"
            value={status}
            onChange={(event) => setStatus(event.target.value)}
            fullWidth
            style={{display: 'none'}}
          />
        </Grid>
        <Grid item xs={6}>
          <TextField
            name="salary"
            label="Salary *"
            Value={salary}
            onChange={handleInputChange}
            fullWidth
            error={!!errors.salary}
            helperText={errors.salary}
          />
        </Grid>
        <Grid item xs={6}>
          <TextField
            name="experienceYear"
            label="Experience Year *"
            value={experienceYear}
            onChange={handleInputChange}
            fullWidth
            error={!!errors.experienceYear}
            helperText={errors.experienceYear}
          />
        </Grid>
        <Grid item xs={4}>
          <TextField
            name="numberOfRecruits"
            label="Number Of Recruits *"
            onChange={handleInputChange}
            fullWidth
            error={!!errors.numberOfRecruits}
            helperText={errors.numberOfRecruits}
          />
        </Grid>
        <Grid item xs={4}>
          <FormControl fullWidth error={!!errors.type}>
            <InputLabel id="type-label">Type Of Work *</InputLabel>
            <Select
              labelId="type-label"
              name="type"
              label="Type Of Work"
              value={type}
              onChange={handleTypeChange}
            >
              {TypeOptions.map((typeOption) => (
                <MenuItem key={typeOption.value} value={typeOption.value}>
                  {typeOption.label}
                </MenuItem>
              ))}
            </Select>
            <FormHelperText>{errors.type}</FormHelperText>
          </FormControl>
        </Grid>
        <Grid item xs={4}>
          <DatePicker
            label="Appication Deadline *"
            name="expiryDate"
            format="DD/MM/YYYY"
            value={expiryDate}
            onChange={handleDateChange}
            fullWidth
            slotProps={{
              field: {clearable: true},
            }}
            renderInput={(props) => (
              <TextField
                {...props}
                fullWidth
                error={!!errors.expiryDate}
                helperText={errors.expiryDate}
              />
            )}
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            name="description"
            label="Description"
            multiline
            rows={4}
            value={description}
            onChange={handleInputChange}
            error={!!errors.description}
            helperText={errors.description}
            fullWidth
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            name="requirements"
            label="Requirements *"
            multiline
            rows={4}
            value={requirements}
            onChange={handleInputChange}
            error={!!errors.requirements}
            helperText={errors.requirements}
            fullWidth
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            name="benefits"
            label="Benefits *"
            multiline
            rows={4}
            value={benefits}
            onChange={handleInputChange}
            error={!!errors.benefits}
            helperText={errors.benefits}
            fullWidth
          />
        </Grid>
      </Grid>
    </LocalizationProvider>
  )
}

export default AddPostJobForm
