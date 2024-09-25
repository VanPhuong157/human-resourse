import {AdapterDayjs} from '@mui/x-date-pickers/AdapterDayjs'
import {LocalizationProvider} from '@mui/x-date-pickers/LocalizationProvider'
import {
  TextField,
  Grid,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  FormHelperText,
} from '@mui/material'
import {DatePicker} from '@mui/x-date-pickers/DatePicker'
import {useState} from 'react'
import {useEffect} from 'react'
const AddUserFamilyForm = ({formErrors}) => {
  const [dateOfBirth, setDateOfBirth] = useState(null)
  const [formData, setFormData] = useState({
    fullname: '',
    relationship: '',
    job: '',
    phoneNumber: '',
  })
  const [errors, setErrors] = useState({
    fullname: '',
    relationship: '',
    job: '',
    phoneNumber: '',
  })

  useEffect(() => {
    if (formErrors) {
      setErrors((prevErrors) => ({
        ...prevErrors,
        ...formErrors,
      }))
    }
  }, [formErrors])
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

  const handleInputChange = (e) => {
    const {name, value} = e.target
    let error = ''

    if (name === 'fullname') {
      if (value.length < 6 || value.length > 100) {
        error = 'Fullname must be between 6 and 100 characters'
      }
    } else if (name === 'relationship') {
      if (value.length < 6 || value.length > 50) {
        error = 'Relationship must be between 6 and 50 characters'
      }
    } else if (name === 'job') {
      if (value.length > 100) {
        error = "Job can't be longer than 100 characters"
      }
    } else if (name === 'phoneNumber') {
      const phonePattern =
        /^(0|\+84)(3[2-9]|5[6|8|9]|7[0|6-9]|8[1-5]|9[0-9])[0-9]{7}$/
      if (value.trim() === '') {
        error = 'PhoneNumber is required'
      } else if (!phonePattern.test(value)) {
        error = 'Invalid phone number format'
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

  const handleDateChange = (newValue) => {
    setDateOfBirth(newValue)
    if (!newValue) {
      setErrors((prevErrors) => ({
        ...prevErrors,
        dateOfBirth: 'DateOfBirth is required',
      }))
    } else {
      setErrors((prevErrors) => ({
        ...prevErrors,
        dateOfBirth: '',
      }))
    }
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
            name="fullname"
            label="Fullname *"
            fullWidth
            value={formData.fullname}
            onChange={handleInputChange}
            error={!!errors.fullname}
            helperText={errors.fullname}
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
            label="DateOfBirth *"
            name="dateOfBirth"
            format="DD/MM/YYYY"
            value={dateOfBirth}
            onChange={handleDateChange}
            fullWidth
            renderInput={(props) => (
              <TextField
                {...props}
                fullWidth
                error={!!errors.dateOfBirth}
                helperText={errors.dateOfBirth}
              />
            )}
            slotProps={{textField: {fullWidth: true}}}
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            name="job"
            label="Job *"
            fullWidth
            value={formData.job}
            onChange={handleInputChange}
            error={!!errors.job}
            helperText={errors.job}
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            name="phoneNumber"
            label="PhoneNumber *"
            fullWidth
            value={formData.phoneNumber}
            onChange={handleInputChange}
            error={!!errors.phoneNumber}
            helperText={errors.phoneNumber}
          />
        </Grid>
      </Grid>
    </LocalizationProvider>
  )
}

export default AddUserFamilyForm
