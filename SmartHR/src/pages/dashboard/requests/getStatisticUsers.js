import {useQuery} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export default function useGetStatisticUsers({departmentId}) {
  const data = useQuery(['get-statistic-users'], () =>
    rootApi.get(path.dashboard.getStatisticUsers({departmentId})),
  )
  return data
}
