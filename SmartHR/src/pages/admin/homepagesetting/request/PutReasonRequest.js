import path from '../../../../api/path'
import { rootApi } from '../../../../api/rootApi'
import {useMutation} from 'react-query'

export const useEditReason = ({reasonId}) => {
  return useMutation('edit-reason', (formData) => {
    return rootApi.put(path.reason.editReasons({reasonId}), formData)
  })
}
