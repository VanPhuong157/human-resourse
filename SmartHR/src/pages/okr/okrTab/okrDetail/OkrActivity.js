import React, {useState, useEffect} from 'react'
import {
  Typography,
  Box,
  Paper,
  Grid,
  TextField,
  IconButton,
  Menu,
  MenuItem,
} from '@mui/material'
import {styled} from '@mui/system'
import useGetOkrDetailActivity from '../../../../pages/okr/requests/getOkrDetailActivity'
import {format} from 'date-fns'
import AddIcon from '@mui/icons-material/Add'
import MoreVertIcon from '@mui/icons-material/MoreVert'
import {useAddOkrHistoryComment} from '../../../../pages/okr/requests/addOkrHistoryComment'
import {showError, showToastSuccess} from '../../../../components/notification'
import {useDeleteOkrHistoryComment} from '../../../../pages/okr/requests/deleteOkrHistoryComment'

const StyledBox = styled(Box)({
  display: 'flex',
  alignItems: 'center',
  padding: '4px 16px',
  // borderBottom: '1px solid #e0e0e0',
})

const StyledPaper = styled(Paper)({
  padding: '10px',
  marginBottom: '8px',
  backgroundColor: '#f9f9f9',
  border: '1px solid #e0e0e0',
  borderRadius: '8px',
})

const OkrActivity = ({okrId, setRefetch}) => {
  const [comment, setComment] = useState('')
  const [anchorEl, setAnchorEl] = useState(null)
  const [selectedCommentId, setSelectedCommentId] = useState(null)
  const {data: dataOkrDetailAcitvity, refetch} = useGetOkrDetailActivity({
    okrId,
  })

  const {mutateAsync: addOkrHistoryCommentAsync} = useAddOkrHistoryComment({
    okrId,
  })
  const {mutateAsync: deleteOkrHistoryCommentAsync} =
    useDeleteOkrHistoryComment({
      okrHistoryId: selectedCommentId,
    })

  const handleAddComment = () => {
    addOkrHistoryCommentAsync(comment)
      .then((response) => {
        showToastSuccess({message: response.data?.message})
        refetch()
        setComment('')
      })
      .catch((error) => {
        showError(error.response?.data?.message)
      })
  }

  useEffect(() => {
    setRefetch(refetch)
  }, [refetch, setRefetch])

  const formatDate = (dateString) => {
    return format(new Date(dateString), 'MMM d, yyyy, hh:mm a')
  }

  const handleMenuClick = (event, commentId) => {
    setAnchorEl(event.currentTarget)
    setSelectedCommentId(commentId)
  }

  const handleMenuClose = () => {
    setAnchorEl(null)
    setSelectedCommentId(null)
  }

  const handleDeleteComment = () => {
    deleteOkrHistoryCommentAsync()
      .then((response) => {
        showToastSuccess({message: response.data?.message})
        refetch()
        setSelectedCommentId('')
      })
      .catch((error) => {
        showError(error.response?.data?.message)
      })
    handleMenuClose()
  }
  const okrData = dataOkrDetailAcitvity?.data
  return (
    <Box ml={10} mt={5}>
      <Typography variant="h4" mb={2}>
        Activity
      </Typography>
      <Paper elevation={3} style={{padding: '20px'}}>
        <Box mb={2} display="flex" alignItems="center">
          <TextField
            label="Add a comment..."
            variant="outlined"
            name="comment"
            value={comment}
            onChange={(e) => setComment(e.target.value)}
            fullWidth
          />
          <IconButton color="primary" onClick={handleAddComment}>
            <AddIcon />
          </IconButton>
        </Box>
        {okrData?.map((okrHistory) => (
          <React.Fragment key={okrHistory.id}>
            <StyledBox>
              <Box display={'flex'} alignItems="center">
                <Typography variant="body2" color="textSecondary" sx={{mr: 2}}>
                  <strong>{okrHistory.userName}</strong>
                </Typography>
                <Typography variant="caption" color="textSecondary">
                  {formatDate(okrHistory?.createdAt)}
                </Typography>
                {okrHistory.type === 'comment' && (
                  <IconButton
                    aria-controls="simple-menu"
                    aria-haspopup="true"
                    onClick={(event) => handleMenuClick(event, okrHistory.id)}
                    size="small"
                    sx={{marginLeft: 'auto', marginTop: '0px'}}
                  >
                    <MoreVertIcon />
                  </IconButton>
                )}
              </Box>
            </StyledBox>
            {okrHistory.type === 'comment' ? (
              <StyledBox>
                <Typography variant="body2" color="textSecondary">
                  {okrHistory.description}
                </Typography>
              </StyledBox>
            ) : (
              <>
                <StyledPaper>
                  <Grid container alignItems="center" spacing={2}>
                    <Grid item xs>
                      {okrHistory.description === '' ? (
                        <>
                          <Typography variant="body2" color="textSecondary">
                            {okrHistory?.keyResult}
                          </Typography>
                          <Typography variant="body2" color="textSecondary">
                            {okrHistory.oldAchieved} {okrHistory.unitOfTarget} (
                            {okrHistory.oldProgress}%{' '}
                            <span>{okrHistory.oldStatus}</span>) ➔{' '}
                            {okrHistory.newAchieved} {okrHistory.unitOfTarget} (
                            {okrHistory.newProgress}%{' '}
                            <span>{okrHistory.newStatus}</span>)
                          </Typography>
                        </>
                      ) : (
                        <Typography variant="body2" color="textSecondary">
                          {okrHistory?.description}
                        </Typography>
                      )}
                    </Grid>
                  </Grid>
                </StyledPaper>
              </>
            )}
          </React.Fragment>
        ))}
      </Paper>
      <Menu
        id="simple-menu"
        anchorEl={anchorEl}
        keepMounted
        open={Boolean(anchorEl)}
        onClose={handleMenuClose}
      >
        <MenuItem onClick={handleDeleteComment}>Delete</MenuItem>
      </Menu>
    </Box>
  )
}

export default OkrActivity
