import {DatePicker} from '@mui/x-date-pickers/DatePicker'
import {AdapterDayjs} from '@mui/x-date-pickers/AdapterDayjs'
import {LocalizationProvider} from '@mui/x-date-pickers/LocalizationProvider'
import {
  TextField,
  InputLabel,
  FormControl,
  Select,
  MenuItem,
  Grid,
  FormHelperText,
} from '@mui/material'
import {useEffect, useState, useCallback} from 'react'
import dayjs from 'dayjs'
import {useGetDepartments} from '../request/PostJobRequest.js'
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

const StatusOptions = [
  {
    label: 'Recruiting',
    value: 'Recruiting',
  },
  {
    label: 'Stop Recruiting',
    value: 'StopRecruiting',
  },
]

const EditPostJobForm = ({data, formErrors}) => {
  const [title, setTitle] = useState(data?.title || '')
  const [department, setDepartment] = useState(data?.departmentId || '')
  const [salary, setSalary] = useState(data?.salary || '')
  const [experienceYear, setExperienceYear] = useState(
    data?.experienceYear || '',
  )
  const [numberOfRecruits, setNumberOfRecruits] = useState(
    data?.numberOfRecruits || '',
  )
  const [type, setType] = useState(data?.type || '')
  const [expiryDate, setExpiryDate] = useState(
    data?.expiryDate ? dayjs(data.expiryDate, 'DD/MM/YYYY') : null,
  )
  const [description, setDescription] = useState(data?.description || '')
  const [requirements, setRequirements] = useState(data?.requirements || '')
  const [benefits, setBenefits] = useState(data?.benefits || '')
  const [status, setStatus] = useState(
    data?.status === '' ? 'Recruiting' : data?.status || 'Recruiting',
  )
  const [departmentList, setDepartmentList] = useState([])
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
  const {data: departmentData, isLoading, error} = useGetDepartments()
  useEffect(() => {
    if (isLoading) {
      console.log('Loading data...')
    } else if (error) {
      console.error('Error loading data:', error)
    } else if (departmentData) {
      setDepartmentList(departmentData?.data?.items)
    }
  }, [departmentData, isLoading, error])

  useEffect(() => {
    setTitle(data.title)
    setSalary(data.salary)
    setExperienceYear(data.experienceYear)
    setNumberOfRecruits(data.numberOfRecruits)
    setType(data.type)
    setDepartment(data.departmentId)
    setDescription(data.description)
    setRequirements(data.requirements)
    setBenefits(data.benefits)
    setStatus(data.status)
    if (data?.expiryDate) {
      setExpiryDate(dayjs(data.expiryDate, 'DD/MM/YYYY'))
    }
  }, [data])

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
      setSalary(value)
      if (!/^\d+$/.test(value)) {
        error = 'Salary must be a number'
      }
    }

    if (name === 'experienceYear') {
      setExperienceYear(value)
      if (!/^\d+$/.test(value)) {
        error = 'Experience Year must be a number'
      }
    }

    if (name === 'numberOfRecruits') {
      setNumberOfRecruits(value)
      if (!/^\d+$/.test(value)) {
        error = 'Number Of Recruits must be a number'
      }
    }
    if (name === 'description') {
      setDescription(value)
      if (value.length > 3000) {
        error = 'Description cannot be longer than 3000 characters'
      }
    }
    if (name === 'requirements') {
      setRequirements(value)
      if (!value) error = 'Requirements is required'
      if (value.length > 500) {
        error = 'Requirements cannot be longer than 500 characters'
      }
    }
    if (name === 'benefits') {
      setBenefits(value)
      if (!value) error = 'Benefits is required'
      if (value.length > 500) {
        error = 'Benefits cannot be longer than 500 characters'
      }
    }

    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: error,
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

  const handleDateChange = (setter) => (newValue) => {
    setter(newValue)
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

        <Grid item xs={4}>
          <FormControl fullWidth error={!!errors.status}>
            <InputLabel id="type-label">Status *</InputLabel>
            <Select
              labelId="type-label"
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
            <FormHelperText>{errors.status}</FormHelperText>
          </FormControl>
        </Grid>
        <Grid item xs={4}>
          <TextField
            name="salary"
            label="Salary *"
            value={salary}
            onChange={handleInputChange}
            fullWidth
            error={!!errors.salary}
            helperText={errors.salary}
          />
        </Grid>
        <Grid item xs={4}>
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
            value={numberOfRecruits}
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
            onChange={handleDateChange(setExpiryDate)}
            // onChange={(newValue) => {
            //   setExpiryDate(newValue)
            // }}
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
            // type="number"
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

export default EditPostJobForm
