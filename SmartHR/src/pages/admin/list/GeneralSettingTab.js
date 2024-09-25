import ComponentTable from '../../../components/table'
import React,{useState,useEffect,useCallback} from 'react'
import Box from '@mui/material/Box'
import Paper from '@mui/material/Paper'
import { Typography,Button } from '@mui/material'
import DeleteIcon from '@mui/icons-material/Delete'
import CloudDownloadIcon from '@mui/icons-material/CloudDownload'
import AddIcon from '@mui/icons-material/Add'
import FormDialog from '../../../components/formDialog/FormDialog'
import AddDepartmentForm from '../create/AddDepartmentForm'
import useGetDepartments from '../../../pages/department/requests/getDepartment'
import { useAddDepartment } from '../../../pages/department/requests/addDepartment'
import { showSuccess,showError } from '../../../components/notification'
const columns = [
  {
    id: 'version',
    label: 'Version',
    width: '5%',
  },
  {
    id: 'imgBackground',
    label: 'ImageBackground',
    width: '15%',
  },
  {
    id: 'titleBody',
    label: 'TitleBody',
    width: '15%',
  },
  {
    id: 'headerAboutUs',
    label: 'Header About Us',
    width: '15%',
  },
  {
    id: 'Description About Us',
    label: 'Description About Us',
    width: '15%',
  },
  {
    id: 'statusJobPost',
    label: 'Status',
    width: '10%',
  },
  {
    id: 'createAt',
    label: 'Last Created',
    width: '25%',
  },

]
const DepartmentHeader = ({
    handleClickAddDepartment,
    // handleClickFilter,
  }) => {
    return (
      <Box className="header">
        <Box display={'flex'}>
          <Box className="header-left">
            <Typography className="header-title">Home Page History</Typography>
          </Box>
        </Box>
        <Box className="header-buttons">
          <Button
            className="header-button"
            startIcon={<DeleteIcon />}
            sx={{textTransform: 'none', color: 'black'}}
          >
            Delete
          </Button>
          {/* <Button
            className="header-button"
            startIcon={<FilterListIcon />}
            sx={{textTransform: 'none', color: 'black'}}
            onClick={handleClickFilter}
          >
            Filter
          </Button> */}
          <Button
            className="header-button"
            variant="outlined"
            startIcon={<CloudDownloadIcon />}
            sx={{
              textTransform: 'none',
              borderColor: 'gray',
              borderRadius: '1px solid gray',
              color: 'black',
            }}
          >
            Export
          </Button>
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


const GeneralSettingTab = () => {
  const [page, setPage] = useState(0)
  const [rowsPerPage, setRowsPerPage] = useState(10)
  const [totalItems] = React.useState('')
  const [openAddDepartmentDialog, setOpenAddDepartmentDialog] = useState(false)
  const [departmentList, setDepartmentList] = useState([])


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
    setOpenAddDepartmentDialog(false)
  }

  const {mutateAsync: mutateAsyncAdd} = useAddDepartment()
  const handleSubmitAddDepartmentDialog = async (formData) => {
    mutateAsyncAdd(formData)
      .then((response) => {
        showSuccess({message: response.data?.message})
        fetchData()
        setOpenAddDepartmentDialog(false)
      })
      .catch((err) => {
        showError(err.response?.data?.message)
        setOpenAddDepartmentDialog(true)
      })
  }

  const {refetch} = useGetDepartments()

  const fetchData = useCallback(async () => {
    try {
      const response = await refetch()
      console.log(response.data?.data)
      if (response && Array.isArray(response?.data?.data)) {
        setDepartmentList(response.data.data)
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
         handleClickAddDepartment={handleClickAddDepartment}></DepartmentHeader>
        <ComponentTable
          columns={columns}
          totalItems={totalItems}
          data={departmentList}
          onChangePage={onChangePage}
          onChangeRowPerPage={onChangeRowPerPage}
          disableColCheckbox={true}
        />
      </Paper>
      <FormDialog
        open={openAddDepartmentDialog}
        onCancel={handleCancelAddDepartmentDialog}
        onConfirm={handleSubmitAddDepartmentDialog}
        title="Add New Department "
        actionName="OK"
        dialogContent={<AddDepartmentForm />}
      />
    </Box>
  )
}

export default GeneralSettingTab