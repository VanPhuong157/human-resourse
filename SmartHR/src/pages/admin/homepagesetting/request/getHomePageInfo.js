import {useQuery} from 'react-query'
import path from '../../../../api/path'
import { rootApi } from '../../../../api/rootApi'

export const useGetHomePageInfo = (pageIndex, pageSize) => {
  const data = useQuery(
    ['get-homepage-info', pageIndex, pageSize],
    () => rootApi.get(path.homepage.getHomePageInfo(pageIndex, pageSize)),
    {
      keepPreviousData: true,
    },
  )

  return data
}
