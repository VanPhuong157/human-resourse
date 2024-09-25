import {useMutation} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export const useEditStatusOKR = ({okrId}) => {
  return useMutation('edit-status-okr', (data) => {
    return rootApi.put(path.okr.editOkrs({okrId}), data)
  })
}
