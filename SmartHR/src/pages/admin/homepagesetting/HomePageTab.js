import ComponentTable from '../../../components/table/index'
import React, {useState, useEffect, useCallback} from 'react'
import Box from '@mui/material/Box'
import Paper from '@mui/material/Paper'
import {Typography, Button} from '@mui/material'
import AddIcon from '@mui/icons-material/Add'
import {showSuccess, showError} from '../../../components/notification'
import CustomEditVersionHomePage from './edit/CustomEditVersionHomePage'
import {useGetHomePageInfo} from './request/getHomePageInfo'
import useUpdateHomePageStatus from './request/UpDateHomePageStatus'
import {StatusCodes} from 'http-status-codes'
import {usePutJobStatus} from './request/putJobStatus'
import EditJobStatus from './edit/EditJobStatus'
import FormDialog from '../../../components/formDialog/FormDialog'
import useGetLastestVersion from './request/getLastestVersion'
const columns = [
  {
    id: 'id',
    label: 'Version',
    width: '5%',
  },
  {
    id: 'imageBackgroundDetail',
    label: 'ImgBackGr',
    width: '10%',
  },
  {
    id: 'title',
    label: 'Title',
    width: '10%',
  },
  {
    id: 'titleBody',
    label: 'TitleBody',
    width: '10%',
  },
  {
    id: 'createAt',
    label: 'CreateAt',
    width: '10%',
    formatDatetime: 'dd-MM-yyyy',
  },
  {
    id: 'address',
    label: 'Address',
    width: '10%',
  },
  {
    id: 'email',
    label: 'Email',
    width: '10%',
  },
  {
    id: 'phoneNumber',
    label: 'PhoneNumber',
    width: '10%',
  },
  {
    id: 'statusJobPost',
    label: 'JobStatus',
    width: '10%',
  },
  {
    id: 'status',
    label: 'Status',
    width: '10%',
  },
  {
    id: 'action',
    label: 'Acion',
    width: '10%',
    update_status: true,
    edit: true,
  },
]
const DepartmentHeader = ({
  handleClickEdit,
  // handleClickFilter,
}) => {
  return (
    <Box className="header" sx={{padding: 5}}>
      <Box display={'flex'}>
        <Box className="header-left">
          <Typography variant="h4" sx={{fontWeight: 'bold', color: '#1976d2'}}>
            Home Page Setting
          </Typography>
        </Box>
      </Box>
      <Box className="header-buttons">
        <Button
          className="header-button"
          variant="contained"
          startIcon={<AddIcon />}
          sx={{
            textTransform: 'none',
            borderColor: 'gray',
            borderRadius: '15px solid gray',
            color: 'white',
            ml: '8px',
          }}
          onClick={handleClickEdit}
        >
          Edit Version
        </Button>
      </Box>
    </Box>
  )
}

const HomePageTab = () => {
  const [page, setPage] = useState(0)
  const [rowsPerPage, setRowsPerPage] = useState(10)
  const [openEditDialog, setOpenEditDialog] = useState(false)
  const [totalItems, setTotalItems] = useState('')
  const [versionList, setVersionList] = useState([])
  const [openStatusDialog, setOpenStatusDialog] = useState(false)
  const [homePageId, setHomePageId] = useState('')
  const [openEditJobStatus, setOpenEditJobStatus] = useState(false)
  const [homePageInfo, setHomePageInfo] = useState(null)

  const onChangePage = (newPage) => {
    setPage(newPage)
  }
  const onChangeRowPerPage = (rowsPerPage) => {
    setRowsPerPage(rowsPerPage)
    setPage(0)
  }
  const onActionUpdateStatus = (data) => {
    setOpenStatusDialog(data)
    setHomePageId(data)
  }

  const {mutateAsync: updateStatus} = useUpdateHomePageStatus({
    id: homePageId?.id,
  })

  const {data: dataVersionHomePage} = useGetLastestVersion()

  const handleSubmitUpdateStatus = () => {
    updateStatus()
      .then((response) => {
        showSuccess({message: response.data?.message})
        setOpenStatusDialog(false)
        fetchData()
      })
      .catch((err) => {
        console.log(err)
        if (err?.response?.status === StatusCodes.BAD_REQUEST) {
          const badRequestMessage = err.response?.data?.title
          showError({
            message: badRequestMessage,
          })
        } else {
          showError(err.response?.data?.message)
        }
      })
  }

  const handleCancelUpdateStatusDialog = () => {
    setOpenStatusDialog(false)
  }

  const handleClickEdit = () => {
    setOpenEditDialog(true)
  }

  const handleCloseEditDialog = () => {
    setOpenEditDialog(false)
  }
  const handleCancelEditJobStatus = () => {
    setOpenEditJobStatus(false)
  }
  const onActionEdit = (data) => {
    setHomePageInfo(data)
    handleClickOpenEditJobStatus()
  }
  const handleClickOpenEditJobStatus = () => {
    setOpenEditJobStatus(true)
  }
  const {mutateAsync} = usePutJobStatus({
    id: homePageInfo?.id,
  })
  const handleConfirmEditJobStatus = (formData) => {
    const {status} = formData
    const dataRequest = {
      statusJobPost: status,
      status: homePageInfo.status,
    }
    mutateAsync(dataRequest)
      .then((response) => {
        showSuccess({message: response.data?.message})
        fetchData()
      })
      .catch((err) => {
        if (err.response?.status === StatusCodes.BAD_REQUEST) {
          const badRequestMessage = err.response?.data?.title
          showError({
            message: badRequestMessage,
          })
        } else {
          showError(err.response?.data?.message)
        }
      })
    setOpenEditJobStatus(false)
  }

  const {refetch} = useGetHomePageInfo(page + 1, rowsPerPage)

  const fetchData = useCallback(async () => {
    try {
      const response = await refetch()
      if (response && Array.isArray(response?.data?.data?.items)) {
        setVersionList(response?.data?.data?.items)
        setTotalItems(response?.data?.data?.totalCount)
      }
    } catch (error) {
      console.error('Error fetching posts:', error)
    }
  }, [refetch])

  useEffect(() => {
    fetchData()
  }, [page, rowsPerPage, refetch, fetchData])

  return (
    <Box sx={{width: '100%'}}>
      <Paper sx={{width: '100%', mb: 2}}>
        <DepartmentHeader handleClickEdit={handleClickEdit}></DepartmentHeader>
        <ComponentTable
          columns={columns}
          data={versionList}
          totalItems={totalItems}
          onChangePage={onChangePage}
          onChangeRowPerPage={onChangeRowPerPage}
          onActionUpdateStatus={onActionUpdateStatus}
          onActionEdit={onActionEdit}
          disableColCheckbox={true}
        />
      </Paper>
      <CustomEditVersionHomePage
        open={openEditDialog}
        onClose={handleCloseEditDialog}
        fetchData={fetchData}
        dataVersionHomePage={dataVersionHomePage}
      />
      <FormDialog
        open={openStatusDialog}
        onCancel={handleCancelUpdateStatusDialog}
        onConfirm={handleSubmitUpdateStatus}
        title="Update Status"
        actionName="Save"
        dialogContent={<Typography>Are you sure to update?</Typography>}
      />

      <FormDialog
        open={openEditJobStatus}
        onCancel={handleCancelEditJobStatus}
        onConfirm={handleConfirmEditJobStatus}
        title="Update status Job Post"
        actionName="Save"
        dialogContent={<EditJobStatus data={homePageInfo} />}
      />
    </Box>
  )
}

export default HomePageTab
