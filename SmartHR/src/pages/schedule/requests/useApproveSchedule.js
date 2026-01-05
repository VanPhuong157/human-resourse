// Tạo file: useApproveSchedule.js
import { useMutation, useQueryClient } from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import { showToastSuccess, showError } from '../../../components/notification'

export default function useApproveSchedule() {
  const queryClient = useQueryClient()

  return useMutation(
    async ({ id, status }) => {
      const url = path.schedule.approve(id)
      const res = await rootApi.put(url, { Status: status })
      return res.data
    },
    {
      onSuccess: () => {
        showToastSuccess('Lịch đã được cập nhật')
        queryClient.invalidateQueries(['schedules'])
      },
      onError: (error) => {
        showError('Cập nhật lịch thất bại: ' + error.message)
      }
    }
  )
}