// Tạo file: useCreateSchedule.js
import { useMutation, useQueryClient } from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'
import { showToastSuccess, showError } from '../../../components/notification'

export default function useCreateSchedule() {
  const queryClient = useQueryClient()

  return useMutation(
    async (createScheduleDTO) => {
      const formData = new FormData()
      formData.append('Title', createScheduleDTO.Title)
      if (createScheduleDTO.Description) formData.append('Description', createScheduleDTO.Description)
      formData.append('StartDate', createScheduleDTO.StartDate.toISOString())
      formData.append('EndDate', createScheduleDTO.EndDate.toISOString())
      formData.append('Priority', createScheduleDTO.Priority)
      createScheduleDTO.ParticipantIds.forEach(id => formData.append('ParticipantIds', id))
      if (createScheduleDTO.Files) {
        createScheduleDTO.Files.forEach(file => formData.append('Files', file))
      }

      const url = path.schedule.create()
      const res = await rootApi.post(url, formData, {
        headers: { 'Content-Type': 'multipart/form-data' }
      })
      return res.data
    },
    {
      onSuccess: () => {
        showToastSuccess('Lịch đã được tạo')
        queryClient.invalidateQueries(['schedules'])
      },
      onError: (error) => {
        showError('Tạo lịch thất bại: ' + error.message)
      }
    }
  )
}