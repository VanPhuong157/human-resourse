import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useMutation} from 'react-query'

export const useAddDepartment = () =>{
    return useMutation('add-department', (formData) => {
      return rootApi.post(path.department.addDepartment,formData)
    })
  }