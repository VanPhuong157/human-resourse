// pages/okr/requests/addOkrHistoryComment.js
import { useMutation } from 'react-query'
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

export const useAddOkrHistoryComment = ({ okrId }) => {
  return useMutation(({ text, files }) => {
    const fd = new FormData()
    fd.append('text', text || '')
    ;(files || []).forEach(f => fd.append('files', f))

    return rootApi.post(
      path.okr.addOkrHistoryComment({ okrId }),
      fd,
      {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      }
    )
  })
}
