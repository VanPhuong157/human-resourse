import {useMutation} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export const useAddOkrHistoryComment = ({okrId}) => {
  return useMutation('add-okr-history-comment', (comment) => {
    return rootApi.post(path.okr.addOkrHistoryComment({okrId}), comment)
  })
}
