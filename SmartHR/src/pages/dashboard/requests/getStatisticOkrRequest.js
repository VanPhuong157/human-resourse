import {useQuery} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export default function useGetStatisticOkrRequest({departmentId}) {
  const data = useQuery(['get-statistic-okr-request'], () =>
    rootApi.get(path.dashboard.getStatisticOkrRequest({departmentId})),
  )
  return data
}
