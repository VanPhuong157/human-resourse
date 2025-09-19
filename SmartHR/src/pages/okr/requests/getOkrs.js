import path from '../../../api/path'
import {rootApi} from '../../../api/rootApi'
import {useQuery} from 'react-query'

export default function useGetOkrs(pageIndex, pageSize, departmentId, filters) {
  const key = [
    'get-okrs',
    pageIndex,
    pageSize,
    departmentId,
    JSON.stringify(filters || {}),
  ]
  const data = useQuery(key, () =>
    rootApi.get(path.okr.getOkrs(pageIndex, pageSize, departmentId, filters)),
  )
  return data
}
