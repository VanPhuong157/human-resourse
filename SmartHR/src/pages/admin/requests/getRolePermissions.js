import {useQuery} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export default function useGetRolePermissions({roleId, pageIndex, pageSize}) {
  const data = useQuery(['get-role-permission'], () =>
    rootApi.get(path.admin.getRolePermissions({roleId, pageIndex, pageSize})),
  )
  return data
}
