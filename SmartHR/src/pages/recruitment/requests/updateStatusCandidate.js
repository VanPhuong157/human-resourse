import {useMutation} from 'react-query'
import { rootApi } from '../../../api/rootApi'
import path from '../../../api/path'

const useUpdateStatusCandidate = ({candidateId}) => {
  return useMutation('update-status-candidate', (data) => {
    return rootApi.put(path.candidate.updateStatus({candidateId}), data)
  })
}

export default useUpdateStatusCandidate
