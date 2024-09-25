import {useMutation} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export const useDeleteOkrHistoryComment = ({okrHistoryId}) => {
  return useMutation('delete-okr-history-comment', () => {
    return rootApi.delete(path.okr.deleteOkrHistoryComment({okrHistoryId}))
  })
}
