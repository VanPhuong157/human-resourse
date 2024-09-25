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
import {useState, useEffect} from 'react'
import {styled} from '@mui/system'
import {useGetPosts} from '../../../../pages/recruitment/job/request/PostJobRequest'
import {StatusCodes} from 'http-status-codes'

const FileUploadButton = styled(Button)(({theme}) => ({
  borderRadius: '54px',
  border: '1px solid #ccc', // Add a border
  color: '#12448a',
  fontWeight: 200,
  width: '100%',
  justifyContent: 'flex-start',
  padding: '7px 12px',
  marginTop: '13px',
  backgroundColor: 'white', // Set background to white
  '&:hover': {
    backgroundColor: '#f5f5f5', // Lighten background on hover
  },
}))

const CustomAddCandidateDialog = ({open, onClose, refetch}) => {
  const [jobList, setJobList] = useState([])
  const [errors, setErrors] = useState({
    FullName: '',
    PhoneNumber: '',
    Email: '',
    CvFile: null,
    JobPostId: '',
  })
  const {data: dataJobPosts, isLoading, error} = useGetPosts(1, 1000)

  useEffect(() => {
    if (isLoading) {
      console.log('Loading data...')
    } else if (error) {
      console.error('Error loading data:', error)
    } else if (dataJobPosts) {
      setJobList(dataJobPosts?.data.items)
    }
  }, [dataJobPosts, isLoading, error])

  const [formData, setFormData] = useState({
    FullName: '',
    PhoneNumber: '',
    Email: '',
    CvFile: null,
    JobPostId: '',
  })

  const validateInput = (name, value) => {
    let error = ''

    if (name === 'FullName') {
      if (!value) error = 'Please enter your full name.'
      if (value.length > 50) {
        error = 'The Full Name must be between 1 and 50 characters long.'
      }
    }

    if (name === 'PhoneNumber') {
      const phoneNumberPattern =
        /^(0|\+84)(3[2-9]|5[689]|7[0|6-9]|8[1-5]|9[0-9])[0-9]{7}$/
      if (!value) {
        error = 'Please enter your phone number.'
      } else if (!phoneNumberPattern.test(value)) {
        error = 'Invalid phone number format.'
      }
    }

    if (name === 'Email') {
      const emailPattern = /^[^@\s]+@gmail\.com$/
      if (!value) {
        error = 'Please enter your email.'
      } else if (!emailPattern.test(value)) {
        error = 'Please enter a valid email address.'
      }
    }

    if (name === 'CvFile') {
      if (!value) {
        error = 'Please attach your CV file.'
      } else if (value.type !== 'application/pdf') {
        error = 'Only PDF files are allowed.'
      }
    }
    if (name === 'JobPostId') {
      if (!value) {
        error = 'Job is required'
      }
    }

    return error
  }

  const handleInputChange = (event) => {
    const {name, value} = event.target
    const error = validateInput(name, value)

    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }))
    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: error,
    }))
  }

  const handleFileUpload = (event) => {
    const file = event.target.files[0]
    const error = validateInput('CvFile', file)

    setFormData((prevData) => ({
      ...prevData,
      CvFile: file,
    }))
    setErrors((prevErrors) => ({
      ...prevErrors,
      CvFile: error,
    }))
  }

  const handleSubmit = async (event) => {
    event.preventDefault()

    const newErrors = {
      FullName: validateInput('FullName', formData.FullName),
      PhoneNumber: validateInput('PhoneNumber', formData.PhoneNumber),
      Email: validateInput('Email', formData.Email),
      CvFile: validateInput('CvFile', formData.CvFile),
      JobPostId: validateInput('JobPostId', formData.JobPostId),
    }

    setErrors(newErrors)

    if (Object.values(newErrors).some((error) => error !== '')) {
      return
    }
    //Submit FormData
    const formDataToSend = new FormData()
    formDataToSend.append('FullName', formData.FullName)
    formDataToSend.append('PhoneNumber', formData.PhoneNumber)
    formDataToSend.append('Email', formData.Email)
    formDataToSend.append('CvFile', formData.CvFile)
    formDataToSend.append('JobPostId', formData.JobPostId)
    try {
      const response = await rootApi.post(
        path.homepage.appylyCV(),
        formDataToSend,
        {
          headers: {
            'Content-Type': 'multipart/form-data',
          },
        },
      )
      if (response.data.code === StatusCodes.BAD_REQUEST) {
        showError(response.data.messsage)
        return
      }
      showSuccess({message: response.data?.message})
      refetch()
      onClose()
    } catch (error) {
      if (error?.response?.status === StatusCodes.BAD_REQUEST) {
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
              onClick={onClose}
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
              Add Candidate
            </Typography>
          </Box>
        </DialogTitle>

        {/* Dialog Apply CV */}
        <DialogContent>
          <form onSubmit={handleSubmit}>
            <Box display="flex" flexDirection="column" gap={2} mt={2}>
              {/* <Input> */}
              <TextField
                label="Full Name"
                name="FullName"
                value={formData.FullName}
                onChange={handleInputChange}
                error={!!errors.FullName}
                helperText={errors.FullName}
                fullWidth
              />
              <TextField
                label="Phone Number"
                name="PhoneNumber"
                value={formData.PhoneNumber}
                onChange={handleInputChange}
                error={!!errors.PhoneNumber}
                helperText={errors.PhoneNumber}
                fullWidth
              />
              <TextField
                label="Email"
                name="Email"
                type="email"
                value={formData.Email}
                onChange={handleInputChange}
                error={!!errors.Email}
                helperText={errors.Email}
                fullWidth
              />

              <FormControl fullWidth error={!!errors.JobPostId}>
                <InputLabel id="jobList-label">Job</InputLabel>
                <Select
                  labelId="jobList-label"
                  name="JobPostId"
                  label="Job"
                  value={formData.JobPostId}
                  onChange={handleInputChange}
                >
                  {jobList.map((jobListOption) => (
                    <MenuItem key={jobListOption.id} value={jobListOption.id}>
                      {jobListOption.title}
                    </MenuItem>
                  ))}
                </Select>
                <FormHelperText>{errors.JobPostId}</FormHelperText>
              </FormControl>

              {/* <FormControl fullWidth>
                <InputLabel id="jobPost-label">JobList</InputLabel>
                <SelectComponent
                  dataSelect={jobList}
                  label="jobList"
                  name="JobPostId"
                  value={jobList?.id}
                  onChange={handleInputChange}
                />
              </FormControl> */}
              {/* </Input> */}
              <Box mt={2}>
                <Typography variant="body1" fontStyle="italic">
                  Attach file CV
                </Typography>
                <input
                  accept=".pdf"
                  style={{display: 'none'}}
                  id="cv-file"
                  type="file"
                  onChange={handleFileUpload}
                />
                <label htmlFor="cv-file">
                  <FileUploadButton component="span">
                    {formData.CvFile ? formData.CvFile.name : 'Upload'}
                  </FileUploadButton>
                </label>
                {errors.CvFile && (
                  <Typography color="error" variant="caption" mt={1}>
                    {errors.CvFile}
                  </Typography>
                )}
              </Box>
            </Box>
          </form>
        </DialogContent>
        <DialogActions>
          <Button
            onClick={handleSubmit}
            variant="contained"
            disabled={isLoading}
            type="submit"
            sx={{
              textTransform: 'none',
              backgroundColor: (theme) => theme.palette.primary.main,
              color: (theme) => theme.palette.primary.contrastText,
              '&:hover': {
                backgroundColor: (theme) => theme.palette.primary.dark,
              },
            }}
          >
            Submit
          </Button>
        </DialogActions>
      </Dialog>
    </>
  )
}

export default CustomAddCandidateDialog
