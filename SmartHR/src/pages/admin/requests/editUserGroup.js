import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useMutation} from 'react-query'

export const useEditUserGroup = ({userGroupId}) => {
  return useMutation('edit-usergroup', (formData) => {
    return rootApi.put(path.userGroup.editUserGroup({userGroupId}), formData)
  })
}
