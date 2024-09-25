import {useQuery} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export const useGetUserByDepart = (pageIndex, pageSize, departmentId) => {
  const data = useQuery(
    ['get-user-by-depart', pageIndex, pageSize, departmentId],
    () =>
      rootApi.get(
        path.user.getUserInformationByDepart(pageIndex, pageSize, departmentId),
      ),
  )
  return data
}
