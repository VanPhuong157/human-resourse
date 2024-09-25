// import {DatePicker} from '@mui/x-date-pickers/DatePicker'
import {AdapterDayjs} from '@mui/x-date-pickers/AdapterDayjs'
import {LocalizationProvider} from '@mui/x-date-pickers/LocalizationProvider'
import {Box, Paper, Typography} from '@mui/material'
// import {useState} from 'react'
// import dayjs from 'dayjs'

const ViewDetailPost = (props) => {
  const {data} = props
  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Box>
        <Typography variant="h7" gutterBottom sx={{paddingLeft: 4}}>
          CreatedBy {data?.createdBy || 'Anonymous'}
        </Typography>
        <Typography variant="h7" gutterBottom sx={{paddingLeft: 4}}>
          Deadline: {data?.expiryDate}
        </Typography>
      </Box>
      <Box sx={{p: 3}}>
        <Paper elevation={3} sx={{p: 2, mb: 3}}>
          <Typography variant="h5" gutterBottom>
            Information
          </Typography>
          <Typography variant="body1">
            Experience: {data?.experienceYear} Year
          </Typography>
          <Typography variant="body1">
            Salary Range: {data?.salary + '/Month' || ' Wage agreement'}
          </Typography>
        </Paper>

        <Paper elevation={3} sx={{p: 2, mb: 3}}>
          <Typography variant="h5" gutterBottom>
            Description
          </Typography>
          <Typography variant="body1" style={{whiteSpace: 'pre-line'}}>
            {data?.description}
          </Typography>
        </Paper>

        <Paper elevation={3} sx={{p: 2, mb: 3}}>
          <Typography variant="h5" gutterBottom>
            Requirement
          </Typography>
          <Typography variant="body1" style={{whiteSpace: 'pre-line'}}>
            {data?.requirements}
          </Typography>
        </Paper>

        <Paper elevation={3} sx={{p: 2}}>
          <Typography variant="h5" gutterBottom>
            Benefit
          </Typography>
          <Typography variant="body1" style={{whiteSpace: 'pre-line'}}>
            {data?.benefits}
          </Typography>
        </Paper>
      </Box>
    </LocalizationProvider>
  )
}

export default ViewDetailPost
