import {useParams} from 'react-router-dom'
import ComponentTable from '../../../components/table'
import React from 'react'
import Box from '@mui/material/Box'
import Paper from '@mui/material/Paper'
import useGetEmployeeHistory from '../request/getEmployeeHistories'
import CircularProgress from '@mui/material/CircularProgress'

const formatDateTime = (dateString) => {
  const date = new Date(dateString)
  const hours = String(date.getHours()).padStart(2, '0')
  const minutes = String(date.getMinutes()).padStart(2, '0')
  const day = String(date.getDate()).padStart(2, '0')
  const month = String(date.getMonth() + 1).padStart(2, '0')
  const year = date.getFullYear()
  return `${hours}:${minutes} ${day}-${month}-${year}`
}

const EmployeeHistoryTab = () => {
  const {userId} = useParams()
  console.log('userId', userId)
  const {data: dataEmployeeHistory, isLoading} = useGetEmployeeHistory({userId})
  console.log('dataEmployeeHistory', dataEmployeeHistory)
  const formattedData = Array.isArray(dataEmployeeHistory?.data)
    ? dataEmployeeHistory.data.map((item) => ({
        ...item,
        startTime: formatDateTime(item.startTime),
        endTime: item.endTime ? formatDateTime(item.endTime) : 'Working',
      }))
    : []

  const columns = [
    {
      id: 'departmentName',
      label: 'DepartmentName',
      width: '25%',
    },
    {
      id: 'roleName',
      label: 'RoleName',
      width: '25%',
    },
    {
      id: 'startTime',
      label: 'StartTime',
      width: '25%',
    },
    {
      id: 'endTime',
      label: 'EndTime',
      width: '25%',
    },
  ]
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
    <Box sx={{width: '100%'}}>
      <Paper sx={{width: '100%', mb: 2}}>
        <ComponentTable
          columns={columns}
          totalItems={dataEmployeeHistory?.data?.totalItems}
          data={formattedData}
          disableColCheckbox={true}
          disablePaginationTable
        />
      </Paper>
    </Box>
  )
}

export default EmployeeHistoryTab
