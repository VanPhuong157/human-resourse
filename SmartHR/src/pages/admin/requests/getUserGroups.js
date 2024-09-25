import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useQuery} from 'react-query'

export const useGetUserGroup = (pageIndex, pageSize, filters) => {
  const data = useQuery(
    ['get-user-group', pageIndex, pageSize, filters],
    () =>
      rootApi.get(path.userGroup.getUserGroup(pageIndex, pageSize, filters)),
    {
      keepPreviousData: true,
    },
  )

  return data
}
