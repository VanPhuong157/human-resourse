import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useQuery} from 'react-query'

export default function useGetEmployeeHistory({userId}) {
  console.log('gọi request')
  const queryFn = () => rootApi.get(path.employee.getEmployeeHistories(userId))
  const enabled = !!userId
  const data = useQuery(['get-employee-history', userId], queryFn, {enabled})
  console.log('data sau khi request: ', data)
  return data
}
