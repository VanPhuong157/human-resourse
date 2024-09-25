import path from '../../../../api/path'
import { rootApi } from '../../../../api/rootApi'
import {useMutation} from 'react-query'

export const useAddReason = () => {
  return useMutation('add-reason', (formData) => {
    return rootApi.post(path.reason.addReasons, formData)
  })
}
