import React, {useState, useEffect} from 'react'
import Box from '@mui/material/Box'
import Paper from '@mui/material/Paper'
import FilterListIcon from '@mui/icons-material/FilterList'
import {Button, Typography} from '@mui/material'
import '../../../../assets/style/candidate/candidatePage/Header.css'
import ComponentTable from '../../../../components/table'
import FormDialog from '../../../../components/formDialog/FormDialog'
import CandidateFilterForm from './CandidateFilterForm'
import CustomDialog from '../../../../components/customDialog'
import useGetCandidates from '../../../../pages/recruitment/requests/getCandidates'
import useGetCandidateCV from '../../../../pages/recruitment/requests/getCandidateCV'
import CandidateUpdateStatusForm from './CandidateUpdateStatusForm'
import useUpdateStatusCandidate from '../../../../pages/recruitment/requests/updateStatusCandidate'
import {showError, showSuccess} from '../../../../components/notification'
import {useLocation} from 'react-router-dom'
import AddIcon from '@mui/icons-material/Add'
import '@react-pdf-viewer/core/lib/styles/index.css'
import '@react-pdf-viewer/toolbar/lib/styles/index.css'
import renderViewerPDF from '../../../../pages/recruitment/utils'
import CustomAddCandidateDialog from '../create/CustomAddCandidateDialog'
import useGetUserPermissions from '../../../../pages/admin/requests/getUserPermissions'
import {baseUrl} from '../../../../api/rootApi'
import {useSearchParams} from 'react-router-dom'
import dayjs from 'dayjs'

const getColumns = (hasUpdateStatusCandidate) => [
  {
    id: 'fullName',
    label: 'Full Name',
    width: '25%',
    custom: true,
    color: '#0070FF',
  },
  {
    id: 'email',
    label: 'Email',
    width: '15%',
  },
  {
    id: 'jobPostTitle',
    label: 'Job Apply',
    width: '15%',
  },
  {
    id: 'phoneNumber',
    label: 'Phone Number',
    width: '15%',
  },
  {
    id: 'dateApply',
    label: 'Date Apply',
    width: '15%',
    formatDatetime: 'dd-MM-yyyy',
  },
  {
    id: 'status',
    label: 'Status',
    width: '15%',
  },
  {
    id: 'cv',
    dialog: true,
    label: 'CV',
    width: '30%',
  },
  {
    id: 'action',
    label: 'Action',
    width: '15%',
    update_status: hasUpdateStatusCandidate,
  },
]

const CandidateHeader = ({handleClickFilter, handleClickAdd, perm}) => {
  const hasCreateCandidate = perm.includes('Candidate:Create')

  return (
    <Box className="header" sx={{padding: 5}}>
      <Box display={'flex'}>
        <Box className="header-left">
          <Typography variant="h4" sx={{fontWeight: 'bold', color: '#1976d2'}}>
            Candidate Management
          </Typography>
        </Box>
      </Box>
      <Box className="header-buttons">
        <Button
          className="header-button"
          startIcon={<FilterListIcon />}
          sx={{textTransform: 'none', color: 'black'}}
          onClick={handleClickFilter}
        >
          Filter
        </Button>
        {hasCreateCandidate && (
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
            onClick={handleClickAdd}
          >
            Add Candidate
          </Button>
        )}
      </Box>
    </Box>
  )
}

const CandidatePage = () => {
  const location = useLocation()
  const [searchParams, setSearchParams] = useSearchParams()
  const jobPostId = searchParams.get('jobPostId') || ''
  const [page, setPage] = React.useState(0)
  const [rowsPerPage, setRowsPerPage] = React.useState(10)
  const [openFilterDialog, setOpenFilterDialog] = useState()
  const [openUpdateStatusDialog, setOpenUpdateStatusDialog] = useState(false)
  const [dataFilterDialog, setDataFilterDialog] = useState({
    startDateApply: searchParams.get('startDateApply') || '',
    endDateApply: searchParams.get('endDateApply') || '',
    email: searchParams.get('email') || '',
    jobPostId: searchParams.get('jobPostId') || '',
    name: searchParams.get('name') || '',
    phone: searchParams.get('phone') || '',
    status: searchParams.get('status') || '',
  })
  const [dataUpdateStatusDialog, setDataUpdateStatusDialog] = useState()
  const [openCVDialog, setOpenCVDialog] = useState(false)
  const [candidateId, setCandidateId] = useState(null)
  const [openAddDialog, setOpenAddDialog] = useState(false)
  const userId = localStorage.getItem('userId')
  const [permissionData, setPermissionData] = useState([])
  const {data: permissions} = useGetUserPermissions({
    userId,
    pageIndex: 1,
    pageSize: 1000,
  })
  const hasUpdateStatusCandidate = permissionData.includes('Candidate:Update')
  const columns = getColumns(hasUpdateStatusCandidate)

  const {data, isLoading, refetch} = useGetCandidates(
    page + 1,
    rowsPerPage,
    dataFilterDialog,
  )
  useEffect(() => {
    if (permissions) {
      const newPerm = permissions?.data?.items.map((perm) => perm.name)
      setPermissionData(newPerm)
    }
  }, [permissions])
  const handleClickAdd = () => {
    setOpenAddDialog(true)
  }

  const handleCloseAddDialog = () => {
    setOpenAddDialog(false)
  }

  const onChangePage = (newPage) => {
    setPage(newPage)
  }

  const onChangeRowPerPage = (rowsPerPage) => {
    setRowsPerPage(rowsPerPage)
    setPage(0)
  }
  const onActionUpdateStatus = (data) => {
    setDataUpdateStatusDialog(data)
    setCandidateId(data.id)
    handleClickUpdateStatus()
  }

  const {mutateAsync} = useUpdateStatusCandidate({
    candidateId: candidateId,
  })

  const {refetch: refetchCandidateCV} = useGetCandidateCV({candidateId})
  useEffect(() => {
    if (candidateId) {
      refetchCandidateCV()
    }
  }, [candidateId, refetchCandidateCV])
  const handleClickUpdateStatus = () => {
    setOpenUpdateStatusDialog(true)
  }

  const handleCancelUpdateStatusDialog = () => {
    setOpenUpdateStatusDialog(false)
  }

  const handleConfirmUpdateStatusDialog = (formData) => {
    setDataUpdateStatusDialog(formData)
    setOpenUpdateStatusDialog(false)
    const {status} = formData
    const dataRequest = {
      newStatus: status,
    }
    mutateAsync(dataRequest)
      .then((response) => {
        showSuccess({message: response.data?.message})
        refetch()
      })
      .catch((err) => {
        if (err.response?.status === '') {
          const badRequestMessage = err.response?.data?.message
          showError({
            message: badRequestMessage,
          })
        } else {
          showError(err.response?.data?.message)
        }
      })
  }
  const handleClickFilter = () => {
    setOpenFilterDialog(true)
  }

  const handleCancelFilterDialog = () => {
    setOpenFilterDialog(false)
  }

  const handleConfirmFilterDialog = (formData) => {
    setDataFilterDialog(formData)
    setOpenFilterDialog(false)
    const newSearchParams = {
      name: formData.name || '',
      email: formData.email || '',
      phone: formData.phone || '',
      startDateApply: formData.startDateApply || '',
      endDateApply: formData.endDateApply || '',
      status: formData.status || '',
      jobPostId: formData.jobPostId || '',
    }

    for (const key in newSearchParams) {
      if (!newSearchParams[key]) {
        delete newSearchParams[key]
      }
    }
    setSearchParams(newSearchParams)
  }

  const handleCVClick = (id) => {
    setCandidateId(id)
    setOpenCVDialog(true)
  }

  const handleCVCancel = () => {
    setOpenCVDialog(false)
    setCandidateId(null)
  }
  useEffect(() => {
    refetch()
  }, [page, rowsPerPage, dataFilterDialog, refetch])

  return (
    <Paper>
      <Box sx={{width: '100%'}}>
        <Paper sx={{width: '100%', mb: 2}}>
          <CandidateHeader
            perm={permissionData}
            handleClickFilter={handleClickFilter}
            handleClickAdd={handleClickAdd}
          />
          <ComponentTable
            columns={columns}
            isLoading={isLoading}
            data={data?.data?.items}
            onClickOpenDialog={handleCVClick}
            disableColCheckbox={true}
            totalItems={data?.data?.totalCount}
            onChangePage={onChangePage}
            onChangeRowPerPage={onChangeRowPerPage}
            onActionUpdateStatus={onActionUpdateStatus}
          />
        </Paper>
      </Box>
      <FormDialog
        open={openFilterDialog}
        onCancel={handleCancelFilterDialog}
        onConfirm={handleConfirmFilterDialog}
        title="Filter Candidate"
        actionName="Filter"
        isLoading={isLoading}
        dialogContent={
          <CandidateFilterForm data={dataFilterDialog} jobPostId={jobPostId} />
        }
      />
      <FormDialog
        open={openUpdateStatusDialog}
        onCancel={handleCancelUpdateStatusDialog}
        onConfirm={handleConfirmUpdateStatusDialog}
        title="Update status candidate"
        actionName="Save"
        dialogContent={
          <CandidateUpdateStatusForm data={dataUpdateStatusDialog} />
        }
      />
      <CustomDialog
        open={openCVDialog}
        onCancel={handleCVCancel}
        onConfirm={handleCVCancel}
        title="CV Detail"
        viewDialog={true}
        showCloseButton={true}
        maxWidth={'xl'}
        dialogContent={renderViewerPDF(
          `${baseUrl}/api/Candidates/download-cv/${candidateId}`,
        )}
      />
      <CustomAddCandidateDialog
        refetch={refetch}
        open={openAddDialog}
        onClose={handleCloseAddDialog}
      />
    </Paper>
  )
}

export default CandidatePage
