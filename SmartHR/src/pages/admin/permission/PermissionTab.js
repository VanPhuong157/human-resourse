import ComponentTable from '../../../components/table'
import React, {useState, useEffect, useCallback} from 'react'
import Box from '@mui/material/Box'
import Paper from '@mui/material/Paper'
import {useGetEmployees} from '../../../pages/employee/request/EmployeeRequest'
import {useNavigate} from 'react-router-dom'
import {Button, Typography} from '@mui/material'
import AddIcon from '@mui/icons-material/Add'
import FilterListIcon from '@mui/icons-material/FilterList'

const columns = [
  {
    id: 'fullName',
    label: 'Name',
    width: '20%',
  },
  {
    id: 'departmentName',
    label: 'Department',
    width: '10%',
  },
  {
    id: 'email',
    label: 'Email',
    width: '10%',
  },
  {
    id: 'roleNames',
    label: 'Role',
    width: '10%',
  },
  {
    id: 'typeOfWork',
    label: 'Type Of Work',
    width: '20%',
  },
  {
    id: 'status',
    label: 'Status',
    width: '5%',
  },
  {
    id: 'action',
    label: 'Action',
    width: '5%',
    edit: true,
  },
]

const PermissionHeader = ({}) => {
  return (
    <Box className="header">
      <Box display={'flex'}>
        <Box className="header-left">
          <Typography className="header-title">UserGroup List</Typography>
        </Box>
      </Box>
      <Box className="header-buttons">
        <Button
          className="header-button"
          startIcon={<FilterListIcon />}
          sx={{textTransform: 'none', color: 'black'}}
          // onClick={handleClickFilter}
        >
          Filter
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
          // onClick={handleClickAddUserGroup}
        >
          Add UserGroup
        </Button>
      </Box>
    </Box>
  )
}

const PermissionTab = () => {
  const navigate = useNavigate()
  const [page, setPage] = useState(0)
  const [rowsPerPage, setRowsPerPage] = useState(10)
  const [totalItems, setTotalItems] = React.useState('')
  const [users, setUsers] = useState([])

  const onChangePage = (newPage) => {
    setPage(newPage)
  }
  const onChangeRowPerPage = (rowsPerPage) => {
    setRowsPerPage(rowsPerPage)
    setPage(0)
  }

  const {refetch} = useGetEmployees(page + 1, rowsPerPage)

  const fetchData = useCallback(async () => {
    try {
      const response = await refetch()
      if (response && Array.isArray(response?.data?.data.items)) {
        setUsers(response.data?.data.items)
        setTotalItems(response?.data?.data?.totalCount)
      }
    } catch (error) {
      console.error('Error fetching posts:', error)
    }
  }, [refetch])

  const onActionEdit = (data) => {
    navigate(`permission/${data.userId}`, {})
  }

  useEffect(() => {
    fetchData()
  }, [page, rowsPerPage, refetch, fetchData])

  return (
    <Box sx={{width: '100%'}}>
      <Paper sx={{width: '100%', mb: 2}}>
        <PermissionHeader></PermissionHeader>
        <ComponentTable
          columns={columns}
          totalItems={totalItems}
          data={users}
          onChangePage={onChangePage}
          onChangeRowPerPage={onChangeRowPerPage}
          disableColCheckbox={true}
          onActionEdit={onActionEdit}
        />
      </Paper>
    </Box>
  )
}

export default PermissionTab
