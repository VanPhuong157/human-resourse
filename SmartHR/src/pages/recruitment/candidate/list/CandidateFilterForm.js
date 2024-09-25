import {DatePicker} from '@mui/x-date-pickers/DatePicker'
import {AdapterDayjs} from '@mui/x-date-pickers/AdapterDayjs'
import {LocalizationProvider} from '@mui/x-date-pickers/LocalizationProvider'
import {TextField, InputLabel, FormControl, Grid} from '@mui/material'
import {useState} from 'react'
import dayjs from 'dayjs'
import {useGetPosts} from '../../../../pages/recruitment/job/request/PostJobRequest'
import SelectComponent from '../../../../components/select'

const StatusOptions = [
  {id: 'Pending', value: 'Pending'},
  {id: 'Interview', value: 'Interview'},
  {id: 'Pass', value: 'Pass'},
  {id: 'NotPass', value: 'NotPass'},
  {id: 'Onboarding', value: 'Onboarding'},
]

const CandidateFilterForm = (props) => {
  const {data} = props
  const [startApply, setStartApply] = useState(
    data?.startApply ? dayjs(data.startApply, 'DD/MM/YYYY') : null,
  )
  const [endApply, setEndApply] = useState(
    data?.endApply ? dayjs(data.endApply, 'DD/MM/YYYY') : null,
  )
  const {data: dataJobPosts} = useGetPosts(1, 1000)

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
        <Grid item xs={12}>
          <TextField
            name="email"
            label="Email Address"
            type="email"
            defaultValue={data?.email}
            fullWidth
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            name="phone"
            label="Phone"
            defaultValue={data?.phone}
            fullWidth
          />
        </Grid>
        <Grid item xs={12}>
          <DatePicker
            label="Start Apply"
            name="startDateApply"
            format="DD/MM/YYYY"
            value={startApply}
            onChange={(newValue) => {
              setStartApply(newValue)
            }}
            renderInput={(props) => <TextField {...props} fullWidth />}
            slotProps={{
              field: {clearable: true},
              textField: {fullWidth: true},
            }}
          />
        </Grid>
        <Grid item xs={12}>
          <DatePicker
            label="End Apply"
            name="endDateApply"
            format="DD/MM/YYYY"
            value={endApply}
            onChange={(newValue) => {
              setEndApply(newValue)
            }}
            renderInput={(props) => <TextField {...props} fullWidth />}
            slotProps={{
              field: {clearable: true},
              textField: {fullWidth: true},
            }}
          />
        </Grid>
        <Grid item xs={12}>
          <FormControl fullWidth>
            <InputLabel id="status-label">Status</InputLabel>
            <SelectComponent
              dataSelect={StatusOptions}
              label="Status"
              name="status"
              value={data?.status}
            />
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <FormControl fullWidth>
            <InputLabel id="jobPost-label">JobPost</InputLabel>
            <SelectComponent
              dataSelect={dataJobPosts?.data?.items}
              label="JobPost"
              name="jobPostId"
              value={data?.jobPostId}
            />
          </FormControl>
        </Grid>
      </Grid>
    </LocalizationProvider>
  )
}

export default CandidateFilterForm
