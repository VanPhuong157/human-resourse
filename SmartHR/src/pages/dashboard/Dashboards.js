// pages/dashboard/WorkDashboard.js
import React, { useState } from 'react'
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  Tooltip,
  ResponsiveContainer,
} from 'recharts'
import {
  Container,
  Grid,
  Paper,
  Typography,
  Box,
  MenuItem,
  Select,
  CircularProgress,
  Tabs,
  Tab,
} from '@mui/material'
import { PieChart } from '@mui/x-charts/PieChart'
import { useDrawingArea } from '@mui/x-charts/hooks'
import { styled } from '@mui/material/styles'

import useGetDepartments from '../../pages/department/requests/getDepartment'
import useGetWorkDashboard from './requests/useGetWorkDashboard'
import useGetUsers from './requests/useGetUsers'

// kích thước pie
const pieSize = { width: 340, height: 220 }

const StyledText = styled('text')(() => ({
  fill: '#111',
  textAnchor: 'middle',
  dominantBaseline: 'central',
  fontSize: 20,
}))

const PieCenterLabel = ({ children }) => {
  const { width, height, left, top } = useDrawingArea()
  return (
    <StyledText x={left + width / 2} y={top + height / 2}>
      {children}
    </StyledText>
  )
}

const HeaderBox = styled(Box)(() => ({
  background: 'linear-gradient(135deg, #1976d2 0%, #1565c0 100%)',
  borderRadius: '12px',
  padding: '24px',
  marginBottom: '16px',
  color: 'white',
  boxShadow: '0 8px 24px rgba(25, 118, 210, 0.15)',
}))

const StyledPaper = styled(Paper)(() => ({
  background: '#ffffff',
  borderRadius: '12px',
  boxShadow: '0 4px 12px rgba(0, 0, 0, 0.08)',
  border: '1px solid rgba(0, 0, 0, 0.05)',
  transition: 'all 0.3s ease',
  '&:hover': {
    boxShadow: '0 8px 24px rgba(0, 0, 0, 0.12)',
  },
}))

// config chung cho pie
const pieSeriesBase = {
  innerRadius: 70,
  outerRadius: 80,
  cx: 120,
}

const WorkDashboard = () => {
  const { data: dataDepartment } = useGetDepartments()
  const { data: userList } = useGetUsers()

  const [departmentId, setDepartmentId] = useState(null)
  const [userId, setUserId] = useState(null)
  // 'okr' | 'policystep' — khớp đúng BE
  const [activeType, setActiveType] = useState('okr')

  const { data, isLoading } = useGetWorkDashboard({
    departmentId,
    userId,
    from: null,
    to: null,
    type: activeType,
  })

  // API trả thẳng WorkDashboardDTO
  const stats = data || {}

  const priorityData = Object.entries(stats.byPriority || {}).map(
    ([k, v]) => ({ label: k, value: v }),
  )

  const statusData = Object.entries(stats.byStatus || {}).map(
    ([k, v]) => ({ label: k, value: v }),
  )

  const totalTasks = stats.totalTasks || 0
  const users = stats.byUser || []

  const tasksByDate = (stats.byDate || []).map((x) => {
    let d = new Date(x.date)
    if (Number.isNaN(d.getTime()) && typeof x.date === 'string') {
      const [dd, mm, yyyy] = x.date.split('/')
      if (dd && mm && yyyy) d = new Date(+yyyy, +mm - 1, +dd)
    }
    return {
      ...x,
      dateLabel: Number.isNaN(d.getTime())
        ? x.date
        : d.toLocaleDateString('vi-VN'),
    }
  })

  const selectedUserStats = userId
    ? users.find((u) => u.userId === userId)
    : null

  const headerTitle =
    activeType === 'okr'
      ? 'Work Dashboard (Daily Task)'
      : 'Work Dashboard (Project Task)'

  if (isLoading) {
    return (
      <Box
        display="flex"
        justifyContent="center"
        alignItems="center"
        minHeight="100vh"
      >
        <CircularProgress />
      </Box>
    )
  }

  return (
    <Container
      maxWidth={false}
      disableGutters
      sx={{ pl: 3, pr: 4, pt: 3, pb: 4 }}
    >
      {/* HEADER */}
      <HeaderBox
        sx={{
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'center',
        }}
      >
        <Box>
          <Typography
            variant="h4"
            sx={{ fontWeight: 'bold', color: 'white', mb: 0.5 }}
          >
            {headerTitle}
          </Typography>
          <Typography
            variant="body2"
            sx={{ color: 'rgba(255,255,255,0.9)' }}
          >
            Theo dõi tiến độ và hiệu suất công việc của team
          </Typography>
        </Box>

        <Box sx={{ display: 'flex', gap: 2 }}>
          {/* chọn phòng ban */}
          <Select
            value={departmentId || 'All Department'}
            onChange={(e) =>
              setDepartmentId(
                e.target.value === 'All Department' ? null : e.target.value,
              )
            }
            variant="outlined"
            size="small"
            sx={{
              minWidth: 200,
              backgroundColor: 'rgba(255,255,255,0.95)',
              borderRadius: '8px',
              '& .MuiOutlinedInput-root': {
                color: '#1976d2',
              },
            }}
          >
            <MenuItem value="All Department">Tất cả Phòng Ban</MenuItem>
            {dataDepartment?.data?.items.map(({ id, name }) => (
              <MenuItem key={id} value={id}>
                {name}
              </MenuItem>
            ))}
          </Select>

          {/* chọn nhân viên */}
          <Select
            value={userId || 'All Users'}
            onChange={(e) =>
              setUserId(e.target.value === 'All Users' ? null : e.target.value)
            }
            variant="outlined"
            size="small"
            sx={{
              minWidth: 220,
              backgroundColor: 'rgba(255,255,255,0.95)',
              borderRadius: '8px',
              '& .MuiOutlinedInput-root': {
                color: '#1976d2',
              },
            }}
          >
            <MenuItem value="All Users">Tất cả Nhân Viên</MenuItem>
            {(userList || []).map(({ id, fullName }) => (
              <MenuItem key={id} value={id}>
                {fullName}
              </MenuItem>
            ))}
          </Select>
        </Box>
      </HeaderBox>

      {/* TABS OKR / POLICYSTEP */}
      <Box sx={{ mb: 3, borderBottom: '1px solid #e0e0e0' }}>
        <Tabs
          value={activeType}
          onChange={(_, v) => setActiveType(v)}
          textColor="primary"
          indicatorColor="primary"
        >
          <Tab label="Daily Task Dashboard" value="okr" />
          <Tab label="Project Task Dashboard" value="policystep" />
        </Tabs>
      </Box>

      {/* NỘI DUNG DASHBOARD */}
      <Grid container spacing={3}>
        {/* Tổng công việc + hiệu suất người được chọn */}
        <Grid item xs={12} sm={6} md={3}>
          <StyledPaper sx={{ p: 3, textAlign: 'center' }}>
            <Typography
              variant="body2"
              sx={{
                color: '#666',
                mb: 1,
                textTransform: 'uppercase',
                letterSpacing: '0.5px',
                fontSize: '12px',
                fontWeight: '600',
              }}
            >
              Tổng Công Việc
            </Typography>
            <Typography
              variant="h2"
              sx={{
                fontWeight: 'bold',
                background: 'linear-gradient(135deg, #1976d2, #1565c0)',
                backgroundClip: 'text',
                WebkitBackgroundClip: 'text',
                WebkitTextFillColor: 'transparent',
                mt: 1,
              }}
            >
              {totalTasks}
            </Typography>

            {selectedUserStats && (
              <Box sx={{ mt: 2, textAlign: 'left', fontSize: 13 }}>
                <Typography sx={{ fontWeight: 600 }}>
                  {selectedUserStats.fullName}
                </Typography>
                <Typography>
                  Tổng task: <b>{selectedUserStats.totalTasks}</b>
                </Typography>
                <Typography>
                  Hoàn thành: <b>{selectedUserStats.completedTasks}</b>
                </Typography>
                <Typography>
                  On-time:{' '}
                  <b>{(selectedUserStats.onTimeRate * 100).toFixed(1)}%</b>
                </Typography>
                <Typography>
                  Điểm hiệu suất:{' '}
                  <b>{(selectedUserStats.finalScore * 100).toFixed(1)}%</b>
                </Typography>
              </Box>
            )}
          </StyledPaper>
        </Grid>

        {/* Mức độ ưu tiên */}
        <Grid item xs={12} sm={6} md={4.5}>
          <StyledPaper sx={{ p: 3, overflow: 'hidden' }}>
            <Typography variant="h6" sx={{ fontWeight: '600', mb: 2 }}>
              Mức Độ Ưu Tiên
            </Typography>
            <Box sx={{ display: 'flex', justifyContent: 'center' }}>
              <Box sx={{ width: pieSize.width, mx: 'auto' }}>
                <PieChart
                  series={[
                    {
                      ...pieSeriesBase,
                      data: priorityData,
                    },
                  ]}
                  {...pieSize}
                  slotProps={{
                    legend: {
                      direction: 'column',
                      position: { vertical: 'middle', horizontal: 'right' },
                    },
                  }}
                >
                  <PieCenterLabel>{totalTasks}</PieCenterLabel>
                </PieChart>
              </Box>
            </Box>
          </StyledPaper>
        </Grid>

        {/* Trạng thái công việc */}
        <Grid item xs={12} sm={12} md={4.5}>
          <StyledPaper sx={{ p: 3, overflow: 'hidden' }}>
            <Typography variant="h6" sx={{ fontWeight: '600', mb: 2 }}>
              Trạng Thái Công Việc
            </Typography>
            <Box sx={{ display: 'flex', justifyContent: 'center' }}>
              <Box sx={{ width: pieSize.width, mx: 'auto' }}>
                <PieChart
                  series={[
                    {
                      ...pieSeriesBase,
                      data: statusData,
                    },
                  ]}
                  {...pieSize}
                  slotProps={{
                    legend: {
                      direction: 'column',
                      position: { vertical: 'middle', horizontal: 'right' },
                    },
                  }}
                >
                  <PieCenterLabel>{totalTasks}</PieCenterLabel>
                </PieChart>
              </Box>
            </Box>
          </StyledPaper>
        </Grid>

        {/* Công việc theo thành viên */}
        <Grid item xs={12} md={6}>
          <StyledPaper sx={{ p: 3 }}>
            <Typography variant="h6" sx={{ fontWeight: '600', mb: 2 }}>
              Công Việc Theo Thành Viên
            </Typography>
            <ResponsiveContainer width="100%" height={280}>
              <BarChart
                data={users}
                layout="vertical"
                barCategoryGap="20%"
                margin={{ left: 120, right: 20, top: 10, bottom: 10 }}
              >
                <XAxis type="number" stroke="#111111ff" />
                <YAxis
                  type="category"
                  dataKey="fullName"
                  width={110}
                  tick={{ fontSize: 16 }}
                  stroke="#161616ff"
                />
                <Tooltip
                  contentStyle={{
                    backgroundColor: '#f8f6f6ff',
                    border: '1px solid #f7f3f3ff',
                    borderRadius: '8px',
                  }}
                />
                <Bar
                  dataKey="totalTasks"
                  name="Tổng"
                  fill="#1976d2"
                  radius={[0, 8, 8, 0]}
                />
              </BarChart>
            </ResponsiveContainer>
          </StyledPaper>
        </Grid>

        {/* Công việc theo thời gian */}
        <Grid item xs={12} md={6}>
          <StyledPaper sx={{ p: 3 }}>
            <Typography variant="h6" sx={{ fontWeight: '600', mb: 2 }}>
              Công Việc Theo Thời Gian
            </Typography>
            <ResponsiveContainer width="100%" height={280}>
              <BarChart
                data={tasksByDate}
                margin={{ left: 0, right: 0, top: 10, bottom: 10 }}
              >
                <XAxis
                  dataKey="dateLabel"
                  stroke="#0e0d0dff"
                  tick={{ fontSize: 14 }}
                  angle={-45}
                  textAnchor="end"
                  height={60}
                />
                <YAxis stroke="#0f0f0fff" />
                <Tooltip
                  contentStyle={{
                    backgroundColor: '#fff',
                    border: '1px solid #ddd',
                    borderRadius: '8px',
                  }}
                />
                <Bar
                  dataKey="count"
                  name="Số Lượng"
                  fill="#549fd1ff"
                  radius={[8, 8, 0, 0]}
                />
              </BarChart>
            </ResponsiveContainer>
          </StyledPaper>
        </Grid>

        {/* Điểm hiệu suất */}
        <Grid item xs={12}>
          <StyledPaper sx={{ p: 3 }}>
            <Typography variant="h6" sx={{ fontWeight: '600', mb: 2 }}>
              Điểm Hiệu Suất Làm Việc
            </Typography>
            <ResponsiveContainer width="100%" height={300}>
              <BarChart
                data={users}
                margin={{ left: 0, right: 0, top: 10, bottom: 10 }}
              >
                <XAxis
                  dataKey="fullName"
                  stroke="#181717ff"
                  tick={{ fontSize: 18 }}
                  textAnchor="center"
                  height={70}
                />
                <YAxis domain={[0, 1]} stroke="#141414ff" />
                <Tooltip
                  contentStyle={{
                    backgroundColor: '#0e0d0dff',
                    border: '1px solid #080808ff',
                    borderRadius: '8px',
                  }}
                  formatter={(value) => (value * 100).toFixed(1) + '%'}
                />
                <Bar
                  dataKey="finalScore"
                  name="Điểm"
                  fill="#f53c35ff"
                  radius={[8, 8, 0, 0]}
                />
              </BarChart>
            </ResponsiveContainer>
          </StyledPaper>
        </Grid>
      </Grid>
    </Container>
  )
}

export default WorkDashboard
