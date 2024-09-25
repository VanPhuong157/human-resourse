import React, {useState, useContext} from 'react'
import {
  Box,
  Grid,
  TextField,
  IconButton,
  InputAdornment,
  Button,
  Typography,
  FormHelperText,
} from '@mui/material'
import Visibility from '@mui/icons-material/Visibility'
import VisibilityOff from '@mui/icons-material/VisibilityOff'
import {showSuccess, showError} from '../../../components/notification'
import useChangePassword from '../requests/changePassword'
import {UserContext} from '../../../context/UserContext'
import {StatusCodes} from 'http-status-codes'

const ChangePasswordComponent = () => {
  const [showPassword, setShowPassword] = useState({
    current: false,
    new: false,
    confirm: false,
  })

  const {user} = useContext(UserContext)

  const [passwords, setPasswords] = useState({
    currentPassword: '',
    newPassword: '',
    confirmPassword: '',
  })

  const [errors, setErrors] = useState({
    newPassword: '',
    confirmPassword: '',
  })

  const handleClickShowPassword = (field) => {
    setShowPassword((prev) => ({
      ...prev,
      [field]: !prev[field],
    }))
  }

  const handleChange = (event) => {
    const {name, value} = event.target
    setPasswords((prev) => ({
      ...prev,
      [name]: value,
    }))
    validatePassword(name, value)
  }

  const handleMouseDownPassword = (event) => {
    event.preventDefault()
  }

  const validatePassword = (field, value) => {
    let error = ''
    const passwordRegex =
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[~!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]).{8,16}$/

    if (field === 'newPassword' || field === 'confirmPassword') {
      if (!passwordRegex.test(value)) {
        error =
          'The password must be 8-16 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.'
      }
    }

    setErrors((prev) => ({
      ...prev,
      [field]: error,
    }))
  }

  const {mutateAsync, isLoading} = useChangePassword({
    userId: user.userId,
  })

  const handleSubmit = async () => {
    const {currentPassword, newPassword, confirmPassword} = passwords
    let valid = true

    if (currentPassword === '') {
      setErrors((prev) => ({
        ...prev,
        currentPassword: 'Old Password is required',
      }))
      valid = false
    } else {
      setErrors((prev) => ({...prev, currentPassword: ''}))
    }

    if (newPassword === '') {
      setErrors((prev) => ({...prev, newPassword: 'New Password is required'}))
      valid = false
    } else if (errors.newPassword) {
      valid = false
    } else {
      setErrors((prev) => ({...prev, newPassword: ''}))
    }

    if (confirmPassword === '') {
      setErrors((prev) => ({
        ...prev,
        confirmPassword: 'Confirm New Password is required',
      }))
      valid = false
    } else if (newPassword !== confirmPassword) {
      setErrors((prev) => ({
        ...prev,
        confirmPassword: 'New Password and Confirm New Password do not match',
      }))
      valid = false
    } else {
      setErrors((prev) => ({...prev, confirmPassword: ''}))
    }

    if (currentPassword === newPassword) {
      showError('New Password cannot be the same as the Old Password')
      valid = false
    }

    if (valid) {
      const dataRequest = {
        oldPassword: currentPassword,
        newPassword: newPassword,
        confirmPassword: confirmPassword,
      }
      try {
        const response = await mutateAsync(dataRequest)
        if (response.data?.code === StatusCodes.OK) {
          showSuccess({message: response.data?.message})
        } else if (response.data?.code === StatusCodes.BAD_REQUEST) {
          showError({message: response.data?.message})
        }
      } catch (err) {
        showError(err.response?.data?.message || 'An error occurred')
      }
    }
  }

  return (
    <Grid container spacing={2}>
      <Grid item xs={12} md={4} sx={{marginTop: '46px', marginLeft: '85px'}}>
        {['currentPassword', 'newPassword', 'confirmPassword'].map((field) => (
          <Box key={field} mb={2}>
            <TextField
              fullWidth
              variant="outlined"
              label={
                field === 'currentPassword'
                  ? 'Old Password'
                  : field === 'newPassword'
                    ? 'New Password'
                    : 'Confirm New Password'
              }
              type={
                showPassword[field.split('Password')[0]] ? 'text' : 'password'
              }
              name={field}
              value={passwords[field]}
              onChange={handleChange}
              InputProps={{
                endAdornment: (
                  <InputAdornment position="end">
                    <IconButton
                      aria-label={`toggle ${field} visibility`}
                      onClick={() =>
                        handleClickShowPassword(field.split('Password')[0])
                      }
                      onMouseDown={handleMouseDownPassword}
                      edge="end"
                    >
                      {showPassword[field.split('Password')[0]] ? (
                        <Visibility />
                      ) : (
                        <VisibilityOff />
                      )}
                    </IconButton>
                  </InputAdornment>
                ),
              }}
              margin="normal"
            />
            <FormHelperText error>{errors[field]}</FormHelperText>
          </Box>
        ))}
        <Box mt={2}>
          <Button
            variant="contained"
            sx={{bgcolor: 'black', color: 'white'}}
            onClick={handleSubmit}
            disabled={isLoading}
          >
            Save
          </Button>
        </Box>
      </Grid>
      <Grid
        item
        xs={12}
        md={7}
        display="flex"
        justifyContent="center"
        alignItems="center"
      >
        <Box
          sx={{
            backgroundColor: '#FFF06A',
            padding: 2,
            borderRadius: 4,
          }}
        >
          <Typography sx={{color: 'black'}}>
            Contain characters from three of the following four categories:
          </Typography>
          <Typography sx={{color: 'black'}}>
            I. English uppercase characters (A through Z)
          </Typography>
          <Typography sx={{color: 'black'}}>
            II. English lowercase characters (a through z)
          </Typography>
          <Typography sx={{color: 'black'}}>
            III. Base 10 digits (0 through 9)
          </Typography>
          <Typography sx={{color: 'black'}}>
            IV. Non-alphabetic characters (&#96;~!@#$%^&*()_+-={}
            []:";'&lt;&gt;?,./&#96;)
          </Typography>
        </Box>
      </Grid>
    </Grid>
  )
}

export default ChangePasswordComponent
