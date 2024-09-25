import {
    styled,
    Typography,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    TextField,
    Box,
    IconButton,
  } from '@mui/material'


const StyledDialog = styled(Dialog)(({theme}) => ({
    '& .MuiDialog-paper': {
      borderRadius: '16px',
    },
    '& .MuiDialogContent-root': {
      padding: theme.spacing(2),
    },
    '& .MuiDialogActions-root': {
      padding: theme.spacing(1),
    },
  }))
  
  const StyledTextField = styled(TextField)(({theme, error}) => ({
    '& .MuiInputLabel-asterisk': {
      color: 'red',
    },
    '& .MuiOutlinedInput-root': {
      '& fieldset': {
        borderColor: error ? 'red' : 'default',
      },
    },
  }))

  export default function RecruitmentDialog() {
    return (
      <>
      <StyledDialog
        open={dialogOpen}
        onClose={handleCloseDialog}
        aria-labelledby="application-form-dialog"
        maxWidth="md"
        fullWidth
      >
        <DialogTitle id="application-form-dialog">
          <Box display="flex" justifyContent="flex-end">
            <IconButton
              aria-label="close"
              onClick={handleCloseDialog}
              sx={{
                position: 'absolute',
                right: 8,
                top: 8,
                color: (theme) => theme.palette.grey[500],
              }}
            >
              <CloseIcon />
            </IconButton>
          </Box>
          <Box display="flex" flexDirection="column" alignItems="center">
            <Typography
              variant="h4"
              component="h1"
              sx={{
                color: '#f08538',
                fontWeight: 800,
                letterSpacing: '0.15px',
                lineHeight: '83%',
                marginTop: '6px',
                textAlign: 'center',
              }}
            >
              START WITH SHRS COMPANY
            </Typography>
          </Box>
        </DialogTitle>

        {/* Dialog Apply CV */}
        <DialogContent>
          <form onSubmit={handleSubmit}>
            <Box display="flex" flexDirection="column" gap={2} mt={2}>
              <InputRow>
                <StyledTextField
                  required
                  label="Full Name"
                  name="FullName"
                  value={formData.FullName}
                  onChange={handleInputChange}
                  fullWidth
                />
                <StyledTextField
                  required
                  label="Phone Number"
                  name="PhoneNumber"
                  value={formData.PhoneNumber}
                  onChange={handleInputChange}
                  fullWidth
                />
                <StyledTextField
                  required
                  label="Email"
                  name="Email"
                  type="email"
                  value={formData.Email}
                  onChange={handleInputChange}
                  fullWidth
                />
              </InputRow>
              <Box mt={2}>
                <Typography variant="body1" fontStyle="italic">
                  Attach file CV
                </Typography>
                <input
                  accept=".pdf"
                  style={{display: 'none'}}
                  id="cv-file"
                  type="file"
                  onChange={handleFileUpload}
                />
                <label htmlFor="cv-file">
                  <FileUploadButton component="span">
                    {formData.CvFile ? formData.CvFile.name : 'Tải lên'}
                  </FileUploadButton>
                </label>
              </Box>
            </Box>
          </form>
        </DialogContent>
        <DialogActions sx={{justifyContent: 'center'}}>
          <SubmitButton onClick={handleSubmit}>Ứng tuyển</SubmitButton>
        </DialogActions>
      </StyledDialog>
      </>
    )
  }
        