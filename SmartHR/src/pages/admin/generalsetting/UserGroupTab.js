import ComponentTable from '../../../components/table'
import React, {useState, useEffect, useCallback} from 'react'
import Box from '@mui/material/Box'
import Paper from '@mui/material/Paper'
import {Typography, Button} from '@mui/material'
import AddIcon from '@mui/icons-material/Add'
// import {useDeleteUserGroup} from 'pages/role/request/deleteUserGroup'
import {useGetUserGroup} from '../../../pages/admin/requests/getUserGroups'
import CustomAddUserGroupDialog from './create/AddUserGroupForm'
import CustomEditUserGroupDialog from './edit/EditUserGroupForm'

const columns = [
  {
    id: 'name',
    label: 'UserGroupName',
    width: '20%',
  },
  {
    id: 'description',
    label: 'Description',
    width: '20%',
  },
  {
    id: 'createdAt',
    label: 'CreateAt',
    width: '20%',
    formatDatetime: 'dd-MM-yyyy',
  },
  {
    id: 'updatedAt',
    label: 'UpdateAt',
    width: '20%',
    formatDatetime: 'dd-MM-yyyy',
  },
  {
    id: 'action',
    label: 'Action',
    width: '20%',
    edit: true,
    // delete: true,
  },
]
const UserGroupHeader = ({handleClickAddUserGroup}) => {
  return (
    <Box className="header" sx={{padding: 5}}>
      <Box display={'flex'}>
        <Box className="header-left">
          <Typography variant="h4" sx={{fontWeight: 'bold', color: '#1976d2'}}>
            User Group Management
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
          onClick={handleClickAddUserGroup}
        >
          Add UserGroup
        </Button>
      </Box>
    </Box>
  )
}

const UserGroupTab = () => {
  const [page, setPage] = useState(0)
  const [rowsPerPage, setRowsPerPage] = useState(10)
  const [openAddUserGroupDialog, setOpenAddUserGroupDialog] = useState(false)
  const [openEditUserGroupDialog, setOpenEditUserGroupDialog] = useState(false)
  const [roleList, setUserGroupList] = useState([])
  const [dataFilterDialog] = useState()
  const [totalItems, setTotalItems] = useState('')
  const [userGroupData, setUserGroupData] = useState()

  const onChangePage = (newPage) => {
    setPage(newPage)
  }
  const onChangeRowPerPage = (rowsPerPage) => {
    setRowsPerPage(rowsPerPage)
    setPage(0)
  }

  const onActionEdit = (data) => {
    console.log('data', data)
    setUserGroupData(data)
    setOpenEditUserGroupDialog(true)
  }
  const handleCancelEditUserGroupDialog = () => {
    setOpenEditUserGroupDialog(false)
  }
  const handleClickAddUserGroup = () => {
    setOpenAddUserGroupDialog(true)
  }

  const handleCancelAddUserGroupDialog = () => {
    setOpenAddUserGroupDialog(false)
  }
  const {refetch} = useGetUserGroup(page + 1, rowsPerPage, dataFilterDialog)

  const fetchData = useCallback(async () => {
    try {
      const response = await refetch()
      console.log(response.data?.data?.items)
      if (response && Array.isArray(response?.data?.data?.items)) {
        setUserGroupList(response?.data?.data?.items)
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
        <UserGroupHeader
          handleClickAddUserGroup={handleClickAddUserGroup}
        ></UserGroupHeader>
        <ComponentTable
          columns={columns}
          data={roleList}
          totalItems={totalItems}
          onChangePage={onChangePage}
          onChangeRowPerPage={onChangeRowPerPage}
          onActionEdit={onActionEdit}
          disableColCheckbox={true}
          // onActionDelete={onActionDelete}
        />
      </Paper>
      <CustomAddUserGroupDialog
        open={openAddUserGroupDialog}
        onClose={handleCancelAddUserGroupDialog}
        fetchData={fetchData}
      />
      <CustomEditUserGroupDialog
        open={openEditUserGroupDialog}
        onClose={handleCancelEditUserGroupDialog}
        userGroupData={userGroupData}
        fetchData={fetchData}
      />
    </Box>
  )
}

export default UserGroupTab
