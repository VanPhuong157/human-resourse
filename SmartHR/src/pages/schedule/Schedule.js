import React, { useState } from 'react'
import {
  Box,
  Button,
  Chip,
  CircularProgress,
  IconButton,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TextField,
  Typography,
  Select,
  MenuItem,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  InputLabel,
  FormControl,
  List,
  ListItem,
  ListItemText,
  ListItemSecondaryAction,
} from '@mui/material'
import AttachFileIcon from '@mui/icons-material/AttachFile'
import DownloadIcon from '@mui/icons-material/Download'
import DeleteIcon from '@mui/icons-material/Delete'
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker'
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider'
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns'

import MultiSelectEditor from '../okr/editor/MultiSelectEditor'
import useGetSchedules from './requests/useGetSchedules'
import useCreateSchedule from './requests/useCreateSchedule'
import useApproveSchedule from './requests/useApproveSchedule'
import useGetAllUsersSimple from './requests/useGetAllUsersSimple'

import { showError, showToastSuccess } from '../../components/notification'
import { baseUrl } from '../../api/rootApi'

const priorityColors = {
  0: { color: 'success', label: 'Thấp' },
  1: { color: 'warning', label: 'Trung bình' },
  2: { color: 'error', label: 'Cao' },
}

const statusColors = {
  0: { color: 'default', label: 'Chờ duyệt' },
  1: { color: 'success', label: 'Đã duyệt' },
  2: { color: 'error', label: 'Từ chối' },
}

const formatDate = (dateStr) => {
  if (!dateStr) return 'N/A'
  const [day, month, year] = dateStr.split('/')
  const date = new Date(year, month - 1, day)
  return isNaN(date.getTime()) ? dateStr : date.toLocaleString('vi-VN')
}

// Hàm download file với token
const handleFileDownload = async (storedName, originalName) => {
  try {
    const token = localStorage.getItem('accessToken')
    if (!token) return showError('Bạn cần đăng nhập để tải file')

    const res = await fetch(`${baseUrl}/api/Schedules/file/${storedName}`, {
      headers: { Authorization: `Bearer ${token}` },
    })

    if (!res.ok) throw new Error('Tải file thất bại')

    const blob = await res.blob()
    const url = window.URL.createObjectURL(blob)
    const a = document.createElement('a')
    a.href = url
    a.download = originalName || storedName
    a.click()
    window.URL.revokeObjectURL(url)
    showToastSuccess('Tải file thành công!')
  } catch {
    showError('Lỗi tải file')
  }
}

const ScheduleDashboard = () => {
  const { data: schedulesData, isLoading: isLoadingSchedules } = useGetSchedules(1, 100)
  const { data: users = [], isLoading: isLoadingUsers } = useGetAllUsersSimple()

  const { mutate: createSchedule, isLoading: isCreating } = useCreateSchedule()
  const { mutate: approveSchedule } = useApproveSchedule()

  const [openCreateDialog, setOpenCreateDialog] = useState(false)
  const [titleTouched, setTitleTouched] = useState(false)
  const [newSchedule, setNewSchedule] = useState({
    Title: '',
    Description: '',
    StartDate: new Date(),
    EndDate: new Date(),
    Priority: 1,
    Files: [],
    ParticipantIds: [],
  })
  const [participantNames, setParticipantNames] = useState([])

  const schedules = schedulesData?.items || []

  const currentUserRole = localStorage.getItem('userRole') || 'Employee'
  const isBOD = currentUserRole === 'BOD'

  const handleCreate = () => {
    if (!newSchedule.Title.trim()) return showError('Vui lòng nhập tiêu đề')
    if (newSchedule.StartDate >= newSchedule.EndDate) return showError('Thời gian bắt đầu phải trước thời gian kết thúc')

    createSchedule(newSchedule, {
      onSuccess: () => {
        showToastSuccess('Tạo lịch thành công!')
        setOpenCreateDialog(false)
        setNewSchedule({ Title: '', Description: '', StartDate: new Date(), EndDate: new Date(), Priority: 1, Files: [], ParticipantIds: [] })
        setParticipantNames([])
        setTitleTouched(false)
      },
    })
  }

  const handleApprove = (id, status) => {
    approveSchedule({ id, status })
  }

  const handleFileChange = (e) => {
    setNewSchedule({ ...newSchedule, Files: [...newSchedule.Files, ...Array.from(e.target.files)] })
  }

  const handleRemoveFile = (i) => {
    setNewSchedule({ ...newSchedule, Files: newSchedule.Files.filter((_, index) => index !== i) })
  }

  const fetchUserOptions = () => users.map(u => ({ value: u.Id, label: u.FullName }))

  const handleSaveParticipants = (ids) => {
    setNewSchedule({ ...newSchedule, ParticipantIds: ids })
    setParticipantNames(users.filter(u => ids.includes(u.Id)).map(u => u.FullName))
  }

  return (
    <LocalizationProvider dateAdapter={AdapterDateFns}>
      <Box sx={{ p: 3 }}>
        <Typography variant="h4" gutterBottom>Quản Lý Lịch Làm Việc</Typography>

        <Button variant="contained" onClick={() => setOpenCreateDialog(true)} sx={{ mb: 3 }}>
          Tạo Lịch Mới
        </Button>

        {isLoadingSchedules || isLoadingUsers ? (
          <Box sx={{ display: 'flex', justifyContent: 'center', my: 8 }}>
            <CircularProgress />
            <Typography sx={{ ml: 2 }}>Đang tải dữ liệu...</Typography>
          </Box>
        ) : schedules.length === 0 ? (
          <Paper sx={{ p: 6, textAlign: 'center' }}>
            <Typography variant="h6" color="text.secondary">Chưa có lịch làm việc nào</Typography>
          </Paper>
        ) : (
          <TableContainer component={Paper} elevation={3}>
            <Table>
              <TableHead>
                <TableRow>
                  <TableCell><strong>Tiêu đề</strong></TableCell>
                  <TableCell><strong>Bắt đầu</strong></TableCell>
                  <TableCell><strong>Kết thúc</strong></TableCell>
                  <TableCell><strong>Ưu tiên</strong></TableCell>
                  <TableCell><strong>Người tham gia</strong></TableCell>
                  <TableCell><strong>File đính kèm</strong></TableCell>
                  <TableCell><strong>Trạng thái</strong></TableCell>
                  <TableCell><strong>Hành động</strong></TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {schedules.map((s) => (
                  <TableRow key={s.id} hover>
                    <TableCell>{s.title}</TableCell>
                    <TableCell>{formatDate(s.startDate)}</TableCell>
                    <TableCell>{formatDate(s.endDate)}</TableCell>
                    <TableCell>
                      <Chip label={priorityColors[s.priority]?.label || 'N/A'} color={priorityColors[s.priority]?.color || 'default'} size="small" />
                    </TableCell>
                    <TableCell>
                      {s.participantNames?.length > 0 ? s.participantNames.map((n, i) => <Chip key={i} label={n} size="small" sx={{ m: 0.25 }} />) : 'Không có'}
                    </TableCell>
                    <TableCell>
                      {s.attachmentPaths?.length > 0 ? (
                        s.attachmentPaths.map((path, i) => {
                          const storedName = path.split('/').pop()
                          // Nếu backend trả attachmentFileNames thì dùng tên gốc, không thì dùng storedName
                          const originalName = s.attachmentFileNames?.[i] || storedName

                          return (
                            <Box key={i} sx={{ display: 'flex', alignItems: 'center', mb: 0.5 }}>
                              <AttachFileIcon fontSize="small" sx={{ mr: 1, color: 'text.secondary' }} />
                              <Typography
                                variant="body2"
                                sx={{
                                  mr: 1,
                                  maxWidth: 180,
                                  overflow: 'hidden',
                                  textOverflow: 'ellipsis',
                                  whiteSpace: 'nowrap',
                                }}
                              >
                                {originalName}
                              </Typography>
                              <IconButton size="small" onClick={() => handleFileDownload(storedName, originalName)}>
                                <DownloadIcon fontSize="small" />
                              </IconButton>
                            </Box>
                          )
                        })
                      ) : 'Không có'}
                    </TableCell>
                    <TableCell>
                      <Chip label={statusColors[s.status]?.label || 'N/A'} color={statusColors[s.status]?.color || 'default'} size="small" />
                    </TableCell>
                    <TableCell>
                      {isBOD && s.status === 0 && (
                        <>
                          <Button size="small" variant="contained" color="success" onClick={() => handleApprove(s.id, 1)} sx={{ mr: 1 }}>Duyệt</Button>
                          <Button size="small" variant="contained" color="error" onClick={() => handleApprove(s.id, 2)}>Từ chối</Button>
                        </>
                      )}
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        )}

        {/* Dialog Tạo Lịch - Không đỏ ngay */}
        <Dialog open={openCreateDialog} onClose={() => { setOpenCreateDialog(false); setTitleTouched(false); }} maxWidth="md" fullWidth>
          <DialogTitle>Tạo Lịch Làm Việc Mới</DialogTitle>
          <DialogContent dividers>
            <TextField
              label="Tiêu đề *"
              value={newSchedule.Title}
              onChange={(e) => setNewSchedule({ ...newSchedule, Title: e.target.value })}
              onBlur={() => setTitleTouched(true)}
              fullWidth
              required
              error={titleTouched && !newSchedule.Title.trim()}
              helperText={titleTouched && !newSchedule.Title.trim() ? 'Tiêu đề là bắt buộc' : ''}
              margin="normal"
              autoFocus
            />
            <TextField
              label="Mô tả"
              value={newSchedule.Description}
              onChange={(e) => setNewSchedule({ ...newSchedule, Description: e.target.value })}
              fullWidth
              multiline
              rows={3}
              margin="normal"
            />
            <DateTimePicker
              label="Thời gian bắt đầu"
              value={newSchedule.StartDate}
              onChange={(v) => setNewSchedule({ ...newSchedule, StartDate: v })}
              slotProps={{ textField: { fullWidth: true, margin: 'normal' } }}
            />
            <DateTimePicker
              label="Thời gian kết thúc"
              value={newSchedule.EndDate}
              onChange={(v) => setNewSchedule({ ...newSchedule, EndDate: v })}
              slotProps={{ textField: { fullWidth: true, margin: 'normal' } }}
            />
            <FormControl fullWidth margin="normal">
              <InputLabel>Ưu tiên</InputLabel>
              <Select value={newSchedule.Priority} label="Ưu tiên" onChange={(e) => setNewSchedule({ ...newSchedule, Priority: Number(e.target.value) })}>
                <MenuItem value={0}><Chip label="Thấp" color="success" size="small" sx={{ mr: 1 }} /> Thấp</MenuItem>
                <MenuItem value={1}><Chip label="Trung bình" color="warning" size="small" sx={{ mr: 1 }} /> Trung bình</MenuItem>
                <MenuItem value={2}><Chip label="Cao" color="error" size="small" sx={{ mr: 1 }} /> Cao</MenuItem>
              </Select>
            </FormControl>

            <Box sx={{ my: 3 }}>
              <Typography variant="subtitle1" gutterBottom>Người tham gia</Typography>
              <MultiSelectEditor
                rowData={{ ParticipantIds: newSchedule.ParticipantIds, ParticipantNames: participantNames }}
                idsKey="ParticipantIds"
                namesKey="ParticipantNames"
                placeholder="Chọn người tham gia"
                fetchOptions={fetchUserOptions}
                onSaveIds={handleSaveParticipants}
              />
            </Box>

            <Box sx={{ my: 3 }}>
              <Typography variant="subtitle1" gutterBottom>Đính kèm file</Typography>
              <Button variant="outlined" component="label">
                Chọn file
                <input type="file" multiple hidden onChange={handleFileChange} />
              </Button>
              {newSchedule.Files.length > 0 && (
                <List sx={{ mt: 2 }}>
                  {newSchedule.Files.map((file, i) => (
                    <ListItem key={i}>
                      <ListItemText primary={file.name} secondary={`${(file.size / 1024).toFixed(1)} KB`} />
                      <ListItemSecondaryAction>
                        <IconButton onClick={() => handleRemoveFile(i)}><DeleteIcon /></IconButton>
                      </ListItemSecondaryAction>
                    </ListItem>
                  ))}
                </List>
              )}
            </Box>
          </DialogContent>
          <DialogActions>
            <Button onClick={() => { setOpenCreateDialog(false); setTitleTouched(false); }}>Hủy</Button>
            <Button onClick={handleCreate} variant="contained" disabled={isCreating || !newSchedule.Title.trim()}>
              {isCreating ? <CircularProgress size={24} /> : 'Tạo lịch'}
            </Button>
          </DialogActions>
        </Dialog>
      </Box>
    </LocalizationProvider>
  )
}

export default ScheduleDashboard