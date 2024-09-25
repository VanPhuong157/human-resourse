import {AdapterDayjs} from '@mui/x-date-pickers/AdapterDayjs'
import {LocalizationProvider} from '@mui/x-date-pickers/LocalizationProvider'
import {TextField, InputLabel, FormControl, Grid} from '@mui/material'
import SelectComponent from '../../../../components/select'

const TypeOptions = [
  {
    id: 'Objective',
    value: 'Objective',
  },
  {
    id: 'KeyResult',
    value: 'KeyResult',
  },
]

const ScopeOptions = [
  {
    id: 'Individual',
    value: 'Individual',
  },
  {
    id: 'Team',
    value: 'Team',
  },
]

const StatusOptions = [
  {
    id: 'Not Started',
    value: 'Not Started',
  },
  {
    id: 'Processing',
    value: 'Processing',
  },
  {
    id: 'Done',
    value: 'Done',
  },
]

const CycleOptions = [
  {
    id: 'Q1 2024',
    value: 'Q1 2024',
  },
  {
    id: 'Q2 2024',
    value: 'Q2 2024',
  },
  {
    id: 'Q3 2024',
    value: 'Q3 2024',
  },
  {
    id: 'Q4 2024',
    value: 'Q4 2024',
  },
  {
    id: 'Q1 2025',
    value: 'Q1 2025',
  },
  {
    id: 'Q2 2025',
    value: 'Q2 2025',
  },
  {
    id: 'Q3 2025',
    value: 'Q3 2025',
  },
  {
    id: 'Q4 2025',
    value: 'Q4 2025',
  },
]

const OkrFilter = (props) => {
  const {data} = props
  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <TextField
            name="title"
            label="Title"
            defaultValue={data?.title}
            fullWidth
          />
        </Grid>
        <Grid item xs={12}>
          <FormControl fullWidth>
            <InputLabel id="type-label">Type</InputLabel>
            <SelectComponent
              dataSelect={TypeOptions}
              label="Type"
              name="type"
              value={data?.type}
            />
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <FormControl fullWidth>
            <InputLabel id="scope-label">Scope</InputLabel>
            <SelectComponent
              dataSelect={ScopeOptions}
              label="Scope"
              name="scope"
              value={data?.scope}
            />
          </FormControl>
        </Grid>
        <Grid item xs={6}>
          <FormControl fullWidth>
            <InputLabel id="approveStatus-label"> Status</InputLabel>
            <SelectComponent
              dataSelect={StatusOptions}
              label="status"
              name="status"
              value={data?.status}
            />
          </FormControl>
        </Grid>
        <Grid item xs={6}>
          <FormControl fullWidth>
            <InputLabel id="cycle-label">Cycle</InputLabel>
            <SelectComponent
              dataSelect={CycleOptions}
              label="Cycle"
              name="cycle"
              value={data?.cycle}
            />
          </FormControl>
        </Grid>
      </Grid>
    </LocalizationProvider>
  )
}

export default OkrFilter
