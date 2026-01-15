import React, {useEffect, useState, useContext} from 'react'
import logo from '../../../assets/logo.png'
import {
  TextField,
  Button,
  Box,
  Typography,
  Container,
  CircularProgress,
} from '@mui/material'
import useLogin from '../request/loginRequest'
import {Link, useNavigate} from 'react-router-dom'
import {UserContext} from '../../../context/UserContext'
import {showSuccess, showError} from '../../../components/notification'
import {createAuthCookie} from '../../../components/utils'
import Cookies from 'js-cookie'
import {StatusCodes} from 'http-status-codes'

const Login = (props) => {
  const {loginContext} = useContext(UserContext)
  const [username, setUserName] = useState('')
  const [password, setPassword] = useState('')
  const [isLoading, setIsLoading] = useState(false)
  const [isRedirecting, setIsRedirecting] = useState(false)
  const navigate = useNavigate()
  const [errors, setErrors] = useState({
    username: '',
    password: '',
  })

  // useEffect(()=>{
  //   let accessToken = localStorage.getItem("accessToken")
  //   if(accessToken){
  //       navigate("/home")
  //   }
  // },[navigate])
  const {mutateAsync} = useLogin()

  // Kiểm tra nếu người dùng đã đăng nhập và điều hướng ngay
  useEffect(() => {
    const checkAuth = async () => {
      const userAuth = Cookies.get('userAuth')
      if (userAuth) {
        setIsRedirecting(true)
        navigate('/dashboard')
      }
    }

    checkAuth()
  }, [navigate])
  if (isRedirecting) {
    return null
  }

  const handleInputChange = (e) => {
    const {name, value} = e.target
    let error = ''

    if (name === 'username') {
      setUserName(value)
      if (value.length < 6) {
        error = 'Username must be at least 6 characters long.'
      }
      if (value.length > 50) {
        error = 'Username cannot exceed 50 characters.'
      }
    }

    if (name === 'password') {
      setPassword(value)
      const passwordRegex =
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).+$/ // Regex kiểm tra chữ hoa, chữ thường, số và ký tự đặc biệt
      if (value.length < 8) {
        error =
          'The Password must be at least 8 characters long and a maximum of 16 characters long.'
      } else if (value.length > 16) {
        error =
          'The Password must be at least 8 characters long and a maximum of 16 characters long.'
      } else if (!passwordRegex.test(value)) {
        error =
          'The Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character (!@#$%^&*).'
      } else {
        error = '' // Không có lỗi, xóa thông báo lỗi
      }
    }

    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: error,
    }))
  }

  const handleLogin = async (e) => {
    e.preventDefault() // Ngăn chặn reload trang khi submit form
    setIsLoading(true)
    if (errors.username || errors.password || !username || !password) {
      showError('Please fix the errors before submitting the form.')
      setIsLoading(false) // Kết thúc tải sau khi hoàn thành API call
      return
    }
    const response = await mutateAsync({username, password})

    try {
      if (response && response.data.data.accessToken) {
        loginContext(username, response.data.data.accessToken)
        createAuthCookie(response.data.data.accessToken)
        localStorage.setItem('refreshToken', response.data.data.refreshToken)
        localStorage.setItem(
          'refreshTokenExpires',
          response.data.data.refreshTokenExpires,
        )
        showSuccess(response?.data?.message)
        navigate('/dashboard')
      }
    } catch (err) {
      if (err?.response?.status === StatusCodes.BAD_REQUEST) {
        console.log(response)
        const badRequestMessage =
          response?.data?.message || response?.data?.title
        showError({
          message: badRequestMessage,
        })
      }
      showError(response?.data?.message)
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
            <Typography component="h1" variant="h5">
              Welcome to Smart HR System
            </Typography>
            <Typography component="" variant="h7">
              Your new password must be different from previously used passwords
            </Typography>
          </Box>
          <Box component="form" onSubmit={handleLogin}>
            <TextField
              margin="normal"
              required
              fullWidth
              id="username"
              label="User Name"
              name="username"
              autoComplete="username"
              autoFocus
              value={username}
              error={!!errors.username}
              helperText={errors.username}
              onChange={handleInputChange}
            />
            <TextField
              margin="normal"
              required
              fullWidth
              name="password"
              label="Password"
              type="password"
              id="password"
              autoComplete="current-password"
              value={password}
              error={!!errors.password}
              helperText={errors.password}
              onChange={handleInputChange}
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
                'Login'
              )}
            </Button>

            <Link to="/forgetPassword"> Forgot password?</Link>
          </Box>
        </Box>
      </Container>
    </Box>
  )
}
export default Login
