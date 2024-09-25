import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useQuery} from 'react-query'

export default function useGetOkrDetailActivity({okrId}) {
  const queryFn = () => rootApi.get(path.okr.getOkrActivity({okrId}))
  const enabled = !!okrId
  const data = useQuery(['get-okr-detail-activity', okrId], queryFn, {enabled})
  return data
}
