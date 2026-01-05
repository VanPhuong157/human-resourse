
import { useQuery } from 'react-query'
import { rootApi } from '../../../api/rootApi'
import path from '../../../api/path'

export default function useGetAllUsersSimple() {
  return useQuery(
    ['all-users-simple'],
    async () => {
      // Gọi API nhẹ nhất có thể - dùng endpoint lấy user của bạn
      const res = await rootApi.get(path.user.getAllUser(1, 1000))
      
      // Chỉ lấy Id và FullName để nhẹ
      const rawItems = res.data?.items || res.data?.data?.items || res.data || []
      
      return rawItems.map(user => ({
        Id: user.Id || user.id,
        FullName: user.UserInformation?.FullName || user.fullName || 'Unknown'
      }))
    },
    {
      staleTime: 10 * 60 * 1000, // Cache 10 phút
      retry: 2,
      refetchOnWindowFocus: false,
    }
  )
}