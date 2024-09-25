import path from '../../../../api/path'
import { rootApi } from '../../../../api/rootApi'
import {useMutation} from 'react-query'

export const useDeleteReason = ({reasonId}) => {
  return useMutation('delete-reason', () => {
    return rootApi.delete(path.reason.deleteReason({reasonId}))
  })
}
