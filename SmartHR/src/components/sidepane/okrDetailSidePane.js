import React from 'react'
import { Drawer, Box } from '@mui/material'
import OkrDetail from '../../pages/okr/okrTab/okrDetail/OkrDetail'

const OkrDetailSidePane = ({ open, onClose, data }) => {
  return (
    <Drawer
      anchor="right"
      open={open}
      onClose={onClose}
      keepMounted
      PaperProps={{
        sx: { width: { xs: '100%', md: 720 }, borderRadius: '12px 0 0 12px' }
      }}
    >
      <Box sx={{ height: '100%', overflowY: 'auto' }}>
        <OkrDetail data={data} />
      </Box>
    </Drawer>
    
  )
}

export default OkrDetailSidePane
