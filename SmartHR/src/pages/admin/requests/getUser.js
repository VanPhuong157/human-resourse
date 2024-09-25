import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useQuery} from 'react-query'

export const useGetUser = (pageIndex, pageSize, filters) => {
  const data = useQuery(
    ['get-user', pageIndex, pageSize, filters],
    () => rootApi.get(path.user.getUser(pageIndex, pageSize, filters)),
    {
      keepPreviousData: true,
    },
  )

  return data
}
