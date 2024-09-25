import React, {useState, useEffect} from 'react'
import {Paper, Box} from '@mui/material'
import ComponentTable from '../../../components/table'
import useGetOkrRequest from '../requests/getOkrRequest'
import CustomDialog from '../../../components/customDialog'
import useGetOkrDetail from '../requests/getOkrDetail'
import ApproveRequest from '../requestTab/approve/ApproveRequest'
import {useEditStatusOKR} from '../requests/editOkrStatus'
import {showError, showSuccess} from '../../../components/notification'
import useGetDepartments from '../../../pages/department/requests/getDepartment'
import FormDialog from '../../../components/formDialog/FormDialog'
import OkrRequestFilter from './filter/OkrRequestFilter'
import Header from '../headerOkr/HeaderOkr'
import EditOkrRequest from '../requestTab/edit/EditOkrRequest'
import OkrRequestDetail from './details/OkrRequestDetails'
import {StatusCodes} from 'http-status-codes'
import CreateOKR from '../../../pages/okr/okrTab/create/CreateOKR'
import useGetUserPermissions from '../../../pages/admin/requests/getUserPermissions'

const getColumns = (
  hasOkrRequestEditPermission,
  hasOkrRequestDetailPermission,
  hasOkrRequestUpdatePermission,
) => [
  {
    id: 'title',
    label: 'Title',
    width: '50%',
    custom: true,
    color: '#0070FF',
    dialog: hasOkrRequestUpdatePermission,
  },
  {id: 'type', label: 'Type', width: '10%'},
  {id: 'scope', label: 'Scope', width: '10%'},
  {id: 'cycle', label: 'Cycle', width: '5%'},
  {id: 'departmentName', label: 'Department', width: '10%'},
  {id: 'owner', label: 'Owner', width: '15%'},
  {id: 'parentAligment', label: 'Parent OKR', width: '15%'},
  {id: 'dateCreated', label: 'Last Updated', width: '15%'},
  {
    id: 'approveStatus',
    label: 'Approve Status',
    width: '10%',
  },
  {
    id: 'action',
    label: 'Action',
    width: '10px',
    edit: hasOkrRequestEditPermission,
    view_detail: hasOkrRequestDetailPermission,
  },
]

const RequestTab = () => {
  const [page, setPage] = useState(0)
  const [rowsPerPage, setRowsPerPage] = useState(10)
  const [openFilterDialog, setOpenFilterDialog] = useState(false)
  const [dataFilterDialog, setDataFilterDialog] = useState({
    title: '',
    type: '',
    scope: '',
    approveStatus: '',
    cycle: '',
  })
  const [openDialog, setOpenDialog] = useState(false)
  const [okrId, setOkrId] = useState(null)
  const [dialogClosed, setDialogClosed] = useState(false)
  const departmentId = localStorage.getItem('departmentId')
  const role = localStorage.getItem('role')
  const [selectedDepartment, setSelectedDepartment] = useState(
    role === 'BOD' ? '' : departmentId,
  )
  // eslint-disable-next-line no-unused-vars
  const [dataUpdateDialog, setDataUpdateDialog] = useState({})
  const [openUpdateDialog, setOpenUpdateDialog] = useState(false)
  const [openViewDetailDialog, setOpenViewDetailDialog] = useState(false)
  const [openAddDetailDialog, setOpenAddDetailDialog] = useState(false)
  const [permissionData, setPermissionData] = useState([])
  const hasOkrRequestEditPermission = permissionData.includes('OkrRequest:Edit')
  const hasOkrRequestDetailPermission =
    permissionData.includes('OkrRequest:Detail')
  const hasOkrRequestUpdatePermission =
    permissionData.includes('OkrRequest:Update')
  const columns = getColumns(
    hasOkrRequestEditPermission,
    hasOkrRequestDetailPermission,
    hasOkrRequestUpdatePermission,
  )

  const userIdPermission = localStorage.getItem('userId')
  const {data: permissions} = useGetUserPermissions({
    userId: userIdPermission,
    pageIndex: 1,
    pageSize: 1000,
  })
  useEffect(() => {
    if (permissions) {
      const newPerm = permissions?.data?.items.map((perm) => perm.name)
      setPermissionData(newPerm)
    }
  }, [permissions])

  const handleAddDetailDialogCancel = () => {
    setOpenAddDetailDialog(false)
  }

  const handleAddDetailDialogOpen = () => {
    setOpenAddDetailDialog(true)
  }
  const handleAddDetailDialogSubmit = () => {
    setOpenAddDetailDialog(false)
    refetch()
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
  }

  const handleDepartmentChange = (event) => {
    setSelectedDepartment(event.target.value)
  }

  const onChangePage = (newPage) => {
    setPage(newPage)
  }

  const onChangeRowPerPage = (rowsPerPage) => {
    setRowsPerPage(rowsPerPage)
    setPage(0)
  }

  const {data, isLoading, refetch} = useGetOkrRequest(
    page + 1,
    rowsPerPage,
    dataFilterDialog,
    selectedDepartment,
  )
  const {data: okrDetailData, refetch: refetchOkrDetail} = useGetOkrDetail({
    okrId,
  })

  const handleTitleClick = (id) => {
    setOkrId(id)
    setOpenDialog(true)
  }

  const handleTitleCancel = () => {
    setOpenDialog(false)
  }

  const {mutateAsync: mutateAsyncEditStatusOkrRequest} = useEditStatusOKR({
    okrId: okrId,
  })

  const onActionViewDetail = (data) => {
    setDataUpdateDialog(data)
    setOkrId(data.id)
    handleClickViewDetail()
  }
  const handleClickViewDetail = () => {
    setOpenViewDetailDialog(true)
  }

  const onActionEdit = (data) => {
    setDataUpdateDialog(data)
    setOkrId(data.id)
    handleClickUpdate()
  }

  const handleClickUpdate = () => {
    setOpenUpdateDialog(true)
  }

  const handleCancelUpdateDialog = () => {
    setOpenUpdateDialog(false)
  }

  const handleCancelViewDialog = () => {
    setOpenViewDetailDialog(false)
  }

  const handleSubmitOkrRequest = async (data) => {
    try {
      const response = await mutateAsyncEditStatusOkrRequest(data)
      if (response.data?.code === 0) {
        showSuccess({message: response.data?.message})
        refetch()
        refetchOkrDetail()
        setOpenDialog(false)
      } else if (response.data?.code === StatusCodes.BAD_REQUEST) {
        showError({message: response.data?.message})
      }
    } catch (err) {
      showError(err.response?.data?.message)
    }
  }

  const formattedData = data?.data?.items
    .map((item) => {
      return {
        ...item,
      }
    })
    .filter((item) => {
      if (role === 'BOD' || role === 'Manager') {
        return true
      }
      return item.ownerId === userIdPermission
    })

  useEffect(() => {
    refetch()
  }, [
    page,
    rowsPerPage,
    selectedDepartment,
    dataFilterDialog,
    dialogClosed,
    refetch,
  ])

  return (
    <Paper>
      <Box sx={{width: '100%'}}>
        <Paper sx={{width: '100%', mb: 2}}>
          <Header
            selectedDepartment={selectedDepartment}
            handleDepartmentChange={handleDepartmentChange}
            departmentsData={useGetDepartments().data}
            isLoading={useGetDepartments().isLoading}
            handleClickFilter={handleClickFilter}
            handleClickAdd={handleAddDetailDialogOpen}
          />
          <ComponentTable
            columns={columns}
            isLoading={isLoading}
            data={formattedData}
            onClickOpenDialog={handleTitleClick}
            onCancel={handleTitleCancel}
            disableColCheckbox={true}
            onActionEdit={onActionEdit}
            onActionViewDetail={onActionViewDetail}
            totalItems={data?.data?.totalCount}
            onChangePage={onChangePage}
            onChangeRowPerPage={onChangeRowPerPage}
            onRowClick={handleTitleClick}
            approveStatusColumnId="approveStatus"
            hideEdit={true}
          />
          <FormDialog
            open={openFilterDialog}
            onCancel={handleCancelFilterDialog}
            onConfirm={handleConfirmFilterDialog}
            title="Filter OKR Request"
            actionName="Filter"
            isLoading={isLoading}
            dialogContent={<OkrRequestFilter data={dataFilterDialog} />}
          />
        </Paper>
        <CustomDialog
          open={openDialog}
          onCancel={handleTitleCancel}
          title="OKR Request Detail"
          viewDialog
          showCloseButton
          maxWidth="lg"
          dialogContent={
            <ApproveRequest
              refetchOkrDetail={refetchOkrDetail}
              okrDetailData={okrDetailData}
              onSubmit={handleSubmitOkrRequest}
              onClose={() => {
                setOpenDialog(false)
                setDialogClosed(true)
              }}
            />
          }
        />{' '}
        <CustomDialog
          open={openUpdateDialog}
          onCancel={handleCancelUpdateDialog}
          title="Edit OKR Request"
          maxWidth="lg"
          showCloseButton
          viewDialog
          dialogContent={
            <EditOkrRequest
              refetch={refetch}
              refetchOkrDetail={refetchOkrDetail}
              okrDetailData={okrDetailData}
              onClose={() => {
                setOpenUpdateDialog(false)
                setDialogClosed(true)
              }}
            />
          }
        />
        <CustomDialog
          open={openViewDetailDialog}
          onCancel={handleCancelViewDialog}
          title="OKR Request Detail"
          viewDialog
          showCloseButton
          maxWidth="lg"
          dialogContent={<OkrRequestDetail okrDetailData={okrDetailData} />}
        />
        <CustomDialog
          open={openAddDetailDialog}
          onCancel={handleAddDetailDialogCancel}
          title="Create OKR"
          viewDialog
          showCloseButton
          maxWidth="lg"
          actionName="Add"
          dialogContent={
            <CreateOKR onClose={() => handleAddDetailDialogSubmit()} />
          }
        />
      </Box>
    </Paper>
  )
}

export default RequestTab
