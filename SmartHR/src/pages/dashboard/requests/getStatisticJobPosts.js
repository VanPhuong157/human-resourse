import {useQuery} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export default function useGetStatisticJobPosts({departmentId}) {
  const data = useQuery(['get-statistic-job-posts'], () =>
    rootApi.get(path.dashboard.getStatisticJobPosts({departmentId})),
  )
  return data
}
