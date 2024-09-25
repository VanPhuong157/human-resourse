import React, {useState, useEffect} from 'react'
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
} from '@mui/material'
import {PieChart} from '@mui/x-charts/PieChart'
import {useDrawingArea} from '@mui/x-charts/hooks'
import {styled} from '@mui/material/styles'
import useGetStatisticCandidates from './requests/getStatisticCandidates'
import useGetStatisticUsers from './requests/getStatisticUsers'
import useGetStatisticJobPosts from './requests/getStatisticJobPosts'
import useGetDepartments from '../../pages/department/requests/getDepartment'
import useGetStatisticOkr from './requests/getStatisticOkr'
import useGetStatisticOkrRequest from './requests/getStatisticOkrRequest'
import useGetStatisticCandidateOfPost from './requests/getStatisticCandidateOfPost'

const size = {width: 400, height: 200}
const StyledText = styled('text')(({theme}) => ({
  fill: theme.palette.text.primary,
  textAnchor: 'middle',
  dominantBaseline: 'central',
  fontSize: 20,
}))

const PieCenterLabel = ({children}) => {
  const {width, height, left, top} = useDrawingArea()
  return (
    <StyledText x={left + width / 2} y={top + height / 2}>
      {children}
    </StyledText>
  )
}

const Dashboard = () => {
  const {data: dataDepartment} = useGetDepartments()
  const [departmentId, setDepartmentId] = useState(null)

  const {data: dataStatisticCandidates, refetch: refetchCandidates} =
    useGetStatisticCandidates({departmentId})
  const {data: dataStatisticUsers, refetch: refetchUsers} =
    useGetStatisticUsers({departmentId})
  const {data: dataStatisticJobPosts, refetch: refetchJobPosts} =
    useGetStatisticJobPosts({departmentId})
  const {data: dataStatisticOkr, refetch: refetchStatisticOkr} =
    useGetStatisticOkr({departmentId})
  const {data: dataStatisticOkrRequest, refetch: refetchStatisticOkrRequest} =
    useGetStatisticOkrRequest({departmentId})
  const {
    data: dataStatisticCandidateOfPost,
    refetch: refetchStatisticCandidateOfPost,
    isLoading: isLoadingStatisticCandidateOfPost,
  } = useGetStatisticCandidateOfPost({departmentId})

  const length = dataStatisticCandidateOfPost?.data?.length || 0
  const heightJobPostChart =
    length === 0
      ? 0
      : length === 1
        ? 80
        : length === 2
          ? length * 60
          : length * 40

  const dataOkrCircle = [
    {value: dataStatisticOkr?.data?.Done, label: 'Done'},
    {value: dataStatisticOkr?.data?.NotStarted, label: 'Not Started'},
    {value: dataStatisticOkr?.data?.Processing, label: 'Processing'},
  ]

  const dataOkrRequestCircle = [
    {value: dataStatisticOkrRequest?.data?.Pending, label: 'Pending'},
    {value: dataStatisticOkrRequest?.data?.Approve, label: 'Approve'},
    {value: dataStatisticOkrRequest?.data?.Reject, label: 'Reject'},
  ]

  const totalDataOkrCircle = dataOkrCircle.reduce(
    (acc, item) => acc + item.value,
    0,
  )
  const totalDataOkrRequestCircle = dataOkrRequestCircle.reduce(
    (acc, item) => acc + item.value,
    0,
  )
  useEffect(() => {
    refetchCandidates()
    refetchUsers()
    refetchJobPosts()
    refetchStatisticOkr()
    refetchStatisticOkrRequest()
    refetchStatisticCandidateOfPost()
  }, [departmentId])

  console.log('datadepartment', dataDepartment)

  if (isLoadingStatisticCandidateOfPost) {
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
    <Container>
      <Typography
        variant="h4"
        sx={{fontWeight: 'bold', color: '#1976d2'}}
        gutterBottom
      >
        Dashboard
      </Typography>

      <Box mb={3}>
        <Select
          value={departmentId || 'All Department'}
          onChange={(e) =>
            setDepartmentId(
              e.target.value === 'All Department' ? null : e.target.value,
            )
          }
          variant="outlined"
          sx={{minWidth: 160}}
        >
          <MenuItem value="All Department">All Department</MenuItem>
          {dataDepartment?.data?.items.map(({id, name}) => (
            <MenuItem key={id} value={id}>
              {name}
            </MenuItem>
          ))}
        </Select>
      </Box>

      <Grid container spacing={3}>
        {[
          {label: 'Employees', data: dataStatisticUsers},
          {label: 'Candidate', data: dataStatisticCandidates},
          {label: 'Job Post', data: dataStatisticJobPosts},
        ].map(({label, data}) => (
          <Grid item xs={4} key={label}>
            <Paper elevation={3} style={{padding: '20px'}}>
              <Typography variant="h6">{label}</Typography>
              <Typography variant="h4">{data?.data.total}</Typography>
              <Typography color={data?.data.percentage >= 0 ? 'green' : 'red'}>
                {data?.data.percentage >= 0 ? '↑' : '↓'} {data?.data.percentage}
                %
              </Typography>
            </Paper>
          </Grid>
        ))}

        {[
          {title: 'OKR Public', data: dataOkrCircle, total: totalDataOkrCircle},
          {
            title: 'OKR Request',
            data: dataOkrRequestCircle,
            total: totalDataOkrRequestCircle,
          },
        ].map(({title, data, total}) => (
          <Grid item xs={6} key={title}>
            <Paper
              elevation={3}
              style={{padding: '20px', display: 'flex', alignItems: 'center'}}
            >
              <Box flex={1}>
                <Typography variant="h6">{title}</Typography>
                <PieChart series={[{data, innerRadius: 80}]} {...size}>
                  <PieCenterLabel>Total: {total}</PieCenterLabel>
                </PieChart>
              </Box>
            </Paper>
          </Grid>
        ))}

        <Grid item xs={12}>
          <Paper elevation={3} style={{padding: '20px'}}>
            <Typography variant="h6">Job Post Statistics</Typography>
            <ResponsiveContainer width="100%" height={heightJobPostChart}>
              <BarChart
                data={dataStatisticCandidateOfPost?.data}
                layout="vertical"
                barCategoryGap="20%"
              >
                <XAxis type="number" tick={false} />
                <YAxis dataKey="title" type="category" width={150} />
                <Tooltip />
                <Bar dataKey="cv" fill="#1976d2" />
              </BarChart>
            </ResponsiveContainer>
          </Paper>
        </Grid>
      </Grid>
    </Container>
  )
}

export default Dashboard
