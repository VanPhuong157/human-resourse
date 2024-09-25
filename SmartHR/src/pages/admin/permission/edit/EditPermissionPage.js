import ComponentTable from '../../../../components/table'
import React, {useState, useEffect, useCallback} from 'react'
import Box from '@mui/material/Box'
import Paper from '@mui/material/Paper'
import {showSuccess, showError} from '../../../../components/notification'
import {useNavigate, useParams} from 'react-router-dom'
import useGetPermissions from '../../../../pages/admin/requests/getPermission'
import useGetUserPermissions from '../../../../pages/admin/requests/getUserPermissions'
import {useEditUserPermission} from '../../../../pages/admin/requests/editUserPermissions'
import {StatusCodes} from 'http-status-codes'
import {Button, Typography} from '@mui/material'
import FilterListIcon from '@mui/icons-material/FilterList'
import FilterPermission from './FilterPermission'
import FormDialog from '../../../../components/formDialog/FormDialog'

const columns = [
  {
    id: 'name',
    label: 'Name',
    width: '90%',
  },
]

const PermissionHeader = ({handleClickFilter}) => {
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
          onClick={handleClickFilter}
        >
          Filter
        </Button>
      </Box>
    </Box>
  )
}
const EditPermissionPage = () => {
  const navigate = useNavigate()
  const {userId} = useParams()
  const [page, setPage] = useState(0)
  const [rowsPerPage, setRowsPerPage] = useState(10)
  const [totalItems, setTotalItems] = useState('')
  const [permissions, setPermissions] = useState([])
  const [openFilterPermission, setOpenFilterPermission] = useState(false)
  const [dataFilter, setDataFilter] = useState('')

  const onChangePage = (newPage) => {
    setPage(newPage)
  }
  const onChangeRowPerPage = (rowsPerPage) => {
    setRowsPerPage(rowsPerPage)
    setPage(0)
  }
  const handleClickFilter = () => {
    setOpenFilterPermission(true)
  }

  const handleCancelFilterDialog = () => {
    setOpenFilterPermission(false)
  }

  const handleConfirmFilterDialog = (formData) => {
    setDataFilter(formData)
    setOpenFilterPermission(false)
  }

  const {refetch} = useGetPermissions(page + 1, rowsPerPage, dataFilter)
  const {data: userPermissions, refetch: refetchUserPermissions} =
    useGetUserPermissions({
      userId: userId,
      pageIndex: page + 1,
      pageSize: 1000,
    })
  const fetchData = useCallback(async () => {
    try {
      const response = await refetch()
      if (response && Array.isArray(response?.data?.data.items)) {
        setPermissions(response.data?.data.items)
        setTotalItems(response?.data?.data?.totalCount)
      }
    } catch (error) {
      console.error('Error fetching posts:', error)
    }
  }, [refetch])

  const onActionEdit = (data) => {
    navigate(`permission/${data.userId}`, {})
  }
  const {mutateAsync: editUserPermissionAsync} = useEditUserPermission({userId})
  const onSave = (data) => {
    editUserPermissionAsync(data)
      .then((response) => {
        showSuccess({message: response.data?.message})
        refetch()
        refetchUserPermissions()
      })
      .catch((err) => {
        if (err.response?.status === StatusCodes.BAD_REQUEST) {
          const badRequestMessage =
            err.response?.data?.message || err.response?.data?.title
          showError({
            message: badRequestMessage,
          })
        } else {
          showError(err.response?.data?.message)
        }
      })
  }
  useEffect(() => {
    fetchData()
  }, [page, rowsPerPage, refetch, fetchData, dataFilter])

  return (
    <Paper>
      <Box sx={{width: '100%'}}>
        <Paper sx={{width: '100%', mb: 2}}>
          <PermissionHeader
            handleClickFilter={handleClickFilter}
          ></PermissionHeader>
          <ComponentTable
            columns={columns}
            totalItems={totalItems}
            data={permissions}
            dataSelected={userPermissions?.data?.items}
            onChangePage={onChangePage}
            onChangeRowPerPage={onChangeRowPerPage}
            onActionEdit={onActionEdit}
            onSave={onSave}
          />
        </Paper>
      </Box>
      <FormDialog
        open={openFilterPermission}
        onCancel={handleCancelFilterDialog}
        onConfirm={handleConfirmFilterDialog}
        title="Filter Permission"
        actionName="OK"
        dialogContent={<FilterPermission data={dataFilter} />}
      />
    </Paper>
  )
}

export default EditPermissionPage
