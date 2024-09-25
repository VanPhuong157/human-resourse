import {useMutation} from 'react-query'
import path from '../../../api/path'
import {rootApi} from '../../../api/rootApi'

export const useEditOkrRequest = ({okrId}) => {
  return useMutation('edit-okr-request', (data) => {
    return rootApi.put(path.okr.editOkrRequest({okrId}), data)
  })
}
