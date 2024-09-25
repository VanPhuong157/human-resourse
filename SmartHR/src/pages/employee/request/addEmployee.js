import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useMutation} from 'react-query'

export const useAddEmployee = () => {
  return useMutation('add-employee', (formData) => {
    return rootApi.post(path.employee.addEmployee, formData)
  })
}
