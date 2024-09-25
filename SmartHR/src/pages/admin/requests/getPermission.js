import {useQuery} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export default function useGetPermissions(pageIndex, pageSize, filters) {
  const data = useQuery(['get-permission', pageIndex, pageSize, filters], () =>
    rootApi.get(path.admin.getPermissions(pageIndex, pageSize, filters)),
  )
  return data
}
