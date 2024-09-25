import {TextField, Grid} from '@mui/material'
import {useEffect, useState} from 'react'
const EditDepartmentForm = ({data, formErrors}) => {
  const [departmentName, setDepartmentName] = useState(data.name || '')
  const [description, setDescription] = useState(data.description || '')
  const [errors, setErrors] = useState({
    departmentName: '',
    description: '',
  })
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

    if (name === 'departmentName') {
      setDepartmentName(value)
      if (value.length > 50) {
        error = 'DepartmentName can not be longer than 50 characters.'
      }
    }

    if (name === 'description') {
      setDescription(value)
      if (value.length > 500) {
        error = 'Description can not be longer than 500 characters.'
      }
    }

    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: error,
    }))
  }
  return (
    <Grid container spacing={2}>
      <Grid item xs={12}>
        <TextField
          name="departmentName"
          label="Department Name *"
          value={departmentName}
          error={!!errors.departmentName}
          helperText={errors.departmentName}
          onChange={handleInputChange}
          fullWidth
        />
      </Grid>
      <Grid item xs={12}>
        <TextField
          name="description"
          label="Description"
          multiline
          rows={3}
          value={description}
          error={!!errors.description}
          helperText={errors.description}
          onChange={handleInputChange}
          fullWidth
        />
      </Grid>
    </Grid>
  )
}

export default EditDepartmentForm
