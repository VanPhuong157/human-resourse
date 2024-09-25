import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useQuery} from 'react-query'

export default function useGetEmployeeInformation({userId}) {
  const queryFn = () => rootApi.get(path.employee.getEmployeeInformation(userId))
  const enabled = !!userId
  const data = useQuery(['get-employee-history', userId], queryFn, {enabled})
  return data
}
