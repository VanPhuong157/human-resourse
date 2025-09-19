'use client'

import React, {
  useEffect,
  useRef,
  useState,
  useMemo,
  forwardRef,
  useImperativeHandle,
} from 'react'
import { AgGridReact } from 'ag-grid-react'
import 'ag-grid-enterprise'
import 'ag-grid-community/styles/ag-grid.css'
import 'ag-grid-community/styles/ag-theme-alpine.css'

import CustomDialog from '../../../components/customDialog'
import OkrDetail from './okrDetail/OkrDetail'
import useGetOkrs from '../requests/getOkrs'
import FormDialog from '../../../components/formDialog/FormDialog'
import OkrFilter from './filter/OkrFilter'
import CreateOKR from './create/CreateOKR'

import { Paper, Chip, LinearProgress, Typography } from '@mui/material'
import { Box } from '@mui/system'
import InsertDriveFileIcon from '@mui/icons-material/InsertDriveFile'
import {
  PRIORITY_COLORS,
  STATUS_COLORS,
  getChipColors,
  nameColors,
} from '../../../components/color/chipColors'

// ===== helper: text ellipsis =====
const EllipsisText = ({ value, valueFormatted }) => {
  const text = valueFormatted ?? value ?? ''
  return (
    <span
      title={text}
      style={{
        display: 'block',
        overflow: 'hidden',
        textOverflow: 'ellipsis',
        whiteSpace: 'nowrap',
        minWidth: 0,
        lineHeight: '1.4',
        padding: '1px 0',
      }}
    >
      {text}
    </span>
  )
}

// ===== progress cell =====
const ProgressCell = ({ value }) => {
  const v = Math.min(Math.max(value ?? 0, 0), 100)
  return (
    <Box
      sx={{
        display: 'flex',
        alignItems: 'center',
        gap: 1.5,
        mt: '4px',
        minWidth: 0,
      }}
    >
      <Box sx={{ width: 128, flexShrink: 0 }}>
        <LinearProgress
          variant="determinate"
          value={v}
          sx={{
            height: 10,
            borderRadius: 6,
            bgcolor: '#E5E7EB',
            '& .MuiLinearProgress-bar': { bgcolor: '#1976d2', borderRadius: 6 },
          }}
        />
      </Box>
      <Typography variant="body2" color="text.secondary" sx={{ minWidth: 36 }}>
        {`${Math.round(v)}%`}
      </Typography>
    </Box>
  )
}

// ===== pastel tag =====
const NameTag = ({ label }) => {
  const { bg, fg } = nameColors(label || '')
  return (
    <span
      title={label}
      style={{
        display: 'inline-flex',
        alignItems: 'center',
        padding: '2px 8px',
        borderRadius: 999,
        fontSize: 13,
        fontWeight: 500,
        background: bg,
        color: fg,
        marginRight: 6,
        marginBottom: 4,
        maxWidth: '100%',
      }}
    >
      <span
        style={{
          overflow: 'hidden',
          textOverflow: 'ellipsis',
          whiteSpace: 'nowrap',
        }}
      >
        {label}
      </span>
    </span>
  )
}

const NamesCell = ({ value }) => {
  const list = Array.isArray(value)
    ? value
    : String(value || '')
        .split(',')
        .map((s) => s.trim())
        .filter(Boolean)
  return (
    <div
      style={{
        display: 'flex',
        flexWrap: 'wrap',
        alignItems: 'center',
        minWidth: 0,
      }}
    >
      {list.map((name, i) => (
        <NameTag key={`${name}-${i}`} label={name} />
      ))}
    </div>
  )
}

// ===== Chip status =====
const StatusChip = ({ value }) => {
  const { bg, fg } = getChipColors(STATUS_COLORS, value)
  return (
    <Box
      component="span"
      sx={{
        display: 'inline-flex',
        alignItems: 'center',
        gap: 1,
        px: 1.25,
        py: 0.25,
        borderRadius: 999,
        bgcolor: bg,
        color: fg,
        fontSize: 14,
        fontWeight: 500,
        lineHeight: 1.3,
        maxWidth: '100%',
      }}
      title={value || ''}
    >
      <Box
        component="span"
        sx={{
          width: 8,
          height: 8,
          borderRadius: '50%',
          bgcolor: fg,
          flex: '0 0 8px',
        }}
      />
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

const MS_PER_DAY = 24 * 60 * 60 * 1000
const startOfDay = (d) => new Date(d.getFullYear(), d.getMonth(), d.getDate())

const OkrTab = forwardRef(
  ({ quickFilter = {}, selectedDepartment = 'All Department' }, ref) => {
    // ----- SIDE PANE -----
    const [detailOpen, setDetailOpen] = useState(false)
    const [okrDetailData, setOkrDetailData] = useState(null)

    const [openFilterDialog, setOpenFilterDialog] = useState(false)
    const [dataFilterDialog, setDataFilterDialog] = useState({
      title: '',
      type: '',
      scope: '',
      status: '',
      cycle: '',
    })
    const [openAddDetailDialog, setOpenAddDetailDialog] = useState(false)

    const departmentId =
      typeof window !== 'undefined'
        ? localStorage.getItem('departmentId')
        : null
    const { data, refetch, isLoading } = useGetOkrs(
      1,
      100,
      departmentId,
      dataFilterDialog,
    )

    useImperativeHandle(ref, () => ({
      openFilter: () => setOpenFilterDialog(true),
      openAdd: () => setOpenAddDetailDialog(true),
    }))

    // khi đổi tab status → bơm vào filter để gọi BE
    useEffect(() => {
      setDataFilterDialog((d) => ({ ...d, status: quickFilter?.status || '' }))
    }, [quickFilter?.status])

    // gọi lại API khi department hoặc filter dialog đổi
    useEffect(() => {
      refetch()
    }, [selectedDepartment, dataFilterDialog, refetch])

    // mở/đóng side pane
    const handleNameClick = (event) => {
      setOkrDetailData({ id: event.data.id }) // đủ id để OkrDetail tự fetch
      setDetailOpen(true)
    }
    const handleDetailClose = () => {
      setDetailOpen(false)
      setOkrDetailData(null)
      refetch()
    }

    const handleConfirmFilterDialog = (formData) => {
      setDataFilterDialog(formData)
      setOpenFilterDialog(false)
    }
    const handleCancelFilterDialog = () => setOpenFilterDialog(false)
    const handleAddDetailDialogCancel = () => setOpenAddDetailDialog(false)
    const handleAddDetailDialogSubmit = () => {
      setOpenAddDetailDialog(false)
      refetch()
    }

    // ===== map + tính field =====
    const rows = useMemo(() => {
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

          const ownersList = Array.isArray(ownersArr)
            ? ownersArr.filter(Boolean)
            : String(ownersArr ?? '')
                .split(',')
                .map((s) => s.trim())
                .filter(Boolean)
          const managersList = Array.isArray(managersArr)
            ? managersArr.filter(Boolean)
            : String(managersArr ?? '')
                .split(',')
                .map((s) => s.trim())
                .filter(Boolean)
          const departmentTags = Array.isArray(departmentsArr)
            ? departmentsArr.filter(Boolean)
            : String(departmentsArr ?? '')
                .split(',')
                .map((s) => s.trim())
                .filter(Boolean)
          const companyTags = Array.isArray(it.company)
            ? it.company.filter(Boolean)
            : String(it.company ?? '')
                .split(',')
                .map((s) => s.trim())
                .filter(Boolean)

          const dueRaw = it.dueDate ?? it.DueDate ?? null
          const due = dueRaw ? new Date(dueRaw) : null
          const dueDay = due ? startOfDay(due) : null

          let remainingDays = null
          let overdue = null
          let computedStatus = it.status || ''
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
            assignees: ownersList,
            managerTags: managersList,
            departmentName: departments,
            departmentTags,
            companyTags,
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

    // ===== Name (group) column =====
    const autoGroupColumnDef = {
      headerName: 'Name',
      width: 250,
      minWidth: 100,
      cellRenderer: 'agGroupCellRenderer',
      suppressHeaderMenuButton: true,
      tooltipValueGetter: (p) => p?.data?.title || p?.value || '',
      cellRendererParams: {
        suppressCount: true,
        innerRenderer: (p) => {
          const title = p?.data?.title || p?.value || ''
          return (
            <span
              style={{
                display: 'flex',
                alignItems: 'center',
                gap: 8,
                minWidth: 0,
                flex: 1,
              }}
            >
              <InsertDriveFileIcon fontSize="small" sx={{ color: '#6B7280' }} />
              <span
                title={title}
                style={{
                  color: '#186BFF',
                  fontWeight: 500,
                  lineHeight: 1.3,
                  whiteSpace: 'nowrap',
                  overflow: 'hidden',
                  textOverflow: 'ellipsis',
                  minWidth: 0,
                  display: 'block',
                }}
              >
                {title}
              </span>
            </span>
          )
        },
      },
    }

    // ===== columns =====
    const columnDefs = [
      {
        field: 'assignees',
        headerName: 'Người thực hiện',
        width: 220,
        minWidth: 160,
        cellRenderer: NamesCell,
      },
      {
        field: 'departmentTags',
        headerName: 'Phòng Ban',
        width: 220,
        minWidth: 160,
        cellRenderer: NamesCell,
      },
      {
        field: 'managerTags',
        headerName: 'Người quản lý',
        width: 220,
        minWidth: 160,
        cellRenderer: NamesCell,
      },

      {
        field: 'status',
        headerName: 'Status',
        width: 120,
        cellRenderer: ({ value }) => <StatusChip value={value} />,
      },

      {
        field: 'note',
        headerName: 'Note',
        flex: 2,
        minWidth: 120,
        cellRenderer: EllipsisText,
      },

      {
        field: 'dueDate',
        headerName: 'Due date',
        width: 120,
        valueFormatter: (p) =>
          p.value
            ? new Date(p.value).toLocaleDateString('vi-VN', {
                day: '2-digit',
                month: '2-digit',
                year: 'numeric',
              })
            : '',
        cellRenderer: EllipsisText,
      },
      {
        field: 'priority',
        headerName: 'Priority',
        width: 120,
        cellRenderer: ({ value }) => {
          const { bg, fg } = getChipColors(PRIORITY_COLORS, value)
          return (
            <Chip
              size="small"
              label={value || ''}
              sx={{
                bgcolor: bg,
                color: fg,
                fontSize: 14,
                '& .MuiChip-label': {
                  lineHeight: 1.3,
                  overflow: 'hidden',
                  textOverflow: 'ellipsis',
                  whiteSpace: 'nowrap',
                },
              }}
            />
          )
        },
      },
      {
        field: 'progress',
        headerName: 'Progress',
        width: 220,
        cellRenderer: ProgressCell,
      },

      {
        field: 'remainingDays',
        headerName: 'Tiến độ còn lại',
        width: 160,
        valueFormatter: (p) => {
          const v = p.value
          if (v === null || v === undefined) return ''
          if (v > 0) return `${v} ngày còn lại`
          if (v === 0) return 'Hôm nay'
          return `${Math.abs(v)} ngày quá hạn`
        },
        cellRenderer: (p) => {
          const text = p.valueFormatted ?? ''
          const isOverdue = typeof p.value === 'number' && p.value < 0
          return (
            <span style={{ color: isOverdue ? '#dc2626' : undefined }}>
              {text}
            </span>
          )
        },
      },

      {
        field: 'companyTags',
        headerName: 'Công ty',
        width: 160,
        minWidth: 120,
        cellRenderer: NamesCell,
      },

      {
        field: 'overdue',
        headerName: 'Quá hạn',
        width: 120,
        valueFormatter: (p) => {
          if (p.value == null) return ''
          const v = Number(p.value)
          return v > 0 ? `${v} ngày quá hạn` : ''
        },
        cellRenderer: (p) =>
          p.value > 0 ? (
            <span style={{ color: '#dc2626' }}>{p.valueFormatted}</span>
          ) : (
            <span>{p.valueFormatted}</span>
          ),
      },

      {
        field: 'createdTime',
        headerName: 'Created time',
        width: 160,
        valueFormatter: (p) => (p.value ? p.value : ''), // BE trả dd/MM/yyyy → giữ nguyên
        cellRenderer: EllipsisText,
      },
      {
        field: 'lastUpdate',
        headerName: 'Last update',
        width: 160,
        valueFormatter: (p) =>
          p.value
            ? new Date(p.value).toLocaleDateString('vi-VN', {
                day: '2-digit',
                month: '2-digit',
                year: 'numeric',
              })
            : '',
        cellRenderer: EllipsisText,
      },
    ]

    // ===== defaultColDef =====
    const defaultColDef = {
      minWidth: 100,
      sortable: false,
      filter: false,
      resizable: true,
      wrapHeaderText: true,
      autoHeaderHeight: true,
      suppressHeaderMenuButton: true,
      suppressMenu: true,
      cellStyle: {
        display: 'flex',
        alignItems: 'center',
        padding: '0 12px',
        fontSize: '14px',
      },
    }

    const getDataPath = ({ id, parentId }) => {
      const result = [id]
      let row = rows.find((r) => r.id === parentId)
      while (row) {
        result.unshift(row.id)
        row = rows.find((r) => r.id === row.parentId)
      }
      return result
    }

    const gridRef = useRef()

    const gridKey = `${selectedDepartment}|${
      dataFilterDialog.status || 'all'
    }|${quickFilter?.company || ''}|${quickFilter?.overdue ? 1 : 0}`

    return (
      <Paper
        elevation={0}
        sx={{ height: '100vh', display: 'flex', flexDirection: 'column' }}
      >
        <Box
          sx={{
            flex: 1,
            minHeight: 0,
            overflow: 'hidden', // tránh scrollbar bọc ngoài
            boxSizing: 'border-box',

            '& .ag-theme-alpine': {
              '--ag-font-size': '14px',
              '--ag-row-height': '52px',
              '--ag-header-height': '44px',
              '--ag-background-color': '#fff',
              '--ag-odd-row-background-color': '#fff',
              '--ag-row-border-color': '#E5E7EB',
              '--ag-header-foreground-color': '#111827',
              '--ag-header-background-color': '#F9FAFB',
            },

            // Ẩn nút 3 chấm menu ở header + để chữ header normal
            '& .ag-theme-alpine .ag-header-cell-menu-button, .ag-theme-alpine .ag-header-cell .ag-icon-menu':
              {
                display: 'none !important',
              },
            '& .ag-theme-alpine .ag-header-cell-text': { fontWeight: 400 },

            '& .ag-theme-alpine .ag-cell': {
              display: 'flex',
              alignItems: 'center',
              lineHeight: 'normal',
              fontSize: '14px',
              minWidth: 0,
            },
            '& .ag-cell-wrapper': {
              display: 'flex',
              alignItems: 'center',
              height: '100%',
              minWidth: 0,
              paddingTop: 0,
              paddingBottom: 0,
            },
            '& .ag-group-cell .ag-group-value': {
              flex: '1 1 auto',
              minWidth: 0,
              display: 'flex',
              alignItems: 'center',
            },
            '& .ag-group-expanded, & .ag-group-contracted': {
              display: 'inline-flex',
              alignItems: 'center',
              height: '100%',
              lineHeight: 1,
            },
            '& .ag-group-expanded.ag-hidden, & .ag-group-contracted.ag-hidden':
              {
                display: 'none !important',
              },
          }}
        >
          {/* Layout 2 cột: trái = bảng, phải = pane chi tiết (cả hai cao 100%) */}
          <div
            style={{
              display: 'grid',
              gridTemplateColumns: detailOpen ? '1fr 720px' : '1fr 0px',
              transition: 'grid-template-columns .25s ease',
              height: '100%', // chiếm toàn bộ 100vh của Paper
              width: '100%',
              boxSizing: 'border-box',
              minWidth: 0,
            }}
          >
            {/* Cột trái: Bảng */}
            <div style={{ minWidth: 0, height: '100%' }}>
              <div className="ag-theme-alpine" style={{ height: '100%', width: '100%' }}>
                <AgGridReact
                  key={gridKey}
                  ref={gridRef}
                  rowData={rows}
                  treeData
                  animateRows
                  autoGroupColumnDef={autoGroupColumnDef}
                  columnDefs={columnDefs}
                  defaultColDef={defaultColDef}
                  getDataPath={getDataPath}
                  getRowId={(p) => p.data?.id ?? p.data?.Id}
                  rowHeight={52}
                  headerHeight={44}
                  groupHeaderHeight={44}
                  pagination={false}
                  onRowClicked={handleNameClick}
                />
              </div>
            </div>

            {/* Cột phải: Pane chi tiết */}
            <div
              style={{
                borderLeft: '1px solid #E5E7EB',
                height: '100%',
                minHeight: 0,
                overflow: 'auto',
                background: '#fff',
              }}
            >
              {detailOpen && (
                <div style={{ height: '100%', display: 'flex', flexDirection: 'column', minHeight: 0 }}>
                  {/* Thanh tiêu đề nhỏ để đóng */}
                  <div style={{ padding: 8, borderBottom: '1px solid #F3F4F6' }}>
                    <button
                      onClick={handleDetailClose}
                      style={{
                        border: 'none',
                        background: 'transparent',
                        cursor: 'pointer',
                        fontSize: 14,
                        color: '#6B7280',
                      }}
                      title="Close"
                    >
                      ✕ Close
                    </button>
                  </div>
                  <div style={{ flex: 1, overflow: 'auto', minHeight: 0 }}>
                    <OkrDetail data={okrDetailData} />
                  </div>
                </div>
              )}
            </div>
          </div>

          {/* Các dialog khác */}
          <FormDialog
            open={openFilterDialog}
            onCancel={handleCancelFilterDialog}
            onConfirm={handleConfirmFilterDialog}
            title="Filter OKR Request"
            actionName="Filter"
            isLoading={isLoading}
            dialogContent={<OkrFilter data={dataFilterDialog} />}
          />

          <CustomDialog
            open={openAddDetailDialog}
            onCancel={handleAddDetailDialogCancel}
            title="Add OKR"
            viewDialog
            showCloseButton
            maxWidth="lg"
            actionName="Submit"
            dialogContent={<CreateOKR onClose={() => handleAddDetailDialogSubmit()} />}
          />
        </Box>
      </Paper>
    )
  },
)

export default OkrTab
