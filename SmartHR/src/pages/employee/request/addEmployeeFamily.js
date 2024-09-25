import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import {useMutation} from 'react-query'

export const useAddEmployeeFamily = ({userId}) => {
  return useMutation('add-employee-family', (data) => {
    return rootApi.post(path.employee.addEmployeeFamily(userId), data)
  })
}
