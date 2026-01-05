// 'use client'
import React, {useRef, useState, useEffect} from 'react'
import {
  Box,
  TextField,
  CircularProgress,
  ClickAwayListener,
  Popover,
  Typography,
  Chip,
} from '@mui/material'
import Autocomplete, {createFilterOptions} from '@mui/material/Autocomplete'
import CloseRoundedIcon from '@mui/icons-material/CloseRounded'
import {showError, showToastSuccess} from '../../../components/notification'
import {nameColors} from '../../../components/color/chipColors'

/** ===== Size ===== */
const POPOVER_W = 300
const POPOVER_H = 540
const HEADER_H = 40

/** ===== Helpers ===== */
const norm = (o) =>
  typeof o === 'string'
    ? {value: o, label: o}
    : {
        value: o?.value ?? o?.id ?? o,
        label: o?.label ?? o?.name ?? String(o?.value ?? o?.id ?? o),
      }

const PillChip = ({label, onDelete}) => {
  const {bg, fg} = nameColors(label || '')
  return (
    <Chip
      label={label}
      onDelete={onDelete}
      deleteIcon={<CloseRoundedIcon sx={{fontSize: 14}} />}
      onMouseDown={(e) => e.stopPropagation()} // giữ popover mở khi xóa
      sx={{
        height: 22,
        borderRadius: 1,
        fontWeight: 600,
        fontSize: 12.5,
        lineHeight: 1,
        px: 0.5,
        bgcolor: bg,
        color: fg,
        '& .MuiChip-deleteIcon': {
          color: fg,
          opacity: 0.7,
          '&:hover': {opacity: 1},
        },
      }}
    />
  )
}

const splitKinds = (idsArr=[]) => {
  const s = new Set((idsArr || []).map(String))
  return {
    users: [...s].filter(x => x.startsWith('u:')),
    depts: [...s].filter(x => x.startsWith('d:')),
  }
}
const sameSet = (a=[], b=[]) => {
  if (a.length !== b.length) return false
  const S = new Set(a); for (const x of b) if (!S.has(x)) return false
  return true
}

/** ===== Component ===== */
const MultiSelectEditor = ({
  rowData,
  idsKey,
  namesKey,
  placeholder = 'Chọn…',
  fetchOptions,
  onSaveIds,
  onCreateOption,
  successMsg,
  errorMsg,
  onAfterClose,          // ⬅️ mới: đóng popover thì callback (để loadSteps)
}) => {
  const anchorRef = useRef(null)
  const saveTimer = useRef(null)

  const [open, setOpen] = useState(false)
  const [options, setOptions] = useState([])
  const [loading, setLoading] = useState(false)
  const [saving, setSaving] = useState(false)
  const [selected, setSelected] = useState([]) // [{value,label}]

  // lấy names hiện có (để hiển thị khi chưa mở)
  const currentIds = Array.isArray(rowData?.[idsKey]) ? rowData[idsKey] : []
  const currentNames = Array.isArray(rowData?.[namesKey])
    ? rowData[namesKey]
    : String(rowData?.[namesKey] ?? '')
        .split(',')
        .map((s) => s.trim())
        .filter(Boolean)

  // snapshot ban đầu để so sánh changed
  const initialSplitRef = useRef(splitKinds(currentIds))

  /** ---- load options 1 lần ---- */
  const ensureOptions = async () => {
    if (!options.length && fetchOptions) {
      try {
        setLoading(true)
        const list = (await fetchOptions()) || []
        setOptions(list.map(norm))
      } finally {
        setLoading(false)
      }
    }
  }

  const handleOpen = async () => {
    await ensureOptions()
    setOpen(true)
  }
  const handleClose = () => {
    setOpen(false)
    onAfterClose?.()   // ⬅️ báo parent để loadSteps
  }

  /** ---- debounce save để tránh re-mount khi đang chọn tiếp ---- */
  const saveIds = async (ids, silent = false) => {
    try {
      setSaving(true)
      const now = splitKinds(ids)
      const prev = initialSplitRef.current
      const changed = {
        users: !sameSet(now.users, prev.users),
        depts: !sameSet(now.depts, prev.depts),
      }
      initialSplitRef.current = now
      await onSaveIds?.(ids, rowData, changed)
      if (!silent) showToastSuccess(successMsg || 'Đã cập nhật')
    } catch {
      showError(errorMsg || 'Cập nhật thất bại')
    } finally {
      setSaving(false)
    }
  }

  const scheduleSave = (ids) => {
    if (saveTimer.current) clearTimeout(saveTimer.current)
    saveTimer.current = setTimeout(() => saveIds(ids), 200)
  }

  useEffect(() => () => clearTimeout(saveTimer.current), [])

  /** ---- đồng bộ selected từ rowData CHỈ khi popover đóng ---- */
  useEffect(() => {
    const next = (Array.isArray(rowData?.[idsKey]) ? rowData[idsKey] : []).map(
      (id, i) => ({
        value: id,
        label:
          (Array.isArray(currentNames) ? currentNames[i] : undefined) ??
          String(id),
      }),
    )
    if (!open) setSelected(next)
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [rowData?.[idsKey], rowData?.[namesKey], open])

  /** ---- onChange không await, chỉ scheduleSave ---- */
  const handleChange = (_, value, reason, details) => {
    const created = details?.option?.__isNew__
    let next = value.map(norm)

    if (created && onCreateOption) {
      onCreateOption(details.option.inputLabel)
        .then((createdOpt) => {
          const fixed = [
            ...value.filter((v) => !v.__isNew__).map(norm),
            norm(createdOpt),
          ]
          setSelected(fixed)
          scheduleSave(fixed.map((x) => x.value))
        })
        .catch(() => showError('Tạo mới thất bại'))
      return
    }

    setSelected(next)
    scheduleSave(next.map((x) => x.value))
  }

  const displayNames = selected.length
    ? selected.map((s) => s.label)
    : currentNames
  const busy = loading || saving
  const filter = createFilterOptions()

  return (
    <>
      {/* cell (hiển thị trong bảng) */}
      <Box
        ref={anchorRef}
        onClick={(e) => { e.stopPropagation(); handleOpen() }}
        onKeyDown={(e) => {
          if (e.key === 'Enter' || e.key === ' ') { e.preventDefault(); e.stopPropagation(); handleOpen() }
          if (e.key === 'Escape') { e.stopPropagation(); handleClose() }
        }}
        tabIndex={0}
        sx={{
          display: 'flex',
          alignItems: 'center',
          flexWrap: 'wrap',
          gap: 0.5,
          minHeight: 32,
          width: '100%',
          px: 0.5,
          py: 0.25,
          cursor: 'pointer',
          outline: 'none',
        }}
        title="Nhấp để chỉnh sửa"
      >
        {displayNames?.length ? (
          displayNames.map((n, i) => <PillChip key={`${n}-${i}`} label={n} />)
        ) : (
          <Typography variant="body2" color="text.disabled">
            Chọn…
          </Typography>
        )}
        {busy && <CircularProgress size={14} sx={{ml: 0.5}} />}
      </Box>

      {/* popover 300x540 */}
      <Popover
        open={open}
        onClose={handleClose}
        anchorEl={anchorRef.current}
        anchorOrigin={{vertical: 'bottom', horizontal: 'left'}}
        transformOrigin={{vertical: 'top', horizontal: 'left'}}
        PaperProps={{
          sx: {
            p: 0,
            width: POPOVER_W,
            height: POPOVER_H,
            display: 'flex',
            flexDirection: 'column',
            border: 'none !important',
            '& *': {borderLeft: 'none !important'},
          },
        }}
      >
        {open && (
          <ClickAwayListener onClickAway={handleClose}>
            <Box
              sx={{display: 'flex', flexDirection: 'column', height: '100%'}}
              onMouseDownCapture={(e) => e.stopPropagation()} // giữ editor
              onKeyDown={(e) => e.stopPropagation()}
            >
              <Autocomplete
                multiple
                open={open}
                disablePortal
                options={options}
                loading={loading}
                value={selected}
                onChange={handleChange}
                selectOnFocus
                clearOnBlur
                handleHomeEndKeys
                getOptionLabel={(o) =>
                  typeof o === 'string' ? o : o?.label ?? ''
                }
                isOptionEqualToValue={(a, b) =>
                  (a?.value ?? a) === (b?.value ?? b)
                }
                filterSelectedOptions
                filterOptions={(opts, params) => {
                  const filtered = filter(opts, params)
                  const inputValue = params.inputValue?.trim()
                  const exists = opts.some(
                    (o) =>
                      (o.label || '').toLowerCase() ===
                      (inputValue || '').toLowerCase(),
                  )
                  if (onCreateOption && inputValue && !exists) {
                    filtered.unshift({
                      __isNew__: true,
                      inputLabel: inputValue,
                      label: `Create "${inputValue}"`,
                      value: inputValue,
                    })
                  }
                  return filtered
                }}
                renderTags={(value, getTagProps) =>
                  value.map((opt, idx) => {
                    const { onDelete } = getTagProps({ index: idx })
                    return (
                      <PillChip
                        key={opt.value ?? opt.label} // key ổn định theo id/value
                        label={opt.label}
                        onDelete={onDelete}
                      />
                    )
                  })
                }
                ListboxProps={{
                  sx: {
                    p: '0 !important',
                    m: '0 !important',
                    pl: '0 !important',
                    ml: '0 !important',
                    maxHeight: POPOVER_H - HEADER_H,
                    height: POPOVER_H - HEADER_H,
                    width: '100%',
                    overflowY: 'auto',
                    bgcolor: '#fff',
                    listStyle: 'none',
                    '& ul': { padding: '0 !important', margin: '0 !important', listStyle: 'none' },
                    '& li': { margin: '0 !important', paddingLeft: '0 !important', listStyle: 'none' },
                    '& .MuiAutocomplete-option': {
                      p: '4px 6px !important',
                      m: 0,
                      minHeight: 'unset',
                      display: 'flex',
                      alignItems: 'center',
                      gap: '4px',
                    },
                  },
                }}
                slotProps={{
                  popper: {sx: {width: '100%'}},
                  paper: {sx: {width: '100%', m: '0 !important', p: '0 !important', bgcolor: '#fff'}},
                  listbox: {sx: {width: '100%'}},
                  clearIndicator: {sx: {display: 'none'}},
                  popupIndicator: {sx: {display: 'none'}},
                }}
                renderOption={(props, option) => (
                  <li {...props}>
                    <span
                      style={{
                        opacity: 0.5,
                        fontWeight: 600,
                        fontSize: 12,
                        width: 10,
                        textAlign: 'center',
                      }}
                    >
                      ⋮⋮
                    </span>
                    <PillChip label={option.__isNew__ ? option.inputLabel : option.label} />
                  </li>
                )}
                renderInput={(params) => (
                  <TextField
                    {...params}
                    placeholder={selected.length ? '' : placeholder}
                    size="small"
                    sx={{
                      px: 0.5,
                      '& .MuiOutlinedInput-root': {
                        minHeight: HEADER_H,
                        alignItems: 'center',
                        borderRadius: 0,
                        fontSize: 13,
                        gap: 0.5,
                        '& .MuiOutlinedInput-notchedOutline': {
                          borderColor: 'divider',
                          borderWidth: 0,
                          borderBottomWidth: 1,
                        },
                        '&:hover .MuiOutlinedInput-notchedOutline': {
                          borderColor: 'divider',
                        },
                        '&.Mui-focused .MuiOutlinedInput-notchedOutline': {
                          borderColor: 'divider',
                          borderWidth: 0,
                          borderBottomWidth: 1,
                        },
                        '& .MuiAutocomplete-tag': {m: 0},
                      },
                    }}
                    InputProps={{
                      ...params.InputProps,
                      endAdornment: (<>{loading ? <CircularProgress size={14} /> : null}</>),
                    }}
                  />
                )}
              />
            </Box>
          </ClickAwayListener>
        )}
      </Popover>
    </>
  )
}

export default MultiSelectEditor
