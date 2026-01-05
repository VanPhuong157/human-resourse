import React, { useState, useEffect, useRef } from 'react'
import {
  Typography,
  Box,
  IconButton,
  Menu,
  MenuItem,
  Chip,
  Stack,
} from '@mui/material'
import { styled } from '@mui/system'
import useGetOkrDetailActivity from '../../../../pages/okr/requests/getOkrDetailActivity'
import { format } from 'date-fns'
import MoreVertIcon from '@mui/icons-material/MoreVert'
import AttachFileIcon from '@mui/icons-material/AttachFile'
import AlternateEmailIcon from '@mui/icons-material/AlternateEmail'
import SendRoundedIcon from '@mui/icons-material/SendRounded'
import InsertDriveFileIcon from '@mui/icons-material/InsertDriveFile'
import PictureAsPdfIcon from '@mui/icons-material/PictureAsPdf'
import DescriptionIcon from '@mui/icons-material/Description'
import TableChartIcon from '@mui/icons-material/TableChart'
import SlideshowIcon from '@mui/icons-material/Slideshow'
import ArchiveIcon from '@mui/icons-material/Archive'
import TextSnippetIcon from '@mui/icons-material/TextSnippet'
import DownloadForOfflineIcon from '@mui/icons-material/DownloadForOffline'

import { useAddOkrHistoryComment } from '../../../../pages/okr/requests/addOkrHistoryComment'
import { showError, showToastSuccess } from '../../../../components/notification'
import { useDeleteOkrHistoryComment } from '../../../../pages/okr/requests/deleteOkrHistoryComment'
import {
  downloadCommentFile,
  previewUrlForCommentFile,
} from '../../../../pages/okr/requests/downloadFile'

// avatar + hàng tiêu đề mỗi activity
const Row = styled(Box)({
  display: 'flex',
  alignItems: 'center',
  padding: '4px 0',
})

const MAX_FILES = 10
const MAX_SIZE = 1000 * 1024 * 1024 // 1000MB
const THUMB_W = 240
const THUMB_H = 120

// ===== helpers cho file =====
const formatBytes = (bytes) => {
  if (bytes === undefined || bytes === null) return ''
  const units = ['B', 'KB', 'MB', 'GB']
  let i = 0
  let n = bytes
  while (n >= 1024 && i < units.length - 1) {
    n /= 1024
    i++
  }
  return `${Math.round(n)} ${units[i]}`
}
const getExt = (name = '') => (name.split('.').pop() || '').toLowerCase()
const fileIcon = (ext) => {
  switch (ext) {
    case 'pdf': return <PictureAsPdfIcon fontSize="small" />
    case 'doc':
    case 'docx': return <DescriptionIcon fontSize="small" />
    case 'xls':
    case 'xlsx': return <TableChartIcon fontSize="small" />
    case 'ppt':
    case 'pptx': return <SlideshowIcon fontSize="small" />
    case 'zip':
    case 'rar': return <ArchiveIcon fontSize="small" />
    case 'txt':
    case 'csv': return <TextSnippetIcon fontSize="small" />
    default: return <InsertDriveFileIcon fontSize="small" />
  }
}

const OkrActivity = ({ okrId, setRefetch }) => {
  const [comment, setComment] = useState('')
  const [anchorEl, setAnchorEl] = useState(null)
  const [selectedCommentId, setSelectedCommentId] = useState(null)

  // files đính kèm (chỉ thêm qua nút/drag/paste, KHÔNG auto add từ base64 trong editor)
  const [files, setFiles] = useState([])
  const fileInputRef = useRef(null)

  const { data: dataOkrDetailAcitvity, refetch } = useGetOkrDetailActivity({ okrId })
  const { mutateAsync: addOkrHistoryCommentAsync, isLoading: isSending } =
    useAddOkrHistoryComment({ okrId })
  const { mutateAsync: deleteOkrHistoryCommentAsync } =
    useDeleteOkrHistoryComment({ okrHistoryId: selectedCommentId })

  useEffect(() => {
    setRefetch(refetch)
  }, [refetch, setRefetch])

  const handleAttachClick = () => fileInputRef.current?.click()

  // gom file & khử trùng theo chữ ký name-size-lastModified
  const addPickedFiles = (incoming) => {
    if (!incoming?.length) return
    const sig = (f) => `${f.name}-${f.size}-${f.lastModified || 0}`
    const map = new Map()
    ;[...files, ...incoming].forEach((f) => f && map.set(sig(f), f))
    let next = Array.from(map.values())
    if (next.length > MAX_FILES) next = next.slice(0, MAX_FILES)
    const overs = next.filter((f) => f.size > MAX_SIZE)
    if (overs.length) {
      showError(
        `File vượt quá ${Math.round(MAX_SIZE / 1024 / 1024)}MB: ${overs.map((f) => f.name).join(', ')}`
      )
      next = next.filter((f) => f.size <= MAX_SIZE)
    }
    setFiles(next)
  }

  const handleFilesSelected = (e) => {
    addPickedFiles(Array.from(e.target.files || []))
    e.target.value = ''
  }

  const removeFileAt = (idx) => {
    const next = files.slice()
    next.splice(idx, 1)
    setFiles(next)
  }

  // --- xử lý dán ảnh từ clipboard ---
  const handlePaste = (e) => {
    const dt = e.clipboardData || window.clipboardData
    if (!dt) return

    let pasted = Array.from(dt.files || []).filter((f) => f.type?.startsWith('image/'))

    if (!pasted.length && dt.items) {
      for (const item of dt.items) {
        if (item.type && item.type.startsWith('image/')) {
          const blob = item.getAsFile()
          if (blob) {
            const name = `pasted-${Date.now()}.png`
            pasted.push(new File([blob], name, { type: blob.type || 'image/png' }))
          }
        }
      }
    }

    if (pasted.length) {
      e.preventDefault() // chặn chèn base64 vào editor
      addPickedFiles(pasted)
    }
    // nếu không có ảnh: cho dán text bình thường
  }

  const addComment = async () => {
    const text = (comment || '').trim()
    if (!text && files.length === 0) return
    try {
      const res = await addOkrHistoryCommentAsync({ text: comment, files })
      showToastSuccess({ message: res.data?.message || 'Added!' })

      await refetch()
      setComment('')
      setFiles([])
      const editableDiv = document.querySelector('[contentEditable]')
      if (editableDiv) editableDiv.textContent = ''
    } catch (e) {
      showError(e?.response?.data?.message || 'Có lỗi khi gửi bình luận')
    }
  }

  const formatTime = (s) => format(new Date(s), 'MMM d, yyyy, hh:mm a')

  const handleMenuClick = (e, id) => {
    setAnchorEl(e.currentTarget)
    setSelectedCommentId(id)
  }
  const handleMenuClose = () => {
    setAnchorEl(null)
    setSelectedCommentId(null)
  }
  const handleDelete = () => {
    deleteOkrHistoryCommentAsync()
      .then((res) => {
        showToastSuccess({ message: res.data?.message })
        refetch()
      })
      .catch((e) => showError(e.response?.data?.message))
      .finally(handleMenuClose)
  }

  const items = dataOkrDetailAcitvity?.data ?? []

  return (
    <Box mt={3} sx={{ width: '100%' }}>
      {/* Ô nhập bình luận */}
      <Box display="flex" alignItems="center" mb={1.5}>
        <Box
          sx={{
            width: 28, height: 28, mr: 1, borderRadius: '50%',
            bgcolor: '#9333ea', color: '#fff', display: 'flex',
            alignItems: 'center', justifyContent: 'center',
            fontSize: 14, fontWeight: 600,
          }}
        >
          P
        </Box>

        <Box
          role="textbox"
          contentEditable
          suppressContentEditableWarning
          onInput={(e) => setComment(e.currentTarget.textContent ?? '')}
          onKeyDown={(e) => {
            if (e.key === 'Enter' && !e.shiftKey) {
              e.preventDefault()
              addComment()
              e.currentTarget.textContent = ''
            }
          }}
          onPaste={handlePaste}
          onDrop={(e) => {
            if (e.dataTransfer?.files?.length) {
              e.preventDefault()
              addPickedFiles(Array.from(e.dataTransfer.files))
            }
          }}
          sx={{
            flex: 1,
            minHeight: 22,
            outline: 'none',
            fontSize: 14,
            color: 'rgba(55,53,47,1)',
            whiteSpace: 'pre-wrap',
            overflowWrap: 'anywhere',
            wordBreak: 'break-word',
            '&:empty:before': {
              content: '"Add a comment…"',
              color: 'rgba(55,53,47,0.6)',
            },
            '& img': {
              maxWidth: '120px',
              maxHeight: '90px',
              objectFit: 'cover',
              display: 'inline-block',
              borderRadius: '6px',
              border: '1px solid #e5e7eb',
              verticalAlign: 'middle',
              marginRight: 6,
            },
          }}
        />

        <Box sx={{ display: 'flex', alignItems: 'center', ml: 1 }}>
          <input
            ref={fileInputRef}
            type="file"
            multiple
            onChange={handleFilesSelected}
            style={{ display: 'none' }}
            accept="image/*,.pdf,.doc,.docx,.xls,.xlsx,.ppt,.pptx,.txt,.csv,.zip,.rar"
          />

          <IconButton size="small" aria-label="attach" onClick={handleAttachClick}>
            <AttachFileIcon fontSize="small" sx={{ color: 'rgba(0,0,0,0.6)' }} />
          </IconButton>

          <IconButton size="small" aria-label="mention" title="@mention (chưa làm)">
            <AlternateEmailIcon fontSize="small" sx={{ color: 'rgba(0,0,0,0.4)' }} />
          </IconButton>

          <IconButton size="small" aria-label="send" onClick={addComment} disabled={isSending}>
            <SendRoundedIcon
              fontSize="small"
              sx={{ color: isSending ? 'rgba(0,0,0,0.2)' : 'rgba(0,0,0,0.6)' }}
            />
          </IconButton>
        </Box>
      </Box>

      {/* Files user đang chọn để gửi */}
      {!!files.length && (
        <Stack direction="row" spacing={1} sx={{ mb: 2, flexWrap: 'wrap', gap: 1 }}>
          {files.map((f, i) => (
            <Chip
              key={i}
              size="small"
              variant="outlined"
              label={`${f.name} (${Math.ceil(f.size / 1024)} KB)`}
              onDelete={() => removeFileAt(i)}
            />
          ))}
        </Stack>
      )}

      {/* Lịch sử */}
      {items.map((h) => {
        const isComment = h.type === 'comment'
        const hasDesc = !!(h.description && h.description.trim())
        const attachments = h.attachments || []

        return (
          <Box key={h.id} sx={{ mb: 2 }}>
            <Row>
              <Typography variant="body2" sx={{ fontWeight: 700, mr: 1 }}>
                {h.userName}
              </Typography>
              <Typography variant="caption" sx={{ color: 'rgba(55,53,47,0.6)' }}>
                {formatTime(h.createdAt)}
              </Typography>

              {isComment && (
                <IconButton size="small" onClick={(e) => handleMenuClick(e, h.id)} sx={{ ml: 'auto' }}>
                  <MoreVertIcon fontSize="small" />
                </IconButton>
              )}
            </Row>

            <Box sx={{ pl: 4 }}>
              {isComment ? (
                <>
                  {hasDesc && (
                    <Typography
                      variant="body2"
                      color="textSecondary"
                      component="div"
                      sx={{ whiteSpace: 'pre-wrap', overflowWrap: 'anywhere', wordBreak: 'break-word' }}
                    >
                      {h.description}
                    </Typography>
                  )}
                </>
              ) : (
                <>
                  {h.keyResult && (
                    <Typography
                      variant="body2"
                      color="textSecondary"
                      component="div"
                      sx={{ whiteSpace: 'pre-wrap', overflowWrap: 'anywhere', wordBreak: 'break-word' }}
                    >
                      {h.keyResult}
                    </Typography>
                  )}

                  <Typography
                    variant="body2"
                    color="textSecondary"
                    component="div"
                    sx={{ whiteSpace: 'pre-wrap', overflowWrap: 'anywhere', wordBreak: 'break-word' }}
                  >
                    {`${h.oldAchieved} ${h.unitOfTarget} (${h.oldProgress}% ${h.oldStatus}) → ${h.newAchieved} ${h.unitOfTarget} (${h.newProgress}% ${h.newStatus})`}
                  </Typography>
                </>
              )}

              {!!attachments.length && (
                <Box sx={{ mt: 1.25, display: 'flex', flexWrap: 'wrap', gap: 1 }}>
                  {attachments.map((a) => {
                    const ext = getExt(a.fileName)
                    if (a.isImage) {
                      // Ảnh: thumbnail 120x120
                      return (
                        <Box
                          key={a.id}
                          role="button"
                          title={a.fileName}
                          onClick={() => window.open(previewUrlForCommentFile(a.id), '_blank')}
                          sx={{
                            width: THUMB_W,
                            height: THUMB_H,
                            borderRadius: 1,
                            overflow: 'hidden',
                            border: '1px solid #e5e7eb',
                            flex: '0 0 auto',
                            cursor: 'pointer',
                            '&:hover': { boxShadow: '0 0 0 2px rgba(0,0,0,0.06) inset' },
                            '& img': { width: '100% !important', height: '100% !important', objectFit: 'cover', display: 'block' },
                          }}
                        >
                          <img src={previewUrlForCommentFile(a.id)} alt={a.fileName} loading="lazy" />
                        </Box>
                      )
                    }
                    // File thường: pill ngang
                    return (
                      <Box
                        key={a.id}
                        sx={{
                          display: 'inline-flex',
                          alignItems: 'center',
                          gap: 1,
                          maxWidth: 380,
                          px: 1,
                          py: 0.5,
                          border: '1px solid #e5e7eb',
                          borderRadius: 999,
                          backgroundColor: '#fff',
                          cursor: 'default',
                          position: 'relative',
                          '&:hover': { backgroundColor: '#fafafa' },
                        }}
                      >
                        <Box sx={{ display: 'flex', alignItems: 'center', color: 'rgba(0,0,0,0.6)' }}>
                          {fileIcon(ext)}
                        </Box>

                        <Box sx={{ minWidth: 0 }}>
                          <Typography
                            variant="body2"
                            sx={{ fontSize: 13, lineHeight: 1.2, maxWidth: 290, overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap' }}
                            title={a.fileName}
                            onClick={() => downloadCommentFile(a.id, a.fileName)}
                          >
                            {a.fileName}
                          </Typography>
                          {a.size != null && (
                            <Typography variant="caption" sx={{ color: 'text.secondary', fontSize: 11 }}>
                              {formatBytes(a.size)}
                            </Typography>
                          )}
                        </Box>

                        <IconButton
                          size="small"
                          onClick={() => downloadCommentFile(a.id, a.fileName)}
                          sx={{ ml: 'auto' }}
                          title="Tải xuống"
                        >
                          <DownloadForOfflineIcon fontSize="small" />
                        </IconButton>
                      </Box>
                    )
                  })}
                </Box>
              )}
            </Box>
          </Box>
        )
      })}

      <Menu
        id="activity-menu"
        anchorEl={anchorEl}
        keepMounted
        open={Boolean(anchorEl)}
        onClose={handleMenuClose}
      >
        <MenuItem onClick={handleDelete}>Delete</MenuItem>
      </Menu>
    </Box>
  )
}

export default OkrActivity
