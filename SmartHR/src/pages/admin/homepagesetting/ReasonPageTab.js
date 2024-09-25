import ComponentTable from '../../../components/table/index'
import React, {useState, useEffect, useCallback} from 'react'
import Box from '@mui/material/Box'
import Paper from '@mui/material/Paper'
import {Typography, Button} from '@mui/material'
import AddIcon from '@mui/icons-material/Add'
import FormDialog from '../../../components/formDialog/FormDialog'
import {showSuccess, showError} from '../../../components/notification'
import AddReasonForm from './create/AddReasonForm'
import {useAddReason} from './request/PostReasonRequest'
import {useGetReasons} from './request/GetReasonsRequest'
import EditReasonForm from './edit/EditReasonForm'
import {useEditReason} from './request/PutReasonRequest'
import {useDeleteReason} from './request/DeleteReasonRequest'
import ConfirmDialog from '../../../components/confirmDialog'
import {StatusCodes} from 'http-status-codes'
const columns = [
  {
    id: 'id',
    label: 'Id',
    width: '10%',
  },
  {
    id: 'title',
    label: 'Title',
    width: '20%',
  },
  {
    id: 'subTitle',
    label: 'SubTitle',
    width: '20%',
  },
  {
    id: 'color',
    label: 'Color',
    width: '20%',
  },
  {
    id: 'content',
    label: 'Content',
    width: '20%',
  },
  {
    id: 'action',
    label: 'Action',
    width: '10%',
    edit: true,
    delete: true,
  },
]
const DepartmentHeader = ({
  handleClickAdd,
  showAddButton,
  // handleClickFilter,
}) => {
  return (
    <Box className="header" sx={{padding: 5}}>
      <Box display={'flex'}>
        <Box className="header-left">
          <Typography variant="h4" sx={{fontWeight: 'bold', color: '#1976d2'}}>
            Home Page Reason
          </Typography>
        </Box>
      </Box>
      <Box className="header-buttons">
        {showAddButton && (
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
            Add Reason
          </Button>
        )}
      </Box>
    </Box>
  )
}

const ReasonPageTab = () => {
  const [page, setPage] = useState(0)
  const [rowsPerPage, setRowsPerPage] = useState(10)
  const [openAddDialog, setOpenAddDialog] = useState(false)
  const [openEditDialog, setOpenEditDialog] = useState(false)
  const [reasonList, setReasonList] = useState([])
  const [dataEditDialog, setDataEditDialog] = useState({})
  const [reasonId, setReasonId] = useState(null)
  const [totalItems, setTotalItems] = useState('')
  const [selectedReasonId, setSelectedReasonId] = useState(null)
  const [openDeleteReason, setOpenDeleteReason] = useState(false)
  const [formErrors, setFormErrors] = useState({})

  const onChangePage = (newPage) => {
    setPage(newPage)
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
  const handleCancelEditDialog = () => {
    setFormErrors({})

    setOpenEditDialog(false)
  }
  const {mutateAsync} = useEditReason({
    reasonId: reasonId,
  })

  const handleEditConfirmDialog = async (formData) => {
    const errors = {}
    if (!formData.title) {
      errors.title = 'Title is required'
    }
    if (!formData.subTitle) {
      errors.subTitle = 'SubTitle is required'
    }
    if (!formData.color) {
      errors.color = 'Color is required'
    }
    if (!formData.content) {
      errors.content = 'Content is required'
    }
    if (Object.keys(errors).length > 0) {
      setFormErrors(errors)
      return
    }
    mutateAsync(formData)
      .then((response) => {
        showSuccess({message: response.data?.message})
        fetchData()
        setFormErrors({})
        handleCancelEditDialog()
      })
      .catch((err) => {
        showError(err.response?.data?.title)
      })
  }

  const handleClickEdit = () => {
    setOpenEditDialog(true)
  }

  const handleCancelDelete = () => {
    setOpenDeleteReason(false)
  }

  const onActionDelete = (data) => {
    setOpenDeleteReason(true)
    setSelectedReasonId(data)
    console.log(selectedReasonId)
  }

  const {mutateAsync: deleteReason} = useDeleteReason({
    reasonId: selectedReasonId?.id,
  })

  const handleSubmitDeleteReason = () => {
    deleteReason()
      .then((response) => {
        showSuccess({message: response.data?.message})
        setOpenDeleteReason(false)
        fetchData()
      })
      .catch((err) => {
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
  const onActionEdit = (data) => {
    setDataEditDialog(data)
    setReasonId(data.id)
    handleClickEdit()
  }

  const {mutateAsync: mutateAsyncAdd} = useAddReason()
  const handleSubmitAddDialog = async (formData) => {
    const errors = {}
    if (!formData.title) {
      errors.title = 'Title is required'
    }
    if (!formData.subTitle) {
      errors.subTitle = 'SubTitle is required'
    }
    if (!formData.color) {
      errors.color = 'Color is required'
    }
    if (!formData.content) {
      errors.content = 'Content is required'
    }
    if (Object.keys(errors).length > 0) {
      setFormErrors(errors)
      return
    }
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

  const {refetch} = useGetReasons()

  const fetchData = useCallback(async () => {
    try {
      const response = await refetch()
      console.log(response?.data?.data?.items)
      if (response && Array.isArray(response?.data?.data?.items)) {
        setReasonList(response?.data?.data?.items)
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
        <DepartmentHeader
          handleClickAdd={handleClickAdd}
          showAddButton={reasonList.length < 4}
        ></DepartmentHeader>
        <ComponentTable
          columns={columns}
          data={reasonList}
          totalItems={totalItems}
          onChangePage={onChangePage}
          onChangeRowPerPage={onChangeRowPerPage}
          onActionEdit={onActionEdit}
          onActionDelete={onActionDelete}
          disableColCheckbox={true}
        />
      </Paper>
      <FormDialog
        open={openAddDialog}
        onCancel={handleCancelAddDialog}
        onConfirm={handleSubmitAddDialog}
        title="Add Reason "
        actionName="Submit"
        dialogContent={<AddReasonForm formErrors={formErrors} />}
      />
      <FormDialog
        open={openEditDialog}
        onCancel={handleCancelEditDialog}
        onConfirm={handleEditConfirmDialog}
        title="Edit Reason "
        actionName="Save"
        dialogContent={
          <EditReasonForm data={dataEditDialog} formErrors={formErrors} />
        }
      />
      <ConfirmDialog
        open={openDeleteReason}
        onCancel={handleCancelDelete}
        onConfirm={handleSubmitDeleteReason}
        title="Delete Reason"
        actionName="Delete"
        dialogContent={<Typography>Are you sure delete?</Typography>}
      />
    </Box>
  )
}

export default ReasonPageTab
