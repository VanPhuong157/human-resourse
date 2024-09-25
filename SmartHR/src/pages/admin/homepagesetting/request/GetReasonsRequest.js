import {useQuery} from 'react-query'
import path from '../../../../api/path'
import { rootApi } from '../../../../api/rootApi'

export const useGetReasons = () => {
  const data = useQuery(
    ['get-reason'],
    () => rootApi.get(path.reason.getReasons()),
    {
      keepPreviousData: true,
    },
  )

  return data
}
