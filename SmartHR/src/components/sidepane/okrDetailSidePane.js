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
        sx: {
          width: { xs: '100%', md: 720 },
          borderRadius: { xs: 0, md: '12px 0 0 12px' },
          height: '100dvh',
          maxHeight: '100dvh',
          display: 'flex',
          flexDirection: 'column'
        }
      }}
    >
      <Box sx={{ flex: 1, overflowY: 'auto' }}>
        <OkrDetail data={data} />
      </Box>
    </Drawer>

  )
}

export default OkrDetailSidePane
