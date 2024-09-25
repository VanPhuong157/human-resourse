import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useMutation} from 'react-query'

export const useDeleteRole = ({roleId}) => {
  return useMutation('delete-role', () => {
    return rootApi.delete(path.role.deleteRole(roleId))
  })
}
