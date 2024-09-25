import {useQuery} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export default function useGetStatisticCandidateOfPost({departmentId}) {
  const data = useQuery(['get-statistic-candidate-of-post'], () =>
    rootApi.get(path.dashboard.getStatisticCandidateOfPost({departmentId})),
  )
  return data
}
