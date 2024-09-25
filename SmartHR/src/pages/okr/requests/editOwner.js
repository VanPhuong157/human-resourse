import {useMutation} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export const useEditOwnerOKR = ({okrId}) => {
  return useMutation('edit-owner-okr', (data) => {
    return rootApi.put(path.okr.editOwnerOkr({okrId}), data)
  })
}
