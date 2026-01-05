// 'use client'
import React, {useState, useEffect, useRef, useContext} from 'react'
import {
  Box, Typography, LinearProgress, Chip, Stack, Divider, Avatar, Tooltip,
  useMediaQuery, useTheme, Collapse, Drawer,
} from '@mui/material'

// Icons
import DateRangeRounded from '@mui/icons-material/DateRangeRounded'
import HourglassEmptyRounded from '@mui/icons-material/HourglassEmptyRounded'
import GroupsRounded from '@mui/icons-material/GroupsRounded'
import AccessTimeRounded from '@mui/icons-material/AccessTimeRounded'
import EditCalendarRounded from '@mui/icons-material/EditCalendarRounded'
import BusinessRounded from '@mui/icons-material/BusinessRounded'
import Groups2Rounded from '@mui/icons-material/Groups2Rounded'
import PersonRounded from '@mui/icons-material/PersonRounded'
import StickyNote2Rounded from '@mui/icons-material/StickyNote2Rounded'
import TagRounded from '@mui/icons-material/TagRounded'
import ApartmentRounded from '@mui/icons-material/ApartmentRounded'
import FlagRounded from '@mui/icons-material/FlagRounded'
import AvTimerRounded from '@mui/icons-material/AvTimerRounded'
import DonutSmallRounded from '@mui/icons-material/DonutSmallRounded'
import FolderOpenRounded from '@mui/icons-material/FolderOpenRounded'

import OkrDetailChart from './OkrDetailChart'
import useGetOkrDetail from '../../../../pages/okr/requests/getOkrDetail'
// import OkrProgress from './OkrProgress'
// import CustomDialog from '../../../../components/customDialog'
import {UserContext} from '../../../../context/UserContext'
// import ChangeOwner from './ChangeOwner'
import {baseUrl} from '../../../../api/rootApi'
import OkrActivity from './OkrActivity'

// màu dùng CHUNG với OKRTab
import {
  PRIORITY_COLORS,
  STATUS_COLORS,
  getChipColors,
  nameColors,
} from '../../../../components/color/chipColors'

// ======= FONT GLOBAL (Notion stack) =======
const APP_FONT =
  'ui-sans-serif, -apple-system, BlinkMacSystemFont, "Segoe UI Variable Display", "Segoe UI", Helvetica, "Apple Color Emoji", Arial, sans-serif, "Segoe UI Emoji", "Segoe UI Symbol"'

// ======= Notion-ish tokens =======
const NOTION = {
  divider: 'rgba(55,53,47,0.09)',
  text: 'rgba(55,53,47,1)',
  textMuted: 'rgba(55,53,47,0.6)',
  labelW: 132,
  // rộng panel phải
  rightColMin: 350,  // ✅ rộng hơn
  rightColMaxPct: 30 // ✅ cho phép chiếm tối đa 35% container
}
const GAP_PX = 32

// Hysteresis theo LEFT-WIDTH
const LEFT_HIDE_LT = 640
const LEFT_SHOW_GT = 700

// ======= helpers =======
// parse ISO hoặc dd/MM/yyyy; lọc 0001-01-01
const parseAnyDate = (v) => {
  if (!v) return null
  if (typeof v === 'string' && v.startsWith('0001-01-01')) return null
  if (typeof v === 'string' && /^\d{2}\/\d{2}\/\d{4}$/.test(v)) {
    const [dd, mm, yyyy] = v.split('/').map(Number)
    const d = new Date(yyyy, mm - 1, dd)
    return isNaN(d.getTime()) ? null : d
  }
  const d = new Date(v)
  return isNaN(d.getTime()) ? null : d
}

const formatDate = (v) => {
  const d = parseAnyDate(v)
  return d
    ? d.toLocaleDateString('vi-VN', {day: '2-digit', month: '2-digit', year: 'numeric'})
    : 'Empty'
}

const daysLeft = (due) => {
  const d = parseAnyDate(due)
  if (!d) return null
  const startOf = (x) => new Date(x.getFullYear(), x.getMonth(), x.getDate())
  const today = startOf(new Date())
  const dueStart = startOf(d)
  return Math.floor((dueStart.getTime() - today.getTime()) / (24 * 60 * 60 * 1000))
}

/** Chip màu (người/công ty/phòng ban) – giống OKRTab */
const PaletteChip = ({label, withAvatar = false}) => {
  const fallback = {bg: 'rgba(229,231,235,1)', fg: 'rgb(107,114,128)'}
  const {bg, fg} = label ? nameColors(label) : fallback
  return (
    <Chip
      size="small"
      label={label || 'Empty'}
      avatar={
        withAvatar && label ? (
          <Avatar sx={{bgcolor: fg, color: '#fff', width: 18, height: 18, fontSize: 12}}>
            {String(label).charAt(0).toUpperCase()}
          </Avatar>
        ) : undefined
      }
      sx={{
        bgcolor: bg,
        color: fg,
        height: 22,
        borderRadius: 5.5,
        '& .MuiChip-label': {px: 0.5, lineHeight: 1.2},
        '& .MuiChip-avatar': {width: 18, height: 18, fontSize: 12},
      }}
    />
  )
}

/** PropertyRow (panel phải) – icon + label + value */
const PropertyRow = ({icon: Icon, label, children}) => (
  <Box
    sx={{
      display: 'grid',
      gridTemplateColumns: `20px ${NOTION.labelW}px 1fr`,
      alignItems: 'center',
      columnGap: 1.5,
      py: 1,
      minWidth: 0,
    }}
  >
    <Box sx={{display: 'flex', alignItems: 'center', justifyContent: 'center', color: NOTION.textMuted}} aria-hidden>
      {Icon ? <Icon sx={{fontSize: 18}} /> : null}
    </Box>
    <Typography variant="body2" sx={{color: NOTION.textMuted, overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap'}} title={label}>
      {label}
    </Typography>
    <Box sx={{textAlign: 'left', minWidth: 0}}>{children}</Box>
  </Box>
)

/** Status & Priority – mapping giống OKRTab */
const StatusPill = ({value}) => {
  const {bg, fg} = getChipColors(STATUS_COLORS, value)
  const label = value || ''
  return (
    <Box component="span" sx={{
      display: 'inline-flex', alignItems: 'center', gap: 1, px: 1, py: 0.25,
      borderRadius: 999, bgcolor: bg, color: fg, fontSize: 13, fontWeight: 600, lineHeight: 1.3, maxWidth: '100%',
    }} title={label}>
      <Box component="span" sx={{width: 8, height: 8, borderRadius: '50%', bgcolor: fg}} />
      <span style={{overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap'}}>{label}</span>
    </Box>
  )
}

const PriorityChip = ({value}) => {
  const {bg, fg} = getChipColors(PRIORITY_COLORS, value)
  return (
    <Chip
      size="small"
      label={value || 'Empty'}
      sx={{bgcolor: bg, color: fg, fontSize: 13, fontWeight: 600, borderRadius: 1.5, height: 24, '& .MuiChip-label': {lineHeight: 1.2, px: 0.5}}}
    />
  )
}

/** Meta 2 dòng, sát nhau */
const MetaItem = ({icon, label, value}) => (
  <Box sx={{minWidth: 160}}>
    <Stack direction="row" alignItems="center" spacing={0.25} sx={{color: NOTION.textMuted, mb: 0.25}}>
      {icon}
      <Typography variant="body2" sx={{fontWeight: 600}}>{label}</Typography>
    </Stack>
    <Box sx={{minWidth: 0}}>{value}</Box>
  </Box>
)

const OkrDetail = ({data}) => {
  const okrId = data?.id
  const [openProgressDialog, setOpenProgressDialog] = useState(false)
  const [openChangeOwnerDialog, setOpenChangeOwnerDialog] = useState(false)
  const [blobUrl, setBlobUrl] = useState('')

  const {user} = useContext(UserContext)
  const {data: dataOkrDetail, refetch} = useGetOkrDetail({okrId})

  const theme = useTheme()
  const belowMd = useMediaQuery(theme.breakpoints.down('md'))

  // user toggle nhớ lại
  const [propsOn, setPropsOn] = useState(true)
  useEffect(() => {
    try {
      const saved = localStorage.getItem('okr.propsOn')
      if (saved !== null) setPropsOn(saved === '1')
    } catch {}
  }, [])
  useEffect(() => {
    try {
      localStorage.setItem('okr.propsOn', propsOn ? '1' : '0')
    } catch {}
  }, [propsOn])

  // đo width container
  const gridRef = useRef(null)
  const [gridW, setGridW] = useState(0)
  useEffect(() => {
    if (!gridRef.current) return
    const ro = new ResizeObserver((entries) => setGridW(entries[0].contentRect.width))
    ro.observe(gridRef.current)
    return () => ro.disconnect()
  }, [])

  // left-width GIẢ ĐỊNH nếu panel mở
  const rightTargetW = Math.max(NOTION.rightColMin, Math.round((gridW * NOTION.rightColMaxPct) / 100))
  const expectedLeftIfShown = Math.max(0, gridW - rightTargetW - GAP_PX)

  // hysteresis tránh nháy
  const [autoHidden, setAutoHidden] = useState(false)
  useEffect(() => {
    setAutoHidden((prev) => {
      if (belowMd || expectedLeftIfShown < LEFT_HIDE_LT) return true
      if (!belowMd && expectedLeftIfShown > LEFT_SHOW_GT) return false
      return prev
    })
  }, [belowMd, expectedLeftIfShown])

  const [drawerOpen, setDrawerOpen] = useState(false)
  const showProps = propsOn && !autoHidden
  const rightWidthPx = `${rightTargetW}px`
  const drawerWidth = Math.min(480, Math.max(340, rightTargetW))

  // refetch on mount/close dialog
  const okrActivityRefetch = useRef(null)
  useEffect(() => { refetch() }, [refetch])




  // tải file (nếu có)
  useEffect(() => {
    if (dataOkrDetail?.data?.actionPlanDetails) {
      fetch(baseUrl + `${dataOkrDetail.data.actionPlanDetails}`)
        .then((r) => r.blob())
        .then((blob) => setBlobUrl(URL.createObjectURL(blob)))
        .catch((e) => console.error('Error fetching file:', e))
    }
  }, [dataOkrDetail?.data?.actionPlanDetails])

  const d = dataOkrDetail?.data || {}
  const due = d?.dueDate || d?.DueDate || null
  const left = daysLeft(due)
  const leftLabel =
    left == null ? 'Empty'
      : left > 0 ? `${left} ngày còn lại`
      : left === 0 ? 'Hôm nay'
      : `${Math.abs(left)} ngày quá hạn`

  // chuẩn hóa chips
  const ownersArr = Array.isArray(d?.ownerNames) ? d.ownerNames : d?.owner ? [d?.owner] : []
  const managersArr = Array.isArray(d?.managerNames) ? d.managerNames : d?.manager ? [d?.manager] : []
  const companyArr = Array.isArray(d?.company) ? d.company : d?.company ? [d?.company] : []
  const departmentArr = Array.isArray(d?.departmentName) ? d.departmentName : d?.departmentName ? [d?.departmentName] : []

  // === Content Properties ===
  const PropertiesContent = () => (
    <Box sx={{pt: 1}}>
      <Typography variant="subtitle2" sx={{color: NOTION.textMuted, fontWeight: 700, mb: 0.5}}>
        Properties
      </Typography>

      <PropertyRow icon={AccessTimeRounded} label="Created time">
        <Typography variant="body2">
          {formatDate(d?.dateCreated ?? d?.DateCreated)}
        </Typography>
      </PropertyRow>

      <PropertyRow icon={BusinessRounded} label="Công ty">
        {companyArr.length ? (
          <Stack direction="row" spacing={0.5} flexWrap="wrap" useFlexGap>
            {companyArr.map((c, i) => <PaletteChip key={i} label={c} />)}
          </Stack>
        ) : (
          <Typography variant="body2" sx={{color: NOTION.textMuted}}>Empty</Typography>
        )}
      </PropertyRow>

      <PropertyRow icon={EditCalendarRounded} label="Last edited time">
        <Typography variant="body2">
          {formatDate(d?.lastUpdated ?? d?.LastUpdated)}
        </Typography>
      </PropertyRow>

      <PropertyRow icon={Groups2Rounded} label="Người quản lý">
        {managersArr.length ? (
          <Stack direction="row" spacing={0.5} flexWrap="wrap" useFlexGap>
            {managersArr.map((m, i) => <PaletteChip key={i} label={m} />)}
          </Stack>
        ) : (
          <Typography variant="body2" sx={{color: NOTION.textMuted}}>Empty</Typography>
        )}
      </PropertyRow>

      <PropertyRow icon={PersonRounded} label="Người tạo">
        <Stack direction="row" spacing={1} alignItems="center" sx={{minWidth: 0}}>
          <Typography variant="body2" sx={{overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap'}}>
            {d?.createdByName || 'Empty'}
          </Typography>
        </Stack>
      </PropertyRow>

      <PropertyRow icon={StickyNote2Rounded} label="Note">
        <Typography variant="body2">{d?.note || 'Empty'}</Typography>
      </PropertyRow>

      <PropertyRow icon={TagRounded} label="Number">
        <Typography variant="body2">{d?.number || 'Empty'}</Typography>
      </PropertyRow>

      <PropertyRow icon={ApartmentRounded} label="Phòng Ban">
        {departmentArr.length ? (
          <Stack direction="row" spacing={0.5} flexWrap="wrap" useFlexGap>
            {departmentArr.map((dep, i) => <PaletteChip key={i} label={dep} />)}
          </Stack>
        ) : (
          <Typography variant="body2" sx={{color: NOTION.textMuted}}>Empty</Typography>
        )}
      </PropertyRow>

      <PropertyRow icon={FlagRounded} label="Priority">
        {/* Priority = ConfidenceLevel */}
        <PriorityChip value={d?.confidenceLevel} />
      </PropertyRow>

      <PropertyRow icon={AvTimerRounded} label="Quá Hạn">
        <Typography variant="body2">
          {left != null && left < 0 ? `${Math.abs(left)} ngày quá hạn` : 'Empty'}
        </Typography>
      </PropertyRow>

      <PropertyRow icon={DonutSmallRounded} label="Status">
        <StatusPill value={d?.status} />
      </PropertyRow>

      <PropertyRow icon={FolderOpenRounded} label="Tên dự án V…">
        <Typography variant="body2">{d?.projectName1 || 'Empty'}</Typography>
      </PropertyRow>

      <PropertyRow icon={FolderOpenRounded} label="Tên dự án V…">
        <Typography variant="body2">{d?.projectName2 || 'Empty'}</Typography>
      </PropertyRow>
    </Box>
  )

  // ===== RENDER =====
  return (
    <Box sx={{
      height: '100%', display: 'flex', flexDirection: 'column', minHeight: 0,
      fontFamily: APP_FONT, bgcolor: '#fff', color: NOTION.text,
      WebkitFontSmoothing: 'antialiased', MozOsxFontSmoothing: 'grayscale',
    }}>
      <Box sx={{flex: 1, overflow: 'auto', minHeight: 0}}>
        {/* grid 2 cột; LUÔN dùng '1fr auto' + panel có Collapse để tránh giật */}
        <Box
          ref={gridRef}
          sx={{
            px: {xs: 2, md: 6}, py: {xs: 1, md: 2},
            display: 'grid', gridTemplateColumns: '1fr auto',
            columnGap: {xs: 0, md: 4}, alignItems: 'start',
          }}
        >
          {/* LEFT */}
          <Box sx={{minWidth: 0, pr: {md: showProps ? 2 : 0}}}>
            <Typography component="h1" sx={{
              fontFamily: APP_FONT, fontSize: {xs: 28, md: 34}, fontWeight: 700,
              lineHeight: 1.2, letterSpacing: 0, mb: 1, color: NOTION.text,
            }}>
              {d?.title || 'Untitled'}
            </Typography>

            {/* META + nút … */}
            <Box sx={{
              mb: 1.5, display: 'flex', alignItems: 'flex-start', flexWrap: 'wrap',
              columnGap: 2, rowGap: 0.5,
            }}>
              <MetaItem
                icon={<DateRangeRounded sx={{fontSize: 18, color: NOTION.textMuted}} />}
                label="Due date"
                value={<Typography variant="body2">{formatDate(due)}</Typography>}
              />
              <MetaItem
                icon={<HourglassEmptyRounded sx={{fontSize: 18, color: NOTION.textMuted}} />}
                label="Tiến độ còn lại"
                value={<Typography variant="body2">{leftLabel}</Typography>}
              />
              <MetaItem
                icon={<GroupsRounded sx={{fontSize: 18, color: NOTION.textMuted}} />}
                label="Người thực hiện"
                value={
                  <Stack direction="row" alignItems="center" spacing={0.5} flexWrap="wrap" useFlexGap>
                    {ownersArr.length ? (
                      ownersArr.map((o, i) => <PaletteChip key={i} label={o} />)
                    ) : (
                      <Typography variant="body2" sx={{color: NOTION.textMuted}}>Empty</Typography>
                    )}
                    <Tooltip title={showProps ? 'Hide details' : 'Show details'}>
                      <Chip
                        label="…"
                        onClick={() => (autoHidden ? setDrawerOpen(true) : setPropsOn((v) => !v))}
                        clickable
                        sx={{
                          ml: 7.5, height: 24, fontWeight: 800, bgcolor: 'transparent',
                          color: NOTION.textMuted, borderRadius: 8,
                          '&:hover': {bgcolor: 'rgba(55,53,47,0.06)'},
                        }}
                      />
                    </Tooltip>
                  </Stack>
                }
              />
            </Box>

            {/* Tiến độ */}
            <Typography variant="subtitle2" sx={{m: 0, p: 0, color: NOTION.textMuted, fontWeight: 600}}>
              Tiến độ
            </Typography>
            <Stack
              direction="row" alignItems="center" spacing={2}
              sx={
                d?.type === 'KeyResult' && user.userId === d?.ownerId
                  ? { '&:hover': { backgroundColor: 'rgba(55,53,47,0.06)', cursor: 'pointer' }, borderRadius: 1, p: 1, width: 'fit-content' }
                  : {}
              }
              onClick={d?.type === 'KeyResult' && user.userId === d?.ownerId ? () => setOpenProgressDialog(true) : undefined}
            >
              <Box sx={{width: 460, maxWidth: '100%'}}>
                <LinearProgress
                  variant="determinate"
                  value={d?.progress ?? 0}
                  sx={{
                    height: 10, borderRadius: 1.5, bgcolor: '#EDEDED',
                    '& .MuiLinearProgress-bar': {bgcolor: '#16a34a', borderRadius: 1.5},
                  }}
                />
              </Box>
              <Typography variant="body2" sx={{minWidth: 56, color: NOTION.textMuted}}>
                {(d?.progress ?? 0) + '%'}
              </Typography>
            </Stack>

            <Box mt={2} sx={{width: '100%', minWidth: 0}}>
              <OkrDetailChart okrId={okrId} />
            </Box>

            <Divider sx={{my: 3, borderColor: NOTION.divider}} />

            {/* Comments */}
            <Typography variant="subtitle2" sx={{m: 0, p: 0, color: NOTION.textMuted, fontWeight: 600, pb: 2}}>
              Comments
            </Typography>
            <Stack direction="row" spacing={1} alignItems="center" sx={{mb: 6}}>
              <OkrActivity
                okrId={okrId}
                setRefetch={(fn) => { okrActivityRefetch.current = fn }}
              />
            </Stack>
          </Box>

          <Collapse in={showProps} orientation="horizontal" collapsedSize={0} timeout={220} sx={{overflow: 'hidden'}}>
            <Box sx={{ width: rightWidthPx, pl: {md: 3}, borderLeft: {md: `1px solid ${NOTION.divider}`} }}>
              <PropertiesContent />
            </Box>
          </Collapse>
        </Box>
      </Box>

      {/* Drawer cho màn hẹp */}
      <Drawer
        anchor="right"
        open={drawerOpen}
        onClose={() => setDrawerOpen(false)}
        ModalProps={{keepMounted: true}}
        sx={{
          '& .MuiDrawer-paper': {
            width: drawerWidth,
            borderLeft: `1px solid ${NOTION.divider}`,
            boxSizing: 'border-box',
            p: 2,
          },
        }}
      >
        <PropertiesContent />
      </Drawer>

      {/* Dialogs (để sẵn nếu dùng) */}
      {/* <CustomDialog
        open={openProgressDialog}
        onCancel={handleDialogClose}
        title="Update Okr Process"
        viewDialog
        showCloseButton
        maxWidth="md"
        dialogContent={<OkrProgress data={dataOkrDetail} onDialogClose={handleDialogClose} />}
      />
      <ChangeOwner
        data={dataOkrDetail}
        open={openChangeOwnerDialog}
        refetchOkrDetail={refetch}
        onClose={handleChangeOwnerDialogClose}
      /> */}
    </Box>
  )
}

export default OkrDetail
