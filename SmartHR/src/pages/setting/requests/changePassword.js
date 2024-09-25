import {useMutation} from 'react-query'
import { rootApi } from '../../../api/rootApi'
import path from '../../../api/path'

const useChangePassword = ({userId}) => {
  return useMutation('change-password', (data) => {
    return rootApi.put(path.auth.changePassword({userId}), data)
  })
}

export default useChangePassword
