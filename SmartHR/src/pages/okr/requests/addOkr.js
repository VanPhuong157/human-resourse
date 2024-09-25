import {useMutation} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export const useAddOkr = () => {
  return useMutation('add-okr', () => {
    return rootApi.post(path.okr.CreateOkr)
  })
}
