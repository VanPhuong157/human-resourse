import path from '../../../api/path'
import {rootApi} from '../../../api/rootApi'
import {useQuery} from 'react-query'

export default function useGetDepartments() {
  const data = useQuery(['get-departments'], () =>
    rootApi.get(path.department.getDepartments()),
  )
  return data
}
