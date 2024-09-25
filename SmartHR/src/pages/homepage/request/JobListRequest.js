import {useQuery} from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export const useGetPosts = (pageIndex, pageSize) => {
  const data = useQuery(
    ['get-postLists', pageIndex, pageSize],
    () => rootApi.get(path.homepage.getPosts(pageIndex, pageSize)),
    {
      keepPreviousData: true,
    },
  )

  return data
}
