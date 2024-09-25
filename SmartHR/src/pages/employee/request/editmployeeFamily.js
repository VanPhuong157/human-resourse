import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useMutation} from 'react-query'

export const useEditEmployeeFamily = ({userId, memberId}) => {
  return useMutation('edit-employee-family', (data) => {
    return rootApi.put(path.employee.editEmployeeFamily(userId, memberId), data)
  })
}
