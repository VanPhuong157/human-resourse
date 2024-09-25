import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useMutation} from 'react-query'

export const useAddRole = () =>{
    return useMutation('add-role', (formData) => {
      return rootApi.post(path.role.addRole,formData)
    })
  }