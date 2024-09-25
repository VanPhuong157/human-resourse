import {AdapterDayjs} from '@mui/x-date-pickers/AdapterDayjs'
import {LocalizationProvider} from '@mui/x-date-pickers/LocalizationProvider'
import {InputLabel, FormControl, Select, MenuItem, Grid} from '@mui/material'
const StatusOptions = [
  {
    label: 'Active',
    value: 'Active',
  },
  {
    label: 'Deactive',
    value: 'Deactive',
  },
]

const EditJobStatus = (props) => {
  const {data} = props

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <FormControl fullWidth>
            <InputLabel id="status-label">Status</InputLabel>
            <Select
              labelId="status-label"
              name="status"
              label="Status"
              defaultValue={data?.status}
            >
              {StatusOptions.map((statusOption) => (
                <MenuItem key={statusOption.value} value={statusOption.value}>
                  {statusOption.label}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
      </Grid>
    </LocalizationProvider>
  )
}

export default EditJobStatus
