// import {rootApi} from '../../../api/rootApi'
// import path from '../../../api/path'
// import {showError, showToastSuccess} from '../../../components/notification'

// const toPartialPayload = (field, newValue) => {
//   switch (field) {
//     case 'title': return {title: newValue}
//     case 'type': return {type: newValue}
//     case 'scope': return {scope: newValue}
//     case 'targetNumber': return {targetNumber: Number(newValue)||0}
//     case 'unitOfTarget': return {unitOfTarget: newValue}
//     case 'cycle': return {cycle: newValue}
//     case 'confidenceLevel': return {confidenceLevel: newValue}
//     case 'result': return {result: newValue}
//     case 'parentId': return {parentId: newValue || null}
//     case 'dueDate': {
//       const d = new Date(newValue); return isNaN(d)?{}:{dueDate: d.toISOString()}
//     }
//     case 'priority': return {priority: newValue}
//     case 'company': return {company: newValue}
//     case 'note': return {note: newValue}
//     case 'status': return {status: newValue}
//     default: return {}
//   }
// }

// /** Trả về handler cho AgGrid onCellValueChanged */
// export const useOkrInlineEdit = ({refetch}) => {
//   return async (p) => {
//     const {field, data, newValue, oldValue} = p
//     if (JSON.stringify(newValue) === JSON.stringify(oldValue)) return
//     const okrId = data.id || data.Id
//     if (!okrId) return

//     try {
//       // Mỗi cột → route API tương ứng
//       if (field === 'assignees') {
//         // nếu BE có endpoint set-owners => gọi 1 lần:
//         await rootApi.put(path.okr.editOwnerOkr({okrId}), {ownerIds: newValue})
//         // nếu BE chỉ có add/remove thì diff new/old rồi gọi nhiều lần (tùy bạn)
//       } else if (field === 'managerTags') {
//         await rootApi.put(path.okr.editOwnerOkr({okrId}), {managerIds: newValue})
//       } else if (field === 'departmentTags') {
//         await rootApi.put(path.okr.updateOkr({okrId}), {departmentIds: newValue})
//       } else if (field === 'progress' || field === 'achieved') {
//         // tiến độ dùng màn riêng → không update inline
//         p.node.setDataValue(field, oldValue)
//         return
//       } else {
//         const payload = toPartialPayload(field, newValue)
//         if (Object.keys(payload).length) {
//           await rootApi.put(path.okr.updateOkr({okrId}), payload)
//         } else {
//           return
//         }
//       }

//       showToastSuccess('Cập nhật thành công!')
//       await refetch()
//     } catch (e) {
//       console.error(e)
//       showError('Cập nhật thất bại.')
//       p.node.setDataValue(field, oldValue) // revert UI
//     }
//   }
// }
