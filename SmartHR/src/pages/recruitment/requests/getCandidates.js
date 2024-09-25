import {useQuery} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export default function useGetCandidates(pageIndex, pageSize, filters) {
  const data = useQuery(['get-candidates'], () =>
    rootApi.get(path.candidate.getCandidates(pageIndex, pageSize, filters)),
  )
  return data
}
