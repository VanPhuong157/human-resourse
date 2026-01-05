import React, { useState } from 'react'
import { Box, TextField, Button, Typography, Grid } from '@mui/material'
import { DateTimePicker, LocalizationProvider } from '@mui/x-date-pickers'
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns'
import { useEditProgessOKR } from '../../../../pages/okr/requests/editOkrProgress'
import { showError, showSuccess } from '../../../../components/notification'

const OkrProgress = ({ data, onDialogClose }) => {
  // chấp nhận cả data hoặc data.data, fallback {}
  const okrData = data?.data || data || {}

  // ép kiểu số & default 0 để tránh undefined
  const [achieve, setAchieve] = useState(
    Number(okrData.achieved ?? 0)
  )
  const [dateTime] = useState(new Date())
  const [comment, setComment] = useState('')
  const [error, setError] = useState('')

  // nếu thiếu id thì hook vẫn khởi tạo được nhưng submit sẽ chặn
  const { mutateAsync: mutateAsyncEditProgessOkr } = useEditProgessOKR({
    okrId: okrData.id,
  })

  const handleAchievedChange = (event) => {
    const v = Number(event.target.value)
    setAchieve(Number.isFinite(v) ? v : 0)
  }

  const handleCommentChange = (event) => setComment(event.target.value)

  const handleSubmitOkrProgress = async () => {
    // guard dữ liệu
    if (!okrData.id) {
      showError('Thiếu OKR id.')
      return
    }

    const targetNumber = Number(okrData.targetNumber ?? 0)

    if (!Number.isFinite(achieve) || achieve <= 0) {
      setError('Achieved value must be greater than 0')
      return
    }
    if (targetNumber > 0 && achieve > targetNumber) {
      setError('Achieved value must be less than or equal to the Target Number')
      return
    }

    setError('')

    try {
      // tuỳ API có thể cần object: { achieved, comment, dateTime }
      const res = await mutateAsyncEditProgessOkr(achieve)
      showSuccess({ message: res?.data?.message || 'Updated' })
      onDialogClose?.()
    } catch (err) {
      showError(err?.response?.data?.message || 'Update failed')
    }
  }

  return (
    <Box
      sx={{
        border: '1px solid #e0e0e0',
        borderRadius: '8px',
        p: 2,
        width: '100%',
        maxWidth: 820,
        bgcolor: '#fff',
      }}
    >
      <Grid container alignItems="center" spacing={2}>
        <Grid item xs>
          <Typography variant="subtitle1" sx={{ fontSize: 30 }}>
            {okrData.title || ''}
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
            sx={{ width: 120 }}
            error={!!error}
            helperText={error}
            inputProps={{ min: 0 }}
          />
        </Grid>

        <Grid item>
          <TextField
            type="number"
            label="Target Number"
            value={Number(okrData.targetNumber ?? 0)}
            disabled
            InputProps={{ readOnly: true }}
            sx={{ width: 160 }}
          />
        </Grid>

        <Grid item>
          <LocalizationProvider dateAdapter={AdapterDateFns}>
            <DateTimePicker
              value={dateTime}
              disabled
              slotProps={{ textField: { InputProps: { readOnly: true } } }}
            />
          </LocalizationProvider>
        </Grid>
      </Grid>

      {/* Nếu cần nhập ghi chú, bỏ comment khối dưới */}
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
        <Button variant="contained" onClick={handleSubmitOkrProgress}>
          Send
        </Button>
      </Box>
    </Box>
  )
}

export default OkrProgress
