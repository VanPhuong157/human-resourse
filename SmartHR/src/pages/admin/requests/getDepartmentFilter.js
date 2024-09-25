import path from '../../../api/path'
import {rootApi} from '../../../api/rootApi'
import {useQuery} from 'react-query'

export default function useGetDepartmentsFilter(pageIndex, pageSize, filters) {
  const data = useQuery(['get-departments-filter', pageIndex, pageSize], () =>
    rootApi.get(
      path.department.getDepartmentFilter(pageIndex, pageSize, filters),
    ),
  )
  return data
}
