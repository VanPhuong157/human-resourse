import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useMutation} from 'react-query'

export const useDeleteEmployeeFamily = ({memberId}) => {
  return useMutation('delete-employee-family', () => {
    return rootApi.delete(path.employee.deleteEmployeeFamily(memberId))
  })
}
