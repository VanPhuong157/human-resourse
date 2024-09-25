import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useMutation} from 'react-query'

export const useEditUserPermission = ({userId}) => {
  return useMutation('edit-employee-family', (data) => {
    return rootApi.put(path.admin.changeUserPermissions({userId}), data)
  })
}
