import {useMutation} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export const useEditProgessOKR = ({okrId}) => {
  return useMutation('edit-progress-okr', (data) => {
    return rootApi.put(path.okr.editProgressOkr({okrId}), data)
  })
}
