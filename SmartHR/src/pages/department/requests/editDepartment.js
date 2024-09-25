import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useMutation} from 'react-query'

export const useEditDepartment = ({departmentId}) => {
  return useMutation('edit-department', (formData) => {
    return rootApi.put(path.department.editDepartment({departmentId}), formData)
  })
}
