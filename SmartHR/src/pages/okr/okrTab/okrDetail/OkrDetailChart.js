import React, { useEffect } from 'react'
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
import { Box, Typography } from '@mui/material'
import useGetOkrDetailActivity from '../../../../pages/okr/requests/getOkrDetailActivity'

// === Notion-like palette ===
const GRID = 'rgba(55,53,47,0.15)'
const TICK = 'rgba(55,53,47,0.6)'
const AREA = '#82ca9d'
const AREA_FILL = 'rgba(130, 202, 157, 0.2)'
const REF = '#16a34a'

const parseDate = (dateString) => new Date(dateString).getTime()

const getQuarter = (dateString) => {
  const month = new Date(dateString).getMonth() + 1
  if (month <= 3) return 'Q1'
  if (month <= 6) return 'Q2'
  if (month <= 9) return 'Q3'
  return 'Q4'
}

const getQuarterMonths = (q) => (
  q === 'Q1' ? ['January','February','March'] :
  q === 'Q2' ? ['April','May','June'] :
  q === 'Q3' ? ['July','August','September'] :
               ['October','November','December']
)

const filteredDataByQuarter = (data, quarter) =>
  data
    .filter((d) => getQuarter(d.createdAt) === quarter)
    .map((d) => ({ ...d, dateTime: parseDate(d.createdAt) }))

const calculateExpectedProgress = (data) => {
  if (!data.length) return []
  const sorted = [...data].sort((a, b) => a.dateTime - b.dateTime)
  const start = new Date(sorted[0].dateTime)
  const qStart = new Date(start.getFullYear(), start.getMonth() - (start.getMonth() % 3), 1)
  const qEnd = new Date(qStart); qEnd.setMonth(qEnd.getMonth() + 3, 0)
  const totalDays = (qEnd - qStart) / (1000 * 60 * 60 * 24)

  return sorted.map((d) => {
    const elapsed = (d.dateTime - qStart.getTime()) / (1000 * 60 * 60 * 24)
    const expected = Math.min((elapsed / totalDays) * 100, 100)
    return { ...d, expectedProgress: expected }
  })
}

const CustomTooltip = ({ active, payload, label }) => {
  if (!active || !payload?.length) return null
  const currentDate = new Date(label)
  const actual = payload[0].value
  const expected = payload[0].payload.expectedProgress
  return (
    <Box sx={{ bgcolor: '#fff', boxShadow: '0 4px 12px rgba(0,0,0,.08)', borderRadius: 1, p: 1 }}>
      <Typography sx={{ fontSize: 12, color: TICK }}>{`Date: ${currentDate.toISOString().split('T')[0]}`}</Typography>
      <Typography sx={{ fontSize: 12 }}>Progress: <strong>{actual}</strong>%</Typography>
      <Typography sx={{ fontSize: 12 }}>Expected: <strong>{expected?.toFixed(2)}</strong>%</Typography>
    </Box>
  )
}

const OkrDetailChart = ({ okrId }) => {
  const { data: dataOkrDetailAcitvity, refetch } = useGetOkrDetailActivity({ okrId })
  useEffect(() => { refetch() }, [refetch])

  if (!dataOkrDetailAcitvity?.data?.length) return <div>Loading...</div>

  const currentQuarter = getQuarter(dataOkrDetailAcitvity.data[0].createdAt)
  const months = getQuarterMonths(currentQuarter)

  const filteredData = filteredDataByQuarter(dataOkrDetailAcitvity.data, currentQuarter)
  const chartData = calculateExpectedProgress(filteredData)

  // Xác định đường tham chiếu (0% -> 100% cuối quý)
  const startDate = new Date(dataOkrDetailAcitvity.data[0].createdAt)
  const qStart = new Date(startDate.getFullYear(), startDate.getMonth() - (startDate.getMonth() % 3), 1)
  const qEnd = new Date(qStart); qEnd.setMonth(qEnd.getMonth() + 3, 0)

  const combinedData = chartData // đủ dùng: dữ liệu thực + expectedProgress

  return (
    // === QUAN TRỌNG: width="100%" để không tràn qua cột phải ===
    <ResponsiveContainer width="100%" height={300}>
      <AreaChart
        data={combinedData}
        margin={{ top: 10, right: 8, left: 0, bottom: 0 }}
      >
        <CartesianGrid stroke={GRID} strokeDasharray="3 3" />
        <XAxis
          dataKey="dateTime"
          type="number"
          domain={['dataMin', 'dataMax']}
          tickFormatter={(tick) => months[new Date(tick).getMonth() % 3]}
          tick={{ fill: TICK, fontSize: 12 }}
          tickLine={false}
          axisLine={{ stroke: GRID }}
        />
        <YAxis
          domain={[0, 100]}
          tickFormatter={(t) => `${t} %`}
          tick={{ fill: TICK, fontSize: 12 }}
          width={34}
          tickLine={false}
          axisLine={{ stroke: GRID }}
        />
        <Tooltip content={<CustomTooltip />} />
        <ReferenceLine
          stroke={REF}
          strokeDasharray="3 3"
          segment={[
            { x: qStart.getTime(), y: 0 },
            { x: qEnd.getTime(), y: 100 },
          ]}
        />
        <Area
          type="monotone"
          dataKey="newProgress"
          stroke={AREA}
          fill={AREA_FILL}
          strokeWidth={3}
          dot={{ r: 5, stroke: AREA, strokeWidth: 2, fill: '#fff' }}
          activeDot={{ r: 7 }}
        />
      </AreaChart>
    </ResponsiveContainer>
  )
}

export default OkrDetailChart
