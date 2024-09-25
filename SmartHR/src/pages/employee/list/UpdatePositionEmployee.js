import {AdapterDayjs} from '@mui/x-date-pickers/AdapterDayjs'
import {LocalizationProvider} from '@mui/x-date-pickers/LocalizationProvider'
import {
  InputLabel,
  FormControl,
  Select,
  MenuItem,
  Grid,
  CircularProgress,
} from '@mui/material'
import useGetDepartments from '../../department/requests/getDepartment'
import useGetRoles from '../../role/request/getRoles'
import {Box} from '@mui/system'

const UpdateRoleEmployee = (props) => {
  const {data} = props
  const {data: dataDepartments, isLoading: isLoadingDepartment} =
    useGetDepartments()
  const {data: dataRoles, isLoading: isLoadingRole} = useGetRoles(1, 1000)
  const DepartmentOptions = []
  if (dataDepartments) {
    dataDepartments?.data?.items.map((dataDepartment) => {
      DepartmentOptions.push({
        label: dataDepartment.name,
        value: dataDepartment.id,
      })
    })
  }

  const RoleOptions = []
  if (dataRoles) {
    dataRoles?.data?.items.map((dataRole) => {
      RoleOptions.push({
        label: dataRole.name,
        value: dataRole.id,
      })
    })
  }

  const defaultDepartmentValue = DepartmentOptions.find(
    (departmentOption) => departmentOption.label === data?.departmentName,
  )?.value
  const defaultRoleValue = RoleOptions.find(
    (roleOption) => roleOption.label === data?.roleNames,
  )?.value

  if (isLoadingDepartment || isLoadingRole) {
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
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <FormControl fullWidth>
            <InputLabel id="department-label">Department</InputLabel>
            <Select
              labelId="department-label"
              name="departmentId"
              label="Department"
              defaultValue={defaultDepartmentValue}
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
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <FormControl fullWidth>
            <InputLabel id="role-label">Role</InputLabel>
            <Select
              labelId="role-label"
              name="roleId"
              label="Role"
              defaultValue={defaultRoleValue}
            >
              {RoleOptions.map((roleOption) => (
                <MenuItem key={roleOption.value} value={roleOption.value}>
                  {roleOption.label}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
      </Grid>
    </LocalizationProvider>
  )
}

export default UpdateRoleEmployee
