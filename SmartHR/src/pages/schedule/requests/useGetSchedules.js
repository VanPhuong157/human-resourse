// Tạo folder mới nếu chưa có: pages/dashboard/schedule/requests/
// Tạo file: useGetSchedules.js
import { useQuery } from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export default function useGetSchedules(pageIndex = 1, pageSize = 10) {
  const queryFn = async () => {
    const url = path.schedule.getAll(pageIndex, pageSize)
    const res = await rootApi.get(url)
    return res.data // PaginatedList<ScheduleDTO>
  }

  return useQuery(['schedules', pageIndex, pageSize], queryFn, { keepPreviousData: true })
}