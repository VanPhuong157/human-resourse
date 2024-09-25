import {useQuery} from 'react-query'
import {rootApi} from '../../../../api/rootApi'
import path from '../../../../api/path'

export const useGetHomePageImage = ({id}) => {
  return useQuery('get-homepage-image', (data) => {
    return rootApi.get(path.homepage.getImageHomePage({id}), data)
  })
}
