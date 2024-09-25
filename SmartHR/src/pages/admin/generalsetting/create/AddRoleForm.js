import {
  TextField,
  Grid,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  FormHelperText,
} from '@mui/material'
import {useState} from 'react'
import {useEffect} from 'react'

const typeOptions = [
  // {
  //   id: 'Basic',
  //   value: 'Basic',
  // },
  {
    id: 'Custom',
    value: 'Custom',
  },
]

const AddRoleForm = ({formErrors}) => {
  const [name, setName] = useState('')
  const [description, setDescription] = useState('')
  const [type, setType] = useState('')

  const [errors, setErrors] = useState({
    name: '',
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

    if (name === 'name') {
      setName(value)
      if (value.length > 50) {
        error = 'RoleName can not be longer than 50 characters.'
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
  const handleTypeChange = (event) => {
    const {value} = event.target
    setType(value)
    setErrors((prevErrors) => ({
      ...prevErrors,
      type: value ? '' : 'Type is required',
    }))
  }
  return (
    <Grid container spacing={2}>
      <Grid item xs={12}>
        <TextField
          name="name"
          label="Role Name *"
          value={name}
          error={!!errors.name}
          helperText={errors.name}
          onChange={handleInputChange}
          fullWidth
        />
      </Grid>
      <Grid item xs={12}>
        <TextField
          name="description"
          label="Description"
          value={description}
          error={!!errors.description}
          helperText={errors.description}
          onChange={handleInputChange}
          fullWidth
        />
      </Grid>
      {/* <Grid item xs={12}>
        <FormControl required fullWidth>
          <InputLabel id="type-label">Type</InputLabel>
          <SelectComponent
            dataSelect={typeOptions}
            label="Type"
            name="type"
            value={type}
          />
        </FormControl>
        
      </Grid> */}
      <Grid item xs={12}>
        <FormControl fullWidth error={!!errors.type}>
          <InputLabel id="type-label">Type *</InputLabel>
          <Select
            labelId="type-label"
            name="type"
            label="Type"
            value={type}
            onChange={handleTypeChange}
          >
            {typeOptions.map((typeOption) => (
              <MenuItem key={typeOption.id} value={typeOption.id}>
                {typeOption.value}
              </MenuItem>
            ))}
          </Select>
          <FormHelperText>{errors.type}</FormHelperText>
        </FormControl>
      </Grid>
    </Grid>
  )
}

export default AddRoleForm
