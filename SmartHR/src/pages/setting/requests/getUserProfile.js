import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useQuery} from 'react-query'

export default function useGetUserInformation({userId}) {
  const queryFn = () => rootApi.get(path.user.getUserInformation({userId}))
  const enabled = !!userId
  const data = useQuery(['get-user-info', userId], queryFn, {enabled})
  return data
}
