// 'use client'
import React, { useState, useEffect, useRef, useContext, useMemo } from 'react'
import {
  Box,
  Typography,
  LinearProgress,
  Chip,
  Stack,
  Paper,
  Divider,
  Button,
  Avatar,
  AvatarGroup,
} from '@mui/material'

import OkrDetailChart from './OkrDetailChart'
import OkrActivity from './OkrActivity'
import useGetOkrDetail from '../../../../pages/okr/requests/getOkrDetail'
import OkrProgress from './OkrProgress'
import CustomDialog from '../../../../components/customDialog'
import { UserContext } from '../../../../context/UserContext'
import ChangeOwner from './ChangeOwner'
import { baseUrl } from '../../../../api/rootApi'
import useGetUserPermissions from '../../../../pages/admin/requests/getUserPermissions'

/** Màu trạng thái (bao gồm Quá hạn) */
const STATUS_STYLES = {
  'Quá hạn': {
    bg: 'rgba(206, 24, 0, 0.165)',
    color: 'rgb(109, 53, 49)',
    label: 'Quá hạn',
  },
  Processing: {
    bg: 'rgba(59,130,246,0.15)',
    color: 'rgb(29,78,216)',
    label: 'Đang xử lý',
  },
  Completed: {
    bg: 'rgba(34,197,94,0.15)',
    color: 'rgb(22,163,74)',
    label: 'Hoàn thành',
  },
  Paused: {
    bg: 'rgba(148,163,184,0.18)',
    color: 'rgb(71,85,105)',
    label: 'Tạm dừng',
  },
}

const Section = ({ title, children, dense }) => (
  <Box mb={dense ? 2 : 4}>
    <Typography variant="h6" fontWeight={700} gutterBottom>
      {title}
    </Typography>
    {children}
  </Box>
)

const KeyValueRow = ({ k, v }) => (
  <Stack
    direction="row"
    justifyContent="space-between"
    alignItems="center"
    sx={{ py: 0.75 }}
  >
    <Typography variant="body2" sx={{ color: 'text.secondary', mr: 2 }}>
      {k}
    </Typography>
    <Typography variant="body2" sx={{ textAlign: 'right' }}>
      {v ?? '—'}
    </Typography>
  </Stack>
)

const OkrDetail = ({ data }) => {
  const okrId = data?.id

  const [openProgressDialog, setOpenProgressDialog] = useState(false)
  const [openChangeOwnerDialog, setOpenChangeOwnerDialog] = useState(false)
  const [blobUrl, setBlobUrl] = useState('')

  const { user } = useContext(UserContext)
  const { data: dataOkrDetail, refetch } = useGetOkrDetail({ okrId })

  /** Quyền đổi Owner */
  const [permissionData, setPermissionData] = useState([])
  const userIdPermission = localStorage.getItem('userId')
  const { data: permissions } = useGetUserPermissions({
    userId: userIdPermission,
    pageIndex: 1,
    pageSize: 1000,
  })
  useEffect(() => {
    if (permissions) {
      const newPerm = permissions?.data?.items.map((perm) => perm.name)
      setPermissionData(newPerm)
    }
  }, [permissions])
  const hasOkrEditOwnerPermission = permissionData.includes('Okr:EditOwner')

  /** Refetch lần đầu và khi đóng dialog */
  const okrActivityRefetch = useRef(null)
  useEffect(() => {
    refetch()
  }, [refetch])

  const handleDialogOpen = () => setOpenProgressDialog(true)
  const handleDialogClose = () => {
    setOpenProgressDialog(false)
    refetch()
    if (okrActivityRefetch.current) okrActivityRefetch.current()
  }

  const handleChangeOwnerDialogOpen = () => setOpenChangeOwnerDialog(true)
  const handleChangeOwnerDialogClose = () => {
    setOpenChangeOwnerDialog(false)
    refetch()
    if (okrActivityRefetch.current) okrActivityRefetch.current()
  }

  /** Tải file action plan -> blob URL để tải xuống */
  useEffect(() => {
    if (dataOkrDetail?.data?.actionPlanDetails) {
      fetch(baseUrl + `${dataOkrDetail.data.actionPlanDetails}`)
        .then((response) => response.blob())
        .then((blob) => {
          const url = URL.createObjectURL(blob)
          setBlobUrl(url)
        })
        .catch((error) => console.error('Error fetching file:', error))
    }
  }, [dataOkrDetail?.data?.actionPlanDetails])

  const d = dataOkrDetail?.data
  const statusStyle = useMemo(
    () => STATUS_STYLES[d?.status] || STATUS_STYLES['Processing'],
    [d?.status]
  )

  return (
    <Box sx={{ height: '100%', display: 'flex', flexDirection: 'column', minHeight: 0 }}>
      {/* HEADER sticky của sidepane */}
      <Box
        sx={{
          px: 6,
          py: 3,
          borderBottom: '1px solid #F3F4F6',
          position: 'sticky',
          top: 0,
          bgcolor: '#fff',
          zIndex: 1,
        }}
      >
        <Typography variant="h3" fontWeight={800} mb={1}>
          {d?.title}
        </Typography>

        <Stack direction="row" alignItems="center" spacing={1.2} flexWrap="wrap">
          <Chip
            label={statusStyle.label || d?.status}
            size="small"
            sx={{
              bgcolor: statusStyle.bg,
              color: statusStyle.color,
              fontWeight: 700,
            }}
          />
          <Chip
            label={`Progress: ${d?.progress ?? 0}%`}
            size="small"
            variant="outlined"
          />
          {d?.cycle && <Chip label={`Cycle: ${d.cycle}`} size="small" />}
          {d?.type && <Chip label={`Type: ${d.type}`} size="small" variant="outlined" />}
          {d?.scope && <Chip label={`Scope: ${d.scope}`} size="small" variant="outlined" />}

          {/* Owner inline */}
          <Stack direction="row" alignItems="center" spacing={1} ml={1}>
            <Typography variant="caption" sx={{ color: 'text.secondary' }}>
              Owner
            </Typography>
            <AvatarGroup
              max={4}
              sx={{ '& .MuiAvatar-root': { width: 24, height: 24, fontSize: 12 } }}
            >
              {d?.owner ? (
                <Avatar>{String(d.owner).charAt(0)}</Avatar>
              ) : (
                <Avatar>?</Avatar>
              )}
            </AvatarGroup>
            {hasOkrEditOwnerPermission && (
              <Button
                size="small"
                variant="outlined"
                onClick={handleChangeOwnerDialogOpen}
                sx={{ ml: 1 }}
              >
                Change Owner
              </Button>
            )}
          </Stack>
        </Stack>
      </Box>

      {/* BODY scroll */}
      <Box sx={{ flex: 1, overflow: 'auto', minHeight: 0 }}>
        <Box sx={{ px: 6, py: 3, maxWidth: 1000, mx: 'auto' }}>
          {/* Progress */}
          <Section title="Tiến độ">
            <Stack
              direction="row"
              alignItems="center"
              spacing={2}
              sx={
                d?.type === 'KeyResult' && user.userId === d?.ownerId
                  ? {
                      '&:hover': {
                        backgroundColor: 'rgba(2, 10, 2, 0.06)',
                        cursor: 'pointer',
                      },
                      borderRadius: 1,
                      p: 1,
                      width: 'fit-content',
                    }
                  : {}
              }
              onClick={
                d?.type === 'KeyResult' && user.userId === d?.ownerId
                  ? handleDialogOpen
                  : undefined
              }
            >
              <Box sx={{ width: 460 }}>
                <LinearProgress
                  variant="determinate"
                  value={d?.progress ?? 0}
                  sx={{ height: 12, borderRadius: 1.5 }}
                />
              </Box>
              <Typography variant="body2" sx={{ minWidth: 56 }}>
                {(d?.progress ?? 0) + '%'}
              </Typography>
            </Stack>

            <Box mt={2}>
              <OkrDetailChart okrId={okrId} />
            </Box>
          </Section>

          <Divider sx={{ my: 3 }} />

          {/* Content */}
          <Section title="Nội dung chính">
            <Paper variant="outlined" sx={{ p: 2.5 }}>
              <Typography whiteSpace="pre-wrap">{d?.content || 'Empty'}</Typography>
            </Paper>
          </Section>

          {/* Properties */}
          <Section title="Thông số">
            <Paper variant="outlined" sx={{ p: 2.5 }}>
              <KeyValueRow k="Target number" v={d?.targerNumber} />
              <KeyValueRow k="Achieved" v={d?.achieved} />
              <KeyValueRow k="Unit of Target" v={d?.unitOfTarget} />
              <KeyValueRow k="Parent Alignment" v={d?.parentAlignment ?? 'Empty'} />
              <KeyValueRow k="Confidence Level" v={d?.confidenceLevel} />
              <KeyValueRow
                k="Department"
                v={
                  Array.isArray(d?.departmentName)
                    ? d?.departmentName.join(', ')
                    : d?.departmentName
                }
              />
              <KeyValueRow k="Owner" v={d?.owner} />
            </Paper>
          </Section>

          {/* Expect Result */}
          <Section title="Kết quả kỳ vọng">
            <Paper variant="outlined" sx={{ p: 2.5 }}>
              <Typography whiteSpace="pre-wrap">{d?.result || 'Empty'}</Typography>
            </Paper>
          </Section>

          {/* Action Plan */}
          <Section title="Action plan">
            {d?.actionPlan ? (
              <Button
                component="a"
                href={blobUrl}
                download={String(d?.actionPlan).split('/').pop()}
              >
                {String(d?.actionPlan).split('/').pop()}
              </Button>
            ) : (
              <Typography>Empty</Typography>
            )}
          </Section>

          {/* Activity */}
          <Divider sx={{ my: 4 }} />
          <Section title="Activity">
            <OkrActivity
              okrId={okrId}
              setRefetch={(refetchFn) => {
                okrActivityRefetch.current = refetchFn
              }}
            />
          </Section>
        </Box>
      </Box>

      {/* Dialogs để ngoài khối scroll */}
      <CustomDialog
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
      />
    </Box>
  )
}

export default OkrDetail
