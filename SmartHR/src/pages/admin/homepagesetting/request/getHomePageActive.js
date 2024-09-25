import path from '../../../../api/path'
import { rootApi } from '../../../../api/rootApi'
import {useQuery} from 'react-query'

export const useGetHomePageActive = () => {
  const data = useQuery(
    ['get-homepage-active'],
    () => rootApi.get(path.homepage.getHomePageActive()),
    {
      keepPreviousData: true,
    },
  )

  return data
}
