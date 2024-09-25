import {
  Box,
  Typography,
  DialogContent,
  Dialog,
  DialogTitle,
  IconButton,
  TextField,
  DialogActions,
  Button,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  FormHelperText,
} from '@mui/material'
import {showSuccess, showError} from '../../../../components/notification'
import {rootApi} from '../../../../api/rootApi'
import CloseIcon from '@mui/icons-material/Close'
import path from '../../../../api/path'
import {useEffect, useState} from 'react'
import {styled} from '@mui/system'
import {StatusCodes} from 'http-status-codes'

const FileUploadButton = styled(Button)(({theme}) => ({
  borderRadius: '54px',
  border: '1px solid #ccc',
  color: '#12448a',
  fontWeight: 200,
  width: '100%',
  justifyContent: 'flex-start',
  padding: '7px 12px',
  marginTop: '13px',
  backgroundColor: 'white',
  '&:hover': {
    backgroundColor: '#f5f5f5',
  },
}))

const JobPostOptions = [
  {
    label: 'Active',
    value: 'Active',
  },
  {
    label: 'Deactive',
    value: 'Deactive',
  },
]

const CustomEditVersionHomePage = ({
  open,
  onClose,
  fetchData,
  dataVersionHomePage,
}) => {
  const [initialized, setInitialized] = useState(false)
  const [initialData, setInitialData] = useState(null)
  const [hasChanges, setHasChanges] = useState(false)

  const [title, setTitle] = useState('')
  const [titleBody, setTitleBody] = useState('')
  const [address, setAddress] = useState('')
  const [email, setEmail] = useState('')
  const [statusJobPost, setStatusJobPost] = useState('')
  const [phoneNumber, setPhoneNumber] = useState('')
  const [imageBackground, setImageBackground] = useState(null)

  useEffect(() => {
    if (!initialized && dataVersionHomePage?.data) {
      const data = dataVersionHomePage?.data
      setTitle(data?.title)
      setTitleBody(data?.titleBody)
      setAddress(data?.address)
      setEmail(data?.email)
      setStatusJobPost(data?.statusJobPost)
      setPhoneNumber(data?.phoneNumber)
      setImageBackground(data?.imageBackgroundDetail)

      setInitialData({
        title: data?.title,
        titleBody: data.titleBody,
        address: data.address,
        email: data.email,
        statusJobPost: data.statusJobPost,
        phoneNumber: data.phoneNumber,
        imageBackground: data.imageBackgroundDetail,
      })

      setInitialized(true)
    }
  }, [dataVersionHomePage, initialized])

  const [errors, setErrors] = useState({
    Title: '',
    TitleBody: '',
    Address: '',
    Email: '',
    ImageBackground: '',
    StatusJobPost: '',
  })

  const validateInput = (name, value) => {
    let error = ''
    switch (name) {
      case 'Title':
        if (!value) error = 'Title is required.'
        if (value.length > 2000) error = 'Title cannot exceed 2000 characters.'
        break
      case 'TitleBody':
        if (!value) error = 'TitleBody is required.'
        if (value.length > 2000)
          error = 'TitleBody cannot exceed 2000 characters.'
        break
      case 'Address':
        if (!value) error = 'Address is required.'
        if (value.length > 200) error = 'Address cannot exceed 200 characters.'
        break
      case 'PhoneNumber':
        const phoneNumberPattern =
          /^(0|\+84)(3[2-9]|5[689]|7[0|6-9]|8[1-5]|9[0-9])[0-9]{7}$/
        if (!value) error = 'PhoneNumber is required.'
        else if (!phoneNumberPattern.test(value))
          error = 'Invalid phone number format'
        break
      case 'Email':
        const emailPattern = /^[^@\s]+@gmail\.com$/
        if (!value) error = 'Please enter your email.'
        else if (!emailPattern.test(value))
          error = 'Please enter a valid email address.'
        break
      case 'StatusJobPost':
        if (!value) error = 'StatusJobPost is required.'
        break
      default:
        break
    }
    return error
  }

  const handleClose = () => {
    setErrors({})
    onClose()
  }

  const checkForChanges = (name, value) => {
    if (initialData) {
      if (name === 'ImageBackground') {
        return value !== initialData.imageBackground
      } else {
        return value !== initialData[name.toLowerCase()]
      }
    }
    return true
  }

  const handleInputChange = (e) => {
    const {name, value} = e.target
    const error = validateInput(name, value)
    setHasChanges(checkForChanges(name, value))

    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: error,
    }))
  }

  const handleFileUpload = (event) => {
    const file = event.target.files[0]

    if (file && file.type !== 'image/jpeg') {
      setErrors((prevErrors) => ({
        ...prevErrors,
        ImageBackground: 'Only .JPG files are allowed.',
      }))
      setImageBackground(null) // Reset the file state if invalid
      setHasChanges(false)
      return
    }

    setImageBackground(file)
    setErrors((prevErrors) => ({
      ...prevErrors,
      ImageBackground: '', // Clear error on valid file
    }))
    setHasChanges(checkForChanges('ImageBackground', file))
  }

  const handleSubmit = async (event) => {
    event.preventDefault()

    const newErrors = {
      Title: validateInput('Title', title),
      PhoneNumber: validateInput('PhoneNumber', phoneNumber),
      Email: validateInput('Email', email),
      TitleBody: validateInput('TitleBody', titleBody),
      Address: validateInput('Address', address),
      StatusJobPost: validateInput('StatusJobPost', statusJobPost),
    }
    setErrors(newErrors)

    if (Object.values(newErrors).some((error) => error !== '')) {
      return
    }

    const formDataToSend = new FormData()
    formDataToSend.append('Title', title)
    formDataToSend.append('TitleBody', titleBody)
    formDataToSend.append('Address', address)
    formDataToSend.append('Email', email)
    formDataToSend.append('StatusJobPost', statusJobPost)
    formDataToSend.append('PhoneNumber', phoneNumber)
    formDataToSend.append(
      'ImageBackground',
      imageBackground || dataVersionHomePage?.data?.imageBackgroundDetail,
    )

    try {
      const response = await rootApi.post(
        path.homepage.homePageSetting(),
        formDataToSend,
        {
          headers: {
            'Content-Type': 'multipart/form-data',
          },
        },
      )
      showSuccess({message: response.data?.message})
      fetchData()
      onClose()
    } catch (error) {
      if (error.response?.status === StatusCodes.BAD_REQUEST) {
        const badRequestMessage = error.response?.data?.title
        showError({
          message: badRequestMessage,
        })
      } else {
        showError(error.response?.data?.message)
      }
    }
  }

  return (
    <>
      <Dialog
        open={open}
        onClose={onClose}
        aria-labelledby="application-form-dialog"
        maxWidth="sm"
        fullWidth
      >
        <DialogTitle id="application-form-dialog">
          <Box display="flex" justifyContent="flex-end">
            <IconButton
              aria-label="close"
              onClick={handleClose}
              sx={{
                position: 'absolute',
                right: 8,
                top: 8,
                color: (theme) => theme.palette.grey[500],
              }}
            >
              <CloseIcon />
            </IconButton>
          </Box>
          <Box display="flex">
            <Typography
              variant="h4"
              component="h1"
              sx={{
                marginTop: '6px',
              }}
            >
              Edit Version
            </Typography>
          </Box>
        </DialogTitle>

        <DialogContent>
          <form onSubmit={handleSubmit}>
            <Box display="flex" flexDirection="column" gap={2} mt={2}>
              <TextField
                required
                label="Title"
                name="Title"
                value={title}
                error={!!errors.Title}
                helperText={errors.Title}
                onChange={(e) => {
                  setTitle(e.target.value)
                  handleInputChange(e)
                }}
                InputLabelProps={{shrink: true}}
              />

              <TextField
                required
                label="TitleBody"
                name="TitleBody"
                value={titleBody}
                error={!!errors.TitleBody}
                helperText={errors.TitleBody}
                onChange={(e) => {
                  setTitleBody(e.target.value)
                  handleInputChange(e)
                }}
                InputLabelProps={{shrink: true}}
              />

              <TextField
                required
                label="Address"
                name="Address"
                value={address}
                error={!!errors.Address}
                helperText={errors.Address}
                onChange={(e) => {
                  setAddress(e.target.value)
                  handleInputChange(e)
                }}
                InputLabelProps={{shrink: true}}
              />

              <TextField
                required
                label="Email"
                name="Email"
                value={email}
                error={!!errors.Email}
                helperText={errors.Email}
                onChange={(e) => {
                  setEmail(e.target.value)
                  handleInputChange(e)
                }}
                InputLabelProps={{shrink: true}}
              />

              <TextField
                required
                label="PhoneNumber"
                name="PhoneNumber"
                value={phoneNumber}
                error={!!errors.PhoneNumber}
                helperText={errors.PhoneNumber}
                onChange={(e) => {
                  setPhoneNumber(e.target.value)
                  handleInputChange(e)
                }}
                InputLabelProps={{shrink: true}}
              />

              <FormControl required error={!!errors.StatusJobPost} fullWidth>
                <InputLabel id="status-job-post-label">
                  Status Job Post
                </InputLabel>
                <Select
                  labelId="status-job-post-label"
                  id="status-job-post"
                  name="StatusJobPost"
                  value={statusJobPost}
                  label="Status Job Post"
                  onChange={(e) => {
                    setStatusJobPost(e.target.value)
                    handleInputChange(e)
                  }}
                >
                  {JobPostOptions.map((option) => (
                    <MenuItem key={option.value} value={option.value}>
                      {option.label}
                    </MenuItem>
                  ))}
                </Select>
                <FormHelperText>{errors.StatusJobPost}</FormHelperText>
              </FormControl>

              <Box display="flex" flexDirection="column">
                <FileUploadButton
                  variant="outlined"
                  component="label"
                  error={!!errors.ImageBackground}
                  helperText={errors.ImageBackground}
                >
                  {imageBackground
                    ? imageBackground instanceof File
                      ? imageBackground.name
                      : imageBackground
                    : 'Upload Image'}
                  <input type="file" hidden onChange={handleFileUpload} />
                </FileUploadButton>
                <FormHelperText error={!!errors.ImageBackground}>
                  {errors.ImageBackground}
                </FormHelperText>
              </Box>
            </Box>
          </form>
        </DialogContent>

        <DialogActions>
          <Button
            onClick={handleSubmit}
            variant="contained"
            color="primary"
            disabled={
              !hasChanges || Object.values(errors).some((error) => error !== '')
            }
          >
            Save
          </Button>
        </DialogActions>
      </Dialog>
    </>
  )
}

export default CustomEditVersionHomePage
