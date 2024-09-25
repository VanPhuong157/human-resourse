import {useLocation, useNavigate, useParams} from 'react-router-dom'
import dayjs from 'dayjs'
import {
  Avatar,
  Box,
  Button,
  TextField,
  MenuItem,
  Grid,
  Paper,
  FormControl,
  InputLabel,
  Select,
} from '@mui/material'
import {AdapterDayjs} from '@mui/x-date-pickers/AdapterDayjs'
import {LocalizationProvider} from '@mui/x-date-pickers/LocalizationProvider'
import {useEditEmployee} from '../request/EmployeeRequest'
import {showError, showSuccess} from '../../../components/notification'
import useGetEmployeeInformation from '../request/getEmployeeInformation'
import CircularProgress from '@mui/material/CircularProgress'
import {useEffect, useState} from 'react'
import {baseUrl} from '../../../api/rootApi'
import useGetUserPermissions from '../../../pages/admin/requests/getUserPermissions'
import useGetDepartments from '../../../pages/department/requests/getDepartment'
import {DatePicker} from '@mui/x-date-pickers/DatePicker'

const EmployeeInformationTab = () => {
  const navigate = useNavigate()
  const handleBackClick = () => {
    navigate(-1)
  }
  const {userId} = useParams()
  const {
    data: dataEmployeeInformation,
    isLoading,
    refetch,
  } = useGetEmployeeInformation({
    userId,
  })
  const employeeInformation = dataEmployeeInformation?.data
  const [dateOfBirth, setDateOfBirth] = useState(
    employeeInformation?.dateOfBirth
      ? dayjs(employeeInformation?.dateOfBirth, 'YYYY-MM-DD')
      : null,
  )
  const [driverLicenseIssueDate, setDriverLicenseIssueDate] = useState(
    employeeInformation?.driverLicenseIssueDate
      ? dayjs(employeeInformation?.driverLicenseIssueDate, 'YYYY-MM-DD')
      : null,
  )
  const [passportIssuedDate, setPassportIssuedDate] = useState(
    employeeInformation?.passportIssuedDate
      ? dayjs(employeeInformation?.passportIssuedDate, 'YYYY-MM-DD')
      : null,
  )
  const [dateOfProvide, setDateOfProvide] = useState(
    employeeInformation?.dateOfProvide
      ? dayjs(employeeInformation?.dateOfProvide, 'YYYY-MM-DD')
      : null,
  )
  const {data: dataDepartments, isLoading: isLoadingDepartment} =
    useGetDepartments()
  const DepartmentOptions = []
  if (dataDepartments) {
    dataDepartments?.data?.items.map((dataDepartment) => {
      DepartmentOptions.push({
        label: dataDepartment.name,
        value: dataDepartment.id,
      })
    })
  }
  const defaultDepartmentValue = DepartmentOptions.find(
    (departmentOption) =>
      departmentOption.label === dataEmployeeInformation?.departmentName,
  )?.value
  const {mutateAsync: editUserAsync} = useEditEmployee()

  const [formData, setFormData] = useState({
    fullName: '',
    dateOfBirth: '',
    addressOfBirth: '',
    gender: '',
    phoneNumber: '',
    ethnic: '',
    religious: '',
    isPartyMember: '',
    idCardNo: '',
    homeTown: '',
    academicLevel: '',
    isUnionMember: '',
    email: '',
    driverLicenseNo: '',
    driverLicenseIssueDate: '',
    driverLicenseIssuePlace: '',
    address: '',
    passportNo: '',
    passportIssuedDate: '',
    passportIssuedAddress: '',
    status: '',
    typeOfWork: '',
    maritalStatus: '',
    healthyStatus: '',
    addressOfProvide: '',
    departmentName: '',
    roleName: '',
    hiCode: '',
    bankingNo: '',
    pitCode: '',
    note: '',
    dateOfProvide: '',
    attachments: '',
    siCode: '',
  })
  const [permissionData, setPermissionData] = useState([])
  const userIdPermission = localStorage.getItem('userId')

  const hasEditEmployeeInformation = permissionData.includes('Employee:Edit')
  const {data: permissions, isLoading: loadingPermission} =
    useGetUserPermissions({
      userId: userIdPermission,
      pageIndex: 1,
      pageSize: 1000,
    })

  useEffect(() => {
    if (permissions) {
      const newPerm = permissions?.data?.items.map((perm) => perm.name)
      setPermissionData(newPerm)
    }
  }, [permissions])

  useEffect(() => {
    if (employeeInformation) {
      setFormData({
        fullName: employeeInformation.fullName,
        dateOfBirth: employeeInformation.dateOfBirth,
        addressOfBirth: employeeInformation.addressOfBirth,
        gender: employeeInformation.gender,
        phoneNumber: employeeInformation.phoneNumber,
        ethnic: employeeInformation.ethnic,
        religious: employeeInformation.religious,
        isPartyMember: employeeInformation.isPartyMember ? 'true' : 'false',
        idCardNo: employeeInformation.idCardNo,
        homeTown: employeeInformation.homeTown,
        academicLevel: employeeInformation.academicLevel,
        isUnionMember: employeeInformation.isUnionMember ? 'true' : 'false',
        email: employeeInformation.email,
        driverLicenseNo: employeeInformation.driverLicenseNo,
        driverLicenseIssueDate: employeeInformation.driverLicenseIssueDate,
        driverLicenseIssuePlace: employeeInformation.driverLicenseIssuePlace,
        address: employeeInformation.address,
        passportNo: employeeInformation.passportNo,
        passportIssuedDate: employeeInformation.passportIssuedDate,
        passportIssuedAddress: employeeInformation.passportIssuedAddress,
        status: employeeInformation.status,
        typeOfWork: employeeInformation.typeOfWork,
        maritalStatus: employeeInformation.maritalStatus,
        healthyStatus: employeeInformation.healthyStatus,
        addressOfProvide: employeeInformation.addressOfProvide,
        departmentName: employeeInformation.departmentName,
        roleName: employeeInformation.roleNames,
        hiCode: employeeInformation.hiCode,
        bankingNo: employeeInformation.bankingNo,
        pitCode: employeeInformation.pitCode,
        note: employeeInformation.note,
        dateOfProvide: employeeInformation.dateOfProvide,
        attachments: employeeInformation.attachments,
        siCode: employeeInformation.siCode,
      })
    }
  }, [employeeInformation])
  const handleSubmit = (event) => {
    event.preventDefault()
    const formData = new FormData(event.currentTarget)
    const formJson = Object.fromEntries(formData.entries())
    formJson.userId = employeeInformation.userId
    const newErrors = {}
    for (let [name, value] of Object.entries(formJson)) {
      const error = handleValidation(name, value)
      if (error) {
        newErrors[name] = error
      }
    }
    setErrors(newErrors)
    const hasError = Object.values(newErrors).some((error) => error !== '')
    if (hasError) {
      return
    }
    formJson.dateOfBirth = dayjs(formJson.dateOfBirth, 'DD/MM/YYYY').format(
      'YYYY-MM-DD',
    )
    formJson.driverLicenseIssueDate = dayjs(
      formJson.driverLicenseIssueDate,
      'DD/MM/YYYY',
    ).format('YYYY-MM-DD')
    formJson.passportIssuedDate = dayjs(
      formJson.passportIssuedDate,
      'DD/MM/YYYY',
    ).format('YYYY-MM-DD')
    formJson.dateOfProvide = dayjs(formJson.dateOfProvide, 'DD/MM/YYYY').format(
      'YYYY-MM-DD',
    )
    editUserAsync(formJson)
      .then((response) => {
        console.log('response', response)
        showSuccess({message: response.data?.message})
        refetch()
        navigate('/user')
      })
      .catch((err) => {
        const badRequestMessage = err.response?.data?.message
        showError({
          message: badRequestMessage || 'An unexpected error occurred',
        })
      })
  }
  const handleChange = (e) => {
    const {name, value} = e.target
    const error = handleValidation(name, value)

    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    })
    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: error,
    }))
  }

  const [errors, setErrors] = useState({
    driverLicenseNo: '',
    gender: '',
    hiCode: '',
    idCardNo: '',
    phoneNumber: '',
    pitCode: '',
    siCode: '',
  })

  const handleValidation = (name, value) => {
    let error = ''
    if (name === 'driverLicenseNo') {
      if (value.length < 12 || value.length > 14) {
        error = 'DriverLicenseNo must be between 12 and 14 characters.'
      } else if (!/^\d+$/.test(value)) {
        error = 'DriverLicenseNo must contain only numbers.'
      }
    }
    if (name === 'phoneNumber') {
      const phoneNumberPattern =
        /^(0|\+84)(3[2-9]|5[689]|7[0|6-9]|8[1-5]|9[0-9])[0-9]{7}$/
      if (!value) {
        error = 'PhoneNumber is required'
      } else if (!phoneNumberPattern.test(value)) {
        error = 'Invalid phone number format.'
      }
    }
    if (name === 'email') {
      const emailPattern = /^[^@\s]+@gmail\.com$/

      if (!value) {
        error = 'Email is required'
      } else if (!emailPattern.test(value)) {
        error = 'Invalid email format.'
      }
    }

    if (name === 'fullName') {
      if (!value) error = 'Full Name is required.'
      if (value.length > 100) {
        error = 'FullName can not be longer than 100 characters'
      }
      if (value.length < 6) {
        error = 'FullName can not be shorter than 6 characters'
      }
    }

    if (name === 'hiCode') {
      if (value.length !== 10) {
        error = 'HiCode must be 10 characters.'
      } else if (!/^\d+$/.test(value)) {
        error = 'HiCode must contain only numbers.'
      }
    }

    if (name === 'idCardNo') {
      if (value.length > 20) {
        error = "IdCardNo can't be longer than 20 characters."
      } else if (!/^\d+$/.test(value)) {
        error = 'IdCardNo must contain only numbers.'
      } else if (!/^\d{12}$/.test(value)) {
        error = 'Invalid CCCD format.'
      }
    }

    if (name === 'pitCode') {
      if (value.length < 10 || value.length > 13) {
        error = 'PitCode must be between 10 and 13 characters.'
      } else if (!/^\d+$/.test(value)) {
        error = 'PitCode must contain only numbers.'
      }
    }

    if (name === 'siCode') {
      if (value.length !== 10) {
        error = 'SiCode must be 10 characters.'
      } else if (!/^\d+$/.test(value)) {
        error = 'SiCode must contain only numbers.'
      }
    }
    if (name === 'gender') {
      if (!value) {
        error = 'Gender is required'
      }
    }

    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: error,
    }))

    return error
  }
  useEffect(() => {
    refetch()
  }, [refetch])
  if (isLoading || loadingPermission || isLoadingDepartment) {
    return (
      <Box
        display="flex"
        justifyContent="center"
        alignItems="center"
        minHeight="100vh"
      >
        <CircularProgress />
      </Box>
    )
  }
  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Paper component="form" onSubmit={handleSubmit}>
        <Box mt={2}>
          <Grid container spacing={2}>
            <Grid item xs={2.9} sx={{mr: 1}}>
              <Avatar
                alt="Avatar"
                src={baseUrl + `/api/UserInformations/download-image/${userId}`}
                sx={{width: 'auto', height: '400px', mb: 2}}
              />
            </Grid>
            <Grid container xs={9} spacing={0.5}>
              <Grid item xs={12} display={'flex'} justifyItems={'center'}>
                <Grid item xs={3} sx={{mr: 1}}>
                  <TextField
                    fullWidth
                    variant="outlined"
                    label="Full Name *"
                    name="fullname"
                    value={formData.fullName}
                    onChange={handleChange}
                    error={!!errors.fullName}
                    helperText={errors.fullName}
                    disabled={!hasEditEmployeeInformation}
                  />
                </Grid>
                <Grid item xs={3} sx={{mr: 1}}>
                  <DatePicker
                    label="Date of Birth"
                    name="dateOfBirth"
                    format="DD/MM/YYYY"
                    value={dateOfBirth}
                    onChange={(newValue) => {
                      setDateOfBirth(newValue)
                    }}
                    fullWidth
                    slotProps={{
                      field: {clearable: true},
                      textField: {fullWidth: true},
                    }}
                    renderInput={(props) => <TextField {...props} fullWidth />}
                  />
                </Grid>
                <Grid item xs={3} sx={{mr: 1}}>
                  <TextField
                    variant="outlined"
                    fullWidth
                    label="Address of Birth"
                    name="addressOfBirth"
                    value={formData.addressOfBirth}
                    onChange={handleChange}
                    disabled={!hasEditEmployeeInformation}
                  />
                </Grid>
                <Grid item xs={3}>
                  <TextField
                    select
                    fullWidth
                    variant="outlined"
                    label="Gender *"
                    name="gender"
                    value={formData.gender}
                    onChange={handleChange}
                    error={!!errors.gender}
                    helperText={errors.gender}
                    disabled={!hasEditEmployeeInformation}
                  >
                    <MenuItem value="Male">Male</MenuItem>
                    <MenuItem value="Female">Female</MenuItem>
                  </TextField>
                </Grid>
              </Grid>
              <Grid item xs={12} display={'flex'} justifyItems={'center'}>
                <Grid item xs={3} sx={{mr: 1}}>
                  <TextField
                    variant="outlined"
                    fullWidth
                    label="Phone Number *"
                    name="phoneNumber"
                    value={formData.phoneNumber}
                    onChange={handleChange}
                    error={!!errors.phoneNumber}
                    helperText={errors.phoneNumber}
                    disabled={!hasEditEmployeeInformation}
                  />
                </Grid>
                <Grid item xs={3} sx={{mr: 1}}>
                  <TextField
                    variant="outlined"
                    fullWidth
                    label="Ethnic"
                    name="ethnic"
                    value={formData.ethnic}
                    onChange={handleChange}
                    disabled={!hasEditEmployeeInformation}
                  />
                </Grid>
                <Grid item xs={3} sx={{mr: 1}}>
                  <TextField
                    fullWidth
                    variant="outlined"
                    label="Religious"
                    name="religious"
                    value={formData.religious}
                    onChange={handleChange}
                    disabled={!hasEditEmployeeInformation}
                  />
                </Grid>
                <Grid item xs={3}>
                  <TextField
                    select
                    fullWidth
                    variant="outlined"
                    label="Is Party Member"
                    name="isPartyMember"
                    value={formData.isPartyMember}
                    onChange={handleChange}
                    disabled={!hasEditEmployeeInformation}
                  >
                    <MenuItem value="true">Yes</MenuItem>
                    <MenuItem value="false">No</MenuItem>
                  </TextField>
                </Grid>
              </Grid>
              <Grid item xs={12} display={'flex'}>
                <Grid item xs={3} sx={{mr: 1}}>
                  <TextField
                    fullWidth
                    variant="outlined"
                    label="ID Card No *"
                    name="idCardNo"
                    value={formData.idCardNo}
                    onChange={handleChange}
                    error={!!errors.idCardNo}
                    helperText={errors.idCardNo}
                    disabled={!hasEditEmployeeInformation}
                  />
                </Grid>
                <Grid item xs={3} sx={{mr: 1}}>
                  <TextField
                    variant="outlined"
                    fullWidth
                    label="Home Town"
                    name="homeTown"
                    value={formData.homeTown}
                    onChange={handleChange}
                    disabled={!hasEditEmployeeInformation}
                  />
                </Grid>
                <Grid item xs={3} sx={{mr: 1}}>
                  <TextField
                    variant="outlined"
                    fullWidth
                    label="Academic Level"
                    name="academicLevel"
                    value={formData.academicLevel}
                    onChange={handleChange}
                    disabled={!hasEditEmployeeInformation}
                  />
                </Grid>
                <Grid item xs={3}>
                  <TextField
                    select
                    fullWidth
                    variant="outlined"
                    label="IsUnionMember"
                    name="isUnionMember"
                    value={formData.isUnionMember}
                    onChange={handleChange}
                    disabled={!hasEditEmployeeInformation}
                  >
                    <MenuItem value="true">Yes</MenuItem>
                    <MenuItem value="false">No</MenuItem>
                  </TextField>
                </Grid>
              </Grid>
              <Grid item xs={12} display={'flex'}>
                <Grid item xs={3} sx={{mr: 1}}>
                  <TextField
                    fullWidth
                    variant="outlined"
                    label="Email *"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                    error={!!errors.email}
                    helperText={errors.email}
                    disabled={!hasEditEmployeeInformation}
                  />
                </Grid>
                <Grid item xs={3} sx={{mr: 1}}>
                  <TextField
                    fullWidth
                    variant="outlined"
                    label="Driver License No *"
                    name="driverLicenseNo"
                    value={formData.driverLicenseNo}
                    onChange={handleChange}
                    error={!!errors.driverLicenseNo}
                    helperText={errors.driverLicenseNo}
                    disabled={!hasEditEmployeeInformation}
                  />
                </Grid>
                <Grid item xs={3} sx={{mr: 1}}>
                  <DatePicker
                    label="Driver License Issue Date"
                    name="driverLicenseIssueDate"
                    format="DD/MM/YYYY"
                    value={driverLicenseIssueDate}
                    onChange={(newValue) => {
                      setDriverLicenseIssueDate(newValue)
                    }}
                    fullWidth
                    slotProps={{
                      field: {clearable: true},
                      textField: {fullWidth: true},
                    }}
                    renderInput={(props) => <TextField {...props} fullWidth />}
                  />
                </Grid>
                <Grid item xs={3}>
                  <TextField
                    fullWidth
                    variant="outlined"
                    label="Driver License Issue Place"
                    name="driverLicenseIssuePlace"
                    value={formData.driverLicenseIssuePlace}
                    onChange={handleChange}
                    disabled={!hasEditEmployeeInformation}
                  />
                </Grid>
              </Grid>
              <Grid item xs={12} display={'flex'}>
                <Grid item xs={3} sx={{mr: 1}}>
                  <TextField
                    fullWidth
                    variant="outlined"
                    label="Address"
                    name="address"
                    value={formData.address}
                    onChange={handleChange}
                    disabled={!hasEditEmployeeInformation}
                  />
                </Grid>
                <Grid item xs={3} sx={{mr: 1}}>
                  <TextField
                    fullWidth
                    variant="outlined"
                    label="Passport No"
                    name="passportNo"
                    value={formData.passportNo}
                    onChange={handleChange}
                    disabled={!hasEditEmployeeInformation}
                  />
                </Grid>
                <Grid item xs={3} sx={{mr: 1}}>
                  <DatePicker
                    label="Passport Issued Date"
                    name="passportIssuedDate"
                    format="DD/MM/YYYY"
                    value={passportIssuedDate}
                    onChange={(newValue) => {
                      setPassportIssuedDate(newValue)
                    }}
                    fullWidth
                    slotProps={{
                      field: {clearable: true},
                      textField: {fullWidth: true},
                    }}
                    renderInput={(props) => <TextField {...props} fullWidth />}
                  />
                </Grid>
                <Grid item xs={3}>
                  <TextField
                    fullWidth
                    variant="outlined"
                    label="Passport Issued Address"
                    name="passportIssuedAddress"
                    value={formData.passportIssuedAddress}
                    onChange={handleChange}
                    disabled={!hasEditEmployeeInformation}
                  />
                </Grid>
              </Grid>
            </Grid>
            <Grid container xs={12} spacing={2} sx={{ml: 2}}>
              <Grid item xs={12} display={'flex'} sx={{mr: 1}}>
                <Grid container xs={7} display={'flex'}>
                  <Grid item xs={3.9} sx={{mr: 1}}>
                    <TextField
                      fullWidth
                      variant="outlined"
                      label="Status"
                      name="status"
                      value={formData.status}
                      onChange={handleChange}
                      disabled={!hasEditEmployeeInformation}
                    />
                  </Grid>
                  <Grid item xs={3.9} sx={{mr: 1}}>
                    <TextField
                      fullWidth
                      variant="outlined"
                      label="Type Of Work"
                      name="typeOfWork"
                      value={formData.typeOfWork}
                      onChange={handleChange}
                      disabled={!hasEditEmployeeInformation}
                    />
                  </Grid>
                  <Grid item xs={3.9}>
                    <TextField
                      fullWidth
                      variant="outlined"
                      label="Marital Status"
                      name="maritalStatus"
                      value={formData.maritalStatus}
                      onChange={handleChange}
                      disabled={!hasEditEmployeeInformation}
                    />
                  </Grid>
                </Grid>
                <Grid container xs={5} display={'flex'}>
                  <Grid item xs={5.9} sx={{mr: 1}}>
                    <TextField
                      fullWidth
                      variant="outlined"
                      label="Healthy Status"
                      name="healthyStatus"
                      value={formData.healthyStatus}
                      onChange={handleChange}
                      disabled={!hasEditEmployeeInformation}
                    />
                  </Grid>
                  <Grid item xs={5.9}>
                    <TextField
                      fullWidth
                      variant="outlined"
                      label="Address Of Provide"
                      name="addressOfProvide"
                      value={formData.addressOfProvide}
                      onChange={handleChange}
                      disabled={!hasEditEmployeeInformation}
                    />
                  </Grid>
                </Grid>
              </Grid>
              <Grid item xs={12} display={'flex'}>
                <Grid container xs={7} spacing={0}>
                  <Grid item xs={12} display={'flex'}>
                    <Grid item xs={3.9} sx={{mr: 1}}>
                      {/* <FormControl fullWidth>
                        <InputLabel id="department-label">
                          Department
                        </InputLabel>
                        <Select
                          labelId="department-label"
                          name="departmentId"
                          label="Department"
                          defaultValue={defaultDepartmentValue}
                          onChange={handleChange}
                          disabled={!hasEditEmployeeInformation}
                        >
                          {DepartmentOptions.map((departmentOption) => (
                            <MenuItem
                              key={departmentOption.value}
                              value={departmentOption.value}
                            >
                              {departmentOption.label}
                            </MenuItem>
                          ))}
                        </Select>
                      </FormControl> */}
                      <TextField
                        fullWidth
                        variant="outlined"
                        label="Department Name"
                        name="departmentName"
                        value={formData.departmentName}
                        onChange={handleChange}
                        disabled={true}
                      />
                    </Grid>
                    <Grid item xs={3.9} sx={{mr: 1}}>
                      <TextField
                        fullWidth
                        variant="outlined"
                        label="Role"
                        name="roleName"
                        value={formData.roleName}
                        onChange={handleChange}
                        disabled={true}
                      />
                    </Grid>
                    {/* <Grid item xs={3.9} sx={{mr: 1}}>
                      <TextField
                        fullWidth
                        variant="outlined"
                        label="Role"
                        name="roleName"
                        value={formData.roleName}
                        onChange={handleChange}
                        disabled={!hasEditEmployeeInformation}
                      />
                    </Grid> */}
                    <Grid item xs={3.9}>
                      <TextField
                        fullWidth
                        variant="outlined"
                        label="Hi Code *"
                        name="hiCode"
                        value={formData.hiCode}
                        onChange={handleChange}
                        error={!!errors.hiCode}
                        helperText={errors.hiCode}
                        disabled={!hasEditEmployeeInformation}
                      />
                    </Grid>
                  </Grid>
                  <Grid item xs={12} display={'flex'} sx={{mt: 2}}>
                    <Grid item xs={3.9} sx={{mr: 1}}>
                      <TextField
                        fullWidth
                        variant="outlined"
                        label="Banking No"
                        name="bankingNo"
                        value={formData.bankingNo}
                        onChange={handleChange}
                        disabled={!hasEditEmployeeInformation}
                      />
                    </Grid>
                    <Grid item xs={3.9} sx={{mr: 1}}>
                      <TextField
                        fullWidth
                        variant="outlined"
                        label="Pit Code *"
                        name="pitCode"
                        value={formData.pitCode}
                        onChange={handleChange}
                        error={!!errors.pitCode}
                        helperText={errors.pitCode}
                        disabled={!hasEditEmployeeInformation}
                      />
                    </Grid>
                    <Grid item xs={3.9}>
                      <DatePicker
                        label="Date of Provide"
                        name="dateOfProvide"
                        format="DD/MM/YYYY"
                        value={dateOfProvide}
                        onChange={(newValue) => {
                          setDateOfProvide(newValue)
                        }}
                        fullWidth
                        slotProps={{
                          field: {clearable: true},
                          textField: {fullWidth: true},
                        }}
                        renderInput={(props) => (
                          <TextField {...props} fullWidth />
                        )}
                      />
                    </Grid>
                  </Grid>
                </Grid>
                <Grid item xs={5} sx={{mr: 1}}>
                  <Grid item xs={12}>
                    <TextField
                      multiline
                      fullWidth
                      variant="outlined"
                      label="Note"
                      name="note"
                      rows={4.1}
                      value={formData.note}
                      onChange={handleChange}
                      disabled={!hasEditEmployeeInformation}
                    />
                  </Grid>
                </Grid>
              </Grid>

              <Grid item xs={12} display={'flex'} sx={{mb: 2}}>
                <Grid container xs={7} spacing={0}>
                  <Grid item xs={12} display={'flex'}>
                    <Grid item xs={3.9} sx={{mr: 1}}>
                      <TextField
                        fullWidth
                        variant="outlined"
                        label="Address Of Provide"
                        name="addressOfProvide"
                        value={formData.addressOfProvide}
                        onChange={handleChange}
                        disabled={!hasEditEmployeeInformation}
                      />
                    </Grid>
                    <Grid item xs={3.9} sx={{mr: 1}}>
                      <TextField
                        fullWidth
                        variant="outlined"
                        label="Si Code *"
                        name="siCode"
                        value={formData.siCode}
                        onChange={handleChange}
                        error={!!errors.siCode}
                        helperText={errors.siCode}
                        disabled={!hasEditEmployeeInformation}
                      />
                    </Grid>
                    <Grid item xs={3.9}></Grid>
                  </Grid>
                </Grid>
                <Grid item xs={5} display={'flex'}>
                  <Grid item xs={6}>
                    <Box alignItems={'right !important'}>
                      <Button
                        onClick={handleBackClick}
                        variant="contained"
                        sx={{bgcolor: 'black', color: 'white'}}
                      >
                        Back
                      </Button>
                    </Box>
                  </Grid>
                  <Grid item xs={6}>
                    <Box alignItems={'right !important'}>
                      {hasEditEmployeeInformation && (
                        <Button
                          type="submit"
                          variant="contained"
                          sx={{bgcolor: 'black', color: 'white'}}
                        >
                          Save
                        </Button>
                      )}
                    </Box>
                  </Grid>
                </Grid>
              </Grid>
            </Grid>
            <Box></Box>
          </Grid>
        </Box>
      </Paper>
    </LocalizationProvider>
  )
}

export default EmployeeInformationTab
