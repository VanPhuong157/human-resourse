import * as React from 'react'
import Dialog from '@mui/material/Dialog'
import DialogActions from '@mui/material/DialogActions'
import DialogContent from '@mui/material/DialogContent'
import DialogTitle from '@mui/material/DialogTitle'
import {Button, CircularProgress} from '@mui/material'

const FormDialog = ({
  open,
  title,
  dialogContent,
  isLoading,
  actionName,
  onConfirm,
  maxWidth,
  onCancel,
  sx,
}) => {
  const handleCancel = () => (onCancel && !isLoading ? onCancel() : undefined)
  const handleSubmit = (event) => {
    event.preventDefault()
    const formData = new FormData(event.currentTarget)
    const formJson = Object.fromEntries(formData.entries())
    if (formJson.expiryDate) {
      const [day, month, year] = formJson.expiryDate.split('/')
      formJson.expiryDate = `${year}-${day}-${month}`
    }
    onConfirm(formJson)
  }

  return (
    open && (
      <Dialog
        open={open}
        maxWidth={maxWidth ? maxWidth : 'sm'}
        fullWidth
        PaperProps={{
          component: 'form',
          onSubmit: handleSubmit,
        }}
      >
        <DialogTitle>{title}</DialogTitle>
        <DialogContent dividers>{dialogContent}</DialogContent>
        <DialogActions>
          <Button
            disabled={isLoading}
            onClick={handleCancel}
            sx={{textTransform: 'none'}}
          >
            Cancel
          </Button>
          <Button
            variant="contained"
            disabled={isLoading}
            endIcon={isLoading ? <CircularProgress size={20} /> : null}
            type="submit"
            sx={{
              textTransform: 'none',
              backgroundColor: (theme) => theme.palette.primary.main,
              color: (theme) => theme.palette.primary.contrastText,
              '&:hover': {
                backgroundColor: (theme) => theme.palette.primary.dark,
              },
              ...sx,
            }}
          >
            {actionName}
          </Button>
        </DialogActions>
      </Dialog>
    )
  )
}

export default FormDialog
