import {useQuery} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export default function useGetStatisticOkr({departmentId}) {
  const data = useQuery(['get-statistic-okr'], () =>
    rootApi.get(path.dashboard.getStatisticOkr({departmentId})),
  )
  return data
}
