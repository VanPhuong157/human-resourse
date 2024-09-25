import React, {useState, useEffect, useCallback} from 'react'
import Box from '@mui/material/Box'
import Paper from '@mui/material/Paper'
import FilterListIcon from '@mui/icons-material/FilterList'
import {Button, CircularProgress} from '@mui/material'
import Typography from '@mui/material/Typography'
import ComponentTable from '../../../../components/table'
import AddIcon from '@mui/icons-material/Add'
import '../../../../assets/style/candidate/candidatePage/Header.css'
import AddPostJobForm from '../create/AddPostJobForm.js'
import FormDialog from '../../../../components/formDialog/FormDialog'
import PostFilter from './PostFilter.js'
import {
  useGetPosts,
  useAddPost,
  useEditPost,
} from '../request/PostJobRequest.js'
import {showSuccess, showError} from '../../../../components/notification'
import EditPostJobForm from '../../../../pages/recruitment/job/edit/EditPostJobForm'
import ViewDetailPost from '../../../../pages/recruitment/job/details/ViewDetailPost'
import {useNavigate} from 'react-router-dom'
import useGetUserPermissions from '../../../../pages/admin/requests/getUserPermissions'
import CustomDialog from '../../../../components/customDialog'
import dayjs from 'dayjs'

const getColumns = (hasEditPost, hasViewDetailsPost) => [
  {
    id: 'title',
    label: 'Title',
    width: '15%',
    navigate: true,
  },
  {
    id: 'departmentName',
    label: 'Department',
    width: '15%',
  },
  {
    id: 'experienceYear',
    label: 'Experience Year',
    width: '15%',
  },
  {
    id: 'type',
    label: 'Type Of Work',
    width: '15%',
  },
  {
    id: 'expiryDate',
    label: 'Application Deadline',
    width: '25%',
    formatDatetime: 'dd-MM-yyyy',
  },
  {
    id: 'status',
    label: 'Status',
    width: '20%',
  },
  {
    id: 'action',
    label: 'Action',
    width: '5%',
    edit: hasEditPost,
    view_detail: hasViewDetailsPost,
  },
]
const JobPostHeader = ({handleClickFilter, handleClickAdd, perm}) => {
  const hasCreatePost = perm.includes('JobPost:Create')
  return (
    <Box className="header" sx={{padding: 5}}>
      <Box display={'flex'}>
        <Box className="header-left">
          <Typography variant="h4" sx={{fontWeight: 'bold', color: '#1976d2'}}>
            Job Post Management
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
        {hasCreatePost && (
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
            Add Job
          </Button>
        )}
      </Box>
    </Box>
  )
}
export default function JobPostPage() {
  const navigate = useNavigate()
  const [rowsPerPage, setRowsPerPage] = useState(10)
  const [listPosts, setListPosts] = useState([])
  const [page, setPage] = useState(0)
  const [totalItems, setTotalItems] = useState('')
  const [openUpdateDialog, setOpenUpdateDialog] = useState(false)
  const [openFilterDialog, setOpenFilterDialog] = useState(false)
  const [openAddDialog, setOpenAddDialog] = useState(false)
  const [dataFilterDialog, setDataFilterDialog] = useState()
  const [openViewDetailDialog, setOpenViewDetailDialog] = useState(false)
  const [dataUpdateDialog, setDataUpdateDialog] = useState({})
  const [postId, setPostId] = useState(null)
  const [loading, setLoading] = useState(true) // Thêm state loading
  const userId = localStorage.getItem('userId')
  const [permissionData, setPermissionData] = useState([])
  const hasEditPost = permissionData.includes('JobPost:Update')
  const hasViewDetailsPost = permissionData.includes('JobPost:Detail')
  const [formErrors, setFormErrors] = useState({})

  const columns = getColumns(hasEditPost, hasViewDetailsPost)
  const {data: permissions} = useGetUserPermissions({
    userId,
    pageIndex: 1,
    pageSize: 1000,
  })
  useEffect(() => {
    if (permissions) {
      const newPerm = permissions?.data?.items.map((perm) => perm.name)
      setPermissionData(newPerm)
      setLoading(false)
    }
  }, [permissions])

  const onChangePage = (newPage) => {
    setPage(newPage)
  }

  const {mutateAsync} = useEditPost({
    postId: postId,
  })
  const handleEditConfirm = async (formData) => {
    const errors = {}

    if (!formData.title) {
      errors.title = 'Title is required'
    }

    if (!formData.department) {
      errors.department = 'Department is required'
    }
    if (!formData.benefits) {
      errors.benefits = 'Benefits is required'
    }
    if (!formData.experienceYear) {
      errors.experienceYear = 'ExperienceYear is required'
    }
    if (!formData.expiryDate) {
      errors.expiryDate = 'ExpiryDate is required'
    }
    if (!formData.numberOfRecruits) {
      errors.numberOfRecruits = 'NumberOfRecruits is required'
    }
    if (!formData.requirements) {
      errors.requirements = 'Requirements is required'
    }
    if (!formData.salary) {
      errors.salary = 'Salary is required'
    }
    if (!formData.status) {
      errors.status = 'Status is required'
    }
    if (!formData.type) {
      errors.type = 'Type is required'
    }
    if (Object.keys(errors).length > 0) {
      setFormErrors(errors)
      return
    }
    formData.expiryDate = dayjs(formData.expiryDate, 'YYYY-DD-MM').format(
      'DD/MM/YYYY',
    )
    mutateAsync(formData)
      .then((response) => {
        showSuccess({message: response.data?.message})
        fetchData()
        setFormErrors({})
        handleCancelUpdateDialog()
      })
      .catch((err) => {
        showError(err.response?.data?.title)
      })
  }

  const onActionEdit = (data) => {
    setDataUpdateDialog(data)
    setPostId(data.id)
    handleClickUpdate()
  }
  const onActionViewDetail = (data) => {
    setDataUpdateDialog(data)
    handleClickViewDetail()
  }
  const onActionNavigate = (data) => {
    const jobPostId = data?.id
    navigate(`/candidate?jobPostId=${jobPostId}`, {state: {jobPostId: {jobPostId}}})
  }

  const handleClickUpdate = () => {
    setOpenUpdateDialog(true)
  }
  const handleCancelUpdateDialog = () => {
    setFormErrors({})

    setOpenUpdateDialog(false)
  }
  const handleClickViewDetail = () => {
    setOpenViewDetailDialog(true)
  }
  const handleCancelViewDialog = () => {
    setOpenViewDetailDialog(false)
  }
  const handleViewDetailsConfirm = () => {
    setOpenViewDetailDialog(false)
  }

  const onChangeRowPerPage = (rowsPerPage) => {
    setRowsPerPage(rowsPerPage)
    setPage(0)
  }
  const handleClickAdd = () => {
    setOpenAddDialog(true)
  }
  const handleCancelAddDialog = () => {
    setFormErrors({})

    setOpenAddDialog(false)
  }
  const handleClickFilter = () => {
    setOpenFilterDialog(true)
  }
  const handleCancelFilterDialog = () => {
    setOpenFilterDialog(false)
  }
  const handleConfirmFilterDialog = (formData) => {
    setDataFilterDialog(formData)
    console.log('data filter', dataFilterDialog)
    setOpenFilterDialog(false)
  }
  const {mutateAsync: mutateAsyncAdd} = useAddPost()

  const handleConfirmAddDialog = async (formData) => {
    const errors = {}

    if (!formData.title) {
      errors.title = 'Title is required'
    }
    if (!formData.department) {
      errors.department = 'Department is required'
    }
    if (!formData.benefits) {
      errors.benefits = 'Benefits is required'
    }
    if (!formData.experienceYear) {
      errors.experienceYear = 'ExperienceYear is required'
    }
    if (!formData.expiryDate) {
      errors.expiryDate = 'ExpiryDate is required'
    }
    if (!formData.numberOfRecruits) {
      errors.numberOfRecruits = 'NumberOfRecruits is required'
    }
    if (!formData.requirements) {
      errors.requirements = 'Requirements is required'
    }
    if (!formData.salary) {
      errors.salary = 'Salary is required'
    }
    if (!formData.status) {
      errors.status = 'Status is required'
    }
    if (!formData.type) {
      errors.type = 'Type is required'
    }
    if (Object.keys(errors).length > 0) {
      setFormErrors(errors)
      return
    }
    formData.expiryDate = dayjs(formData.expiryDate, 'YYYY-DD-MM').format(
      'DD/MM/YYYY',
    )
    mutateAsyncAdd(formData)
      .then((response) => {
        showSuccess({message: response.data?.message})
        fetchData()
        setFormErrors({})

        setOpenAddDialog(false)
      })
      .catch((err) => {
        showError(err.response?.data?.title)
        setOpenAddDialog(true)
      })
  }

  const {refetch} = useGetPosts(page + 1, rowsPerPage, dataFilterDialog)
  const fetchData = useCallback(async () => {
    try {
      const response = await refetch()
      if (response && Array.isArray(response?.data?.data?.items)) {
        setListPosts(response.data.data.items)
        setTotalItems(response?.data?.data?.totalCount)
      }
    } catch (error) {
      console.error('Error fetching posts:', error)
    }
  }, [refetch])

  useEffect(() => {
    fetchData()
    console.log(listPosts)
  }, [page, rowsPerPage, refetch, fetchData, dataFilterDialog])

  if (loading) {
    return (
      <Box
        sx={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          height: '100vh',
        }}
      >
        <CircularProgress />
      </Box>
    )
  }

  return (
    <Paper>
      <Box sx={{width: '100%'}}>
        <Paper sx={{width: '100%', mb: 2}}>
          <JobPostHeader
            perm={permissionData}
            handleClickFilter={handleClickFilter}
            handleClickAdd={handleClickAdd}
          />
          <ComponentTable
            columns={columns}
            data={listPosts}
            totalItems={totalItems}
            fetchData={fetchData}
            onChangePage={onChangePage}
            onChangeRowPerPage={onChangeRowPerPage}
            onActionEdit={onActionEdit}
            onActionViewDetail={onActionViewDetail}
            disableColCheckbox={true}
            onActionNavigate={onActionNavigate}
          />
        </Paper>
      </Box>
      <FormDialog
        open={openFilterDialog}
        onCancel={handleCancelFilterDialog}
        onConfirm={handleConfirmFilterDialog}
        title="Filter"
        actionName="Filter"
        dialogContent={<PostFilter data={dataFilterDialog} />}
      />
      <FormDialog
        open={openAddDialog}
        onCancel={handleCancelAddDialog}
        onConfirm={handleConfirmAddDialog}
        title="Add Job Post"
        actionName="Submit"
        maxWidth="lg"
        dialogContent={<AddPostJobForm formErrors={formErrors} />}
      />
      <FormDialog
        open={openUpdateDialog}
        onCancel={handleCancelUpdateDialog}
        onConfirm={handleEditConfirm}
        title="Edit Post"
        actionName="Save"
        maxWidth="lg"
        dialogContent={
          <EditPostJobForm data={dataUpdateDialog} formErrors={formErrors} />
        }
      />
      <CustomDialog
        open={openViewDetailDialog}
        onCancel={handleCancelViewDialog}
        onConfirm={handleViewDetailsConfirm}
        title={dataUpdateDialog.title}
        viewDialog={true}
        showCloseButton={true}
        maxWidth={'lg'}
        dialogContent={<ViewDetailPost data={dataUpdateDialog} />}
      />
    </Paper>
  )
}
