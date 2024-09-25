import * as React from 'react'
import Dialog from '@mui/material/Dialog'
import DialogActions from '@mui/material/DialogActions'
import DialogContent from '@mui/material/DialogContent'
import DialogTitle from '@mui/material/DialogTitle'
import Button from '@mui/material/Button'
import CircularProgress from '@mui/material/CircularProgress'
import Slide from '@mui/material/Slide'

const ConfirmDialog = ({
  open,
  onCancel,
  onConfirm,
  title,
  actionName,
  dialogContent,
  isLoading,
  sx,
  maxWidth,
  onClose,
  ...rest
}) => {
  const handleClose = (event) =>
    onClose && !isLoading ? onClose(event) : undefined
  const handleCancel = () => (onCancel && !isLoading ? onCancel() : undefined)

  return (
    open && (
      <Dialog
        open={open}
        maxWidth={maxWidth}
        fullWidth
        onClose={handleClose}
        TransitionComponent={React.forwardRef((props, ref) => (
          <Slide direction="up" ref={ref} {...props} />
        ))}
        {...rest}
      >
        <DialogTitle>{title}</DialogTitle>
        <DialogContent dividers>{dialogContent}</DialogContent>
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
            onClick={onConfirm}
            sx={{
              textTransform: 'none',
              backgroundColor: (theme) => theme.palette.error.main,
              color: (theme) => theme.palette.error.contrastText,
              '&:hover': {
                backgroundColor: (theme) => theme.palette.error.dark,
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

export default ConfirmDialog
