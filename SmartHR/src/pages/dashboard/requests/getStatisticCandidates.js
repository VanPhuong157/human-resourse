import {useQuery} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export default function useGetStatisticCandidates({departmentId}) {
  const data = useQuery(['get-statistic-candidates'], () =>
    rootApi.get(path.dashboard.getStatisticCandidates({departmentId})),
  )
  return data
}
