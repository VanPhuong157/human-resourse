import {useQuery} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export default function useGetUserPermissions({userId, pageIndex, pageSize}) {
  const data = useQuery(['get-permission'], () =>
    rootApi.get(path.admin.getUserPermissions({userId, pageIndex, pageSize})),
  )
  return data
}
