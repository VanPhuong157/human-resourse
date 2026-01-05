// 'use client'

import React, {useCallback, useEffect, useMemo, useRef, useState} from 'react'
import {
  Box,
  Stack,
  Typography,
  TextField,
  InputAdornment,
  LinearProgress,
  Chip,
  Tooltip,
} from '@mui/material'
import SearchIcon from '@mui/icons-material/Search'
import {AgGridReact} from 'ag-grid-react'
import 'ag-grid-enterprise'
import 'ag-grid-community/styles/ag-grid.css'
import 'ag-grid-community/styles/ag-theme-alpine.css'

import {LocalizationProvider, DesktopDatePicker} from '@mui/x-date-pickers'
import {AdapterDateFns} from '@mui/x-date-pickers/AdapterDateFns'

import path from '../../../api/path'
import {rootApi} from '../../../api/rootApi'
import MultiSelectEditor from '../../okr/editor/MultiSelectEditor'
import OkrRequestDetails from '../../okr/requestTab/details/OkrRequestDetails'

/* small helpers */
const unwrap = (r) => r?.data ?? r?.Data ?? r
const statusColor = (s) =>
  s === 'Submitted'
    ? 'warning'
    : s === 'Approved'
      ? 'success'
      : s === 'Rejected'
        ? 'error'
        : 'default'
const fmtDate = (v) => (v ? new Date(v).toLocaleDateString('vi-VN') : '')

/* Date picker cell */
function DateCell({rowData, field, onSave}) {
  const anchorRef = useRef(null)
  const [open, setOpen] = useState(false)
  const [value, setValue] = useState(() =>
    rowData?.[field] ? new Date(rowData[field]) : null,
  )
  return (
    <Box
      ref={anchorRef}
      onClick={(e) => {
        e.stopPropagation()
        setOpen(true)
      }}
      tabIndex={0}
      sx={{
        width: '100%',
        height: '100%',
        display: 'flex',
        alignItems: 'center',
        px: 1,
        cursor: 'pointer',
      }}
    >
      <Typography variant="body2">
        {value ? value.toLocaleDateString('vi-VN') : ''}
      </Typography>
      <LocalizationProvider dateAdapter={AdapterDateFns}>
        <DesktopDatePicker
          open={open}
          onClose={() => setOpen(false)}
          value={value}
          onChange={async (nv) => {
            setValue(nv)
            await onSave?.(
              nv
                ? `${nv.getFullYear()}-${String(nv.getMonth() + 1).padStart(2, '0')}-${String(nv.getDate()).padStart(2, '0')}`
                : null,
            )
            setOpen(false)
          }}
          slotProps={{
            textField: {sx: {display: 'none'}},
            popper: {anchorEl: anchorRef.current},
          }}
        />
      </LocalizationProvider>
    </Box>
  )
}

export default function OnePageWorkflow() {
  const gridRef = useRef(null)
  const [loading, setLoading] = useState(false)
  const [search, setSearch] = useState('')
  const [rows, setRows] = useState([])

  const [drawerOpen, setDrawerOpen] = useState(false)
  const [drawerStep, setDrawerStep] = useState(null)

  // load list, dùng search hiện tại
  const loadSteps = useCallback(async () => {
    setLoading(true)
    try {
      let url = path.workflow.getSteps
      if (typeof url === 'function') {
        url = url(1, 200, {q: search || undefined})
      }
      const {data} = await rootApi.get(url)
      const items = unwrap(data)?.items ?? unwrap(data) ?? []

      // chuẩn hóa + lấy parentId để build tree
      const normalized = items.map((it) => ({
        id: it.id ?? it.Id,
        parentId: it.parentId ?? it.ParentId ?? null, // field cha
        code: it.code ?? it.Code,
        title: it.title ?? it.Title,
        content:
          it.content ?? it.Content ?? it.description ?? it.Description ?? '',
        executors: it.executors ?? it.Executors ?? [],
        reviewers: it.reviewers ?? it.Reviewers ?? [],
        approvers: it.approvers ?? it.Approvers ?? [],
        executorDepartments:
          it.executorDepartments ?? it.ExecutorDepartments ?? null,
        reviewerDepartments:
          it.reviewerDepartments ?? it.ReviewerDepartments ?? null,
        approverDepartments:
          it.approverDepartments ?? it.ApproverDepartments ?? null,
        departments: it.departments ?? it.Departments ?? [],
        execDate: it.execDate ?? it.ExecDate ?? null,
        reviewDate: it.reviewDate ?? it.ReviewDate ?? null,
        approveDate: it.approveDate ?? it.ApproveDate ?? null,
        lawRef: it.lawRef ?? it.LawRef ?? '',
        documents: it.documents ?? it.Documents ?? [],
        latestSubmission: it.latestSubmission ?? it.LatestSubmission ?? null,
      }))

      // build đường dẫn tree [cha, con, ...]
      const byId = new Map(normalized.map((r) => [r.id, r]))
      normalized.forEach((r) => {
        const pathArr = []
        let cur = r
        while (cur) {
          pathArr.unshift(cur.title)
          cur = cur.parentId ? byId.get(cur.parentId) : null
        }
        r._treePath = pathArr.length ? pathArr : [r.title]
      })

      setRows(normalized)
    } finally {
      setLoading(false)
    }
  }, [search])

  // load lần đầu
  useEffect(() => {
    loadSteps()
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])

  const fetchMixedOptions = useCallback(async () => {
    const uRes = await rootApi.get(path.user.getUser(1, 100, undefined))
    const users = (unwrap(uRes?.data)?.items ?? unwrap(uRes?.data) ?? []).map(
      (u) => ({
        value: `u:${u.id ?? u.userId ?? u.Id ?? u.UserId}`,
        label:
          u.userInformation?.fullName ??
          u.fullName ??
          u.name ??
          u.email ??
          String(u.id ?? ''),
      }),
    )

    let dList = []
    try {
      if (path.department.getDepartmentFilter) {
        const dRes = await rootApi.get(
          path.department.getDepartmentFilter(1, 100, undefined),
        )
        dList = unwrap(dRes?.data)?.items ?? unwrap(dRes?.data) ?? []
      } else {
        const dRes = await rootApi.get(path.department.getDepartments())
        dList = unwrap(dRes?.data)?.items ?? unwrap(dRes?.data) ?? []
      }
    } catch {
      dList = []
    }
    const depts = dList.map((d) => ({
      value: `d:${d.id ?? d.Id}`,
      label: d.name ?? d.Name ?? d.code ?? String(d.id ?? ''),
    }))
    return [...users, ...depts]
  }, [])

  const patchStep = useCallback(async (id, payload) => {
    const url = path.workflow.updateStep?.(id) ?? `workflow/steps/${id}`
    await rootApi.put(url, payload)
  }, [])

  const buildMixedFrom = (people = [], departments = []) => {
    const userIds = people
      .map((x) => x.userId ?? x.UserId ?? x.id ?? x.Id)
      .filter(Boolean)
    const userNames = people
      .map((x) => x.name ?? x.Name ?? x.fullName ?? x.FullName ?? '')
      .filter(Boolean)
    const deptIds = departments.map((x) => x.id ?? x.Id).filter(Boolean)
    const deptNames = departments
      .map((x) => x.name ?? x.Name ?? '')
      .filter(Boolean)
    return {
      mixedIds: [
        ...userIds.map((id) => `u:${id}`),
        ...deptIds.map((id) => `d:${id}`),
      ],
      mixedNames: [...userNames, ...deptNames],
    }
  }

  // update 1 row trong state (không reload list) – dùng cho ngày, content, PL
  const updateRowLocal = useCallback((id, changes) => {
    setRows((prev) => prev.map((r) => (r.id === id ? {...r, ...changes} : r)))
  }, [])

  const columnDefs = useMemo(() => {
    const mixedEditor = (role) => (p) => {
      const cfg = {
        executor: {
          role: 'executor',
          placeholder: 'Chọn người / phòng ban thực hiện',
        },
        reviewer: {
          role: 'reviewer',
          placeholder: 'Chọn người / phòng ban thẩm định',
        },
        approver: {
          role: 'approver',
          placeholder: 'Chọn người / phòng ban phê duyệt',
        },
      }[role]

      const people =
        role === 'executor'
          ? p.data.executors
          : role === 'reviewer'
            ? p.data.reviewers
            : p.data.approvers
      const deptByRole =
        role === 'executor'
          ? p.data.executorDepartments ?? p.data.departments
          : role === 'reviewer'
            ? p.data.reviewerDepartments ?? p.data.departments
            : p.data.approverDepartments ?? p.data.departments

      const {mixedIds, mixedNames} = buildMixedFrom(people, deptByRole)

      // tiện hàm lấy userId từ mảng people
      const extractUserIds = (arr = []) =>
        arr.map((x) => x.userId ?? x.UserId ?? x.id ?? x.Id).filter(Boolean)

      return (
        <MultiSelectEditor
          rowData={{...p.data, mixedIds, mixedNames}}
          idsKey="mixedIds"
          namesKey="mixedNames"
          placeholder={cfg.placeholder}
          fetchOptions={fetchMixedOptions}
          onSaveIds={async (encodedIds, row, changed) => {
            const opts = await fetchMixedOptions()

            const userIdsSelected = encodedIds
              .filter((x) => String(x).startsWith('u:'))
              .map((x) => String(x).slice(2))

            const deptIdsRaw = encodedIds
              .filter((x) => String(x).startsWith('d:'))
              .map((x) => String(x).slice(2))

            const validDeptIds = new Set(
              opts
                .filter((o) => String(o.value).startsWith('d:'))
                .map((o) => String(o.value).slice(2)),
            )
            const departmentIds = deptIdsRaw.filter((id) =>
              validDeptIds.has(id),
            )

            // --- LẤY USER HIỆN TẠI CỦA CẢ 3 VAI TRÒ ---
            let executorIds = extractUserIds(row.executors)
            let reviewerIds = extractUserIds(row.reviewers)
            let approverIds = extractUserIds(row.approvers)

            // --- GÁN USER MỚI CHO VAI TRÒ ĐANG SỬA ---
            if (role === 'executor') {
              executorIds = userIdsSelected
              reviewerIds = reviewerIds.filter(
                (id) => !executorIds.includes(id),
              )
              approverIds = approverIds.filter(
                (id) => !executorIds.includes(id),
              )
            } else if (role === 'reviewer') {
              reviewerIds = userIdsSelected
              executorIds = executorIds.filter(
                (id) => !reviewerIds.includes(id),
              )
              approverIds = approverIds.filter(
                (id) => !reviewerIds.includes(id),
              )
            } else if (role === 'approver') {
              approverIds = userIdsSelected
              executorIds = executorIds.filter(
                (id) => !approverIds.includes(id),
              )
              reviewerIds = reviewerIds.filter(
                (id) => !approverIds.includes(id),
              )
            }

            // remove trùng lần nữa cho chắc
            const toSet = (arr) => Array.from(new Set(arr))
            executorIds = toSet(executorIds)
            reviewerIds = toSet(reviewerIds)
            approverIds = toSet(approverIds)

            // payload luôn gửi đủ 3 role để BE clear đúng vai trò
            const payload = {
              executorIds,
              reviewerIds,
              approverIds,
            }
            // phòng ban chỉ update cho vai trò đang sửa
            if (role === 'executor')
              payload.executorDepartmentIds = departmentIds
            if (role === 'reviewer')
              payload.reviewerDepartmentIds = departmentIds
            if (role === 'approver')
              payload.approverDepartmentIds = departmentIds

            await patchStep(row.id, payload)

            // refetch list từ BE – tree vẫn giữ trạng thái nhờ rememberGroupStateWhenNewData
            await loadSteps()
          }}
        />
      )
    }

    return [
      {
        headerName: 'Số TT',
        field: 'code',
        width: 120,

        headerClass: 'ag-center-header',
      },
      {
        headerName: 'Tiến trình',
        field: 'title',
        hide: true,
      },
      {
        headerName: 'Nội dung',
        field: 'content',
        minWidth: 260,
        flex: 1.4,
        editable: true,
        cellEditor: 'agTextCellEditor',
        cellClass: 'ag-cell-wrap-text',
        autoHeight: true,
        headerClass: 'ag-center-header',
      },
      {
        headerName: 'Trách nhiệm / Thời gian thực hiện',
        marryChildren: true,
        headerClass: 'ag-center-header',
        children: [
          {
            headerName: 'Thực hiện',
            marryChildren: true,
            headerClass: 'ag-center-header',
            children: [
              {
                headerName: 'Người thực hiện',
                field: 'executors',
                minWidth: 220,
                autoHeight: true,
                cellRenderer: mixedEditor('executor'),
              },
              {
                headerName: 'Ngày',
                field: 'execDate',
                width: 140,
                headerClass: 'ag-center-header',
                cellRenderer: (p) => (
                  <DateCell
                    rowData={p.data}
                    field="execDate"
                    onSave={async (v) => {
                      await patchStep(p.data.id, {execDate: v})
                      updateRowLocal(p.data.id, {execDate: v})
                    }}
                  />
                ),
              },
            ],
          },
          {
            headerName: 'Kiểm tra, Thẩm định',
            marryChildren: true,
            headerClass: 'ag-center-header',
            children: [
              {
                headerName: 'Người thẩm định',
                field: 'reviewers',
                minWidth: 220,
                autoHeight: true,
                cellRenderer: mixedEditor('reviewer'),
              },
              {
                headerName: 'Ngày',
                field: 'reviewDate',
                width: 140,
                headerClass: 'ag-center-header',
                cellRenderer: (p) => (
                  <DateCell
                    rowData={p.data}
                    field="reviewDate"
                    onSave={async (v) => {
                      await patchStep(p.data.id, {reviewDate: v})
                      updateRowLocal(p.data.id, {reviewDate: v})
                    }}
                  />
                ),
              },
            ],
          },
          {
            headerName: 'Phê duyệt',
            marryChildren: true,
            headerClass: 'ag-center-header',
            children: [
              {
                headerName: 'Người phê duyệt',
                field: 'approvers',
                minWidth: 220,
                autoHeight: true,
                cellRenderer: mixedEditor('approver'),
              },
              {
                headerName: 'Ngày',
                field: 'approveDate',
                width: 140,
                headerClass: 'ag-center-header',
                cellRenderer: (p) => (
                  <DateCell
                    rowData={p.data}
                    field="approveDate"
                    onSave={async (v) => {
                      await patchStep(p.data.id, {approveDate: v})
                      updateRowLocal(p.data.id, {approveDate: v})
                    }}
                  />
                ),
              },
            ],
          },
          {
            headerName: 'theo quy định PL',
            field: 'lawRef',
            minWidth: 220,
            flex: 1,
            editable: true,
            cellEditor: 'agTextCellEditor',
            cellClass: 'ag-cell-wrap-text',
            autoHeight: true,
            headerClass: 'ag-center-header',
          },
        ],
      },
      {
        headerName: 'Hồ sơ / Tài liệu',
        field: 'docs',
        minWidth: 260,
        flex: 1,
        headerClass: 'ag-center-header',
        editable: false,
        autoHeight: true,
        cellRenderer: (p) => {
          const docs = p.data.documents ?? p.data.Documents ?? []
          const files = docs
            .filter((d) => (d.category ?? d.Category) !== 1)
            .map((d) => ({
              id: d.id ?? d.Id,
              name: d.fileName ?? d.FileName,
              url: d.downloadUrl ?? d.DownloadUrl,
            }))
          if (!files.length)
            return <span style={{color: '#999'}}>Chưa có tệp.</span>
          return (
            <div
              style={{
                display: 'flex',
                flexDirection: 'column',
                gap: 6,
                padding: '6px 0',
              }}
            >
              {files.map((f) => (
                <a
                  key={f.id}
                  href={f.url}
                  target="_blank"
                  rel="noreferrer"
                  style={{
                    display: 'inline-block',
                    padding: '2px 8px',
                    borderRadius: 12,
                    textDecoration: 'none',
                    background: '#eef2ff',
                    border: '1px solid #c7d2fe',
                  }}
                >
                  {f.name}
                </a>
              ))}
            </div>
          )
        },
      },
      {
        headerName: 'Văn bản phê duyệt',
        field: 'approvalDoc',
        minWidth: 260,
        flex: 1,
        headerClass: 'ag-center-header',
        editable: false,
        autoHeight: true,
        cellRenderer: (p) => {
          const docs = p.data.documents ?? p.data.Documents ?? []
          const files = docs
            .filter((d) => (d.category ?? d.Category) === 1)
            .map((d) => ({
              id: d.id ?? d.Id,
              name: d.fileName ?? d.FileName,
              url: d.downloadUrl ?? d.DownloadUrl,
            }))
          if (!files.length)
            return <span style={{color: '#999'}}>Chưa có VB phê duyệt.</span>
          return (
            <div
              style={{
                display: 'flex',
                flexDirection: 'column',
                gap: 6,
                padding: '6px 0',
              }}
            >
              {files.map((f) => (
                <a
                  key={f.id}
                  href={f.url}
                  target="_blank"
                  rel="noreferrer"
                  style={{
                    display: 'inline-block',
                    padding: '2px 8px',
                    borderRadius: 12,
                    textDecoration: 'none',
                    background: '#ecfdf5',
                    border: '1px solid #bbf7d0',
                  }}
                >
                  {f.name}
                </a>
              ))}
            </div>
          )
        },
      },
      {
        headerName: 'Latest Status',
        field: 'latestSubmission',
        width: 200,
        headerClass: 'ag-center-header',
        cellRenderer: (p) => {
          const s = p.value
          if (!s) return <Chip size="small" label="—" />
          const tip = (
            <Box sx={{p: 0.5}}>
              <Typography variant="caption">
                Submitted: {fmtDate(s.submittedAt)}
              </Typography>
              <br />
              <Typography variant="caption">
                Approved: {fmtDate(s.approvedAt)}
              </Typography>
              <br />
              <Typography variant="caption">
                Rejected: {fmtDate(s.rejectedAt)}
              </Typography>
            </Box>
          )
          return (
            <Tooltip title={tip}>
              <Chip
                size="small"
                color={statusColor(s.status)}
                label={s.status}
                variant="outlined"
              />
            </Tooltip>
          )
        },
      },
      {
        headerName: 'Last Updated',
        width: 160,
        headerClass: 'ag-center-header',
        valueGetter: (p) =>
          p.data?.latestSubmission?.lastUpdated ||
          p.data?.latestSubmission?.createdAt ||
          null,
        valueFormatter: (p) => fmtDate(p.value),
      },
    ]
  }, [fetchMixedOptions, patchStep, updateRowLocal, loadSteps])

  const defaultColDef = useMemo(
    () => ({
      resizable: true,
      sortable: true,
      filter: true,
      editable: false,
      headerClass: 'ag-center-header',
      cellStyle: {display: 'flex', alignItems: 'center'},
    }),
    [],
  )

  const autoGroupColumnDef = useMemo(
    () => ({
      headerName: 'Tiến trình',
      minWidth: 280,
      flex: 1.2,
      cellClass: 'ag-cell-wrap-text',
      autoHeight: true,
      pinned: 'left',
      headerClass: 'ag-center-header',
      cellRendererParams: {
        suppressCount: true,
      },
    }),
    [],
  )

  const getRowStyle = useCallback((params) => {
    if (!params.data) return null
    if (!params.data.parentId) {
      return {
        fontWeight: 600,
        backgroundColor: '#f1f1f1',
        color: '#111827',
      }
    }
    return null
  }, [])

  const onCellClicked = useCallback((e) => {
    const editable = new Set([
      'content',
      'executors',
      'reviewers',
      'approvers',
      'execDate',
      'reviewDate',
      'approveDate',
      'lawRef',
      'docs',
      'approvalDoc',
    ])
    if (!e.colDef || editable.has(e.colDef.field)) return
    setDrawerStep(e.data)
    setDrawerOpen(true)
  }, [])

  const onCellValueChanged = useCallback(
    async (e) => {
      const field = e.colDef.field
      if (!['content', 'lawRef'].includes(field)) return
      const id = e.data.id
      const newValue = e.newValue
      await patchStep(id, {[field]: newValue})
      updateRowLocal(id, {[field]: newValue})
    },
    [patchStep, updateRowLocal],
  )

  return (
    <Box
      sx={{
        p: 2,
        height: '100%',
        display: 'flex',
        flexDirection: 'column',
        gap: 1,
      }}
    >
      <Stack
        direction="row"
        alignItems="center"
        justifyContent="space-between"
        sx={{mb: 1}}
      >
        <Typography variant="h6">One-Page Project Workflow</Typography>
        <TextField
          size="small"
          placeholder="Tìm Code/Title"
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          onKeyDown={(e) => e.key === 'Enter' && loadSteps()}
          InputProps={{
            startAdornment: (
              <InputAdornment position="start">
                <SearchIcon />
              </InputAdornment>
            ),
          }}
        />
      </Stack>

      <Box
        className="ag-theme-alpine"
        sx={{
          flex: 1,
          minHeight: 0,
          borderRadius: 2,
          overflow: 'hidden',
          position: 'relative',
        }}
      >
        {loading && (
          <LinearProgress
            sx={{position: 'absolute', top: 0, left: 0, right: 0, zIndex: 2}}
          />
        )}
        <AgGridReact
          ref={gridRef}
          rowData={rows}
          columnDefs={columnDefs}
          defaultColDef={defaultColDef}
          animateRows
          rowHeight={48}
          suppressMovableColumns
          onCellClicked={onCellClicked}
          onCellValueChanged={onCellValueChanged}
          getRowStyle={getRowStyle}
          /* GIỮ TRẠNG THÁI TREE KHI DATA ĐỔI */
          getRowId={(params) => String(params.data.id)}
          rememberGroupStateWhenNewData={true}
          /* TREE MODE */
          treeData
          getDataPath={(data) => data._treePath || [data.title]}
          groupDefaultExpanded={0}
          autoGroupColumnDef={autoGroupColumnDef}
        />
      </Box>

      <OkrRequestDetails
        open={drawerOpen}
        step={drawerStep}
        onClose={() => setDrawerOpen(false)}
        onReloadSteps={loadSteps}
      />
    </Box>
  )
}
