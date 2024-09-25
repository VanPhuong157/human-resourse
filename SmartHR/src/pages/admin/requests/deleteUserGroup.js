import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useMutation} from 'react-query'

export const useDeleteUserGroup = ({userGroupId}) => {
  return useMutation('delete-User-Group', () => {
    return rootApi.delete(path.userGroup.deleteUserGroup(userGroupId))
  })
}
