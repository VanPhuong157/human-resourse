import {DatePicker} from '@mui/x-date-pickers/DatePicker'
import {AdapterDayjs} from '@mui/x-date-pickers/AdapterDayjs'
import {LocalizationProvider} from '@mui/x-date-pickers/LocalizationProvider'
import {TextField, InputLabel, FormControl, Grid} from '@mui/material'

const FilterPermission = (props) => {
  const {data} = props
  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <TextField
            name="name"
            label="Name"
            defaultValue={data?.name}
            fullWidth
          />
        </Grid>
      </Grid>
    </LocalizationProvider>
  )
}

export default FilterPermission
