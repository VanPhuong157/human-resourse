import { useQuery } from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export default function useGetUsers() {
  const queryFn = async () => {
    // lấy trang 1, tối đa 1000 user
    const url = path.user.getAllUser(1, 1000)
    const res = await rootApi.get(url)

    const raw = res.data
    const list =
      raw?.items ??
      raw?.data?.items ??
      raw

    return list || []
  }

  return useQuery(['users-all'], queryFn)
}
