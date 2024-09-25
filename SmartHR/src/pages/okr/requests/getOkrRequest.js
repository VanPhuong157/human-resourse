import {useQuery} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export default function useGetOkrRequest(
  pageIndex,
  pageSize,
  filters,
  departmentId,
) {
  const data = useQuery(['get-okr-request'], () =>
    rootApi.get(
      path.okr.getOkrRequest(pageIndex, pageSize, filters, departmentId),
    ),
  )
  return data
}
