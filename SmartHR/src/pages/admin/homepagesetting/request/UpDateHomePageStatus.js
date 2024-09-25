import path from '../../../../api/path'
import {rootApi} from '../../../../api/rootApi'
import {useMutation} from 'react-query'

const useUpdateHomePageStatus = ({id}) => {
  return useMutation('update-status-homepage', () => {
    return rootApi.put(path.homepage.updateHomePageStatus({id}))
  })
}

export default useUpdateHomePageStatus
