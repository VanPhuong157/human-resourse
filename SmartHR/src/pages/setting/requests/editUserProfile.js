import {useMutation} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export const useEditUserInfomation = ({userId}) => {
  return useMutation('edit-user-info', (data) => {
    return rootApi.put(path.user.editUserInformation({userId}), data)
  })
}
