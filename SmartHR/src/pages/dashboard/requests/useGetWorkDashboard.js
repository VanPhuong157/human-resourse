// pages/dashboard/requests/useGetWorkDashboard.js
import { useQuery } from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export default function useGetWorkDashboard({ departmentId, userId, from, to, type }) {
  const queryFn = async () => {
    const res = await rootApi.get(
      path.statistic.workDashboard(type, { departmentId, userId, from, to })
    )
    return res.data // WorkDashboardDTO
  }

  return useQuery(
    ['work-dashboard', type, departmentId, userId, from, to],
    queryFn,
    { keepPreviousData: true }
  )
}
