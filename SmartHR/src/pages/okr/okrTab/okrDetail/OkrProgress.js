import React, {useState} from 'react'
import {Box, TextField, Button, Typography, Grid} from '@mui/material'
import {DateTimePicker, LocalizationProvider} from '@mui/x-date-pickers'
import {AdapterDateFns} from '@mui/x-date-pickers/AdapterDateFns'
import {useEditProgessOKR} from '../../../../pages/okr/requests/editOkrProgress'
import {showError, showSuccess} from '../../../../components/notification'

const OkrProgress = ({data, onDialogClose}) => {
  const okrData = data?.data
  const [achieve, setAchieve] = useState(okrData.achieved || 0)
  const [dateTime] = useState(new Date())
  const [comment, setComment] = useState('')
  const [error, setError] = useState('')
  const {mutateAsync: mutateAsyncEditProgessOkr} = useEditProgessOKR({
    okrId: okrData.id,
  })

  const handleAchievedChange = (event) => {
    setAchieve(event.target.value)
  }

  const handleCommentChange = (event) => {
    setComment(event.target.value)
  }
  const handleSubmitOkrProgress = () => {
    const targetNumber = okrData.targerNumber
    if (achieve <= 0) {
      setError('Achieved value must be greater than 0')
      return
    }
    if (achieve > targetNumber) {
      setError('Achieved value must be less than or equal to the Target Number')
      return
    }
    setError('')
    mutateAsyncEditProgessOkr(achieve)
      .then((response) => {
        showSuccess({message: response.data?.message})
        onDialogClose()
      })
      .catch((err) => {
        showError(err.response?.data?.message)
      })
  }
  return (
    <Box
      sx={{
        border: '1px solid #e0e0e0',
        borderRadius: '8px',
        padding: '16px',
        width: '100%',
        maxWidth: '820px',
        backgroundColor: '#fff',
      }}
    >
      <Grid container alignItems="center" spacing={2}>
        <Grid item></Grid>
        <Grid item xs>
          <Typography variant="subtitle1" sx={{fontSize: '30px'}}>
            {okrData.title}
          </Typography>
        </Grid>
      </Grid>
      <Grid container alignItems="center" spacing={2} mt={2}>
        <Grid item>
          <TextField
            type="number"
            label="Achieved"
            value={achieve}
            onChange={handleAchievedChange}
            sx={{width: '100px'}}
            error={!!error}
            helperText={error}
          />
        </Grid>
        <Grid item>
          <TextField
            type="number"
            label="Target Number"
            value={okrData.targerNumber}
            disabled
            InputProps={{
              readOnly: true,
            }}
            sx={{width: '160px'}}
          />
        </Grid>
        <Grid item>
          <LocalizationProvider dateAdapter={AdapterDateFns}>
            <DateTimePicker
              renderInput={(props) => <TextField {...props} />}
              InputProps={{readOnly: true}}
              disabled
              value={dateTime}
            />
          </LocalizationProvider>
        </Grid>
      </Grid>
      {/* <Box mt={2}>
        <TextField
          label="Add a comment..."
          variant="outlined"
          fullWidth
          multiline
          rows={3}
          value={comment}
          onChange={handleCommentChange}
        />
      </Box> */}
      <Box mt={2} display="flex" justifyContent="flex-end">
        <Button
          variant="contained"
          color="primary"
          onClick={handleSubmitOkrProgress}
        >
          Send
        </Button>
      </Box>
    </Box>
  )
}

export default OkrProgress
