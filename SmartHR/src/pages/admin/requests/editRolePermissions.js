import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useMutation} from 'react-query'

export const useEditRolePermissions = ({roleId}) => {
  return useMutation('edit-role-permissions', (data) => {
    return rootApi.put(path.admin.changRolePermissions({roleId}), data)
  })
}
