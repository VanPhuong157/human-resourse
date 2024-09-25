import * as React from 'react'
import Dialog from '@mui/material/Dialog'
import DialogActions from '@mui/material/DialogActions'
import DialogContent from '@mui/material/DialogContent'
import DialogTitle from '@mui/material/DialogTitle'
import {Button, CircularProgress, IconButton} from '@mui/material'
import CloseIcon from '@mui/icons-material/Close'

const CustomDialog = ({
  open,
  title,
  dialogContent,
  isLoading,
  actionName,
  onConfirm,
  maxWidth,
  onCancel,
  sx,
  showCloseButton,
  viewDialog,
}) => {
  const handleCancel = () => (onCancel && !isLoading ? onCancel() : undefined)
  const handleSubmit = (event) => {
    event.preventDefault()
    const formData = new FormData(event.currentTarget)
    const formJson = Object.fromEntries(formData.entries())
    onConfirm(formJson)
  }
  return (
    open && (
      <Dialog
        open={open}
        maxWidth={maxWidth}
        fullWidth
        PaperProps={{
          component: 'form',
          onSubmit: handleSubmit,
        }}
      >
        <DialogTitle>{title}</DialogTitle>
        {showCloseButton && (
          <IconButton
            aria-label="close"
            onClick={handleCancel}
            sx={{
              position: 'absolute',
              right: 8,
              top: 8,
              color: (theme) => theme.palette.grey[500],
            }}
          >
            <CloseIcon />
          </IconButton>
        )}
        <DialogContent
          dividers
          sx={{
            overflowX: 'hidden',
          }}
        >
          {dialogContent}
        </DialogContent>
        {!viewDialog && (
          <DialogActions>
            <Button
              disabled={isLoading}
              onClick={handleCancel}
              sx={{
                textTransform: 'none',
              }}
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
        )}
      </Dialog>
    )
  )
}

export default CustomDialog
