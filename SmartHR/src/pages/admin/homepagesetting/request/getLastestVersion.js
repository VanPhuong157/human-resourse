import {useQuery} from 'react-query'
import path from '../../../../api/path'
import {rootApi} from '../../../../api/rootApi'

export default function useGetLastestVersion() {
  const data = useQuery(['get-lastest'], () =>
    rootApi.get(path.homepage.getLastestVersion()),
  )
  return data
}
