import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useQuery} from 'react-query'

export default function useGetEmployeeFamily({userId}) {
  const queryFn = () => rootApi.get(path.employee.getEmployeeFamily(userId))
  const enabled = !!userId
  const data = useQuery(['get-employee-family', userId], queryFn, {enabled})
  return data
}
