// 'use client'

import React, {
  useCallback,
  useEffect,
  useMemo,
  useRef,
  useState,
  useContext,
} from 'react'
import {
  Box,
  Stack,
  Chip,
  Typography,
  IconButton,
  TextField,
  Button,
  Drawer,
  Divider,
  LinearProgress,
  Avatar,
  List,
  ListItem,
  ListItemAvatar,
  ListItemText,
  Paper,
  MenuItem,
} from '@mui/material'
import CloseRoundedIcon from '@mui/icons-material/CloseRounded'
import InsertDriveFileIcon from '@mui/icons-material/InsertDriveFile'
import CloudUploadIcon from '@mui/icons-material/CloudUpload'
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline'
import CommentIcon from '@mui/icons-material/Comment'
import PictureAsPdfIcon from '@mui/icons-material/PictureAsPdf'
import DescriptionIcon from '@mui/icons-material/Description'
import TableChartIcon from '@mui/icons-material/TableChart'
import SlideshowIcon from '@mui/icons-material/Slideshow'
import ArchiveIcon from '@mui/icons-material/Archive'
import TextSnippetIcon from '@mui/icons-material/TextSnippet'
import DownloadForOfflineIcon from '@mui/icons-material/DownloadForOffline'

import path from '../../../../api/path'
import { rootApi } from '../../../../api/rootApi'
import { UserContext } from '../../../../context/UserContext'
import { downloadByUrl, downloadCommentFile } from '../../../okr/requests/downloadFile'

/* ------------ helpers ------------ */
const parseAnyDate = (v) => {
  if (!v) return null
  if (v instanceof Date) return isNaN(v.getTime()) ? null : { date: v, hasTime: true }
  if (typeof v === 'number') return { date: new Date(v), hasTime: true }
  if (typeof v === 'string') {
    const s = v.trim()
    if (/^\d{4}-\d{2}-\d{2}T/.test(s)) {
      const d = new Date(s); return isNaN(d.getTime()) ? null : { date: d, hasTime: true }
    }
    let m = s.match(/^(\d{4})-(\d{2})-(\d{2})$/)
    if (m) return { date: new Date(+m[1], +m[2] - 1, +m[3]), hasTime: false }
    m = s.match(/^(\d{2})\/(\d{2})\/(\d{4})$/)
    if (m) return { date: new Date(+m[3], +m[2] - 1, +m[1]), hasTime: false }
    m = s.match(/^(\d{2})\/(\d{2})\/(\d{4})[ T](\d{2}):(\d{2})$/)
    if (m) { const d = new Date(+m[3], +m[2] - 1, +m[1], +m[4], +m[5]); return { date: d, hasTime: true } }
  }
  return null
}
const fmtDateTimeSmart = (v) => {
  const p = parseAnyDate(v); if (!p) return ''
  return p.hasTime
    ? p.date.toLocaleString('vi-VN', { hour: '2-digit', minute: '2-digit' }) + ' ' + p.date.toLocaleDateString('vi-VN')
    : p.date.toLocaleDateString('vi-VN')
}
const parseDate = (v) => parseAnyDate(v)?.date || null
const fmtKB = (bytes) => (bytes ? `${(bytes / 1024).toFixed(1)} KB` : '')
const unwrap = (r) => r?.data ?? r?.Data ?? r
const statusChipColor = (s) =>
  s === 'Submitted' ? 'warning'
  : s === 'Approved'  ? 'success'
  : s === 'Rejected'  ? 'error'
  : s === 'NeedsChanges' ? 'default'
  : 'default'

const iconByAction = (action) => {
  switch (action) {
    case 'Submit': return '📤'
    case 'RequestChanges': return '📝'
    case 'Resubmit': return '♻️'
    case 'Pass': return '✅'
    case 'Approve': return '🟢'
    case 'Reject': return '⛔'
    default: return '•'
  }
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
const formatBytes = (bytes) => {
  if (bytes === undefined || bytes === null) return ''
  const units = ['B', 'KB', 'MB', 'GB']; let i = 0; let n = bytes
  while (n >= 1024 && i < units.length - 1) { n /= 1024; i++ }
  return `${Math.round(n)} ${units[i]}`
}
const getSubmissionId = (sub) => sub?.id ?? sub?.Id ?? null
const getGuid = (x) => (x ? String(x).toLowerCase() : '')

const pickSubmission = (row) =>
  row?.latestSubmission || row?.LatestSubmission ||
  row?.submission || row?.Submission || null

export default function OkrRequestDetails({
  open,
  step,
  onClose,
  onReloadSteps,
}) {
  const { user } = useContext(UserContext)
  const currentUserId = user?.userId

  const [loading, setLoading] = useState(false)
  const [submission, setSubmission] = useState(null)
  const [stepRow, setStepRow] = useState(null)
  const [actionNote, setActionNote] = useState('')
  const [commentDraft, setCommentDraft] = useState('')
  const [attached, setAttached] = useState([])
  const [nextReviewerId, setNextReviewerId] = useState('')
  const [backToUserId, setBackToUserId] = useState('')
  const filePickerRef = useRef(null)

  const status = (submission?.status ?? submission?.Status ?? 'Draft')
  const sid = getSubmissionId(submission)
  const nextId = submission?.nextReviewerId ?? submission?.NextReviewerId

  const userHas = (arr, id)=> Array.isArray(arr) && arr.some(x => (x.userId ?? x.UserId ?? x.id ?? x.Id) === id)

  const role = useMemo(()=>{
    const s = stepRow || step
    if (!s || !currentUserId) return null
    if (userHas(s.executors, currentUserId)) return 'executor'
    if (userHas(s.approvers, currentUserId)) return 'approver'
    if (userHas(s.reviewers, currentUserId)) return 'reviewer'
    return null
  }, [stepRow, step, currentUserId])

  const isNextMe = getGuid(nextId) === getGuid(currentUserId)

  // Cho phép upload & gửi lại khi NeedsChanges (reviewer / executor / người được chỉ định)
  const canResubmitNow = status === 'NeedsChanges' && (role === 'reviewer' || role === 'executor' || isNextMe)
  // Executor được submit khi Draft
  const canSubmitExecutor  = status === 'Draft' && role === 'executor'
  // Reviewer cũng có thể submit nếu BE có trạng thái 'Rejected' tách biệt (an toàn)
  const canReviewerFixRejected = status === 'Rejected' && role === 'reviewer'

  const canShowUpload = canResubmitNow || canSubmitExecutor || canReviewerFixRejected
  const canDeleteFiles = canShowUpload

  // Reviewer chỉ PASS/BACK khi đang review, không phải NeedsChanges
  const canReviewNow =
    (status === 'Submitted' && role === 'reviewer') ||
    (status === 'InReview'  && role === 'reviewer')

  // ---- file pick ----
  const addAttached = (fileList)=>{
    const add = Array.from(fileList || []).map((f,i)=>({
      id:`local-${Date.now()}-${i}`, name:f.name, size:f.size, type:f.type,
      url:URL.createObjectURL(f), raw:f
    }))
    setAttached(prev => [...prev, ...add])
  }
  const removeAttached = (id)=>{
    setAttached(prev=>{
      const tgt = prev.find(x=>x.id===id)
      if (tgt?.url) URL.revokeObjectURL(tgt.url)
      return prev.filter(x=>x.id!==id)
    })
  }

  // ---- load data ----
  const ensureAndLoadSubmission = useCallback(async () => {
    if (!open || !step) return
    setLoading(true)
    try {
      const r = await rootApi.get(path.workflow.getStepRow(step.id))
      const row = unwrap(r?.data)
      setStepRow(row)

      let sub = pickSubmission(row)
      let submissionId = getSubmissionId(sub)

      if (!submissionId) {
        const body = { title: row?.title || row?.code || 'Submission', description: row?.content || '' }
        const created = await rootApi.post(path.workflow.createSubmission(step.id), body)
        submissionId = getSubmissionId(unwrap(created?.data))
      }

      if (submissionId) {
        const res = await rootApi.get(path.workflow.getSubmission(submissionId))
        sub = unwrap(res?.data)
      } else sub = null

      setSubmission(sub)
    } finally { setLoading(false) }
  }, [open, step])

  useEffect(()=>{ ensureAndLoadSubmission() }, [ensureAndLoadSubmission])

  const refreshSubmission = useCallback(async () => {
    if (!step) return
    const r = await rootApi.get(path.workflow.getStepRow(step.id))
    const row = unwrap(r?.data)
    setStepRow(row)
    const sub = pickSubmission(row)
    const submissionId = getSubmissionId(sub)
    if (submissionId) {
      const res = await rootApi.get(path.workflow.getSubmission(submissionId))
      setSubmission(unwrap(res?.data))
    } else setSubmission(null)
  }, [step])

  // ---- actions ----
  const actionApprove = async (id, msg)=> rootApi.post(path.workflow.approve(id), { note: msg || '' })
  const actionReject  = async (id, msg)=> rootApi.post(path.workflow.reject(id),  { note: msg || '' })
  const actionPass    = async (id, msg, toUserId)=> rootApi.post(path.workflow.pass(id), { note: msg || '', toUserId: toUserId || null })
  const actionReqChg  = async (id, msg, toUserId)=> rootApi.post(path.workflow.requestChanges(id), { note: msg || '', toUserId: toUserId || null })

  const doSubmit = async (id, note, files=[])=>{
    const fd = new FormData()
    if (note) fd.append('note', note)
    files.forEach(f => fd.append('files', f.raw))
    const token = localStorage.getItem('accessToken') || ''
    await rootApi.post(String(path.workflow.submit(id)).replace(/^\/+/, ''), fd, {
      headers: { Authorization: `Bearer ${token}` },
      transformRequest: [(d)=>d],
    })
  }

  const doResubmit = async (id, note, files=[])=>{
    if (files.length > 0) {
      for (const f of files) {
        const fd = new FormData()
        fd.append('file', f.raw)
        await rootApi.post(path.workflow.uploadFile(id), fd, {
          headers: { 'Content-Type': 'multipart/form-data' },
        })
      }
    }
    await rootApi.post(path.workflow.resubmit(id), { note: note || '' })
  }

  const deleteFile = async (fid)=>{
    if (!canDeleteFiles) return
    const submissionId = getSubmissionId(submission); if (!submissionId) return
    setLoading(true)
    try{
      await rootApi.delete(path.workflow.deleteFile(submissionId, fid))
      await refreshSubmission()
    } finally { setLoading(false) }
  }

  const sendComment = async ()=>{
    const submissionId = getSubmissionId(submission)
    if (!submissionId || !commentDraft.trim()) return
    setLoading(true)
    try{
      await rootApi.post(path.workflow.addComment(submissionId), { content: commentDraft.trim() })
      setCommentDraft('')
      await refreshSubmission()
    } finally { setLoading(false) }
  }

  const reviewers = stepRow?.reviewers || []
  const executors = stepRow?.executors || []

  return (
    <Drawer anchor="right" open={open} onClose={onClose} PaperProps={{sx:{width:560}}}>
      {loading && <LinearProgress sx={{position:'absolute', inset:0, top:0}}/>}

      <Stack direction="row" alignItems="center" justifyContent="space-between" sx={{p:2}}>
        <Typography variant="h6">Chi tiết công việc</Typography>
        <IconButton onClick={onClose}><CloseRoundedIcon/></IconButton>
      </Stack>
      <Divider/>

      <Box sx={{p:2}}>
        <Typography color="text.secondary">{(stepRow?.code ?? step?.code) || '—'}</Typography>
        <Typography variant="h6">{(stepRow?.title ?? step?.title) || '—'}</Typography>

        <Typography variant="body2" color="text.secondary" sx={{mt:0.5}}>
          Tạo: {fmtDateTimeSmart(submission?.createdAt)} • Cập nhật: {fmtDateTimeSmart(submission?.lastUpdated)}
        </Typography>

        {/* Upload + Submit/Resubmit */}
        {sid && canShowUpload && (
          <Stack spacing={1.5} sx={{ mt: 1.25, mb: 1.25 }}>
            <Stack direction="row" spacing={1}>
              <input
                ref={filePickerRef}
                type="file"
                multiple
                accept="image/*,.pdf,.doc,.docx,.xls,.xlsx"
                style={{display:'none'}}
                onChange={(e)=>{ if(e.target.files?.length) addAttached(e.target.files); if(filePickerRef.current) filePickerRef.current.value='' }}
              />
              <Button variant="outlined" onClick={()=>filePickerRef.current?.click()} startIcon={<CloudUploadIcon/>}>Đính kèm</Button>
            </Stack>

            <TextField
              placeholder="Nội dung"
              multiline minRows={4} fullWidth
              value={actionNote} onChange={(e)=>setActionNote(e.target.value)}
              sx={{'& .MuiOutlinedInput-root':{alignItems:'flex-start'}}}
            />

            {attached.length>0 && (
              <Stack direction="row" spacing={1} flexWrap="wrap" useFlexGap>
                {attached.map(f=>(
                  <Paper key={f.id} variant="outlined" sx={{p:.75, display:'flex', alignItems:'center', gap:1, maxWidth:'100%', minWidth:0, flex:1}}>
                    {f.type?.startsWith('image/') ? (
                      <img src={f.url} alt={f.name} style={{width:56, height:56, objectFit:'cover', borderRadius:4}}/>
                    ) : <InsertDriveFileIcon fontSize="small"/>}
                    <Stack sx={{minWidth:0, flex:1}}>
                      <Typography variant="body2" title={f.name} sx={{fontSize:13, overflow:'hidden', textOverflow:'ellipsis', whiteSpace:'nowrap'}}>{f.name}</Typography>
                      <Typography variant="caption" color="text.secondary">{fmtKB(f.size)}</Typography>
                    </Stack>
                    <IconButton size="small" onClick={()=>removeAttached(f.id)} sx={{flexShrink:0}}><CloseRoundedIcon fontSize="small"/></IconButton>
                  </Paper>
                ))}
              </Stack>
            )}

            <Stack direction="row" justifyContent="flex-end">
              <Button
                variant="contained"
                onClick={async ()=>{
                  setLoading(true)
                  try{
                    if (status === 'NeedsChanges') {
                      await doResubmit(sid, actionNote, attached)
                    } else {
                      await doSubmit(sid, actionNote, attached)
                    }
                    setAttached([]); setActionNote('')
                    await refreshSubmission(); onReloadSteps?.()
                  } finally { setLoading(false) }
                }}
              >
                {status === 'NeedsChanges' ? 'GỬI LẠI (RESUBMIT)' : 'GỬI TRÌNH DUYỆT'}
              </Button>
            </Stack>
          </Stack>
        )}

        {/* TIMELINE */}
        <Divider sx={{my:1.5}}/>
        <Typography variant="subtitle2" sx={{mb:1}}>Timeline</Typography>
        {submission?.events?.length ? (
          <Stack spacing={1} sx={{maxHeight:220, overflow:'auto', pr:1}}>
            {submission.events
              .slice()
              .sort((a,b)=>(parseDate(b.at)?.getTime()??0)-(parseDate(a.at)?.getTime()??0))
              .map(ev=>(
                <Paper key={ev.id} sx={{p:1, display:'grid', gridTemplateColumns:'28px 1fr auto', alignItems:'center', gap:1}}>
                  <Box sx={{fontSize:18, textAlign:'center'}}>{iconByAction(ev.action)}</Box>
                  <Box sx={{minWidth:0}}>
                    <Box sx={{display:'flex', alignItems:'center', gap:.5, flexWrap:'wrap'}}>
                      <Typography variant="body2" fontWeight={600}>{ev.action}</Typography>
                      <Typography variant="body2">→</Typography>
                      <Chip size="small" variant="outlined" color={statusChipColor(ev.toStatus)} label={ev.toStatus}/>
                      {ev.note && <Typography variant="body2" sx={{ml:.5}} color="text.secondary">— {ev.note}</Typography>}
                    </Box>
                    <Typography variant="caption" color="text.secondary">{ev.byRole || '—'} • {fmtDateTimeSmart(ev.at)}</Typography>
                  </Box>
                  <Box/>
                </Paper>
            ))}
          </Stack>
        ) : (
          <Typography variant="caption" color="text.secondary">Chưa có sự kiện.</Typography>
        )}

        {/* FILES */}
        <Divider sx={{my:1.5}}/>
        <Typography variant="subtitle2" sx={{mb:1}}>Tệp đã gửi</Typography>
        {submission?.files?.length ? (
          <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 1 }}>
            {submission.files.map((f) => {
              const id   = f.id ?? f.Id
              const name = f.fileName ?? f.FileName ?? 'file'
              const size = f.fileSize ?? f.FileSize
              const url  = f.downloadUrl || f.DownloadUrl
              const ext  = getExt(name)
              const onDownload = (e) => {
                e.stopPropagation()
                if (url) return downloadByUrl(url, name)
                if (id)  return downloadCommentFile(id, name)
              }
              return (
                <Box key={id || name} sx={{
                  display:'inline-flex', alignItems:'center', gap:1, maxWidth:420, px:1, py:.5,
                  border:'1px solid #e5e7eb', borderRadius:999, bgcolor:'#fff', '&:hover':{ backgroundColor:'#fafafa' }
                }}>
                  <Box sx={{ display:'flex', alignItems:'center', color:'rgba(0,0,0,0.6)' }}>{fileIcon(ext)}</Box>
                  <Box sx={{ minWidth:0 }}>
                    <Typography
                      variant="body2" title={name} onClick={onDownload}
                      sx={{ fontSize:13, lineHeight:1.2, maxWidth:300, overflow:'hidden', textOverflow:'ellipsis', whiteSpace:'nowrap', cursor:'pointer', fontWeight:500 }}
                    >{name}</Typography>
                    {size != null && <Typography variant="caption" sx={{ color:'text.secondary', fontSize:11 }}>{formatBytes(size)}</Typography>}
                  </Box>
                  <IconButton size="small" onClick={onDownload} title="Tải xuống" sx={{ ml:'auto' }}>
                    <DownloadForOfflineIcon fontSize="small" />
                  </IconButton>
                  {canDeleteFiles && (
                    <IconButton size="small" color="error" onClick={()=>deleteFile(id)} title="Xoá tệp">
                      <DeleteOutlineIcon fontSize="small" />
                    </IconButton>
                  )}
                </Box>
              )
            })}
          </Box>
        ) : (
          <Typography variant="caption" color="text.secondary">Chưa có tệp nào trong submission.</Typography>
        )}

        {/* Reviewer: PASS/BACK (không hiện ở NeedsChanges) */}
        {sid && canReviewNow && (
          <Stack spacing={1.25} sx={{ mt: 2, mb: 1 }}>
            <TextField
              label="Người thẩm định kế tiếp (tuỳ chọn)"
              size="small"
              select
              value={nextReviewerId}
              onChange={(e)=>setNextReviewerId(e.target.value)}
            >
              <MenuItem value="">— Không chọn —</MenuItem>
              {(reviewers || []).filter(u => (u.userId ?? u.UserId) !== currentUserId).map(u=>(
                <MenuItem key={u.userId ?? u.UserId} value={u.userId ?? u.UserId}>
                  {u.name ?? u.Name ?? u.fullName ?? 'User'}
                </MenuItem>
              ))}
            </TextField>

            <TextField
              label="Gửi trả cho (tuỳ chọn)"
              size="small"
              select
              value={backToUserId}
              onChange={(e)=>setBackToUserId(e.target.value)}
            >
              <MenuItem value="">— Không chọn —</MenuItem>
              {(executors || []).map(u=>(
                <MenuItem key={u.userId ?? u.UserId} value={u.userId ?? u.UserId}>
                  {u.name ?? u.Name ?? u.fullName ?? 'User'}
                </MenuItem>
              ))}
              {(reviewers || []).filter(u => (u.userId ?? u.UserId) !== currentUserId).map(u=>(
                <MenuItem key={'rv-' + (u.userId ?? u.UserId)} value={u.userId ?? u.UserId}>
                  (Reviewer) {u.name ?? u.Name ?? u.fullName ?? 'User'}
                </MenuItem>
              ))}
            </TextField>

            <TextField
              size="small"
              placeholder="Ghi chú duyệt / yêu cầu chỉnh sửa..."
              value={actionNote}
              onChange={(e) => setActionNote(e.target.value)}
              multiline
              minRows={2}
              fullWidth
            />

            <Stack direction="row" spacing={1}>
              <Button
                variant="contained"
                color="success"
                onClick={async () => {
                  setLoading(true)
                  try {
                    await actionPass(sid, actionNote, nextReviewerId || null)
                    setActionNote(''); setNextReviewerId('')
                    await refreshSubmission(); onReloadSteps?.()
                  } finally { setLoading(false) }
                }}
              >
                PASS (ACCEPT)
              </Button>

              <Button
                variant="outlined"
                color="warning"
                onClick={async () => {
                  setLoading(true)
                  try {
                    await actionReqChg(sid, actionNote, backToUserId || null)
                    setActionNote(''); setBackToUserId('')
                    await refreshSubmission(); onReloadSteps?.()
                  } finally { setLoading(false) }
                }}
              >
                BACK
              </Button>
            </Stack>
          </Stack>
        )}

        {/* Approver */}
        {sid && role === 'approver' && status === 'ForApproval' && (
          <Stack spacing={1} sx={{ mt: 2, mb: 1 }}>
            <TextField size="small" placeholder="Ghi chú phê duyệt / từ chối..." value={actionNote} onChange={(e)=>setActionNote(e.target.value)} />
            <Stack direction="row" spacing={1}>
              <Button variant="contained" color="success" onClick={async ()=>{
                setLoading(true)
                try{ await actionApprove(sid, actionNote); setActionNote(''); await refreshSubmission(); onReloadSteps?.() }
                finally{ setLoading(false) }
              }}>APPROVE</Button>
              <Button variant="outlined" color="error" onClick={async ()=>{
                setLoading(true)
                try{ await actionReject(sid, actionNote); setActionNote(''); await refreshSubmission(); onReloadSteps?.() }
                finally{ setLoading(false) }
              }}>REJECT</Button>
            </Stack>
          </Stack>
        )}

        {/* Comments */}
        <Divider sx={{my:1.5}}/>
        <Typography variant="subtitle2">Comments</Typography>
        <Stack direction="row" spacing={1} sx={{mt:.5}}>
          <TextField
            size="small" fullWidth placeholder="Viết nhận xét..." value={commentDraft}
            onChange={(e)=>setCommentDraft(e.target.value)} variant="outlined"
            sx={{
              '& .MuiOutlinedInput-notchedOutline': {border:'none'},
              '&:hover .MuiOutlinedInput-notchedOutline': {border:'none'},
              '&.Mui-focused .MuiOutlinedInput-notchedOutline': {border:'none'},
              bgcolor:'#f8fafc',
            }}
          />
          <Button variant="contained" size="small" startIcon={<CommentIcon/>} onClick={sendComment}>Gửi</Button>
        </Stack>

        <Box sx={{mt:1, maxHeight:160, overflow:'auto'}}>
          <List dense sx={{py:0}}>
            {submission?.comments?.length ? (
              submission.comments.map(c=>(
                <ListItem key={c.id} sx={{px:0}}>
                  <ListItemAvatar><Avatar>{(c.byRole || '?')[0]}</Avatar></ListItemAvatar>
                  <ListItemText
                    primary={<Typography variant="body2">{c.content}</Typography>}
                    secondary={<Typography variant="caption" color="text.secondary">
                      {c.byUserName} • {fmtDateTimeSmart(c.createdAt)}
                    </Typography>}
                  />
                </ListItem>
              ))
            ) : (
              <Typography variant="caption" color="text.secondary">Chưa có bình luận.</Typography>
            )}
          </List>
        </Box>
      </Box>
    </Drawer>
  )
}
