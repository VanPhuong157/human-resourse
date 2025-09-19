import React, {useState} from 'react'
import {
  TextField,
  Button,
  Box,
  Typography,
  Container,
  CircularProgress,
} from '@mui/material'
import logo from '../../../assets/logo.png'
import {Link} from 'react-router-dom'
import useForgotPassword from '../request/forgotPasswordRequest'
import {showError, showSuccess} from '../../../components/notification'
import {StatusCodes} from 'http-status-codes'

const ForgetPassword = (props) => {
  const [isLoading, setIsLoading] = useState(false)
  const [email, setEmail] = useState('')
  const [emailError, setEmailError] = useState('')

  const {data, mutateAsync} = useForgotPassword({
    email: email,
  })

  const handleEmailChange = (e) => {
    const value = e.target.value
    setEmail(value)

    // Kiểm tra tính hợp lệ của email
    const emailRegex = /^[^@\s]+@gmail\.com$/
    if (!emailRegex.test(value)) {
      setEmailError('Invalid email format')
    } else {
      setEmailError('')
    }
  }

  const handleForgetPassword = async (e) => {
    e.preventDefault() // Ngăn chặn reload trang khi submit form
    setIsLoading(true)
    try {
      // Gọi API login và xử lý kết quả
      const response = await mutateAsync({email})
      console.log(response?.data?.code)
      if (response?.data?.code === StatusCodes.NOT_FOUND) {
        const badRequestMessage = response?.data?.message
        console.log('bad', badRequestMessage)
        showError({
          message: badRequestMessage,
        })
      }
      if (
        response &&
        response?.data &&
        response?.data?.code === StatusCodes.OK
      ) {
        showSuccess(response.data.message)
      }
    } catch (err) {
      const responseData = err?.response?.data
      let message = ''

      if (responseData) {
        if (typeof responseData === 'string') {
          message = responseData
        } else if (typeof responseData?.message === 'string') {
          message = responseData.message
        }
      }

      if (!message) {
        message = err?.message || 'An unexpected error occurred.'
        console.error('Unexpected error shape in ForgetPassword:', err)
      }

      showError(message)
    } finally {
      setIsLoading(false) // Kết thúc tải sau khi hoàn thành API call
    }
  }

  return (
    <Box
      sx={{
        backgroundColor: '#F4F5FA',
        minHeight: '98vh',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        padding: 0,
        margin: 0,
        overflow: 'hidden',
      }}
    >
      <Container
        maxWidth="xs"
        component="main"
        sx={{backgroundColor: 'white', borderRadius: 1, boxShadow: 3, p: 3}}
      >
        <Box
          sx={{
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
            justifyContent: 'center',
          }}
        >
          <Box sx={{width: 200, height: 200}}>
            <img
              src={logo}
              alt="Logo"
              style={{width: '100%', height: '100%'}}
            />
          </Box>
          <Box>
            <Typography component="h1" variant="h5" align="left" sx={{mb: 1}}>
              Forgot Password 🔒
            </Typography>
            <Typography component="" variant="h7" align="left">
              Enter your email and we'll send you instructions to reset your
              password
            </Typography>
          </Box>
          <Box component="form" onSubmit={handleForgetPassword}>
            <TextField
              margin="normal"
              required
              fullWidth
              id="username"
              label="Email"
              name="email"
              type="email"
              autoComplete="email"
              autoFocus
              value={email}
              onChange={handleEmailChange}
              error={!!emailError}
              helperText={emailError}
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{mt: 3, mb: 2, backgroundColor: '#16171A', color: 'white'}}
              disabled={isLoading} // Disable nút khi đang tải
            >
              {isLoading ? (
                <CircularProgress size={24} color="inherit" />
              ) : (
                'Submit'
              )}
            </Button>
            <Link to="/login"> Back To Login</Link>
          </Box>
        </Box>
      </Container>
    </Box>
  )
}
export default ForgetPassword
