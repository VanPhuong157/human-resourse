import React, {useEffect} from 'react'
import {
  AreaChart,
  Area,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  ResponsiveContainer,
  ReferenceLine,
} from 'recharts'
import {Box, Typography} from '@mui/material'
import useGetOkrDetailActivity from '../../../../pages/okr/requests/getOkrDetailActivity'

const parseDate = (dateString) => new Date(dateString).getTime()

const getQuarter = (dateString) => {
  const month = new Date(dateString).getMonth() + 1
  if (month >= 1 && month <= 3) return 'Q1'
  if (month >= 4 && month <= 6) return 'Q2'
  if (month >= 7 && month <= 9) return 'Q3'
  if (month >= 10 && month <= 12) return 'Q4'
}

const getQuarterMonths = (quarter) => {
  switch (quarter) {
    case 'Q1':
      return ['January', 'February', 'March']
    case 'Q2':
      return ['April', 'May', 'June']
    case 'Q3':
      return ['July', 'August', 'September']
    case 'Q4':
      return ['October', 'November', 'December']
    default:
      return []
  }
}

const getDefaultDatesForQuarter = (quarter) => {
  const year = new Date().getFullYear()
  switch (quarter) {
    case 'Q1':
      return [`${year}-01-01`, `${year}-02-15`, `${year}-03-31`]
    case 'Q2':
      return [`${year}-04-01`, `${year}-05-15`, `${year}-06-30`]
    case 'Q3':
      return [`${year}-07-01`, `${year}-08-15`, `${year}-09-30`]
    case 'Q4':
      return [`${year}-10-01`, `${year}-11-15`, `${year}-12-31`]
    default:
      return []
  }
}

const filteredDataByQuarter = (data, quarter) =>
  data
    .filter((d) => getQuarter(d.createdAt) === quarter)
    .map((d) => ({...d, dateTime: parseDate(d.createdAt)}))

const calculateExpectedProgress = (data) => {
  if (data.length === 0) return []

  // Sort data by date
  const sortedData = data.sort((a, b) => a.dateTime - b.dateTime)

  // Define the start and end of the quarter
  const startDate = new Date(sortedData[0].dateTime)
  const quarterStart = new Date(
    startDate.getFullYear(),
    startDate.getMonth() - (startDate.getMonth() % 3),
    1,
  )
  const quarterEnd = new Date(quarterStart)
  quarterEnd.setMonth(quarterEnd.getMonth() + 3, 0) // Last day of the quarter

  // Calculate total days in the quarter
  const totalDaysInQuarter = (quarterEnd - quarterStart) / (1000 * 60 * 60 * 24)

  return sortedData.map((d) => {
    // Calculate the progress as the percentage of elapsed days in the quarter
    const elapsedDays =
      (d.dateTime - quarterStart.getTime()) / (1000 * 60 * 60 * 24)
    const progress = Math.min((elapsedDays / totalDaysInQuarter) * 100, 100)
    return {...d, expectedProgress: progress}
  })
}
const CustomTooltip = ({active, payload, label}) => {
  if (active && payload && payload.length) {
    const currentDate = new Date(label)
    const actualProgress = payload[0].value
    const expectedProgress = payload[0].payload.expectedProgress

    return (
      <Box className="custom-tooltip">
        <Typography>{`Date: ${currentDate.toISOString().split('T')[0]}`}</Typography>
        <Typography>
          Progress:<strong>{` ${actualProgress}`}</strong>%
        </Typography>
        <Typography>
          Expected:<strong>{` ${expectedProgress.toFixed(2)}`}</strong>%
        </Typography>
      </Box>
    )
  }

  return null
}

const OkrDetailChart = ({okrId}) => {
  const {data: dataOkrDetailAcitvity, refetch} = useGetOkrDetailActivity({
    okrId,
  })

  useEffect(() => {
    refetch()
  }, [refetch])

  if (
    !dataOkrDetailAcitvity ||
    !dataOkrDetailAcitvity.data ||
    dataOkrDetailAcitvity.data.length === 0
  ) {
    return <div>Loading...</div>
  }

  const currentQuarter = getQuarter(dataOkrDetailAcitvity.data[0].createdAt)
  const defaultDates = getDefaultDatesForQuarter(currentQuarter)
  const filteredData = filteredDataByQuarter(
    dataOkrDetailAcitvity.data,
    currentQuarter,
  )
  const chartData = calculateExpectedProgress(filteredData)
  const months = getQuarterMonths(currentQuarter)

  const startDate = new Date(dataOkrDetailAcitvity.data[0].createdAt)
  const quarterStart = new Date(
    startDate.getFullYear(),
    startDate.getMonth() - (startDate.getMonth() % 3),
    1,
  )
  const quarterEnd = new Date(quarterStart)
  quarterEnd.setMonth(quarterEnd.getMonth() + 3, 0)

  const totalDaysInQuarter = (quarterEnd - quarterStart) / (1000 * 60 * 60 * 24)

  const defaultData = defaultDates.map((date) => {
    const dateTime = parseDate(date)
    const elapsedDays =
      (dateTime - quarterStart.getTime()) / (1000 * 60 * 60 * 24)
    const expectedProgress = Math.min(
      (elapsedDays / totalDaysInQuarter) * 100,
      100,
    )
    return {
      dateTime,
      newProgress: dataOkrDetailAcitvity.data.newProgress,
      expectedProgress,
    }
  })

  const combinedData = [...defaultData, ...chartData]

  return (
    <ResponsiveContainer width={700} height={300} style={{margin: '0px 50px'}}>
      <AreaChart
        data={combinedData}
        margin={{
          top: 10,
          right: 30,
          left: 0,
          bottom: 0,
        }}
      >
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis
          dataKey="dateTime"
          type="number"
          domain={['dataMin', 'dataMax']}
          tickFormatter={(tick) => months[new Date(tick).getMonth() % 3]}
        />
        <YAxis domain={[0, 100]} tickFormatter={(tick) => `${tick} %`} />
        <Tooltip content={<CustomTooltip />} />
        <ReferenceLine
          stroke="green"
          strokeDasharray="3 3"
          segment={[
            {x: combinedData[0]?.dateTime || 0, y: 0},
            {x: parseDate(defaultDates[2]), y: 100},
          ]}
        />
        <Area
          type="monotone"
          dataKey="newProgress"
          stroke="#82ca9d"
          fill="rgba(130, 202, 157, 0.2)"
          strokeWidth={3}
          dot={{r: 5, stroke: '#82ca9d', strokeWidth: 2, fill: '#fff'}}
          activeDot={{r: 8}}
        />
      </AreaChart>
    </ResponsiveContainer>
  )
}

export default OkrDetailChart
