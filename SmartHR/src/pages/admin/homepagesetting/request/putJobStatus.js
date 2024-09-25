import path from '../../../../api/path'
import {rootApi} from '../../../../api/rootApi'
import {useMutation} from 'react-query'

export const usePutJobStatus = ({id}) => {
  return useMutation('edit-job-status', (data) => {
    return rootApi.put(path.homepage.editHomePage({id}), data)
  })
}
