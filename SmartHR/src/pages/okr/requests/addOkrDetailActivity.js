import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useMutation} from 'react-query'

export default function useAddOkrDetailActivity({okrId}) {
  return useMutation('add-okr-detail-activity', (comment) => {
    return rootApi.post(path.okr.addOkrActivity({okrId}), comment)
  })
}
