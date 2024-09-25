import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useQuery} from 'react-query'

export default function useGetOkrDetail({okrId}) {
  const queryFn = () => rootApi.get(path.okr.getOkrDetail({okrId}))
  const enabled = !!okrId
  const data = useQuery(['get-okr-detail', okrId], queryFn, {enabled})
  return data
}
