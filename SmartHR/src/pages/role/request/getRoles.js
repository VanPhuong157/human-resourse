import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useQuery} from 'react-query'

export default function useGetRoles(pageIndex, pageSize, filters) {
  const data = useQuery(['get-roles', pageIndex, pageSize], () =>
    rootApi.get(path.role.getRoles(pageIndex, pageSize, filters)),
  )
  return data
}
