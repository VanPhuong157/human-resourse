import {useQuery, useMutation} from 'react-query'
import path from '../../../api/path'
import {rootApi} from '../../../api/rootApi'

export const useGetEmployees = (pageIndex, pageSize, filters) => {
  const data = useQuery(['get-employees', pageIndex, pageSize], () =>
    rootApi.get(path.employee.getEmployees(pageIndex, pageSize, filters)),
  )
  return data
}
export const useEditEmployee = () => {
  return useMutation('edit-employee', (formData) => {
    return rootApi.put(path.employee.editEmployee, formData, {})
  })
}
export const useGetRoles = (pageIndex, pageSize) => {
  const data = useQuery(
    ['get-roles'],
    () => rootApi.get(path.role.getRoles(pageIndex, pageSize)),
    {
      keepPreviousData: true,
    },
  )

  return data
}
export const useAddNewUser = () => {
  return useMutation('add-new-user', (formData) => {
    return rootApi.post(path.user.addNewUser, formData)
  })
}

export const useUpdateStatusEmployee = ({userId}) => {
  return useMutation('update-status-employee', (data) => {
    return rootApi.put(path.employee.updateStatusEmployee({userId}), data)
  })
}

export const useUpdatePositionEmployee = () => {
  return useMutation('update-position-employee', (data) => {
    return rootApi.post(path.employee.updatePositionEmployee, data)
  })
}
