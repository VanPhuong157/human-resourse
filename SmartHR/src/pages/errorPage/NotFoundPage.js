import React from 'react';
import { Box, Typography, Button } from '@mui/material';
import { Link } from 'react-router-dom';

const NotFoundPage = () => {
  return (
    <Box
      sx={{
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        flexDirection: 'column',
        minHeight: '100vh',
        backgroundColor: '#f5f5f5',
      }}
    >
      <Typography variant="h1" style={{ color: '#3f51b5' }}>
        404
      </Typography>
      <Typography variant="h6" style={{ marginBottom: '2rem' }}>
        Trang bạn đang tìm kiếm không tồn tại.
      </Typography>
      <Button
        variant="contained"
        component={Link}
        to="/"
        style={{ textTransform: 'none' }}
      >
        Quay về trang chủ
      </Button>
    </Box>
  );
};

export default NotFoundPage;