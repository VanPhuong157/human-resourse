import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useMutation} from 'react-query'

export const useAddUserGroup = () => {
  return useMutation('add-userGroup', (formData) => {
    return rootApi.post(path.userGroup.addUserGroup, formData)
  })
}
