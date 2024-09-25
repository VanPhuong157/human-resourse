import {
  TextField,
  Grid,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  FormHelperText,
} from '@mui/material'
import {useEffect, useState} from 'react'

const colors = [
  {value: '#3498db', label: 'Blue'},
  {value: '#e74c3c', label: 'Red'},
  {value: '#2ecc71', label: 'Green'},
  {value: '#f39c12', label: 'Yellow'},
]
const EditReasonForm = ({data, formErrors}) => {
  const [title, setTitle] = useState(data?.title || '')
  const [subTitle, setSubTitle] = useState(data?.subTitle || '')
  const [color, setColor] = useState(data?.color || '')
  const [content, setContent] = useState(data?.content || '')

  useEffect(() => {
    setTitle(data.title)
    setSubTitle(data.subTitle)
    setColor(data.color)
    setContent(data.content)
  }, [data])
  useEffect(() => {
    if (formErrors) {
      setErrors((prevErrors) => ({
        ...prevErrors,
        ...formErrors,
      }))
    }
  }, [formErrors])

  const [errors, setErrors] = useState({
    title: '',
    subTitle: '',
    color: '',
    content: '',
  })

  const handleInputChange = (e) => {
    const {name, value} = e.target
    let error = ''

    if (name === 'title') {
      setTitle(value)
      if (value.length > 200) {
        error = 'Title cannot exceed 200 characters.'
      }
    }

    if (name === 'subTitle') {
      setSubTitle(value)
      if (value.length > 200) {
        error = 'SubTitle cannot exceed 200 characters.'
      }
    }
    if (name === 'content') {
      setContent(value)
      if (value.length > 200) {
        error = 'Content cannot exceed 200 characters.'
      }
    }

    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: error,
    }))
  }
  const handleColorChange = (event) => {
    const {value} = event.target
    setColor(value)
    setErrors((prevErrors) => ({
      ...prevErrors,
      color: value ? '' : 'Color is required',
    }))
  }

  return (
    <Grid container spacing={2}>
      <Grid item xs={12}>
        <TextField
          name="title"
          label="Title"
          value={title}
          error={!!errors.title}
          helperText={errors.title}
          onChange={handleInputChange}
          fullWidth
        />
      </Grid>
      <Grid item xs={12}>
        <TextField
          name="subTitle"
          label="SubTitle"
          value={subTitle}
          error={!!errors.subTitle}
          helperText={errors.subTitle}
          onChange={handleInputChange}
          fullWidth
        />
      </Grid>

      <Grid item xs={12}>
        <FormControl fullWidth error={!!errors.color}>
          <InputLabel id="color-label">Color</InputLabel>
          <Select
            labelId="color-label"
            name="color"
            label="Color"
            value={color}
            onChange={handleColorChange}
          >
            {colors.map((colorOption) => (
              <MenuItem key={colorOption.value} value={colorOption.value}>
                {colorOption.label}
              </MenuItem>
            ))}
          </Select>
          <FormHelperText>{errors.color}</FormHelperText>
        </FormControl>
      </Grid>

      <Grid item xs={12}>
        <TextField
          name="content"
          label="Content"
          value={content}
          error={!!errors.content}
          helperText={errors.content}
          onChange={handleInputChange}
          fullWidth
        />
      </Grid>
    </Grid>
  )
}

export default EditReasonForm
