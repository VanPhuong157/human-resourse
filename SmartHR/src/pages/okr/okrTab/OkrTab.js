import {
  useEffect,
  useRef,
  useState,
  useMemo,
  forwardRef,
  useImperativeHandle,
} from 'react'
import {
  useReactTable,
  getCoreRowModel,
  getExpandedRowModel,
  flexRender,
} from '@tanstack/react-table'
import {
  Paper,
  Chip,
  LinearProgress,
  Typography,
  Box,
  TextField,
  CircularProgress,
  Select,
  MenuItem,
  IconButton,
} from '@mui/material'
import InsertDriveFileIcon from '@mui/icons-material/InsertDriveFile'
import CloseRoundedIcon from '@mui/icons-material/CloseRounded'
import GroupsOutlinedIcon from '@mui/icons-material/GroupsOutlined'
import BusinessOutlinedIcon from '@mui/icons-material/BusinessOutlined'
import RadioButtonUncheckedIcon from '@mui/icons-material/RadioButtonUnchecked'
import NotesOutlinedIcon from '@mui/icons-material/NotesOutlined'
import CalendarTodayOutlinedIcon from '@mui/icons-material/CalendarTodayOutlined'
import FlagOutlinedIcon from '@mui/icons-material/FlagOutlined'
import ShowChartOutlinedIcon from '@mui/icons-material/ShowChartOutlined'
import AccessTimeOutlinedIcon from '@mui/icons-material/AccessTimeOutlined'
import WarningAmberOutlinedIcon from '@mui/icons-material/WarningAmberOutlined'
import ScheduleOutlinedIcon from '@mui/icons-material/ScheduleOutlined'
import UpdateOutlinedIcon from '@mui/icons-material/UpdateOutlined'
import TextSnippetOutlinedIcon from '@mui/icons-material/TextSnippetOutlined'

import CustomDialog from '../../../components/customDialog'
import OkrDetail from './okrDetail/OkrDetail'
import useGetOkrs from '../requests/getOkrs'
import FormDialog from '../../../components/formDialog/FormDialog'
import OkrFilter from './filter/OkrFilter'
import CreateOKR from './create/CreateOKR'
import OkrProgress from './okrDetail/OkrProgress'
import {showError, showToastSuccess} from '../../../components/notification'
import MultiSelectEditor from '../editor/MultiSelectEditor'

import {rootApi} from '../../../api/rootApi'
import path from '../../../api/path'

import {
  PRIORITY_COLORS,
  STATUS_COLORS,
  TYPE_COLORS,
  getChipColors,
} from '../../../components/color/chipColors'
import {
  DriveFileMoveRounded,
  PeopleRounded,
  SupervisorAccountRounded,
} from '@mui/icons-material'

/* ---------- helpers ---------- */
const MS_PER_DAY = 24 * 60 * 60 * 1000
const startOfDay = (d) => new Date(d.getFullYear(), d.getMonth(), d.getDate())

const isPersonalRow = (row) => {
  const scope = (row.scope || row.Scope || '').toLowerCase()
  const type = (row.type || row.Type || '').toLowerCase()
  return (
    scope === 'cá nhân' ||
    scope === 'personal' ||
    type === 'cá nhân' ||
    type === 'personal'
  )
}

const EllipsisText = ({value}) => {
  const text = value ?? ''
  return (
    <span
      title={text}
      style={{
        display: 'block',
        overflow: 'hidden',
        textOverflow: 'ellipsis',
        whiteSpace: 'nowrap',
        minWidth: 0,
        lineHeight: '1.5',
        padding: '2px 0',
        fontSize: '14px',
        color: '#37352F',
      }}
    >
      {text}
    </span>
  )
}

const ProgressBar = ({value}) => {
  const v = Math.min(Math.max(value ?? 0, 0), 100)
  return (
    <Box
      sx={{
        display: 'flex',
        alignItems: 'center',
        gap: 1,
        mt: '2px',
        minWidth: 0,
      }}
    >
      <Box sx={{width: 100, flexShrink: 0}}>
        <LinearProgress
          variant="determinate"
          value={v}
          sx={{
            height: 8.5,
            borderRadius: 3,
            bgcolor: 'rgba(55, 53, 47, 0.08)',
            '& .MuiLinearProgress-bar': {
              bgcolor: '#04af2f',
              borderRadius: 3,
            },
          }}
        />
      </Box>
      <Typography
        variant="body2"
        sx={{
          minWidth: 36,
          fontSize: '13px',
          color: 'rgba(55, 53, 47, 0.65)',
          fontWeight: 600,
        }}
      >
        {`${Math.round(v)}%`}
      </Typography>
    </Box>
  )
}

const TypeChip = ({value}) => {
  const {bg, fg} = getChipColors(TYPE_COLORS, value || 'default')
  return (
    <Box
      component="span"
      sx={{
        display: 'inline-flex',
        alignItems: 'center',
        gap: 0.5,
        px: 1,
        py: 0.375,
        borderRadius: '4px',
        bgcolor: bg,
        color: fg,
        fontSize: '13px',
        fontWeight: 500,
        lineHeight: 1.4,
        maxWidth: '100%',
      }}
      title={value || ''}
    >
      <span
        style={{
          overflow: 'hidden',
          textOverflow: 'ellipsis',
          whiteSpace: 'nowrap',
        }}
      >
        {value || ''}
      </span>
    </Box>
  )
}

/* ---------- map payload (partial update) ---------- */
const toPartialPayload = (field, newValue) => {
  switch (field) {
    case 'title':
      return {title: newValue}
    case 'type':
      return {type: newValue}
    case 'scope':
      return {scope: newValue}
    case 'targetNumber':
      return {targetNumber: Number(newValue) || 0}
    case 'unitOfTarget':
      return {unitOfTarget: newValue}
    case 'cycle':
      return {cycle: newValue}
    case 'confidenceLevel':
      return {confidenceLevel: newValue}
    case 'result':
      return {result: newValue}
    case 'parentId':
      return {parentId: newValue || null}
    case 'dueDate': {
      const d = new Date(newValue)
      return isNaN(d) ? {} : {dueDate: d.toISOString()}
    }
    case 'priority':
      return {priority: newValue}
    case 'company':
      return {company: newValue}
    case 'note':
      return {note: newValue}
    case 'status':
      return {status: newValue}
    default:
      return {}
  }
}

/* ---------- build tree from flat ---------- */
const buildTree = (flat) => {
  const map = new Map()
  flat.forEach((r) => map.set(r.id, {...r, children: []}))
  const roots = []
  flat.forEach((r) => {
    const node = map.get(r.id)
    if (r.parentId && map.has(r.parentId)) {
      map.get(r.parentId).children.push(node)
    } else {
      roots.push(node)
    }
  })
  return roots
}

/* ---------- small generic editable cells ---------- */
const EditableSelectCell = ({value, options = [], onSave, renderDisplay}) => {
  const [editing, setEditing] = useState(false)
  const [cur, setCur] = useState(value ?? '')
  const [saving, setSaving] = useState(false)

  useEffect(() => {
    setCur(value ?? '')
  }, [value])

  const commit = async (next) => {
    if (next === value) {
      setEditing(false)
      return
    }
    try {
      setSaving(true)
      await onSave(next)
      showToastSuccess('Đã cập nhật')
    } catch (e) {
      showError('Cập nhật thất bại')
      console.error(e)
    } finally {
      setSaving(false)
      setEditing(false)
    }
  }

  if (editing) {
    return (
      <Select
        size="small"
        autoFocus
        open
        value={cur}
        onChange={(e) => {
          const next = e.target.value
          setCur(next)
          commit(next)
        }}
        onClose={() => setEditing(false)}
        sx={{
          minWidth: 160,
          fontSize: '14px',
          '& .MuiOutlinedInput-notchedOutline': {
            borderColor: 'rgba(55, 53, 47, 0.16)',
          },
        }}
        MenuProps={{MenuListProps: {dense: true}}}
      >
        {options.map((op) => (
          <MenuItem key={op} value={op} sx={{fontSize: '14px'}}>
            {op}
          </MenuItem>
        ))}
      </Select>
    )
  }

  return (
    <Box
      onClick={(e) => {
        e.stopPropagation()
        setEditing(true)
      }}
      sx={{
        display: 'flex',
        alignItems: 'center',
        width: 'calc(100% + 24px)',
        minHeight: 32,
        minWidth: 0,
        marginX: '-12px',
        paddingX: '12px',
        paddingY: '8px',
        cursor: 'pointer',
        '&:hover': {
          backgroundColor: 'rgba(55,53,47,0.04)',
          borderRadius: '4px',
        },
      }}
      title="Nhấp để chỉnh sửa"
    >
      {saving ? (
        <CircularProgress size={14} sx={{mr: 0.5}} />
      ) : renderDisplay ? (
        renderDisplay(value)
      ) : (
        <EllipsisText value={value} />
      )}
    </Box>
  )
}

const EditableTextCell = ({value, onSave, placeholder = ''}) => {
  const [editing, setEditing] = useState(false)
  const [cur, setCur] = useState(value ?? '')
  const [saving, setSaving] = useState(false)

  useEffect(() => {
    setCur(value ?? '')
  }, [value])

  const commit = async () => {
    if (cur === value) {
      setEditing(false)
      return
    }
    try {
      setSaving(true)
      await onSave(cur)
      showToastSuccess('Đã cập nhật')
    } catch (e) {
      showError('Cập nhật thất bại')
      console.error(e)
    } finally {
      setSaving(false)
      setEditing(false)
    }
  }

  if (editing) {
    return (
      <TextField
        size="small"
        autoFocus
        placeholder={placeholder}
        value={cur}
        onChange={(e) => setCur(e.target.value)}
        onBlur={commit}
        onKeyDown={(e) => {
          if (e.key === 'Enter') commit()
          if (e.key === 'Escape') setEditing(false)
        }}
        sx={{
          minWidth: 180,
          '& .MuiOutlinedInput-root': {
            fontSize: '14px',
            '& fieldset': {borderColor: 'rgba(55, 53, 47, 0.16)'},
          },
        }}
      />
    )
  }

  return (
    <Box
      onClick={(e) => {
        e.stopPropagation()
        setEditing(true)
      }}
      sx={{
        display: 'flex',
        alignItems: 'center',
        width: 'calc(100% + 24px)',
        minHeight: 32,
        minWidth: 0,
        marginX: '-12px',
        paddingX: '12px',
        paddingY: '8px',
        cursor: 'text',
        '&:hover': {
          backgroundColor: 'rgba(55,53,47,0.04)',
          borderRadius: '4px',
        },
      }}
      title="Nhấp để chỉnh sửa"
    >
      {saving && <CircularProgress size={14} sx={{mr: 0.5}} />}
      <EllipsisText value={value} />
    </Box>
  )
}

const EditableDateCell = ({value, onSave}) => {
  const [editing, setEditing] = useState(false)
  const [cur, setCur] = useState(
    value ? new Date(value).toISOString().slice(0, 10) : '',
  )
  const [saving, setSaving] = useState(false)

  useEffect(() => {
    setCur(value ? new Date(value).toISOString().slice(0, 10) : '')
  }, [value])

  const commit = async (nextVal = cur) => {
    try {
      setSaving(true)
      await onSave(nextVal)
      showToastSuccess('Đã cập nhật')
    } catch (e) {
      showError('Cập nhật thất bại')
      console.error(e)
    } finally {
      setSaving(false)
      setEditing(false)
    }
  }

  if (editing) {
    return (
      <TextField
        type="date"
        size="small"
        autoFocus
        value={cur}
        onChange={(e) => {
          const next = e.target.value
          setCur(next)
          commit(next)
        }}
        onBlur={() => commit()}
        onKeyDown={(e) => {
          if (e.key === 'Enter') commit()
          if (e.key === 'Escape') setEditing(false)
        }}
        sx={{
          minWidth: 160,
          '& .MuiOutlinedInput-root': {
            fontSize: '14px',
            '& fieldset': {borderColor: 'rgba(55, 53, 47, 0.16)'},
          },
        }}
      />
    )
  }

  const label = value
    ? new Date(value).toLocaleDateString('vi-VN', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
      })
    : ''

  return (
    <Box
      onClick={(e) => {
        e.stopPropagation()
        setEditing(true)
      }}
      sx={{
        display: 'flex',
        alignItems: 'center',
        width: 'calc(100% + 24px)',
        minHeight: 32,
        minWidth: 0,
        marginX: '-12px',
        paddingX: '12px',
        paddingY: '8px',
        cursor: 'pointer',
      }}
      title="Nhấp để chỉnh sửa"
    >
      {saving && <CircularProgress size={14} sx={{mr: 0.5}} />}
      <EllipsisText value={label} />
    </Box>
  )
}
/* ---------- Multi-select cell (owner/manager/department) ---------- */
/* ---------- side pane sizes ---------- */
const DEFAULT_PANE_WIDTH = 720
const MIN_PANE_WIDTH = 420
const MAX_PANE_WIDTH = 1200
const LS_KEY = 'okrPaneWidth'

const OkrTab = forwardRef(
  ({quickFilter = {}, selectedDepartment = 'All Department'}, ref) => {
    /* ---------- Side pane state ---------- */
    const [detailOpen, setDetailOpen] = useState(false)
    const [okrDetailData, setOkrDetailData] = useState(null)

    const [paneWidth, setPaneWidth] = useState(() => {
      const saved = Number(localStorage.getItem(LS_KEY))
      if (Number.isFinite(saved))
        return Math.max(MIN_PANE_WIDTH, Math.min(MAX_PANE_WIDTH, saved))
      return DEFAULT_PANE_WIDTH
    })
    const dragState = useRef({startX: 0, startW: DEFAULT_PANE_WIDTH})
    const [dragging, setDragging] = useState(false)

    const [openFilterDialog, setOpenFilterDialog] = useState(false)
    const [dataFilterDialog, setDataFilterDialog] = useState({
      title: '',
      type: '',
      scope: '',
      status: '',
      cycle: '',
    })
    const [openAddDetailDialog, setOpenAddDetailDialog] = useState(false)

    /* ---------- Progress dialog ---------- */
    const [openProgressDialog, setOpenProgressDialog] = useState(false)
    const [progressRow, setProgressRow] = useState(null)
    const openProgressForRow = (row) => {
      setProgressRow(row)
      setOpenProgressDialog(true)
    }
    const handleProgressDialogClose = () => {
      setOpenProgressDialog(false)
      setProgressRow(null)
      refetch()
    }

    const departmentId =
      typeof window !== 'undefined'
        ? localStorage.getItem('departmentId')
        : null

    const {data, refetch, isLoading} = useGetOkrs(
      1,
      100,
      departmentId,
      dataFilterDialog,
    )

    useImperativeHandle(ref, () => ({
      openFilter: () => setOpenFilterDialog(true),
      openAdd: () => setOpenAddDetailDialog(true),
    }))

    useEffect(() => {
      setDataFilterDialog((d) => ({...d, status: quickFilter?.status || ''}))
    }, [quickFilter?.status])

    useEffect(() => {
      refetch()
    }, [selectedDepartment, dataFilterDialog, refetch])

    /* ---------- Map rows ---------- */
    const flatRows = useMemo(() => {
      const src = data?.data?.items || []
      const today = startOfDay(new Date())

      return src
        .map((it) => {
          const ownersArr =
            it?.ownerNames ??
            it?.OwnerNames ??
            (it?.Owner ? [it.Owner] : []) ??
            []
          const managersArr = it?.managerNames ?? it?.ManagerNames ?? []
          const departmentsArr = it?.departmentName ?? it?.DepartmentName ?? []

          const owners = Array.isArray(ownersArr)
            ? ownersArr.filter(Boolean).join(', ')
            : String(ownersArr ?? '')
          const managers = Array.isArray(managersArr)
            ? managersArr.filter(Boolean).join(', ')
            : String(managersArr ?? '')
          const departments = Array.isArray(departmentsArr)
            ? departmentsArr.filter(Boolean).join(', ')
            : String(departmentsArr ?? '')

          const dueRaw = it.dueDate ?? it.DueDate ?? null
          const due = dueRaw ? new Date(dueRaw) : null
          const dueDay = due ? startOfDay(due) : null

          let remainingDays = null
          let overdue = null
          const computedStatus = it.status || ''
          if (dueDay) {
            const diff = Math.floor(
              (dueDay.getTime() - today.getTime()) / MS_PER_DAY,
            )
            remainingDays = diff
            overdue = diff < 0 ? Math.abs(diff) : null
          }

          return {
            ...it,
            assignee: owners,
            manager: managers,
            departmentName: departments,

            assignees: Array.isArray(ownersArr)
              ? ownersArr.filter(Boolean)
              : String(ownersArr ?? '')
                  .split(',')
                  .map((s) => s.trim())
                  .filter(Boolean),
            managerTags: Array.isArray(managersArr)
              ? managersArr.filter(Boolean)
              : String(managersArr ?? '')
                  .split(',')
                  .map((s) => s.trim())
                  .filter(Boolean),
            departmentTags: Array.isArray(departmentsArr)
              ? departmentsArr.filter(Boolean)
              : String(departmentsArr ?? '')
                  .split(',')
                  .map((s) => s.trim())
                  .filter(Boolean),

            ownerIds: it?.ownerIds ?? it?.OwnerIds ?? it?.ownerId ?? [],
            managerIds: it?.managerIds ?? it?.ManagerIds ?? it?.managerId ?? [],
            departmentIds:
              it?.departmentIds ?? it?.DepartmentIds ?? it?.departmentId ?? [],

            remainingDays,
            overdue,
            createdTime: it.dateCreated ?? it.DateCreated ?? null,
            lastUpdate: it.lastUpdated ?? it.LastUpdated ?? null,
            status: computedStatus,
          }
        })
        .filter((item) => {
          if (
            selectedDepartment !== 'All Department' &&
            item.departmentId !== selectedDepartment
          )
            return false
          if (
            quickFilter.company &&
            (item.company || '').toUpperCase() !==
              quickFilter.company.toUpperCase()
          )
            return false
          if (quickFilter.overdue) {
            const d = item.dueDate ? new Date(item.dueDate) : null
            const done = item.status === 'Done'
            if (!d || done || startOfDay(d) >= startOfDay(new Date()))
              return false
          }
          return true
        })
    }, [data, selectedDepartment, quickFilter])

    /* ---------- Build tree for TanStack ---------- */
    const dataTree = useMemo(() => buildTree(flatRows), [flatRows])

    /* ---------- fetchers ---------- */
    const pickItems = (res) =>
      res?.data?.items ?? res?.data?.data?.items ?? res?.items ?? []

    const fetchUsers = async () => {
      try {
        const res = await rootApi.get(path.user?.getUsers?.() ?? '/users')
        const items = pickItems(res)
        return items.map((u) => ({
          value: u.id ?? u.Id,
          label:
            u.fullName ??
            u.FullName ??
            u.name ??
            u.userName ??
            String(u.id ?? u.Id),
        }))
      } catch {
        return []
      }
    }

    const fetchDepartments = async () => {
      try {
        const res = await rootApi.get(path.department.getDepartments())
        const items = pickItems(res)
        return items.map((d) => ({
          value: d.id ?? d.Id,
          label: d.name ?? d.Name ?? String(d.id ?? d.Id),
        }))
      } catch {
        return []
      }
    }

    /* ---------- common partial update ---------- */
    const updatePartial = async (okrId, field, newVal) => {
      const payload = toPartialPayload(field, newVal)
      if (!payload || !Object.keys(payload).length) return
      await rootApi.put(path.okr.updateOkr({okrId}), payload)
      refetch()
    }

    /* ---------- Columns ---------- */
    const columns = useMemo(
      () => [
        {
          id: 'title',
          header: 'Name',
          headerIcon: (
            <TextSnippetOutlinedIcon
              sx={{fontSize: 14, mr: 0.5, color: 'rgba(55, 53, 47, 0.45)'}}
            />
          ),
          accessorFn: (row) => row.title || row.Title || '',
          cell: ({row, getValue}) => {
            const title = getValue() || ''
            const canExpand = row.getCanExpand()
            const depth = row.depth
            const openSide = (e) => {
              e.stopPropagation()
              setOkrDetailData({id: row.original.id || row.original.Id})
              setDetailOpen(true)
            }
            return (
              <span
                className="name-cell"
                style={{display: 'flex', alignItems: 'center', minWidth: 0}}
              >
                <span
                  {...{
                    onClick: row.getToggleExpandedHandler(),
                    style: {
                      cursor: canExpand ? 'pointer' : 'default',
                      paddingLeft: 4 + depth * 20,
                      display: 'inline-flex',
                      alignItems: 'center',
                      gap: 8,
                      minWidth: 0,
                    },
                  }}
                  title={title}
                >
                  {canExpand ? (
                    <span
                      style={{
                        fontWeight: 400,
                        width: 16,
                        display: 'inline-block',
                        textAlign: 'center',
                        color: 'rgba(55, 53, 47, 0.4)',
                        fontSize: '14px',
                      }}
                    >
                      {row.getIsExpanded() ? '▾' : '▸'}
                    </span>
                  ) : (
                    <span style={{width: 16, display: 'inline-block'}} />
                  )}
                  <InsertDriveFileIcon
                    sx={{fontSize: 16, color: 'rgba(55, 53, 47, 0.4)'}}
                  />
                  <span
                    className="name-text"
                    style={{
                      color: '#37352F',
                      fontWeight: 500,
                      fontSize: '14px',
                      lineHeight: 1.5,
                      whiteSpace: 'nowrap',
                      overflow: 'hidden',
                      textOverflow: 'ellipsis',
                      minWidth: 0,
                      display: 'block',
                      flex: '1 1 auto',
                      paddingRight: 84,
                    }}
                  >
                    {title}
                  </span>
                </span>

                <button
                  className="open-btn icon"
                  data-open="1"
                  onClick={openSide}
                  title="Open in side peek"
                  style={{
                    position: 'absolute',
                    right: 8,
                    display: 'inline-flex',
                    alignItems: 'center',
                    gap: 4,
                    fontSize: 12,
                    fontWeight: 600,
                    borderRadius: 4,
                    border: '1px solid rgba(55, 53, 47, 0.16)',
                    background: 'white',
                    color: 'rgba(65, 64, 61, 0.65)',
                    cursor: 'pointer',
                    padding: '4px 4px',
                    transition: 'opacity 0.15s ease, background 0.15s ease',
                  }}
                  onMouseEnter={(e) => {
                    e.currentTarget.style.background = 'rgba(55, 53, 47, 0.04)'
                  }}
                  onMouseLeave={(e) => {
                    e.currentTarget.style.background = 'white'
                  }}
                >
                  <DriveFileMoveRounded sx={{fontSize: 14}} />
                  <span
                    className="open-text"
                    style={{
                      fontWeight: 500,
                      letterSpacing: '.02em',
                      fontSize: 11,
                    }}
                  >
                    OPEN
                  </span>
                </button>
              </span>
            )
          },
          size: 280,
        },

        {
          id: 'assignees',
          header: 'Người thực hiện',
          headerIcon: (
            <PeopleRounded
              sx={{fontSize: 16, mr: 0.5, color: 'rgba(55, 53, 47, 0.45)'}}
            />
          ),
          accessorFn: (row) => row,
          cell: ({row}) => (
            <MultiSelectEditor
              rowData={row.original}
              idsKey="ownerIds"
              namesKey="assignees"
              placeholder="Chọn người thực hiện…"
              fetchOptions={fetchUsers}
              onSaveIds={async (ids, r) => {
                const okrId = r.id || r.Id
                await rootApi.put(path.okr.editOwnerOkr({okrId}), {
                  ownerIds: ids,
                })
                refetch()
              }}
              successMsg="Cập nhật người thực hiện thành công!"
              errorMsg="Cập nhật người thực hiện thất bại."
            />
          ),
          size: 200,
        },
        {
          id: 'managerTags',
          header: 'Người quản lý',
          headerIcon: (
            <SupervisorAccountRounded
              sx={{fontSize: 14, mr: 0.5, color: 'rgba(55, 53, 47, 0.45)'}}
            />
          ),
          accessorFn: (row) => row,
          cell: ({row}) => (
            <MultiSelectEditor
              rowData={row.original}
              idsKey="managerIds"
              namesKey="managerTags"
              placeholder="Chọn người quản lý…"
              fetchOptions={fetchUsers}
              onSaveIds={async (ids, r) => {
                const okrId = r.id || r.Id
                await rootApi.put(path.okr.editOwnerOkr({okrId}), {
                  managerIds: ids,
                })
                refetch()
              }}
              successMsg="Cập nhật người quản lý thành công!"
              errorMsg="Cập nhật người quản lý thất bại."
            />
          ),
          size: 200,
        },
        {
          id: 'departmentTags',
          header: 'Phòng Ban',
          headerIcon: (
            <BusinessOutlinedIcon
              sx={{fontSize: 14, mr: 0.5, color: 'rgba(55, 53, 47, 0.45)'}}
            />
          ),
          accessorFn: (row) => row,
          cell: ({row}) => (
            <MultiSelectEditor
              rowData={row.original}
              idsKey="departmentIds"
              namesKey="departmentTags"
              placeholder="Chọn phòng ban…"
              fetchOptions={fetchDepartments}
              onSaveIds={async (ids, r) => {
                const okrId = r.id || r.Id
                await rootApi.put(path.okr.updateDepartmentOkr({okrId}), {
                  departmentIds: ids,
                })
                refetch()
              }}
              successMsg="Cập nhật phòng ban thành công!"
              errorMsg="Cập nhật phòng ban thất bại."
            />
          ),
          size: 180,
        },

        {
          id: 'type',
          header: 'Loại',
          headerIcon: (
            <GroupsOutlinedIcon
              sx={{fontSize: 14, mr: 0.5, color: 'rgba(55, 53, 47, 0.45)'}}
            />
          ),
          accessorFn: (row) => row.type,
          cell: ({row, getValue}) => (
            <EditableSelectCell
              value={getValue()}
              options={['Công việc chung', 'Công việc cá nhân']}
              onSave={(v) =>
                updatePartial(row.original.id || row.original.Id, 'type', v)
              }
              renderDisplay={(v) => <TypeChip value={v} />}
            />
          ),
          size: 140,
        },

        {
          id: 'status',
          header: 'Status',
          headerIcon: (
            <RadioButtonUncheckedIcon
              sx={{fontSize: 14, mr: 0.5, color: 'rgba(55, 53, 47, 0.45)'}}
            />
          ),
          accessorFn: (row) => row.status,
          cell: ({row, getValue}) => {
            const val = getValue()
            const {bg, fg} = getChipColors(STATUS_COLORS, val)
            return (
              <EditableSelectCell
                value={val}
                options={['To Do', 'Processing', 'Done', 'PostPone', 'Quá Hạn']}
                onSave={(v) =>
                  updatePartial(row.original.id || row.original.Id, 'status', v)
                }
                renderDisplay={(v) => (
                  <Box
                    component="span"
                    sx={{
                      display: 'inline-flex',
                      alignItems: 'center',
                      gap: 0.75,
                      px: 1,
                      py: 0.375,
                      borderRadius: '16px',
                      bgcolor: bg,
                      color: fg,
                      fontSize: '13px',
                      fontWeight: 500,
                      lineHeight: 1.4,
                      maxWidth: '100%',
                    }}
                  >
                    <Box
                      component="span"
                      sx={{
                        width: 6,
                        height: 6,
                        borderRadius: '50%',
                        bgcolor: fg,
                      }}
                    />
                    <span
                      style={{
                        overflow: 'hidden',
                        textOverflow: 'ellipsis',
                        whiteSpace: 'nowrap',
                      }}
                    >
                      {v || ''}
                    </span>
                  </Box>
                )}
              />
            )
          },
          size: 120,
        },

        {
          id: 'note',
          header: 'Note',
          headerIcon: (
            <NotesOutlinedIcon
              sx={{fontSize: 14, mr: 0.5, color: 'rgba(55, 53, 47, 0.45)'}}
            />
          ),
          accessorFn: (row) => row.note,
          cell: ({row, getValue}) => (
            <EditableTextCell
              value={getValue() ?? ''}
              onSave={(v) =>
                updatePartial(row.original.id || row.original.Id, 'note', v)
              }
              placeholder="Ghi chú…"
            />
          ),
          size: 220,
        },

        {
          id: 'dueDate',
          header: 'Due date',
          headerIcon: (
            <CalendarTodayOutlinedIcon
              sx={{fontSize: 14, mr: 0.5, color: 'rgba(55, 53, 47, 0.45)'}}
            />
          ),
          accessorFn: (row) => row.dueDate,
          cell: ({row, getValue}) => (
            <EditableDateCell
              value={getValue()}
              onSave={(v) =>
                updatePartial(row.original.id || row.original.Id, 'dueDate', v)
              }
            />
          ),
          size: 120,
        },

        {
          id: 'confidenceLevel',
          header: 'Priority',
          headerIcon: (
            <FlagOutlinedIcon
              sx={{fontSize: 14, mr: 0.5, color: 'rgba(55, 53, 47, 0.45)'}}
            />
          ),
          accessorFn: (row) => row.confidenceLevel,
          cell: ({row, getValue}) => {
            const v = getValue()
            const {bg, fg} = getChipColors(PRIORITY_COLORS, v)
            return (
              <EditableSelectCell
                value={v}
                options={['Low', 'Medium', 'High']}
                onSave={(nv) =>
                  updatePartial(
                    row.original.id || row.original.Id,
                    'confidenceLevel',
                    nv,
                  )
                }
                renderDisplay={(val) => (
                  <Chip
                    size="small"
                    label={val || ''}
                    sx={{
                      bgcolor: bg,
                      color: fg,
                      fontSize: '13px',
                      fontWeight: 500,
                      borderRadius: '4px',
                      height: '24px',
                      '& .MuiChip-label': {
                        lineHeight: 1.4,
                        px: 1,
                      },
                    }}
                  />
                )}
              />
            )
          },
          size: 120,
        },

        {
          id: 'progress',
          header: 'Progress',
          headerIcon: (
            <ShowChartOutlinedIcon
              sx={{fontSize: 14, mr: 0.5, color: 'rgba(55,53,47,.45)'}}
            />
          ),
          accessorFn: (row) => row.progress,
          cell: ({row, getValue}) => {
            const isLeaf = !row.getCanExpand()
            const canEdit = isLeaf // <-- sửa ở đây

            return (
              <button
                type="button"
                onClick={(e) => {
                  e.stopPropagation()
                  if (canEdit) openProgressForRow(row.original)
                }}
                onKeyDown={(e) => {
                  if ((e.key === 'Enter' || e.key === ' ') && canEdit) {
                    e.preventDefault()
                    e.stopPropagation()
                    openProgressForRow(row.original)
                  }
                }}
                title={canEdit ? 'Cập nhật tiến độ' : 'Chỉ chỉnh ở dòng lá'}
                style={{
                  all: 'unset',
                  display: 'block',
                  width: '100%',
                  cursor: canEdit ? 'pointer' : 'default',
                  pointerEvents: 'auto',
                }}
              >
                <ProgressBar value={getValue()} />
              </button>
            )
          },
          size: 220,
        },

        {
          id: 'remainingDays',
          header: 'Tiến độ còn lại',
          headerIcon: (
            <AccessTimeOutlinedIcon
              sx={{fontSize: 14, mr: 0.5, color: 'rgba(55, 53, 47, 0.45)'}}
            />
          ),
          accessorFn: (row) => row.remainingDays,
          cell: ({getValue}) => {
            const v = getValue()
            let txt = ''
            if (v === null || v === undefined) txt = ''
            else if (v > 0) txt = `${v} ngày còn lại`
            else if (v === 0) txt = 'Hôm nay'
            else txt = `${Math.abs(v)} ngày quá hạn`
            const isOverdue = typeof v === 'number' && v < 0
            return (
              <span
                style={{
                  color: isOverdue ? '#E03E3E' : 'rgba(55, 53, 47, 0.65)',
                  fontSize: '14px',
                }}
              >
                {txt}
              </span>
            )
          },
          size: 160,
        },

        {
          id: 'company',
          header: 'Công ty',
          headerIcon: (
            <BusinessOutlinedIcon
              sx={{fontSize: 14, mr: 0.5, color: 'rgba(55, 53, 47, 0.45)'}}
            />
          ),
          accessorFn: (row) => row.company,
          cell: ({row, getValue}) => (
            <EditableTextCell
              value={getValue() ?? ''}
              onSave={(v) =>
                updatePartial(row.original.id || row.original.Id, 'company', v)
              }
              placeholder="Công ty"
            />
          ),
          size: 160,
        },

        {
          id: 'overdue',
          header: 'Quá hạn',
          headerIcon: (
            <WarningAmberOutlinedIcon
              sx={{fontSize: 14, mr: 0.5, color: 'rgba(55, 53, 47, 0.45)'}}
            />
          ),
          accessorFn: (row) => row.overdue,
          cell: ({getValue}) =>
            getValue() > 0 ? (
              <span
                style={{color: '#E03E3E', fontSize: '14px'}}
              >{`${getValue()} ngày quá hạn`}</span>
            ) : (
              <span style={{fontSize: '14px', color: 'rgba(55, 53, 47, 0.65)'}}>
                {getValue() ? `${getValue()} ngày quá hạn` : ''}
              </span>
            ),
          size: 120,
        },

        {
          id: 'createdTime',
          header: 'Created time',
          headerIcon: (
            <ScheduleOutlinedIcon
              sx={{fontSize: 14, mr: 0.5, color: 'rgba(55, 53, 47, 0.45)'}}
            />
          ),
          accessorFn: (row) => row.createdTime,
          cell: ({getValue}) => <EllipsisText value={getValue()} />,
          size: 160,
        },

        {
          id: 'lastUpdate',
          header: 'Last update',
          headerIcon: (
            <UpdateOutlinedIcon
              sx={{fontSize: 14, mr: 0.5, color: 'rgba(55, 53, 47, 0.45)'}}
            />
          ),
          accessorFn: (row) => row.lastUpdate,
          cell: ({getValue}) => {
            const v = getValue()
            const label = v
              ? new Date(v).toLocaleDateString('vi-VN', {
                  day: '2-digit',
                  month: '2-digit',
                  year: 'numeric',
                })
              : ''
            return <EllipsisText value={label} />
          },
          size: 160,
        },
      ],
      // eslint-disable-next-line react-hooks/exhaustive-deps
      [refetch],
    )

    /* ---------- TanStack table ---------- */
    const [expanded, setExpanded] = useState(true)
    const [columnSizing, setColumnSizing] = useState({})

    const table = useReactTable({
      data: dataTree,
      columns,
      getCoreRowModel: getCoreRowModel(),
      getExpandedRowModel: getExpandedRowModel(),
      getSubRows: (row) => row.children || [],
      state: {
        expanded,
        columnSizing,
      },
      onExpandedChange: setExpanded,
      onColumnSizingChange: setColumnSizing,
      columnResizeMode: 'onChange',
      enableColumnResizing: true,
      getRowId: (row) => row.id || row.Id,
    })

    /* ---------- side pane drag ---------- */
    useEffect(() => {
      const handleMouseMove = (e) => {
        if (!dragging) return
        const delta = dragState.current.startX - e.clientX
        const next = Math.max(
          MIN_PANE_WIDTH,
          Math.min(MAX_PANE_WIDTH, dragState.current.startW + delta),
        )
        setPaneWidth(next)
      }
      const handleMouseUp = () => {
        if (!dragging) return
        setDragging(false)
        document.body.style.cursor = ''
        document.body.style.userSelect = ''
        localStorage.setItem(LS_KEY, String(paneWidth))
        window.removeEventListener('mousemove', handleMouseMove)
        window.removeEventListener('mouseup', handleMouseUp)
      }
      if (dragging) {
        window.addEventListener('mousemove', handleMouseMove)
        window.addEventListener('mouseup', handleMouseUp)
      }
      return () => {
        window.removeEventListener('mousemove', handleMouseMove)
        window.removeEventListener('mouseup', handleMouseUp)
      }
    }, [dragging, paneWidth])

    const gridKey = `${selectedDepartment}|${dataFilterDialog.status || 'all'}|${quickFilter?.company || ''}|${quickFilter?.overdue ? 1 : 0}`

    return (
      <>
        <Paper
          elevation={0}
          sx={{
            height: '100vh',
            display: 'flex',
            flexDirection: 'column',
            bgcolor: '#FFFFFF',
          }}
        >
          <Box
            sx={{
              flex: 1,
              minHeight: 0,
              overflowX: 'scroll', // 👈 thanh cuộn ngang CHỈ ở đây
              overflowY: 'auto',
              position: 'relative',
              boxSizing: 'border-box',
              p: 0,
              scrollbarGutter: 'stable both-edges', // 👈 tránh layout “nhảy” khi có scrollbar
              /* Notion-style hover effects */
              '& .name-cell': {
                position: 'relative',
                display: 'flex',
                alignItems: 'center',
                minWidth: 0,
                flex: 1,
              },
              '& .name-cell .open-btn': {
                opacity: 0,
                pointerEvents: 'none',
                transition: 'opacity .15s ease, background .15s ease',
              },
              '& .name-cell:hover .open-btn': {
                opacity: 1,
                pointerEvents: 'auto',
              },
            }}
          >
            <div>
              <div
                style={{
                  position: 'sticky',
                  top: 0,
                  zIndex: 2,
                  display: 'flex',
                  background: '#FAFAFA',
                  borderBottom: '1px solid rgba(55, 53, 47, 0.09)',
                  height: 36,
                  alignItems: 'center',
                  fontWeight: 500,
                  fontSize: '13px',
                  color: 'rgba(55, 53, 47, 0.65)',
                }}
              >
                {table.getHeaderGroups().map((hg) =>
                  hg.headers.map((header, idx) => (
                    <div
                      key={header.id}
                      style={{
                        padding: '0 12px',
                        width: header.getSize(),
                        flexShrink: 0,
                        position: 'relative',
                        display: 'flex',
                        alignItems: 'center',
                        borderRight:
                          idx < hg.headers.length - 1
                            ? '1px solid rgba(55, 53, 47, 0.09)'
                            : 'none',
                      }}
                    >
                      {header.column.columnDef.headerIcon}
                      {flexRender(
                        header.column.columnDef.header,
                        header.getContext(),
                      )}
                      {header.column.getCanResize() && (
                        <div
                          onMouseDown={header.getResizeHandler()}
                          onTouchStart={header.getResizeHandler()}
                          className={`resizer ${header.column.getIsResizing() ? 'isResizing' : ''}`}
                          style={{
                            position: 'absolute',
                            right: 0,
                            top: 0,
                            height: '100%',
                            width: '5px',
                            background: header.column.getIsResizing()
                              ? 'rgba(59, 130, 246, 0.5)'
                              : 'transparent',
                            cursor: 'col-resize',
                            userSelect: 'none',
                            touchAction: 'none',
                            transition: 'background 0.15s ease',
                            zIndex: 1,
                          }}
                          onMouseEnter={(e) => {
                            if (!header.column.getIsResizing()) {
                              e.currentTarget.style.background =
                                'rgba(55, 53, 47, 0.16)'
                            }
                          }}
                          onMouseLeave={(e) => {
                            if (!header.column.getIsResizing()) {
                              e.currentTarget.style.background = 'transparent'
                            }
                          }}
                        />
                      )}
                    </div>
                  )),
                )}
              </div>

              {isLoading ? (
                <Box
                  sx={{
                    p: 3,
                    display: 'flex',
                    alignItems: 'center',
                    gap: 1.5,
                    color: 'rgba(55, 53, 47, 0.65)',
                  }}
                >
                  <CircularProgress
                    size={16}
                    sx={{color: 'rgba(55, 53, 47, 0.4)'}}
                  />
                  <span style={{fontSize: '14px'}}>Đang tải...</span>
                </Box>
              ) : (
                table.getRowModel().rows.map((row) => (
                  <div
                    key={row.id}
                    className="MuiTableRow-root"
                    style={{
                      display: 'flex',
                      borderBottom: '1px solid rgba(55, 53, 47, 0.09)',
                      minHeight: 40,
                      alignItems: 'center',
                      background: '#fff',
                      transition: 'background 0.1s ease',
                    }}
                    onMouseEnter={(e) => {
                      e.currentTarget.style.background =
                        'rgba(55, 53, 47, 0.03)'
                    }}
                    onMouseLeave={(e) => {
                      e.currentTarget.style.background = '#fff'
                    }}
                  >
                    {row.getVisibleCells().map((cell, idx) => (
                      <div
                        key={cell.id}
                        style={{
                          padding: '8px 12px',
                          display: 'flex',
                          alignItems: 'center',
                          width: cell.column.getSize(),
                          flexShrink: 0,
                          borderRight:
                            idx < row.getVisibleCells().length - 1
                              ? '1px solid rgba(55, 53, 47, 0.09)'
                              : 'none',
                        }}
                      >
                        {flexRender(
                          cell.column.columnDef.cell,
                          cell.getContext(),
                        )}
                      </div>
                    ))}
                  </div>
                ))
              )}
            </div>

            <FormDialog
              open={openFilterDialog}
              onCancel={() => setOpenFilterDialog(false)}
              onConfirm={(formData) => {
                setDataFilterDialog(formData)
                setOpenFilterDialog(false)
              }}
              title="Filter OKR"
              actionName="Filter"
              isLoading={isLoading}
              dialogContent={<OkrFilter data={dataFilterDialog} />}
            />

            <CustomDialog
              open={openAddDetailDialog}
              onCancel={() => setOpenAddDetailDialog(false)}
              title="New Task"
              viewDialog
              showCloseButton
              maxWidth="lg"
              actionName="Submit"
              dialogContent={
                <CreateOKR
                  onClose={() => {
                    setOpenAddDetailDialog(false)
                    refetch()
                  }}
                />
              }
            />
          </Box>
        </Paper>

        <Box
          onClick={() => {
            setDetailOpen(false)
            setOkrDetailData(null)
            refetch()
          }}
          sx={{
            position: 'fixed',
            inset: 0,
            right: `${detailOpen ? paneWidth : 0}px`,
            bgcolor: detailOpen ? 'rgba(15, 15, 15, 0.1)' : 'transparent',
            transition: 'background-color .2s ease',
            pointerEvents: detailOpen ? 'auto' : 'none',
            zIndex: 1200,
          }}
        />
        <Box
          sx={{
            position: 'fixed',
            top: 0,
            right: 0,
            height: '100vh',
            width: `${paneWidth}px`,
            bgcolor: '#fff',
            borderLeft: '1px solid rgba(55, 53, 47, 0.09)',
            zIndex: 1300,
            display: 'flex',
            flexDirection: 'column',
            boxShadow: '-2px 0 8px rgba(15, 15, 15, 0.05)',
            minWidth: 0,
            transform: `translateX(${detailOpen ? '0' : '100%'})`,
            transition: 'transform .2s ease',
          }}
        >
          <Box
            role="separator"
            aria-label="Resize side pane"
            onMouseDown={(e) => {
              e.preventDefault()
              dragState.current = {startX: e.clientX, startW: paneWidth}
              setDragging(true)
              document.body.style.cursor = 'ew-resize'
              document.body.style.userSelect = 'none'
            }}
            sx={{
              position: 'absolute',
              left: 0,
              top: 0,
              bottom: 0,
              width: 8,
              cursor: 'col-resize',
              zIndex: 1301,
              '&::after': {
                content: '""',
                position: 'absolute',
                left: 3,
                top: '50%',
                transform: 'translateY(-50%)',
                width: 2,
                height: 48,
                borderRadius: 1,
                backgroundColor: 'rgba(55, 53, 47, 0.16)',
              },
              '&:hover::after': {backgroundColor: 'rgba(55, 53, 47, 0.3)'},
            }}
          />
          <Box sx={{p: 1.5}}>
            <IconButton
              onClick={() => {
                setDetailOpen(false)
                setOkrDetailData(null)
                refetch()
              }}
              title="Close"
              size="small"
              sx={{
                '&:hover': {
                  bgcolor: 'rgba(55, 53, 47, 0.08)',
                },
              }}
            >
              <CloseRoundedIcon
                sx={{fontSize: 20, color: 'rgba(55, 53, 47, 0.5)'}}
              />
            </IconButton>
          </Box>
          <Box sx={{flex: 1, overflow: 'auto', minHeight: 0}}>
            <OkrDetail data={okrDetailData} />
          </Box>
        </Box>

        <CustomDialog
          open={openProgressDialog}
          onCancel={() => {
            handleProgressDialogClose()
          }}
          title="Cập nhật tiến độ"
          viewDialog
          showCloseButton
          maxWidth="md"
          dialogContent={
            progressRow ? (
              <OkrProgress
                data={{
                  id: progressRow.id || progressRow.Id,
                  title: progressRow.title || progressRow.Title,
                  achieved: progressRow.achieved ?? progressRow.Achieved ?? 0,
                  targetNumber:
                    progressRow.targetNumber ?? progressRow.TargetNumber ?? 0,
                }}
                onDialogClose={handleProgressDialogClose}
              />
            ) : null
          }
        />
      </>
    )
  },
)

export default OkrTab
