import {AdapterDayjs} from '@mui/x-date-pickers/AdapterDayjs'
import {LocalizationProvider} from '@mui/x-date-pickers/LocalizationProvider'
import {
  TextField,
  Grid,
  MenuItem,
  Select,
  InputLabel,
  FormControl,
  FormHelperText,
} from '@mui/material'
import {DatePicker} from '@mui/x-date-pickers/DatePicker'
import {useEffect, useState} from 'react'
import dayjs from 'dayjs'

const EditUserFamilyForm = ({data, formErrors}) => {
  const [dateOfBirth, setDateOfBirth] = useState(dayjs(data?.dateOfBirth, 'DD/MM/YYYY'))
  const [formData, setFormData] = useState({
    memberId: data.id,
    fullName: data.fullName,
    relationship: data.relationship,
    job: data.job,
    phoneNumber: data.phoneNumber,
  })
  const [errors, setErrors] = useState({
    fullName: '',
    relationship: '',
    job: '',
    phoneNumber: '',
  })
  const RelationshipOptions = [
    {
      label: 'Father',
      value: 'Father',
    },
    {
      label: 'Mother',
      value: 'Mother',
    },
    {
      label: 'Wife',
      value: 'Wife',
    },
    {
      label: 'Husband',
      value: 'Husband',
    },
    {
      label: 'Child',
      value: 'Child',
    },
    {
      label: 'Brother',
      value: 'Brother',
    },
    {
      label: 'Sister',
      value: 'Sister',
    },
  ]
  useEffect(() => {
    setFormData({
      memberId: data.id,
      fullName: data.fullName,
      relationship: data.relationship,
      job: data.job,
      phoneNumber: data.phoneNumber,
    })
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

    if (name === 'fullName') {
      if (value.length < 6 || value.length > 100) {
        error = 'Fullname must be between 6 and 100 characters'
      }
    } else if (name === 'relationship') {
      if (value.length < 6 || value.length > 50) {
        error = 'Relationship must be between 6 and 50 characters'
      }
    } else if (name === 'job') {
      if (value.length > 100) {
        error = 'Job must not exceed 100 characters'
      }
    } else if (name === 'phoneNumber') {
      if (value.trim() === '') {
        error = 'PhoneNumber is required'
      }
    }

    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }))

    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: error,
    }))
  }

  const handleRelationshipChange = (event) => {
    const {value} = event.target
    setFormData((prevData) => ({
      ...prevData,
      relationship: value,
    }))
    setErrors((prevErrors) => ({
      ...prevErrors,
      relationship: value ? '' : 'Relationship is required',
    }))
  }

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <TextField
            name="fullName"
            label="FullName"
            value={formData.fullName}
            fullWidth
            onChange={handleInputChange}
            error={!!errors.fullName}
            helperText={errors.fullName}
          />
        </Grid>
        <Grid item xs={12}>
          <FormControl fullWidth error={!!errors.relationship}>
            <InputLabel id="relationship-label">Relationship *</InputLabel>
            <Select
              labelId="relationship-label"
              name="relationship"
              label="Relationship"
              value={formData.relationship}
              onChange={handleRelationshipChange}
            >
              {RelationshipOptions.map((RelationshipOption) => (
                <MenuItem
                  key={RelationshipOption.value}
                  value={RelationshipOption.value}
                >
                  {RelationshipOption.label}
                </MenuItem>
              ))}
            </Select>
            <FormHelperText>{errors.relationship}</FormHelperText>
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <DatePicker
            label="DateOfBirth"
            name="dateOfBirth"
            format="DD/MM/YYYY"
            value={dateOfBirth}
            onChange={(newValue) => {
              setDateOfBirth(newValue)
            }}
            renderInput={(props) => <TextField {...props} fullWidth required />}
            slotProps={{textField: {fullWidth: true}}}
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            name="job"
            label="Job"
            value={formData.job}
            fullWidth
            onChange={handleInputChange}
            error={!!errors.job}
            helperText={errors.job}
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            name="phoneNumber"
            label="PhoneNumber"
            value={formData.phoneNumber}
            fullWidth
            onChange={handleInputChange}
            error={!!errors.phoneNumber}
            helperText={errors.phoneNumber}
          />
        </Grid>
      </Grid>
    </LocalizationProvider>
  )
}

export default EditUserFamilyForm
