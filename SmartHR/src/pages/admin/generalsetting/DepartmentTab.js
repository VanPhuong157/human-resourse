import ComponentTable from '../../../components/table'
import React, {useState, useEffect, useCallback} from 'react'
import Box from '@mui/material/Box'
import Paper from '@mui/material/Paper'
import {Typography, Button} from '@mui/material'
import AddIcon from '@mui/icons-material/Add'
import FormDialog from '../../../components/formDialog/FormDialog'
import AddDepartmentForm from '../generalsetting/create/AddDepartmentForm'
import {useAddDepartment} from '../../../pages/department/requests/addDepartment'
import {showSuccess, showError} from '../../../components/notification'
import EditDepartmentForm from './edit/EditDepartmentForm'
import {useEditDepartment} from '../../../pages/department/requests/editDepartment'
import {StatusCodes} from 'http-status-codes'
import useGetDepartmentsFilter from '../../../pages/admin/requests/getDepartmentFilter'
const columns = [
  {
    id: 'name',
    label: 'DepartmentName',
    width: '25%',
  },
  {
    id: 'description',
    label: 'Description',
    width: '25%',
  },
  {
    id: 'createdAt',
    label: 'CreatedAt',
    width: '25%',
    formatDatetime: 'dd-MM-yyyy',
  },
  {
    id: 'createdBy',
    label: 'CreatedBy',
    width: '25%',
  },
  {
    id: 'action',
    label: 'Action',
    width: '25%',
    edit: true,
  },
]
const DepartmentHeader = ({handleClickAddDepartment}) => {
  return (
    <Box className="header" sx={{padding: 5}}>
      <Box display={'flex'}>
        <Box className="header-left">
          <Typography variant="h4" sx={{fontWeight: 'bold', color: '#1976d2'}}>
            Department Management
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
          onClick={handleClickAddDepartment}
        >
          Add Department
        </Button>
      </Box>
    </Box>
  )
}

const DepartmentTab = () => {
  const [page, setPage] = useState(0)
  const [rowsPerPage, setRowsPerPage] = useState(10)
  const [openAddDepartmentDialog, setOpenAddDepartmentDialog] = useState(false)
  const [openEditDepartmentDialog, setOpenEditDepartmentDialog] =
    useState(false)
  const [departmentData, setDepartmentData] = useState()
  const [totalItems, setTotalItems] = useState('')

  const [departmentList, setDepartmentList] = useState([])
  const [formErrors, setFormErrors] = useState({})

  const onChangePage = (newPage) => {
    setPage(newPage)
  }
  const onChangeRowPerPage = (rowsPerPage) => {
    setRowsPerPage(rowsPerPage)
    setPage(0)
  }

  const handleClickAddDepartment = () => {
    setOpenAddDepartmentDialog(true)
  }

  const handleCancelAddDepartmentDialog = () => {
    setFormErrors({})

    setOpenAddDepartmentDialog(false)
  }

  const {mutateAsync: mutateAsyncAdd} = useAddDepartment()
  const handleSubmitAddDepartmentDialog = async (formData) => {
    const errors = {}
    if (!formData.departmentName) {
      errors.departmentName = 'DepartmentName is required'
    }
    if (Object.keys(errors).length > 0) {
      setFormErrors(errors)
      return
    }
    mutateAsyncAdd(formData)
      .then((response) => {
        if (response?.data?.code === StatusCodes.BAD_REQUEST) {
          showError(response?.data?.message)
        } else {
          showSuccess({message: response.data?.message})
          fetchData()
          setFormErrors({})
          setOpenAddDepartmentDialog(false)
        }
      })
      .catch((err) => {
        showError(err.response?.data?.title)
        setOpenAddDepartmentDialog(true)
      })
  }

  const onActionEdit = (data) => {
    console.log('data', data)
    setDepartmentData(data)
    setOpenEditDepartmentDialog(true)
  }
  const handleCancelEditDialog = () => {
    setFormErrors({})

    setOpenEditDepartmentDialog(false)
  }

  const {mutateAsync} = useEditDepartment({
    departmentId: departmentData?.id,
  })
  const handleEditConfirm = async (formData) => {
    const errors = {}
    if (!formData.departmentName) {
      errors.departmentName = 'DepartmentName is required'
    }
    if (Object.keys(errors).length > 0) {
      setFormErrors(errors)
      return
    }
    mutateAsync(formData)
      .then((response) => {
        if (response?.data?.code === StatusCodes.BAD_REQUEST) {
          showError(response?.data?.message)
        } else {
          showSuccess({message: response.data?.message})
          fetchData()
          setFormErrors({})
          handleCancelEditDialog()
        }
      })
      .catch((err) => {
        showError(err.response?.data?.message)
      })
  }

  const {refetch} = useGetDepartmentsFilter(page + 1, rowsPerPage)

  const fetchData = useCallback(async () => {
    try {
      const response = await refetch()
      console.log(response.data?.data)
      if (response && Array.isArray(response?.data?.data?.items)) {
        setDepartmentList(response?.data?.data?.items)
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
          handleClickAddDepartment={handleClickAddDepartment}
        ></DepartmentHeader>
        <ComponentTable
          columns={columns}
          totalItems={totalItems}
          data={departmentList}
          onChangePage={onChangePage}
          onChangeRowPerPage={onChangeRowPerPage}
          disableColCheckbox={true}
          onActionEdit={onActionEdit}
        />
      </Paper>
      <FormDialog
        open={openAddDepartmentDialog}
        onCancel={handleCancelAddDepartmentDialog}
        onConfirm={handleSubmitAddDepartmentDialog}
        title="Add New Department "
        actionName="Submit"
        dialogContent={<AddDepartmentForm formErrors={formErrors} />}
      />
      <FormDialog
        open={openEditDepartmentDialog}
        onCancel={handleCancelEditDialog}
        onConfirm={handleEditConfirm}
        title="Edit Department "
        actionName="Save"
        dialogContent={
          <EditDepartmentForm data={departmentData} formErrors={formErrors} />
        }
      />
    </Box>
  )
}

export default DepartmentTab
