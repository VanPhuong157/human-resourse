import ComponentTable from '../../../../components/table'
import React, {useState, useEffect, useCallback, useRef} from 'react'
import Box from '@mui/material/Box'
import Paper from '@mui/material/Paper'
import {showSuccess, showError} from '../../../../components/notification'
import {useNavigate, useParams} from 'react-router-dom'
import useGetPermissions from '../../requests/getPermission'
// import {UserContext} from 'context/UserContext'
// import {useContext} from 'react'
import {Button, Typography} from '@mui/material'
import AddIcon from '@mui/icons-material/Add'
import {StatusCodes} from 'http-status-codes'
import useGetRolePermissions from '../../requests/getRolePermissions'
import {useEditRolePermissions} from '../../requests/editRolePermissions'
import FilterListIcon from '@mui/icons-material/FilterList'
import FormDialog from '../../../../components/formDialog/FormDialog'
import FilterPermission from './FilterPermission'
import eventBus, {EVENT_BUS_KEY} from '../../../../services/eventBus'

const columns = [
  {
    id: 'name',
    label: 'Name',
    width: '90%',
  },
]

const RoleHeader = ({handleClickSaveRole, handleClickFilter}) => {
  return (
    <Box className="header" sx={{padding: 5}}>
      <Box display={'flex'}>
        <Box className="header-left">
          <Typography variant="h4" sx={{fontWeight: 'bold', color: '#1976d2'}}>
            Role Permission Management
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
          onClick={handleClickSaveRole}
        >
          Save
        </Button>
      </Box>
    </Box>
  )
}

const EditPermissionRolePage = () => {
  const navigate = useNavigate()
  const {roleId} = useParams()
  const [page, setPage] = useState(0)
  const [rowsPerPage, setRowsPerPage] = useState(10)
  const [totalItems, setTotalItems] = React.useState('')
  const [permissions, setPermissions] = useState([])
  const [dataFilter, setDataFilter] = useState('')
  const [openFilterPermission, setOpenFilterPermission] = useState(false)
  const selected = useRef()
  // const location = useLocation()
  // const {data} = location.state || {} // Lấy dữ liệu từ states
  // const isBasicType = data?.type === 'Basic'

  console.log('check - get', selected)

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
  const {data: rolePermissions, refetch: refetchRolePermissions} =
    useGetRolePermissions({
      roleId: roleId,
      pageIndex: page + 1,
      pageSize: 1000,
    })
  console.log(rolePermissions)
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
    navigate(`rolePermission/${data.id}`, {})
  }
  const {mutateAsync: editRolePermissionAsync} = useEditRolePermissions({
    roleId,
  })

  const onSave = (data) => {
    console.log('check', data)
    editRolePermissionAsync(data)
      .then((response) => {
        showSuccess({message: response.data?.message})
        refetch()
        refetchRolePermissions()
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
    eventBus.on(EVENT_BUS_KEY.SELECTED_COLUMNS, (newSelected) => {
      console.log('check - get', newSelected)
      selected.current = newSelected
    })
  }, [])

  useEffect(() => {
    fetchData()
  }, [page, rowsPerPage, refetch, fetchData, dataFilter])

  return (
    <Paper>
      <Box sx={{width: '100%'}}>
        <Paper sx={{width: '100%', mb: 2}}>
          <RoleHeader
            handleClickFilter={handleClickFilter}
            handleClickSaveRole={() => onSave(selected.current)}
            // type={isBasicType}
          ></RoleHeader>
          <ComponentTable
            columns={columns}
            totalItems={totalItems}
            data={permissions}
            dataSelected={rolePermissions?.data?.items}
            onChangePage={onChangePage}
            onChangeRowPerPage={onChangeRowPerPage}
            onSave={onSave}
            onActionEdit={onActionEdit}
            // disableCheckBox={isBasicType}

            // displaySaveButton={true}
          />
        </Paper>
      </Box>
      <FormDialog
        open={openFilterPermission}
        onCancel={handleCancelFilterDialog}
        onConfirm={handleConfirmFilterDialog}
        title="Filter Permission"
        actionName="Filter"
        dialogContent={<FilterPermission data={dataFilter} />}
      />
    </Paper>
  )
}

export default EditPermissionRolePage
