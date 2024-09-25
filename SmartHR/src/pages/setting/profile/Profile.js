import React, {useEffect, useState, useContext} from 'react'
import {
  Grid,
  Box,
  Avatar,
  Button,
  TextField,
  MenuItem,
  Typography,
} from '@mui/material'
import PhotoCamera from '@mui/icons-material/PhotoCamera'
import useGetUserInformation from '../requests/getUserProfile'
import {UserContext} from '../../../context/UserContext'
import {showError, showSuccess} from '../../../components/notification'
import {rootApi, baseUrl} from '../../../api/rootApi'
import path from '../../../api/path'
import PersonRoundedIcon from '@mui/icons-material/PersonRounded'
import {useNavigate} from 'react-router-dom'
import useGetUserPermissions from '../../../pages/admin/requests/getUserPermissions'
import {format} from 'date-fns'
import {DatePicker} from '@mui/x-date-pickers/DatePicker'
import dayjs from 'dayjs'
import {AdapterDayjs} from '@mui/x-date-pickers/AdapterDayjs'
import {LocalizationProvider} from '@mui/x-date-pickers/LocalizationProvider'

const ProfilePage = () => {
  const {user} = useContext(UserContext)
  const userId = user.userId
  const [originalProfile, setOriginalProfile] = useState(null)
  const navigate = useNavigate()
  const [permissionData, setPermissionData] = useState([])
  const {data: permissions} = useGetUserPermissions({
    userId,
    pageIndex: 1,
    pageSize: 1000,
  })
  const [errors, setErrors] = useState({
    FullName: '',
    Gender: '',
    Email: '',
    Code: '',
    roleName: '',
    IdCardNo: '',
    PhoneNumber: '',
    DateOfBirth: '',
    Address: '',
  })
  const hasDetailEmployeeInformation = permissionData.includes(
    'EmployeeInformation:Detail',
  )

  const {data: userInfo, refetch: refetchUserInfo} = useGetUserInformation({
    userId: userId,
  })

  const [profile, setProfile] = useState({
    FullName: '',
    Gender: '',
    Email: '',
    Code: '',
    roleName: '',
    IdCardNo: '',
    PhoneNumber: '',
    DateOfBirth: '',
    Address: '',
  })
  const [imagePreview, setImagePreview] = useState({
    ImageFile: null,
    ImagePreviewUrl: baseUrl + `/api/UserInformations/download-image/${userId}`,
  })

  const validateInput = (name, value) => {
    let error = ''

    if (name === 'FullName') {
      if (!value) error = 'FullName is required.'
      if (value.length > 100) {
        error = 'FullName cannot be longer than 100 characters'
      }
      if (value.length < 6) {
        error = 'FullName cannot be shorter than 6 characters'
      }
    }
    if (name === 'Gender') {
      if (!value) error = 'Gender is required'
    }
    if (name === 'Email') {
      const emailPattern = /^[^@\s]+@gmail\.com$/
      if (!value) {
        error = 'Email is required'
      } else if (!emailPattern.test(value)) {
        error = 'Invalid email format.'
      }
    }
    if (name === 'IdCardNo') {
      const IdCardNoPattern = /^\d{9}|\d{12}$/
      if (!value) {
        error = 'IdCardNo is required'
      } else if (!IdCardNoPattern.test(value)) {
        error = 'Invalid IdCardNo format.'
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

    return error
  }

  useEffect(() => {
    if (permissions) {
      const newPerm = permissions?.data?.items.map((perm) => perm.name)
      setPermissionData(newPerm)
    }
    if (userInfo) {
      const newProfile = {
        FullName: userInfo.data.fullName || '',
        Gender: userInfo.data.gender || '',
        Email: userInfo.data.email || '',
        Code: userInfo.data.code || '',
        roleName: userInfo.data.roleName || '',
        IdCardNo: userInfo.data.idCardNo || '',
        PhoneNumber: userInfo.data.phoneNumber || '',
        DateOfBirth: userInfo.data.dateOfBirth
          ? dayjs(userInfo.data.dateOfBirth, 'DD-MM-YYYY').format('YYYY-MM-DD')
          : '',
        Address: userInfo.data.address || '',
      }
      setProfile(newProfile)
      setOriginalProfile(newProfile)
    }
  }, [userInfo, refetchUserInfo, permissions])
  const handleFileChange = (e) => {
    const file = e.target.files[0]
    if (file) {
      const reader = new FileReader()

      reader.onloadend = () => {
        setImagePreview({
          ImageFile: file,
          ImagePreviewUrl: reader.result,
        })
      }

      reader.readAsDataURL(file)
    }
  }

  const handleDateChange = (newValue) => {
    if (newValue) {
      const formattedDate = dayjs(newValue).format('YYYY-MM-DD') // Use DD-MM-YYYY if that's the expected format
      setProfile((prevProfile) => ({
        ...prevProfile,
        DateOfBirth: formattedDate,
      }))
    } else {
      setProfile((prevProfile) => ({
        ...prevProfile,
        DateOfBirth: '',
      }))
    }
  }

  const handleSaveChange = async (event) => {
    event.preventDefault()

    const newErrors = {
      FullName: validateInput('FullName', profile.FullName),
      Gender: validateInput('Gender', profile.Gender),
      Email: validateInput('Email', profile.Email),
      IdCardNo: validateInput('IdCardNo', profile.IdCardNo),
      PhoneNumber: validateInput('PhoneNumber', profile.PhoneNumber),
    }
    setErrors(newErrors)

    if (Object.values(newErrors).some((error) => error !== '')) {
      return
    }

    const isProfileChanged = Object.keys(profile).some(
      (key) => profile[key] !== originalProfile[key],
    )

    if (!isProfileChanged && !imagePreview.ImageFile) {
      showError({message: 'There are no changes to save data.'})
      return
    }

    const formDataToSend = new FormData()
    formDataToSend.append('FullName', profile.FullName)
    formDataToSend.append('Gender', profile.Gender)
    formDataToSend.append('Email', profile.Email)
    formDataToSend.append('IdCardNo', profile.IdCardNo)
    formDataToSend.append('PhoneNumber', profile.PhoneNumber)
    formDataToSend.append('Address', profile.Address)
    if (imagePreview.ImageFile && imagePreview.ImagePreviewUrl) {
      formDataToSend.append('ImageFile', imagePreview.ImageFile)
    }
    if (profile.DateOfBirth) {
      formDataToSend.append('DateOfBirth', profile.DateOfBirth)
    }

    try {
      const response = await rootApi.put(
        path.user.editUserInformation({
          userId: userId,
        }),
        formDataToSend,
        {
          headers: {
            'Content-Type': 'multipart/form-data',
          },
        },
      )
      showSuccess({message: response.data?.message})
      await refetchUserInfo()
      setOriginalProfile(profile)
    } catch (error) {
      if (error.response?.status === '') {
        const badRequestMessage = error.response?.data?.message
        showError({
          message: badRequestMessage,
        })
      } else {
        showError(error.response?.data?.message)
      }
    }
  }

  const handleChange = (e) => {
    const {name, value} = e.target
    const error = validateInput(name, value)
    setProfile((prevProfile) => ({
      ...prevProfile,
      [name]: value,
    }))
    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: error,
    }))
  }

  const handleCancel = async () => {
    await refetchUserInfo()
  }

  const handleViewDetail = () => {
    navigate(`/user/detail/${userId}`)
  }
  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Box sx={{flexGrow: 1, p: 3}}>
        <Grid container spacing={2} alignItems="center">
          <Grid
            item
            xs={12}
            container
            spacing={2}
            ml={5}
            alignItems="center"
            mb={4.5}
          >
            <Grid item>
              <Avatar
                alt="Profile Picture"
                src={
                  imagePreview.ImagePreviewUrl ||
                  'https://img.freepik.com/premium-vector/default-avatar-profile-icon-social-media-user-image-gray-avatar-icon-blank-profile-silhouette-vector-illustration_561158-3467.jpg'
                }
                sx={{width: 160, height: 160}}
              />
            </Grid>
            <Grid item>
              <Button
                variant="contained"
                component="label"
                startIcon={<PhotoCamera />}
                sx={{ml: 4}}
              >
                Upload new photo
                <input
                  hidden
                  accept="image/*"
                  type="file"
                  onChange={handleFileChange}
                />
              </Button>
              {hasDetailEmployeeInformation && (
                <Button
                  variant="contained"
                  onClick={handleViewDetail}
                  startIcon={<PersonRoundedIcon />}
                  sx={{ml: 4}}
                >
                  View Detail Personal
                </Button>
              )}
              <Typography variant="body2" sx={{mt: 2.5, ml: 4}}>
                Allowed JPG, GIF or PNG. Max size of 800K
              </Typography>
            </Grid>
            <Grid
              item
              xs={1.5}
              container
              spacing={2}
              alignItems="center"
              ml={50}
            >
              <Grid item>
                <Typography variant="subtitle1">Code:</Typography>
              </Grid>
              <Grid item xs>
                <TextField
                  fullWidth
                  variant="outlined"
                  name="Code"
                  value={profile.Code}
                  InputProps={{
                    readOnly: true,
                  }}
                  disabled="true"
                />
              </Grid>
            </Grid>
          </Grid>
          <Grid item xs={12}>
            <Grid container spacing={2}>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Full Name"
                  variant="outlined"
                  name="FullName"
                  value={profile.FullName}
                  error={!!errors.FullName}
                  helperText={errors.FullName}
                  onChange={handleChange}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  select
                  fullWidth
                  label="Gender"
                  variant="outlined"
                  name="Gender"
                  value={profile.Gender}
                  error={!!errors.Gender}
                  helperText={errors.Gender}
                  onChange={handleChange}
                  SelectProps={{
                    displayEmpty: true,
                    sx: {textAlign: 'left'},
                  }}
                >
                  <MenuItem value="Male">Male</MenuItem>
                  <MenuItem value="Female">Female</MenuItem>
                  <MenuItem value="Other">Other</MenuItem>
                </TextField>
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Email"
                  variant="outlined"
                  name="Email"
                  value={profile.Email}
                  error={!!errors.Email}
                  helperText={errors.Email}
                  onChange={handleChange}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Role"
                  variant="outlined"
                  name="roleName"
                  value={profile.roleName}
                  InputProps={{
                    readOnly: true,
                  }}
                  disabled="true"
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="ID Card No"
                  variant="outlined"
                  name="IdCardNo"
                  error={!!errors.IdCardNo}
                  helperText={errors.IdCardNo}
                  value={profile.IdCardNo}
                  onChange={handleChange}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Phone Number"
                  variant="outlined"
                  name="PhoneNumber"
                  value={profile.PhoneNumber}
                  error={!!errors.PhoneNumber}
                  helperText={errors.PhoneNumber}
                  onChange={handleChange}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <DatePicker
                  label="Date of Birth"
                  name="DateOfBirth"
                  format="DD/MM/YYYY"
                  value={
                    profile.DateOfBirth
                      ? dayjs(profile.DateOfBirth, 'YYYY-MM-DD')
                      : null
                  }
                  onChange={handleDateChange}
                  fullWidth
                  slotProps={{
                    field: {clearable: true},
                    textField: {fullWidth: true},
                  }}
                  renderInput={(props) => <TextField {...props} fullWidth />}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Address"
                  variant="outlined"
                  name="Address"
                  value={profile.Address}
                  onChange={handleChange}
                />
              </Grid>
            </Grid>
          </Grid>
          <Grid item xs={7} container justifyContent="flex-end" spacing={2}>
            <Grid item>
              <Button variant="contained" onClick={handleSaveChange}>
                Save
              </Button>
            </Grid>
            <Grid item>
              <Button variant="outlined" onClick={handleCancel}>
                Cancel
              </Button>
            </Grid>
          </Grid>
        </Grid>
      </Box>
    </LocalizationProvider>
  )
}

export default ProfilePage
