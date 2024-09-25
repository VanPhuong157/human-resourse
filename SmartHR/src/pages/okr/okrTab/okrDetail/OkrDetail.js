import React, {useState, useEffect, useRef, useContext} from 'react'
import {Grid, Typography, LinearProgress, Box, Button} from '@mui/material'
import OkrDetailChart from './OkrDetailChart'
import OkrActivity from './OkrActivity'
import useGetOkrDetail from '../../../../pages/okr/requests/getOkrDetail'
import OkrProgress from './OkrProgress'
import CustomDialog from '../../../../components/customDialog'
import {UserContext} from '../../../../context/UserContext'
import ChangeOwner from './ChangeOwner'
import {baseUrl} from '../../../../api/rootApi'
import useGetUserPermissions from '../../../../pages/admin/requests/getUserPermissions'

const OkrDetail = ({data}) => {
  const [openProgressDialog, setOpenProgressDialog] = useState(false)
  const [openChangeOwnerDialog, setOpenChangeOwnerDialog] = useState(false)

  const [blobUrl, setBlobUrl] = useState('')

  const okrId = data?.id
  const {user} = useContext(UserContext)
  const {data: dataOkrDetail, refetch} = useGetOkrDetail({okrId})

  const [permissionData, setPermissionData] = useState([])
  const hasOkrEditOwnerPermission = permissionData.includes('Okr:EditOwner')

  const userIdPermission = localStorage.getItem('userId')
  const {data: permissions} = useGetUserPermissions({
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

  const okrActivityRefetch = useRef(null)
  useEffect(() => {
    refetch()
  }, [refetch])

  const handleDialogOpen = () => {
    setOpenProgressDialog(true)
  }

  const handleDialogClose = () => {
    setOpenProgressDialog(false)
    refetch()
    if (okrActivityRefetch.current) {
      okrActivityRefetch.current()
    }
  }

  const handleChangeOwnerDialogOpen = () => {
    setOpenChangeOwnerDialog(true)
  }

  const handleChangeOwnerDialogClose = () => {
    setOpenChangeOwnerDialog(false)
    refetch()
    if (okrActivityRefetch.current) {
      okrActivityRefetch.current()
    }
  }

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

  return (
    <Box width={800}>
      <Typography variant="h4" component="h1" gutterBottom>
        {dataOkrDetail?.data?.title}
      </Typography>
      <Box width={'100%'}>
        <Box
          display="flex"
          alignItems="center"
          justifyContent="center"
          mb={2}
          sx={
            dataOkrDetail?.data?.type === 'KeyResult' &&
            user.userId === dataOkrDetail?.data?.ownerId
              ? {
                  '&:hover': {
                    paddingTop: '2px',
                    paddingBottom: '2px',
                    backgroundColor: 'rgba(2, 10, 2, 0.1)', // Adjust color as needed
                    cursor:
                      dataOkrDetail?.data?.type === 'KeyResult'
                        ? 'pointer'
                        : 'default',
                  },
                }
              : {} // No hover effect if the user is not the owner
          }
        >
          <Box>
            <LinearProgress
              variant="determinate"
              value={dataOkrDetail?.data?.progress}
              style={{
                height: 10,
                borderRadius: 5,
                width: 600,
                marginRight: 20,
                padding: '1px',
              }}
              onClick={
                dataOkrDetail?.data?.type === 'KeyResult' &&
                user.userId === dataOkrDetail?.data?.ownerId
                  ? handleDialogOpen
                  : null
              }
            />
            <CustomDialog
              open={openProgressDialog}
              onCancel={handleDialogClose}
              title="Update Okr Process"
              viewDialog
              showCloseButton
              maxWidth="md"
              dialogContent={
                <OkrProgress
                  data={dataOkrDetail}
                  onDialogClose={handleDialogClose}
                />
              }
            />
          </Box>
          <Typography variant="body2" color="textSecondary">
            {dataOkrDetail?.data?.status}
          </Typography>
        </Box>
        <OkrDetailChart okrId={data?.id} />
      </Box>
      <Grid
        container
        width={'100%'}
        spacing={1}
        mt={2}
        ml={10}
        sx={{
          width: 'auto',
          wordWrap: 'break-word',
        }}
      >
        <Grid item xs={5}>
          <Typography variant="body1">Main Result & Content:</Typography>
        </Grid>
        <Grid item xs={7}>
          <Typography variant="body1" sx={{wordWrap: 'break-word'}}>
            {dataOkrDetail?.data?.content}
          </Typography>
        </Grid>
        <Grid item xs={5}>
          <Typography variant="body1">Target number:</Typography>
        </Grid>
        <Grid item xs={7}>
          <Typography variant="body1">
            {dataOkrDetail?.data?.targerNumber}
          </Typography>
        </Grid>
        <Grid item xs={5}>
          <Typography variant="body1">Achieved:</Typography>
        </Grid>
        <Grid item xs={7}>
          <Typography variant="body1">
            {dataOkrDetail?.data?.achieved}
          </Typography>
        </Grid>
        <Grid item xs={5}>
          <Typography variant="body1">Unit of Target:</Typography>
        </Grid>
        <Grid item xs={7}>
          <Typography variant="body1">
            {dataOkrDetail?.data?.unitOfTarget}
          </Typography>
        </Grid>
        <Grid item xs={5}>
          <Typography variant="body1">Progressing:</Typography>
        </Grid>
        <Grid item xs={7}>
          <Typography variant="body1">
            {dataOkrDetail?.data?.progress} %
          </Typography>
        </Grid>
        <Grid item xs={5}>
          <Typography variant="body1">Parent Alignment:</Typography>
        </Grid>
        <Grid item xs={7}>
          <Typography variant="body1">
            {dataOkrDetail?.data?.parentAlignment ?? 'Empty'}
          </Typography>
        </Grid>
        <Grid item xs={5}>
          <Typography variant="body1">Type:</Typography>
        </Grid>
        <Grid item xs={7}>
          <Typography variant="body1">{dataOkrDetail?.data?.type}</Typography>
        </Grid>
        <Grid item xs={5}>
          <Typography variant="body1">Cycle:</Typography>
        </Grid>
        <Grid item xs={7}>
          <Typography variant="body1">{dataOkrDetail?.data?.cycle}</Typography>
        </Grid>
        <Grid item xs={5}>
          <Typography variant="body1">Scope:</Typography>
        </Grid>
        <Grid item xs={7}>
          <Typography variant="body1">{dataOkrDetail?.data?.scope}</Typography>
        </Grid>
        <Grid item xs={5}>
          <Typography variant="body1">Confidence Level:</Typography>
        </Grid>
        <Grid item xs={7}>
          <Typography variant="body1">
            {dataOkrDetail?.data?.confidenceLevel}
          </Typography>
        </Grid>
        <Grid item xs={5}>
          <Typography variant="body1">Action Plan:</Typography>
        </Grid>
        <Grid item xs={7}>
          {dataOkrDetail?.data?.actionPlan ? (
            <a
              href={blobUrl}
              download={dataOkrDetail?.data?.actionPlan?.split('/').pop()}
              style={{
                color: 'blue',
                textDecoration: 'underline',
                cursor: 'pointer',
              }}
            >
              {dataOkrDetail?.data?.actionPlan?.split('/').pop()}
            </a>
          ) : (
            <Typography variant="body1">Empty</Typography>
          )}
        </Grid>
        <Grid item xs={5}>
          <Typography variant="body1">Expect Result:</Typography>
        </Grid>
        <Grid item xs={7}>
          <Typography variant="body1">{dataOkrDetail?.data?.result}</Typography>
        </Grid>
        <Grid item xs={5}>
          <Typography variant="body1">Department:</Typography>
        </Grid>
        <Grid item xs={7}>
          <Typography variant="body1">
            {dataOkrDetail?.data?.departmentName}
          </Typography>
        </Grid>
        <Grid item xs={5}>
          <Typography variant="body1">Owner:</Typography>
        </Grid>
        <Grid item xs={7} display="flex" alignItems="center">
          <Typography variant="body1">{dataOkrDetail?.data?.owner}</Typography>
          {hasOkrEditOwnerPermission && (
            <Button
              variant="outlined"
              size="small"
              sx={{ml: 2}}
              onClick={handleChangeOwnerDialogOpen}
            >
              Change Owner
            </Button>
          )}
        </Grid>
      </Grid>
      <OkrActivity
        okrId={data?.id}
        setRefetch={(refetchFn) => {
          okrActivityRefetch.current = refetchFn
        }}
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
